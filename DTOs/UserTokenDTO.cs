namespace APICatalogo.DTOs
{
    public class UserTokenDTO
    {
        public string? Message { get; set; }
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool Authenticated { get; set; }

    }
}
