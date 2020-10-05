namespace ServerlessStreamR.Client.Models
{
    public class UserInformation
    {
        public ClientPrincipal ClientPrincipal { get; set; }
    }

    public class ClientPrincipal
    {
        public string IdentityProvider { get; set; }
        public string UserDetails { get; set; }
    }
}