using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordApi.Shared.Configuration;
using RecordApi.Shared.Services;

namespace RecordApi.Runner
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            
            serviceCollection.AddSingleton<IFileProcessor, FileProcessor>();
            serviceCollection.AddSingleton<IRecordOutPutService, RecordOutPutService>();
            //CreateHostBuilder(args).Build().Run();
            var serviceProvider = serviceCollection.BuildServiceProvider();
            var myService = serviceProvider.GetRequiredService<IRecordOutPutService>();

            Console.WriteLine("-- Begin Output to Console Screen--");
            myService.RecordsSortedByColor();
            myService.RecordsSortedByDateOfBirth();
            myService.RecordsSortedByLastNameDescending();
            

        }

       
    }
}
