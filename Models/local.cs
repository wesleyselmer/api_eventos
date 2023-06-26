using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api_eventos.Models;

public class Local {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Endereço { get; set; }

    public string? Capacidade { get; set; }
}