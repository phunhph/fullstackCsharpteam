using System;
using System.Collections.Generic;

namespace fullstackCsharp.Models;

public partial class Attendane
{
    public int IdA { get; set; }

    public DateTime AttendaneDate { get; set; }

    public TimeSpan? Checkin { get; set; }

    public TimeSpan? Checkout { get; set; }

    public int IdU { get; set; }

    public virtual User? IdUNavigation { get; set; }
    public double? WorkHours
    {
        get
        {
            if (Checkin == null & Checkout == null)
            {
                return null;
            }
            else if (Checkout == null)
            {
                return null;
            }
            else if (Checkin == null)
            {
                return null;
            }
            else
            {
                double workHours = 0;

                // nếu gán checkin = checkintime (nếu checkin rỗng gán = timespan zero == 0)
                TimeSpan checkinTime = Checkin ?? TimeSpan.Zero;
                //như checkin
                TimeSpan checkoutTime = Checkout ?? TimeSpan.Zero;

                //nghỉ trưa
                TimeSpan breakTime = new TimeSpan(1, 0, 0);

                //thời gian bắt đầu làm
                TimeSpan workDayStart = new TimeSpan(8, 5, 0);

                //thời gian tan làm
                TimeSpan workDayEnd = new TimeSpan(17, 5, 0);

                //về sớm buổi sáng
                TimeSpan leaveMorning = new TimeSpan(9, 30, 0);

                //về sớm buổi chiều
                TimeSpan leaveAfter = new TimeSpan(15, 30, 0);

                //thời gian bắt đầu buổi sáng
                TimeSpan morningStart = new TimeSpan(8, 5, 0);

                //thời gian kết thúc buổi sáng
                TimeSpan morningEnd = new TimeSpan(12, 5, 0);

                //thời gian bắt đầu buổi chiều
                TimeSpan afterStart = new TimeSpan(13, 5, 0);

                //thời gian kết thúc buổi chiều
                TimeSpan afterEnd = new TimeSpan(17, 5, 0);

                //-------lửng lơ----------
                // vào > 9h30 ra <16h30
                if (checkinTime > leaveMorning && checkoutTime < leaveAfter)
                {
                    workHours = 0;
                }

                else if (checkinTime <= workDayStart && checkoutTime > workDayStart && checkoutTime < leaveMorning)
                {
                    workHours = 0;
                }
                else if (checkinTime > leaveAfter)
                {
                    workHours = 0;
                }
                //--------- CẢ NGÀY------------
                // vào <8h ra >17h 
                if (checkinTime <= workDayStart && checkoutTime >= workDayEnd)
                {
                    workHours = (workDayEnd - workDayStart - breakTime).TotalHours;
                }
                // vào < 8h ra sau 15h30 trước 17h = về sớm
                else if (checkinTime <= workDayStart && checkoutTime >= leaveAfter && checkoutTime <= workDayEnd)
                {
                    workHours = (checkoutTime - workDayStart - breakTime).TotalHours;
                }
                // vào >8h - 9h30 ra >17h = đi muộn 
                else if (checkinTime > workDayStart && checkinTime <= leaveMorning && checkoutTime >= workDayEnd)
                {
                    workHours = (workDayEnd - checkinTime - breakTime).TotalHours;
                }
                // vào 8h-9h30 ra 15h30-17h = đi muộn về sớm
                else if (checkinTime > workDayStart && checkinTime <= leaveMorning && checkoutTime >= leaveAfter && checkoutTime < workDayEnd)
                {
                    workHours = (checkoutTime - checkinTime - breakTime).TotalHours;
                }
                // ------------- BUỔI SÁNG ---------
                // vào <8h ra 12h-15h = làm buổi sáng = 4 giờ
                if (checkinTime <= morningStart && checkoutTime >= morningEnd && checkoutTime < leaveAfter)
                {
                    workHours = (morningEnd - morningStart).TotalHours;
                }
                //vào <8h ra 9h30-12h00 = về sớm
                else if (checkinTime <= morningStart && checkoutTime > leaveMorning && checkoutTime < morningEnd)
                {
                    workHours = (checkoutTime - morningStart).TotalHours;
                }
                // vào 8h-9h30 ra 12h-15h30 = đi muộn
                else if (checkinTime > morningStart && checkinTime <= leaveMorning && checkoutTime >= morningEnd && checkoutTime <= leaveAfter)
                {
                    workHours = (morningEnd - checkinTime).TotalHours;
                }
                // vào 8h-9h30 ra 9h30-12h = đi muộn về sớm
                else if (checkinTime > morningStart && checkinTime <= leaveMorning && checkoutTime > leaveMorning && checkoutTime < morningEnd)
                {
                    workHours = (checkoutTime - checkinTime).TotalHours;
                }
                //--------- BUỔI CHIỀU -----------
                // vào 9h30-13h05 về >17h05 = công chiều = 4h
                if (checkinTime > leaveMorning && checkinTime <= afterStart && checkoutTime >= afterEnd)
                {
                    workHours = (afterEnd - afterStart).TotalHours;
                }
                //vào 9h30 -13h05 về 15h30-17h30 = về sớm
                else if (checkinTime > leaveMorning && checkinTime <= afterStart && checkoutTime > leaveAfter && checkoutTime < afterEnd)
                {
                    workHours = (checkoutTime - afterStart).TotalHours;
                }
                //vào 13h05 - 15h50 về 15h05 - 17h05 = đi muộn về sớm
                else if (checkinTime > afterStart && checkinTime <= leaveAfter && checkoutTime > leaveAfter && checkoutTime < afterEnd)
                {
                    workHours = (checkoutTime - checkinTime).TotalHours;
                }
                // vào 13h05 - 15h30 về >= 17h05 = đi muộn
                else if (checkinTime > afterStart && checkinTime <= leaveAfter && checkoutTime >= afterEnd)
                {
                    workHours = (afterEnd - checkinTime).TotalHours;
                }

                return workHours;
            }

        }
    }
}