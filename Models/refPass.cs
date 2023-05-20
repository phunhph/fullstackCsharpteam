namespace fullstackCsharp.Models
{
    public class refPass
    {
        public string password { get; set; }
        public string newpassword { get; set; }
        public string key { get; set; }

        public refPass() { }

        public refPass(string password, string newpassword, string key) {

            this.password = password;
            this.newpassword = newpassword;
            this.key = key;


        }
    }
}
