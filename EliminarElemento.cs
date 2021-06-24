using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MongoDB.Driver;
using MongoDB.Bson;

namespace com.joel
{
    public static class EliminarElemento
    {
        [FunctionName("EliminarElemento")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = null)] HttpRequest req,
            ILogger log)
        {
            var dbConnection = new MongoClient(System.Environment.GetEnvironmentVariable("MongoDBAtlasCString")); 
            var dbName = dbConnection.GetDatabase("ClaseAzuero");
            var collection = dbName.GetCollection<Persona>("Personas");

            String nombre = req.Query["nombre"];

            try
            {
                var filter = Builders<Persona>.Filter.Eq("nombre",nombre);
                await collection.DeleteOneAsync(filter);
                return new OkObjectResult("Se eliminaron los datos");
            }
            catch (System.Exception e)
            {

                return new BadRequestObjectResult("Hubo un error al eliminar" + e.Message);
            }
        
        }
    }
}
