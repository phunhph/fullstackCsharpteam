namespace fullstackCsharp.Models
{
    public class Email
    {
        public string user { get; set; }
        public string To { get; set; }
        public string Subject;
        public string Body { get; set; }
        
        public Email()
        {
            
        }
        public Email (string To, string body, string user)
        {
            this.To = To;
            Subject = "Quên mật khẩu";
            Body = body;
            this.user = user;
        }
    }
}
