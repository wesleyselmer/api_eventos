using api_eventos.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api_eventos.Services;

public class LocalService
{
    private readonly IMongoCollection<Local> _localCollection;

    public LocalService(
        IOptions<EventosDatabaseSettings> EventosDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                EventosDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                EventosDatabaseSettings.Value.DatabaseName);

            _localCollection = mongoDatabase.GetCollection<Local>(
                EventosDatabaseSettings.Value.LocalCollectionName);
        }

        public async Task<List<Local>> GetLocaisAsync() =>
            await _localCollection.Find(_ => true).ToListAsync();
        
        public async Task<Local?> GetLocalAsync(string id) =>
            await _localCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateLocalAsync(Local local) =>
            await _localCollection.InsertOneAsync(local);

        public async Task UpdateLocalAsync(string id, Local updatedLocal) =>
            await _localCollection.ReplaceOneAsync(x => x.Id == id, updatedLocal);
        
        public async Task RemoveLocalAsync(string id) =>
            await _localCollection.DeleteOneAsync(x => x.Id == id);
}