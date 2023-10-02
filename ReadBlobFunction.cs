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
    const string blobUri = "https://storups4y6hkxcqibfd.blob.core.windows.net/mycontainer/hello.txt";
    const string sasToken = "?sv=2020-04-08&ss=b&srt=o&se=2023-10-02T07%3A07%3A21Z&sp=r&sig=mqFpjaa0WiKantzmVVHxw8iofPUh4KzuQPlvlwXBhXg%3D";
    var blobClient = new BlobClient(new Uri(blobUri + sasToken));
    var blobContent = await blobClient.OpenReadAsync();
    using var reader = new StreamReader(blobContent.Value.Content);
    var content = await reader.ReadToEndAsync();
    return new OkObjectResult(content);
}
