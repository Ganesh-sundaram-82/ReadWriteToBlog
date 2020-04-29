using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace Read_Write_BlobStorage
{
    public class BlobOperations
    {
        string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=cloudshell1558105445;AccountKey=/1IPwcon7Ge7q2VkTTx2GBKJqYIiNtJ8xY4tdCEK6IJhIn4hzwMfplr1mW3IbofMWU5cUFlnojflLLPwPj4hcQ==;EndpointSuffix=core.windows.net";
        string Container = "container1";


        public BlobOperations()
        {

        }

        public async Task GetBlobFromStorage()
        {
            //https://cloudshell1558105445.blob.core.windows.net/container1
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(ConnectionString);
            var blobclient = cloudStorageAccount.CreateCloudBlobClient();
            var container = blobclient.GetContainerReference(Container);

            BlobContinuationToken continuationToken = null;
            BlobResultSegment resultSegment = null;
            List<string> names = new List<string>();

            do
            {
                resultSegment = await container.ListBlobsSegmentedAsync(continuationToken);

                // Get the name of each blob.
                names.AddRange(resultSegment.Results.OfType<CloudBlob>().Select(b => b.Name));
                continuationToken = resultSegment.ContinuationToken;
            } while (continuationToken != null);

            names.ForEach(n => { Console.WriteLine($"File Name: {n}"); });
        }

        public async Task Save(Stream fileStream, string name)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(Container);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);
            await blockBlob.UploadFromStreamAsync(fileStream);
        }

        public void UploadToBlob()
        {
            FileStream fs = new FileStream(@"C:\docs\testdocuments.pdf", FileMode.Open);
            var taskoperation = Save(fs, Container);
            fs.Close();
        }

    }
}
