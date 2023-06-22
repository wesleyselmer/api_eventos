using api_eventos.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api_eventos.Services;

public class EventosService
{
    private readonly IMongoCollection<Evento> _eventosCollection;

    public EventosService(
        IOptions<EventosDatabaseSettings> EventosDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                EventosDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                EventosDatabaseSettings.Value.DatabaseName);

            _eventosCollection = mongoDatabase.GetCollection<Evento>(
                EventosDatabaseSettings.Value.EventosCollectionName);
        }

        public async Task<List<Evento>> GetEventosAsync() =>
            await _eventosCollection.Find(_ => true).ToListAsync();
        
        public async Task<Evento?> GetEventoAsync(string id) =>
            await _eventosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateEventoAsync(Evento evento) =>
            await _eventosCollection.InsertOneAsync(evento);

        public async Task UpdateEventoAsync(string id, Evento updatedEvento) =>
            await _eventosCollection.ReplaceOneAsync(x => x.Id == id, updatedEvento);
        
        public async Task RemoveEventoAsync(string id) =>
            await _eventosCollection.DeleteOneAsync(x => x.Id == id);
}