using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Projeto_Viagens_WebAPI_MongoDB.Models
{
    public class Address
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Neighborhood { get; set; }
        public string PostalCode { get; set; }
        public string Complement { get; set; }
        public City City { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
