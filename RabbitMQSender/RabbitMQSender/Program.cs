using RabbitMQ.Client;

/* How to Test on Mac
 *  - Install Docker
 *  - Install Colima & Run
 *      > colima start
 *  - Run RabbitMQ locally using this query:
 *      > docker run -d --hostname rabbitmq-local --name rabbitmq-local -p 5672:5672 -p 15672:15672 rabbitmq:3-management
 *  - Check if RabbitMQ is Running At: http://localhost:15672/
 *  - Credentials:
 *      > User: guest
 *      > Pass: guest
 *  - Send the message by Running this code
 *
 *  - Go to the api and check the "ProvidersStartupSetup" to and comment this to run on Dev/Test:
 *    #if RELEASE && PRODUCTION
 *              services.AddHostedService<SmartLynxFlightKeysQueueListener>();
 *    #endif
 * 
 *  - Run the Api to read the messages
 */


var factory = new ConnectionFactory { HostName = "localhost" };
await using var connection = await factory.CreateConnectionAsync();
await using var channel = await connection.CreateChannelAsync();

var queueName = "smartlynx-queue-flightkeys-dev";
await channel.QueueDeclareAsync(
    queue: queueName, 
    durable: false, 
    exclusive: false, 
    autoDelete: false,
    arguments: null);


var filePath = "/Users/victorkaikecezemerpeixoto/Downloads/Flights sent 22/92884855-c0eb-42ea-8062-9dadc158cf3b/RequestBody.eff";
var fileBytes = await File.ReadAllBytesAsync(filePath);
var body = fileBytes;


await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, body: body);

Console.WriteLine(" Message Sent.");
Console.ReadLine();