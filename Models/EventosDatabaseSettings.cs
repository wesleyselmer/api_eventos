namespace api_eventos.Models;

public class EventosDatabaseSettings 
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set;} = null!;
    public string EventosCollectionName { get; set; } = null!;
}