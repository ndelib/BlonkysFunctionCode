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
    const string blobUri = "https://storhn4ufkp2zml3w6x.blob.core.windows.net/mycontainer/hello.txt";
    const string sasToken = "?sv=2020-04-08&ss=b&srt=o&se=2023-10-02T03%3A24%3A45Z&sp=r&sig=Wq5zbreB2zF6hbnHlJ4svMf55eBCHye6kM1qsU4b7dk%3D";
    var blobClient = new BlobClient(new Uri(blobUri + sasToken));
    var blobContent = await blobClient.OpenReadAsync();
    using var reader = new StreamReader(blobContent.Value.Content);
    var content = await reader.ReadToEndAsync();
    return new OkObjectResult(content);
}
