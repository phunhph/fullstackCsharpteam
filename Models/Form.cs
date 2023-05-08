namespace fullstackCsharp.Models
{
    public class Form
    {
        public int Soform { get; set; }
        public string id { get; set; }
        public string id_nv { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public float tong { get; set; }
        public string TrangThai { get; set; }

        public float check { get; set; }
        public Form()
        {

        }
        public Form(int Soform , string id, string id_nv, string start, string end, string TrangThai, float tong, float check)
        {
            this.Soform = Soform;
            this.id = id;
            this.id_nv = id_nv;
            this.start = start;
            this.end = end;
            this.TrangThai = TrangThai;
            this.tong = tong;
            this.check = check;
        }
    }
}
