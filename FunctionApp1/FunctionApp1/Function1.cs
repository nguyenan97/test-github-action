using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
//1 122222222222 123
namespace FunctionApp1
{
    public class Function1
    {
        [FunctionName("Function1")]
        public void Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"----FunctionApp1 3.1 C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
