using FluentNHibernate.Mapping;

namespace Switchie.Entity
{
    public class FeatureMap : ClassMap<Feature>
    {
        public FeatureMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.LastModifiedDate)
                .Not.Nullable().Default("getDate()")
                .Index("IX_Feature_LastModifiedDate");
            Map(x => x.Name)
                .Not.Nullable()
                .Length(1000)
                .Unique();
            Map(x => x.Percentage)
                .Not.Nullable();
            Map(x => x.IsEnabled)
                .Not.Nullable();
            HasMany(x => x.Groups).KeyColumn("FeatureId").Inverse().Cascade.All().Not.LazyLoad();
            HasMany(x => x.Users).KeyColumn("FeatureId").Inverse().Cascade.All().Not.LazyLoad();
        }
    }

    public class FeatureGroupMap : ClassMap<FeatureGroup>
    {
        public FeatureGroupMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Name)
                .Not.Nullable()
                .Length(1000)
                .Unique();
            References(x => x.Feature, "FeatureId").Cascade.None();
        }
    }

    public class FeatureUserMap : ClassMap<FeatureUser>
    {
        public FeatureUserMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.Name)
                .Not.Nullable()
                .Length(1000)
                .Unique();
            References(x => x.Feature, "FeatureId").Cascade.None();
        }
    }
}
