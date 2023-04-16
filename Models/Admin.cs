namespace fullstackCsharp.Models
{
    public class Admin
    {
        public Admin(string Manv, string Tennv, string sex)
        {
            Manv = Manv;
            Tennv = Tennv;
            sex = sex;

        }
        public string Manv { get; }
        public string Tennv { get; }
        public string sex { get; }
    }
}
