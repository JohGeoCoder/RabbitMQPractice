// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, World! Sending");

var factory = new ConnectionFactory()
{
    HostName = "66.175.215.111",
    Port = 5672,
    UserName = "testuser",
    Password = ""
};

using var connection =  factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "ExternalQueue",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);


var message = "Hello World";
var body = Encoding.UTF8.GetBytes(message);


while (true)
{
    channel.BasicPublish(exchange: "", routingKey: "ExternalQueue", body: body, basicProperties: null);
    Console.WriteLine($"Sent: {message}");

    var input = Console.ReadLine();

    if (input == "quit") break;
}