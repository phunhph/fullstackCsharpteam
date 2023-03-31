
namespace fullstackCsharp.Models
{
    public class Login
    {

        public string user { get; set; }
        public string password { get; set; }
        public int rank { get; set; }
        public Login()
        {
            
        }
        public Login(string user, string password,int rank)
        {
            this.user = user;
            this.password = password;
            this.rank = rank;
        }
        
    }
}
