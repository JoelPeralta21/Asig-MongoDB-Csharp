using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace com.joel
{
    public class Persona
    {
        [BsonElement ("nombre")]
        public string nombre{get;set;}

        [BsonElement ("edad")]
        public int edad{get;set;}

    }
}