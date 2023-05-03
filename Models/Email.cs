namespace fullstackCsharp.Models
{
    public class Email
    {
     
        public string To { get; set; }
        public string Subject;
        public string Body { get; set; }
        
        public Email()
        {
            
        }
        public Email (string To, string body)
        {
            this.To = To;
            Subject = "Quên mật khẩu";
            Body = body;
            
        }
    }
}
