using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainClasses;
using MongoDB.Driver;
using System.Security.Authentication;
using MongoDB.Bson;

namespace PeladaBackend.Repositories
{
    public class QuadraRepository : IQuadraRepository
    {
        // connection string para o MongoDB contratado no Azure
        const string ConnectionString = @"mongodb://quadrasdb:CGLXYaQ24dTrGK7GpNe8LrKx3mjH94fyYCB5v19iK6qS1GE2qtPeYYPTjhja5vCctZctZlwlH1CmdqpAWXmeXA==@quadrasdb.documents.azure.com:10250/?ssl=true&sslverifycertificate=false";

        // propriedades SSL para acesso ao MongoDB
        static SslSettings sslSettings = new SslSettings()
        {
            EnabledSslProtocols = SslProtocols.Tls12
        };

        // Busca no MongoDB a quadra com o id especificado
        public Quadra Get(string id)
        {
            IMongoCollection<Quadra> quadrasCollection = obterReferenciaParaColecaoQuadra();
            var filter = Builders<Quadra>.Filter.Eq("_id", ObjectId.Parse(id));

            return quadrasCollection.Find(filter).SingleOrDefault();
        }

        // Busca no MongoDb todas as quadras
        public IEnumerable<Quadra> GetAllQuadras()
        {
            IMongoCollection<Quadra> quadrasCollection = obterReferenciaParaColecaoQuadra();

            return quadrasCollection.Find(new BsonDocument()).ToList();
        }

        // Obtém uma referência para a coleção de quadras
        private static IMongoCollection<Quadra> obterReferenciaParaColecaoQuadra()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
            settings.SslSettings = sslSettings;

            var mongoClient = new MongoClient(settings);
            var database = mongoClient.GetDatabase("admin");
            var quadrasCollection = database.GetCollection<Quadra>("quadras");

            return quadrasCollection;
        }
    }
}