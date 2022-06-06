using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using OrderManagement.API.Common.Validators;
using OrderManagement.API.Core.Middlewares;
using OrderManagement.API.Core.RabbitMQ;
using OrderManagement.API.Core.RabbitMQ.Interface;
using OrderManagement.API.Core.Services;
using OrderManagement.API.Core.Services.Interface;
using OrderManagement.API.Core.UnitOfWork;
using OrderManagement.API.Models.Entity;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .Enrich.FromLogContext()
  .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers().AddFluentValidation(x => x
    .RegisterValidatorsFromAssemblyContaining<CreateOrderRequestModelValidator>()
    .RegisterValidatorsFromAssemblyContaining<OrderStatusUpdateRequestModelValidator>());

builder.Services.AddDbContext<OrderManagementContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMessageProducer, MessageProducer>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IOrderStatusService, OrderStatusService>();

builder.Services.AddAutoMapper(typeof(Program));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggingMiddleware>();

app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
