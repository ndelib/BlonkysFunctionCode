using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;

public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, 
    ILogger log)
{
    const string blobUri = "https://stor2p0ezjly98h5qs7.blob.core.windows.net/mycontainer/hello.txt";
    const string sasToken = "?sv=2020-04-08&ss=b&srt=o&se=2023-10-02T07%3A22%3A37Z&sp=r&sig=zRqjNND7PNHJJ9xhnRLln31lNwIUM2zft0efSwaexeY%3D";
    var blobClient = new BlobClient(new Uri(blobUri + sasToken));
    var blobContent = await blobClient.OpenReadAsync();
    using var reader = new StreamReader(blobContent.Value.Content);
    var content = await reader.ReadToEndAsync();
    return new OkObjectResult(content);
}
