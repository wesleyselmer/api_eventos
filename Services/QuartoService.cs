using api_eventos.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api_eventos.Services;

public class QuartoService
{
    private readonly IMongoCollection<Quarto> _quartoCollection;

    public QuartoService(
        IOptions<EventosDatabaseSettings> EventosDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                EventosDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                EventosDatabaseSettings.Value.DatabaseName);

            _quartoCollection = mongoDatabase.GetCollection<Quarto>(
                EventosDatabaseSettings.Value.QuartoCollectionName);
        }

        public async Task<List<Quarto>> GetQuartosAsync() =>
            await _quartoCollection.Find(_ => true).ToListAsync();
        
        public async Task<Quarto?> GetQuartoAsync(string id) =>
            await _quartoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateQuartoAsync(Quarto quarto) =>
            await _quartoCollection.InsertOneAsync(quarto);

        public async Task UpdateQuartoAsync(string id, Quarto updatedQuarto) =>
            await _quartoCollection.ReplaceOneAsync(x => x.Id == id, updatedQuarto);
        
        public async Task RemoveQuartoAsync(string id) =>
            await _quartoCollection.DeleteOneAsync(x => x.Id == id);
}