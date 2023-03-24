namespace fullstackCsharp.Models
{
    public class Admin
    {
        public Admin(object v1, object v2, object v3)
        {
            V1 = v1;
            V2 = v2;
            V3 = v3;
        }
        public object V1 { get; }
        public object V2 { get; }
        public object V3 { get; }
    }
}
