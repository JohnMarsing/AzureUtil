using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

internal class Program
{
	private static void Main(string[] args)
	{
		var config = new ConfigurationBuilder()
				.AddUserSecrets<Program>()
				.Build();

		string blobConnectionString = $"{config["Blob:ConnectionString"]}";
		string blobContainer = $"{config["Blob:Container"]}";

		BlobServiceClient blobServiceClient = new BlobServiceClient(blobConnectionString);
		BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobContainer);

		List<string> blobNamesList = containerClient.GetBlobs().Select(b => b.Name).ToList();

		//foreach (string item in blobNamesList)
		//{
		//	Console.WriteLine($"SELECT Id, YouTubeId, GraphicFile FROM WeeklyVideo WHERE WeeklyVideoTypeId=1 AND ShabbatWeekId={item.Substring(0,3).TrimStart('0')} -- {item.Substring(15,11)}");
		//}

		foreach (string item in blobNamesList)
		{
			Console.WriteLine($"UPDATE WeeklyVideo SET GraphicFile='{item}' WHERE WeeklyVideoTypeId=1 AND ShabbatWeekId={item.Substring(0, 3).TrimStart('0')}");
		}
	}
}
