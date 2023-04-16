using System.Drawing.Printing;

namespace fullstackCsharp.Models
{
    public class Staff
    {
        private string manv;
        private string tennv;
        private string sex;
        private string address;
        private string user;
        private string password;

    public Staff()
        {
        }

        public Staff(string manv, string tennv, string sex, string address, string user, string password)
        {
            this.manv = manv;
            this.tennv = tennv;
            this.sex = sex;
            this.address = address;
            this.user = user;
            this.password = password;
        }

        public string Manv { get => manv; set => manv = value; }
        public string Tennv { get => tennv; set => tennv = value; }
        public string Sex { get => sex; set => sex = value; }
        public string Address { get => address; set => address = value; }
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }



        //public Staff(int)
    }
    
}
