using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StorageAccountAzure
{
    class Program
    {
        static void Main(string[] args)
        {
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));
            var myClient = storageAccount.CreateCloudBlobClient();
            var container = myClient.GetContainerReference("images-backup");
            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

            var blockBlob = container.GetBlockBlobReference("backup/mikepic.png");
            using (var fileStream = System.IO.File.OpenRead(@"c:\mikepic.png"))
            {
                blockBlob.UploadFromStream(fileStream);
            }

            Console.ReadLine();
        }
    }
}
