using FluentNHibernate.Mapping;
using TLCOnline.Models;
using TLCOnline.Resources;

namespace TLCOnline.Data.Mappings
{
    public class AppointmentMap : ClassMap<AppointmentModel>
    {
        public AppointmentMap()
        {
            Table(Labels.Appointments);
            Id(x => x.AppointId);
            Map(x => x.CustMyKad);
            Map(x => x.LaserName);
            Map(x => x.LaserPrice);
            Map(x => x.StaffId);
            Map(x => x.AppointDate);
            Map(x => x.AppointTime);
            Map(x => x.Status);
        }
    }
}