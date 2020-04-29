using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Read_Write_BlobStorage
{
    public class DepencencyClasses
    {
        private readonly IConfigurationRoot _configurationRoot;
        public DepencencyClasses(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        public void ReadFromConfig()
        {
            Console.WriteLine($"HashCode: {this.GetHashCode()}");
            Console.WriteLine($"Conn str: {_configurationRoot["BlobStorageAccountConnection"]}");
            Console.WriteLine($"Container: {_configurationRoot["BlobContainer"]}");
        }
    }
}
