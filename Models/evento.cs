using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api_eventos.Models;

public class Evento {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Data { get; set; }

    public string Local { get; set; } = null!;
}