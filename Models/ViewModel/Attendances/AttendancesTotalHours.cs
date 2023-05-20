namespace fullstackCsharp.Models.ViewModel.Attendances
{
    public class AttendancesTotalHours
    {
        public int IdA { get; set; }
        public int IdU { get; set; }
        public string FullName { get; set; }
        public TimeSpan CheckIn { get; set; }
        public TimeSpan CheckOut { get; set; }
        public decimal totakwork { get; set; }

        internal static object FromSqlInterpolated(string v)
        {
            throw new NotImplementedException();
        }
    }
}
