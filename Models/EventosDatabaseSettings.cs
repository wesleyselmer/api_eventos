namespace api_eventos.Models;

public class EventosDatabaseSettings 
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set;} = null!;
    public string EventoCollectionName { get; set; } = null!;
    public string LocalCollectionName { get; set; } = null!;
    public string PessoaCollectionName { get; set; } = null!;
    public string QuartoCollectionName { get; set; } = null!;
}