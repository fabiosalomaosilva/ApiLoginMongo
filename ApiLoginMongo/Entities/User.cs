using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiLoginMongo.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }
        public bool EmailValidated { get; set; }
        public bool Active { get; set; }
        public string CellphoneNumber { get; set; }
    }
}
