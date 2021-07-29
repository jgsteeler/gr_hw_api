using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordApi.Shared.Configuration
{
    public static class SharedExtensionMethods
    {
        public static IHostBuilder AddSharedLibrary(this IHostBuilder builder) {

            return builder.ConfigureServices((context, services) => {
            

            
            });
        }
    }
}
