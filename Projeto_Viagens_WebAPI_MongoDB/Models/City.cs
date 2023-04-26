using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Projeto_Viagens_WebAPI_MongoDB.Models
{
    public class City
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
