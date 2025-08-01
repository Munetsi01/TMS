namespace Api.Models.User
{
    public class LoginUserResponse : IResponse
    {
        public string Token { get; set; }

        public DateTime TokenExpiryDate { get; set; }
    }
}
