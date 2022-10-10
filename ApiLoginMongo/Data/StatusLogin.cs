using ApiLoginMongo.Dtos;

namespace ApiLoginMongo.Data
{
    public class StatusLogin
    {
        public ResponseUserDto ResponseUser { get; set; }
        public StatusLoginResult StatusLoginResult { get; set; }
    }
}
