namespace Projeto_Viagens_WebAPI_MongoDB.Config
{
    public interface IProjetoViagensWebAPIMongoDBSettings
    {
        string CityCollectionName { get; set; }
        string AddressCollectionName { get; set; }
        string CustomerCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
