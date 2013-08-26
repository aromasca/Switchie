using System;
using System.IO;
using System.Linq;
using Switchie.Data;
using Switchie.Entity;

namespace Switchie
{
    public class Switchie
    {
        private readonly FeaturePoller _featurePoller;

        public Switchie()
        {
            _featurePoller = new FeaturePoller();
        }

        public bool User_Is_User_In_Percntage(string feature, Guid userid)
        {
            var hashCode = string.Format("{0}_{1}", userid, feature).GetHashCode();
            Feature feat;
            if (_featurePoller.Features.TryGetValue(feature, out feat))
                return (Math.Abs(hashCode) % 100) < feat.Percentage;
            
            throw new DirectoryNotFoundException("couldnt find feature in features dictionary!");
        }

        public bool Is_Feature_Enabled(string feature)
        {
            Feature feat;
            if (_featurePoller.Features.TryGetValue(feature, out feat))
                return feat.IsEnabled;
            
                throw new DirectoryNotFoundException("couldnt find feature in features dictionary!");
        }

        public bool Is_In_Users(string feature, Guid userId)
        {
            Feature feat;
            if (_featurePoller.Features.TryGetValue(feature, out feat))
                return feat != null && feat.IsEnabled && feat.Users.Count(x => x.Id == userId) > 0;
            
                throw new DirectoryNotFoundException("couldnt find feature in features dictionary!");
        }

        public bool Is_In_Groups(string feature, string group)
        {
            Feature feat;
            if (_featurePoller.Features.TryGetValue(feature, out feat))
                return feat != null && feat.IsEnabled && feat.Groups.Count(x => x.Name == group) > 0;
            
                throw new DirectoryNotFoundException("couldnt find feature in features dictionary!");
        }
    }
}
