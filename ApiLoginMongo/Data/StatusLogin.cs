using ApiLoginMongo.Entities;

namespace ApiLoginMongo.Data
{
    public class StatusLogin
    {
        public User User { get; set; }
        public StatusLoginResult StatusLoginResult { get; set; }
    }
}
