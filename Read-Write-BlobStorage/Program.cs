using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.IO;

namespace Read_Write_BlobStorage
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var Configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                    .Build();

            var serviceProvider = new ServiceCollection()
                                      .AddSingleton<IConfigurationRoot>(Configuration)
                                      //.AddSingleton<DepencencyClasses>()
                                      .AddTransient<DepencencyClasses>()
                                      //.AddScoped<DepencencyClasses>()
                                      .BuildServiceProvider();

            var objDepencencyClasses1 = serviceProvider.GetService<DepencencyClasses>();
            var objDepencencyClasses2 = serviceProvider.GetService<DepencencyClasses>();
            var objDepencencyClasses3 = serviceProvider.GetService<DepencencyClasses>();
            var objDepencencyClasses4 = serviceProvider.GetService<DepencencyClasses>();
            objDepencencyClasses1.ReadFromConfig();
            objDepencencyClasses2.ReadFromConfig();
            objDepencencyClasses3.ReadFromConfig();
            objDepencencyClasses4.ReadFromConfig();


            //Console.WriteLine(Configuration["BlobStorageAccountConnection"]);
            ////BlobOperations blobOperations = new BlobOperations();
            ////blobOperations.UploadToBlob();

            ////await blobOperations.GetBlobFromStorage();

            Console.WriteLine("Process Completed..");
            Console.Read();
        }
    }
}
