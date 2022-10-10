using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiLoginMongo.Entities
{
    public class Role
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<string> UsersId { get; set; }
    }
}
