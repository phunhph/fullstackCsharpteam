namespace fullstackCsharp.Models
{
    public class Diemdanh
    {
        public DateTime id { get; set; }
        public string id_nv { get; set; }
        public string id_a { get; set; }
        public string timein { get; set; }
        public string name { get; set; }
        public string timeout { get; set; }
        public Diemdanh()
        {

        }
        public Diemdanh(DateTime id, string id_nv,string name, string timein, string timeout,string id_a)
        {
            this.name  = name;
            this.timein = timein;  
            this.timeout = timeout;
            this.id = id;
            this.id_nv = id_nv;
            this.id_a = id_a;
        }

    }
}
