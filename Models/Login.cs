
namespace fullstackCsharp.Models
{
    public class Login
    {

        public string user { get; set; }
        public string password { get; set; }
        public string name;
        public int rank { get; set; }
        public Login()
        {
            
        }
        public Login(string user, string password,int rank,string names)
        {
            this.user = user;
            this.password = password;
            this.rank = rank;
            this.name = names;
        }

       
    }
}
