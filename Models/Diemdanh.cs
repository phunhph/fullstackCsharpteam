namespace fullstackCsharp.Models
{
    public class Diemdanh
    {
        public string id { get; set; }
        public string id_nv { get; set; }
        public string timein { get; set; }
        public string name { get; set; }
        public string timeout { get; set; }
        public Diemdanh()
        {

        }
        public Diemdanh(string id, string id_nv,string name, string timein, string timeout)
        {
            this.name  = name;
            this.timein = timein;  
            this.timeout = timeout;
            this.id = id;
            this.id_nv = id_nv;
        }

    }
}
