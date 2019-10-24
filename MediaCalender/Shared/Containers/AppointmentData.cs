using System;
using System.Collections.Generic;
using System.Text;

namespace MediaCalender.Shared.Containers
{
    public class AppointmentData
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsAllDay = true;
        public bool IsReadonly = true;
    }
}
