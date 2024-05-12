namespace Hotel.MVC.Controllers
{
    public partial class AccountController
    {
        public class TokenResponse
        {
            public string Token { get; set; }
            public DateTime Expiration { get; set; }
        }
     
    }
}
