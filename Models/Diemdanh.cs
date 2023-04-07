namespace fullstackCsharp.Models
{
    public class Diemdanh
    {
        public string id { get; set; }
        public string id_nv { get; set; }
        public string timein { get; set; }
        public string name;
        public string timeout { get; set; }
        public Diemdanh()
        {

        }
        public Diemdanh(string name, string timein, string timeout, string id,string id_nv)
        {
            this.name  = name;
            this.timein = timein;  
            this.timeout = timeout;
            this.id = id;
            this.id_nv = id_nv;
        }
    }
}
