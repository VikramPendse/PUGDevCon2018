#region Namespaces
using Microsoft.Azure.Documents.Client;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
#endregion


namespace PUGDevCon_Cosmos_QuerySchema
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () =>
            {
                var endpoint = ConfigurationSettings.AppSettings["CosmoEndpoint"];
                var masterKey = ConfigurationSettings.AppSettings["CosmoMasterKey"];
                using (var client = new DocumentClient(new Uri(endpoint), masterKey))
                {
                    Console.WriteLine("\r\n-- Querying Document (JSON) --");

                    var databaseDefinition = "<Your Schema Name>";
                    var collectionDefinition = "<Your Collection Name>";

                    var document = new FeedOptions { EnableCrossPartitionQuery = true };
                    //var response = client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(databaseDefinition.Id.ToString(), collectionDefinition.Id.ToString()),
                    //    "select * from c").ToList();

                    //var response = client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(databaseDefinition, collectionDefinition),
                    //"SELECT mCol.title, mCol.genres FROM movieCollection mCol where mCol.imdbRating >= 5", document).ToList();

                    //var response = client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(databaseDefinition, collectionDefinition),
                    //"SELECT mCol.title, mCol.genres, mCol.storyline FROM movieCollection mCol where mCol.imdbRating > 7", document).ToList();

                    var response = client.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri(databaseDefinition, collectionDefinition),
                    "SELECT mCol.title, mCol.releaseDate FROM movieCollection mCol order by mCol.releaseDate DESC", document).ToList();

                    foreach (var item in response)
                    {
                        Console.WriteLine(item);
                        Console.WriteLine("\r\n");
                    }

                    Console.ReadKey();
                }

            }).Wait();
        }
    }
}