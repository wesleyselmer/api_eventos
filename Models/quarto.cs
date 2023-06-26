using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api_eventos.Models;

public class Quarto {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Capacidade { get; set; }

    public string? Detalhes { get; set; }
}