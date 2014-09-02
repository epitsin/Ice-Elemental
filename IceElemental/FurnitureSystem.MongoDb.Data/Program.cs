using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureSystem.MongoDb.Data
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionStr = "mongodb://localhost/";
            var mongoClient = new MongoClient(connectionStr);
            var mongoServer = mongoClient.GetServer();
            var movieSystemDb = mongoServer.GetDatabase("MovieSystem");
        }
    }
}
