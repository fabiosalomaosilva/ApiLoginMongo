namespace ApiLoginMongo.Dtos
{
    public class ResponseRegisterDto
    {
        public ResponseRegisterModelDto Model { get; set; }
        public string Message { get; set; }
        public ResponseRegisterStatus ResponseRegisterStatus { get; set; }
    }

    public class ResponseRegisterModelDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CellphoneNumber { get; set; }
        public string Role { get; set; }
    }

    public enum ResponseRegisterStatus
    {
        Success,
        EmailDuplicado,
        ErroServidor
    }
}
