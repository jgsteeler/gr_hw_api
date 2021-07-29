using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RecordApi.Shared.Configuration;

namespace GenericHostSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            var myService = new RecordApi.Shared.Services.RecordOutPutService();

            Console.WriteLine("-- Begin Output to Console Screen--");
            myService.RecordsSortedByColor();
            myService.RecordsSortedByDateOfBirth();
            myService.RecordsSortedByLastNameDescending();
            

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                   
                })
                .AddSharedLibrary();
    }
}
