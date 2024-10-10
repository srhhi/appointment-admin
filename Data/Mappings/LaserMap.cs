using FluentNHibernate.Mapping;
using TLCOnline.Models;
using TLCOnline.Resources;

namespace TLCOnline.Data.Mappings
{
    public class LaserMap : ClassMap<LaserModel>
    {
        public LaserMap()
        {
            Table(Labels.Lasers);
            Id(x => x.LaserName);
            Map(x => x.LaserDesc);
            Map(x => x.NormalPrice);
            Map(x => x.Discount);
            Map(x => x.LaserPrice);
        }
    }
}