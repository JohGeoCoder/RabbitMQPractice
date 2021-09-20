using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World! Receiving");

var factory = new ConnectionFactory()
{
    HostName = "66.175.215.111",
    Port = 5672,
    UserName = "testuser",
    Password = ""
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "ExternalQueue",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"Received: {message}");
};

channel.BasicConsume(queue: "ExternalQueue",
    autoAck: true,
    consumer: consumer);


Console.ReadLine();