# How to Test on Mac
- Install Docker
- Install Colima & Run
    > colima start
- Run RabbitMQ locally using this query:
    > docker run -d --hostname rabbitmq-local --name rabbitmq-local -p 5672:5672 -p 15672:15672 rabbitmq:3-management
- Check if RabbitMQ is Running At: http://localhost:15672/
- Credentials:
    > User: guest
    > Pass: guest
- Send the message by Running this code
- Go to the api and check the "ProvidersStartupSetup" to and comment this to run on Dev/Test:

```
#if RELEASE && PRODUCTION
            services.AddHostedService<SmartLynxFlightKeysQueueListener>();
#endif 
```

- Run the Api to read the messages


