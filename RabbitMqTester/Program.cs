// See https://aka.ms/new-console-template for more information

using System.Text;
using RabbitMQ.Client;
const string queueName = "rabbit-mq-connection-test";
var host = Environment.GetEnvironmentVariable("host");
var username = Environment.GetEnvironmentVariable("username") ?? "";
var password = Environment.GetEnvironmentVariable("password") ?? "";

Console.WriteLine($"Host name set to: {host}");
Console.WriteLine($"Username set to: {username}");
Console.WriteLine($"Password set to: {password}");

var factory = new ConnectionFactory
{
    HostName = host,
    UserName = username,
    Password = password
};
using var connection = factory.CreateConnection();

Console.WriteLine("Connection Successful");
using var channel = connection.CreateModel();


channel.QueueDeclare(queue: queueName,
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

const string message = "Hello World!";
var body = Encoding.UTF8.GetBytes(message);

Console.WriteLine($"Publishing '{message}' message");

channel.BasicPublish(exchange: string.Empty,
    routingKey: queueName,
    basicProperties: null,
    body: body);

Console.WriteLine($"Published '{message}' message");

Console.WriteLine($"Deleting Queue");

channel.QueueDelete(queueName);

Console.WriteLine($"Queue Deleted");

await Task.Delay(30000);
