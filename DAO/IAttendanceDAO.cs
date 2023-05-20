using fullstackCsharp.Models;
using fullstackCsharp.Models.ViewModel.Attendances;

namespace fullstackCsharp.DAO
{
    public interface IAttendanceDAO
    {
        public IEnumerable<AttendancesTotalHours> GetAttendancesTotalHours();

        public AttendancesTotalHours SelectAt();

        public IEnumerable<TotalWorkMonth> GetTotalWorkMonth(int? month, int? year);
        public IEnumerable<TotalWorkHoursSelf> GetTotalWorkHoursSelfs( int? year,int idu);


    }
}
