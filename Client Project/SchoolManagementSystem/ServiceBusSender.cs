using Microsoft.Extensions.Configuration;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using SchoolManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    public class ServiceBusSender
    {
        private readonly QueueClient _queueClient;
        private readonly IConfiguration _configuration;
        private const string QUEUE_NAME = "SchoolBoardQueue";

        public ServiceBusSender(IConfiguration configuration)
        {
            _configuration = configuration;
            _queueClient = new QueueClient(_configuration["Data:ConectionStrings:ServiceBusConnectionString"], QUEUE_NAME, ReceiveMode.PeekLock, RetryPolicy.Default);
        }

        public async Task SendMessage(SchoolDetailsViewModel payload)
        {
            string data = JsonConvert.SerializeObject(payload);
            Message message = new Message(Encoding.UTF8.GetBytes(data));

            await _queueClient.SendAsync(message);
        }
    }
}
