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
    public static class ActualizarElemento
    {
        [FunctionName("ActualizarElemento")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var dbConnection = new MongoClient(System.Environment.GetEnvironmentVariable("MongoDBAtlasCString")); 
            var dbName = dbConnection.GetDatabase("ClaseAzuero");
            var collection = dbName.GetCollection<Persona>("Personas");
            
            String nombre = req.Query["nombre"];
            int edad = Int32.Parse(req.Query["edad"]);
            var persona = new Persona();
            persona.nombre = nombre;
            persona.edad = edad;
            try
            {
                var filter = Builders<Persona>.Filter.Eq("nombre",nombre);
                var update = Builders<Persona>.Update.Set("edad",edad);
                await collection.UpdateOneAsync(filter,update);
                return new OkObjectResult("Se actualizaron los datos");
            }
            catch (System.Exception e)
            {
                
                return new BadRequestObjectResult("Hubo un error al insertar" + e.Message);
            }

        

        }
    }
}
