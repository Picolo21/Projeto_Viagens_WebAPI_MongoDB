using MongoDB.Driver;
using Projeto_Viagens_WebAPI_MongoDB.Config;
using Projeto_Viagens_WebAPI_MongoDB.Models;

namespace Projeto_Viagens_WebAPI_MongoDB.Services
{
    public class CityService
    {
        private readonly IMongoCollection<City> _city;

        public CityService(IProjetoViagensWebAPIMongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _city = database.GetCollection<City>(settings.CityCollectionName);
        }

        public List<City> Get() => _city.Find(x => true).ToList();

        public City Get(string id) => _city.Find(x => x.Id == id).FirstOrDefault();

        public City Create(City city)
        {
            _city.InsertOne(city);
            return city;
        }

        public void Update(string id, City city) => _city.ReplaceOne(x => x.Id == id, city);

        public void Delete(string id) => _city.DeleteOne(x => x.Id == id);

        public void Delete(City city) => _city.DeleteOne(x => x.Id == city.Id);
    }
}
