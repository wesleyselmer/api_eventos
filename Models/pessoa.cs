using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api_eventos.Models;

public class Pessoa {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string Nome { get; set; } = null!;

    public string? Sobrenome { get; set; }

    public string CPF { get; set; } = null!;

    public string? DataNascimento { get; set; }
}