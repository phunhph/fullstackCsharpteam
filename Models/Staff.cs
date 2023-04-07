namespace fullstackCsharp.Models
{
    public class Staff
    {
      
        private object v1;
        private object v2;
        private object v3;

        public Staff(object v1, object v2, object v3)
        {
            this.V1 = v1;
            this.v2 = v2;
            this.v3 = v3;
        }

        public object V1 { get => v1; set => v1 = value; }
        public object V2 { get => v2; set => v2 = value; }
        public object V3 { get => v3; set => v3 = value; }
    }
}
