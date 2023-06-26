using api_eventos.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api_eventos.Services;

public class PessoaService
{
    private readonly IMongoCollection<Pessoa> _PessoaCollection;

    public PessoaService(
        IOptions<EventosDatabaseSettings> EventosDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                EventosDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                EventosDatabaseSettings.Value.DatabaseName);

            _PessoaCollection = mongoDatabase.GetCollection<Pessoa>(
                EventosDatabaseSettings.Value.PessoaCollectionName);
        }

        public async Task<List<Pessoa>> GetPessoasAsync() =>
            await _PessoaCollection.Find(_ => true).ToListAsync();
        
        public async Task<Pessoa?> GetPessoaAsync(string id) =>
            await _PessoaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreatePessoaAsync(Pessoa pessoa) =>
            await _PessoaCollection.InsertOneAsync(pessoa);

        public async Task UpdatePessoaAsync(string id, Pessoa updatedPessoa) =>
            await _PessoaCollection.ReplaceOneAsync(x => x.Id == id, updatedPessoa);
        
        public async Task RemovePessoaAsync(string id) =>
            await _PessoaCollection.DeleteOneAsync(x => x.Id == id);
}