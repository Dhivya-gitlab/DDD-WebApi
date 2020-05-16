using AutoMapper.Configuration;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StudentEducationBoardService.Api.AppModels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentEducationBoardService.Api.AzureServiceBusImp
{
    public interface IServiceBusConsumer
    {
        void RegisterOnMessageHandlerAndReceiveMessages();
        Task CloseQueueAsync();
    }

    public class ServiceBusConsumer : IServiceBusConsumer
    {
        private readonly IProcessData _processData;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
        private readonly QueueClient _queueClient;
        private const string QUEUE_NAME = "SchoolBoardQueue";
        private readonly ILogger _logger;


        public ServiceBusConsumer(IProcessData processData, Microsoft.Extensions.Configuration.IConfiguration configuration, ILogger<ServiceBusConsumer> logger)
        {
            _processData = processData;
            _configuration = configuration;
            _logger = logger;
            _queueClient = new QueueClient(_configuration["Data:ConectionStrings:ServiceBusConnectionString"], QUEUE_NAME, ReceiveMode.PeekLock, RetryPolicy.Default);
        }

        public void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = 1,
                AutoComplete = false
            };

            _queueClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

        private async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            var schoolDetails = JsonConvert.DeserializeObject<SchoolDetailsDto>(Encoding.UTF8.GetString(message.Body));
            _processData.Process(schoolDetails);
            await _queueClient.CompleteAsync(message.SystemProperties.LockToken);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            _logger.LogError(exceptionReceivedEventArgs.Exception, "Message handler encountered an exception");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;

            _logger.LogDebug($"- Endpoint: {context.Endpoint}");
            _logger.LogDebug($"- Entity Path: {context.EntityPath}");
            _logger.LogDebug($"- Executing Action: {context.Action}");

            return Task.CompletedTask;
        }

        public async Task CloseQueueAsync()
        {
            await _queueClient.CloseAsync();
        }
    }
    public interface IProcessData
    {
        void Process(SchoolDetailsDto schoolDetails);
    }
    public class ProcessData : IProcessData
    {

        public void Process(SchoolDetailsDto schoolDetails)
        {
            //DataServiceSimi.Data.Add(new Payload
            //{
            //    Name = myPayload.Name,
            //    Goals = myPayload.Goals
            //});
        }
    }
}

