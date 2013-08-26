using System;
using System.Collections.Concurrent;
using System.Configuration;
using Switchie.Entity;

namespace Switchie.Data
{
    public class FeaturePoller
    {
        public ConcurrentDictionary<string, Feature> Features;
        private readonly FluentNhibernatePoller<Feature> _fluentNhibernatePoller = new FluentNhibernatePoller<Feature>(
            TimeSpan.FromSeconds(Convert.ToInt32(ConfigurationManager.AppSettings["Feature.Seconds.To.Poll.Time"] ?? "60")));
        public FeaturePoller()
        {
            Features = new ConcurrentDictionary<string, Feature>();
            _fluentNhibernatePoller.ItemsChanged += FeaturePoller_ItemsChanged;
            _fluentNhibernatePoller.Start();
        }

        private void FeaturePoller_ItemsChanged(object sender, ItemsChangedArgs<Feature> e)
        {
            foreach (var feature in e.ItemsChanged)
                Features.AddOrUpdate(feature.Name, feature, (oK, oV) => feature);
        }
    }
}
