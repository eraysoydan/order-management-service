using OrderManagement.API.Common.Enums;
using OrderManagement.API.Common.Exceptions;
using OrderManagement.API.Core.UnitOfWork;
using OrderManagement.API.Models;
using OrderManagement.API.Models.Entity;
using AutoMapper;
using OrderManagement.API.Core.Services.Interface;
using OrderManagement.API.Core.RabbitMQ.Interface;

namespace OrderManagement.API.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;
        private readonly IMessageProducer _messageProducer;

        public OrderService(IUnitOfWork unitOfWork, ILogger<OrderService> logger, IMapper mapper, IMessageProducer messageProducer)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _messageProducer = messageProducer;
        }

        public async Task<IEnumerable<GetOrdersResponseModel>> GetOrdersAsync()
        {
            var orders = (from order in await _unitOfWork.OrderRepository.GetAllAsync()
                          join item in await _unitOfWork.ItemRepository.GetAllAsync() on order.ItemCode equals item.ItemCode
                          join status in await _unitOfWork.OrderStatusRepository.GetAllAsync() on order.StatusId equals status.Id
                          select new GetOrdersResponseModel
                          {
                              Id = order.Id,
                              CustomerOrderNo = order.CustomerOrderNo,
                              OutputAddress = order.OutputAddress,
                              DestinationAddress = order.DestinationAddress,
                              Quantity = order.Quantity,
                              QuantityUnit = order.QuantityUnit,
                              Weight = order.Weight,
                              WeightUnit = order.WeightUnit,
                              ItemName = item.ItemName,
                              Status = status.Name,
                              Note = order.Note
                          }).ToList();

            return orders;
        }

        public async Task<CreateOrderResponseModel> AddOrdersAsync(CreateOrderRequestModel orderRequests)
        {
            var response = new CreateOrderResponseModel();
            foreach (var orderRequest in orderRequests.CreateOrderRequestItems)
            {
                try
                {
                    if (_unitOfWork.OrderRepository.FindAsync(x => x.CustomerOrderNo == orderRequest.CustomerOrderNo).Result.Any())
                        throw new InternalValidationException($"This order no already exists.");

                    #region Add Order
                    var order = _mapper.Map<Order>(orderRequest);
                    order.SystemOrderNo = Guid.NewGuid().ToString();
                    order.StatusId = (int)Common.Enums.OrderStatus.OrderReceived;
                    order.StatusUpdateDate = DateTime.Now;
                    order.CreateDate = DateTime.Now;

                    await _unitOfWork.OrderRepository.AddAsync(order);
                    #endregion

                    #region Add Item
                    if (!_unitOfWork.ItemRepository.FindAsync(x => x.ItemCode == orderRequest.OrderItem.Code).Result.Any())
                    {
                        var item = new Item
                        {
                            ItemCode = orderRequest.OrderItem.Code,
                            ItemName = orderRequest.OrderItem.Name,
                            CreateDate = DateTime.Now
                        };

                        await _unitOfWork.ItemRepository.AddAsync(item);
                    }
                    #endregion

                    await _unitOfWork.CommitAsync();

                    response.CreateOrderResponseItems.Add(new CreateOrderResponseItem { CustomerOrderNo = order.CustomerOrderNo, SystemOrderNo = order.SystemOrderNo, Status = 0 });
                    _logger.LogInformation($"Order {order.CustomerOrderNo} has been successfully saved.");
                }
                catch (InternalValidationException ex)
                {
                    response.CreateOrderResponseItems.Add(new CreateOrderResponseItem
                    {
                        CustomerOrderNo = orderRequest.CustomerOrderNo,
                        Status = (int)ResponseStatus.Success,
                        ErrorMessage = $"{ex.Message}"
                    });

                    _logger.LogError(ex, $"[CustomerOrderNo: {orderRequest.CustomerOrderNo}] - {ex.Message}");
                }
                catch (Exception ex)
                {
                    response.CreateOrderResponseItems.Add(new CreateOrderResponseItem
                    {
                        CustomerOrderNo = orderRequest.CustomerOrderNo,
                        Status = (int)ResponseStatus.Fail,
                        ErrorMessage = $"An error occurred while creating the order. See logs for details."
                    });

                    _logger.LogError(ex, $"[CustomerOrderNo: {orderRequest.CustomerOrderNo}] - {ex.Message}");
                }
            }

            return response;
        }

        public async Task<OrderStatusUpdateResponseModel> UpdateOrderStatusAsync(OrderStatusUpdateRequestModel orderStatusUpdateRequest)
        {
            OrderStatusUpdateResponseModel response;
            try
            {
                var order = _unitOfWork.OrderRepository.FindAsync(x => x.CustomerOrderNo == orderStatusUpdateRequest.CustomerOrderNo).Result.FirstOrDefault();
                if (order == null)
                    throw new InternalValidationException($"Order number {orderStatusUpdateRequest.CustomerOrderNo} could not be found.");

                if (!_unitOfWork.OrderStatusRepository.FindAsync(x => x.Id == orderStatusUpdateRequest.StatusId).Result.Any())
                    throw new InternalValidationException($"The status information for the statusId '{orderStatusUpdateRequest.StatusId}' sent was not found.");

                order.StatusId = orderStatusUpdateRequest.StatusId;
                order.StatusUpdateDate = DateTime.Now;

                _unitOfWork.OrderRepository.Edit(order);

                var status = _unitOfWork.OrderStatusRepository
                    .FindAsync(x => x.Id == order.StatusId).Result
                    .FirstOrDefault().Name;

                response = new OrderStatusUpdateResponseModel
                {
                    UpdatedAt = order.StatusUpdateDate,
                    Status = (int)ResponseStatus.Success,
                };

                _messageProducer.SendMessage(new { CustomerOrderNo = order.CustomerOrderNo, Status = status, UpdatedDate = order.StatusUpdateDate });
                _logger.LogInformation($"Status information of order number {order.CustomerOrderNo} has been added to the queue.");

                await _unitOfWork.CommitAsync();
                _logger.LogInformation($"Status information of order number {order.CustomerOrderNo} has been updated successfully.");
            }
            catch (InternalValidationException ex)
            {
                response = new OrderStatusUpdateResponseModel
                {
                    Status = (int)ResponseStatus.Fail,
                    ErrorMessage = ex.Message,
                };

                _logger.LogInformation(ex, $"[CustomerOrderNo: {orderStatusUpdateRequest.CustomerOrderNo}] - {ex.Message}");
            }
            catch (Exception ex)
            {
                response = new OrderStatusUpdateResponseModel
                {
                    Status = (int)ResponseStatus.Fail,
                    ErrorMessage = "An error occurred while updating the status information. See logs for details.",
                };

                _logger.LogError(ex, $"[CustomerOrderNo: {orderStatusUpdateRequest.CustomerOrderNo}] - {ex.Message}");
            }

            return response;
        }
    }
}