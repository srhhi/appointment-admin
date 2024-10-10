using FluentNHibernate.Mapping;
using TLCOnline.Models;
using TLCOnline.Resources;

namespace TLCOnline.Data.Mappings
{
    public class StaffMap : ClassMap<StaffModel>
    {
        public StaffMap()
        {
            Table(Labels.Staffs);
            Id(x => x.StaffId);
            Map(x => x.Position);
            Map(x => x.MyKad);
            Map(x => x.Email);
            Map(x => x.StaffName);
            Map(x => x.Gender);
            Map(x => x.PhoneNo);
            Map(x => x.BirthDate);
            Map(x => x.RegisterDate);
            Map(x => x.HireDate);
            Map(x => x.Password);
            Map(x => x.Status);
        }
    }
}