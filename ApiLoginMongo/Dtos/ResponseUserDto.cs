namespace ApiLoginMongo.Dtos
{
    public class ResponseUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string CellphoneNumber { get; set; }
        public TokenDto Token { get; set; }
        public string RoleId { get; set; }
        public string Role { get; set; }

    }
}
