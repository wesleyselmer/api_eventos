using api_eventos.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api_eventos.Services;

public class EventoService
{
    private readonly IMongoCollection<Evento> _eventoCollection;

    public EventoService(
        IOptions<EventosDatabaseSettings> EventosDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                EventosDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                EventosDatabaseSettings.Value.DatabaseName);

            _eventoCollection = mongoDatabase.GetCollection<Evento>(
                EventosDatabaseSettings.Value.EventoCollectionName);
        }

        public async Task<List<Evento>> GetEventosAsync() =>
            await _eventoCollection.Find(_ => true).ToListAsync();
        
        public async Task<Evento?> GetEventoAsync(string id) =>
            await _eventoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateEventoAsync(Evento evento) =>
            await _eventoCollection.InsertOneAsync(evento);

        public async Task UpdateEventoAsync(string id, Evento updatedEvento) =>
            await _eventoCollection.ReplaceOneAsync(x => x.Id == id, updatedEvento);
        
        public async Task RemoveEventoAsync(string id) =>
            await _eventoCollection.DeleteOneAsync(x => x.Id == id);
}