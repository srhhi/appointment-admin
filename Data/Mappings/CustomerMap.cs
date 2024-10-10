using FluentNHibernate.Mapping;
using TLCOnline.Models;
using TLCOnline.Resources;

namespace TLCOnline.Data.Mappings
{
    public class CustomerMap : ClassMap<CustomerModel>
    {
        public CustomerMap() 
        {
            Table(Labels.Customers);
            Id(x => x.CustMyKad);
            Map(x => x.Email);
            Map(x => x.CustName);
            Map(x => x.Gender);
            Map(x => x.PhoneNo);
            Map(x => x.BirthDate);
            Map(x => x.RegisterDate);
        }
    }
}