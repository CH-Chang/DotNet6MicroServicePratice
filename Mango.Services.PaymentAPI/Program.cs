using Mango.MessageBus;
using Mango.Services.PaymentAPI.Extension;
using Mango.Services.PaymentAPI.Messaging;
using Mango.Services.PaymentAPI.RabbitMQSender;
using PaymentProcessor;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IProcessPayment, ProcessPayment>();
builder.Services.AddSingleton<IAzureServiceBusConsumer, AzureServiceBusConsumer>();
builder.Services.AddSingleton<IMessageBus, AzureServiceBusMessageBus>();
builder.Services.AddHostedService<RabbitMQPaymentConsumer>();
builder.Services.AddSingleton<IRabbitMQPaymentMessageSender, RabbitMQPaymentMessageSender>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseAzureServiceBusConsumer();

app.Run();
