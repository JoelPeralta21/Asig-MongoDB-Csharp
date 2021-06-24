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

namespace com.joel
{
    public static class CrearElemento
    {
        [FunctionName("CrearElemento")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var dbConnection = new MongoClient(System.Environment.GetEnvironmentVariable("MongoDBAtlasCString")); 
            var dbName = dbConnection.GetDatabase("ClaseAzuero");
            var collection = dbName.GetCollection<Persona>("Personas");

            // var Juan = new Persona();
            // Juan.nombre = "Juan Pedro";
            // Juan.edad = 25;

            // var Maria = new Persona{
            //     nombre = "Maria Elena",
            //     edad = 27
            // };
            
            String nombre = req.Query["nombre"];
            int edad = Int32.Parse(req.Query["edad"]);
            var persona = new Persona();
            persona.nombre = nombre;
            persona.edad = edad;
            try
            {
                // await collection.InsertOneAsync(Juan);
                // await collection.InsertOneAsync(Maria);
                await collection.InsertOneAsync(persona);
                return new OkObjectResult("Se guardaron los datos");
            }
            catch (System.Exception e)
            {
                
                return new BadRequestObjectResult("Hubo un error al insertar" + e.Message);
            }

        

        }
    }
}
