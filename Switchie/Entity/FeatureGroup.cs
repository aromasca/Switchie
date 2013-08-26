using System;

namespace Switchie.Entity
{
    public class FeatureGroup
    {
        private Guid id;
        private string name;
        private Feature feature;

        public virtual Guid Id
        {
            get { return id; }
            set { id = value; }
        }
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
        public virtual Feature Feature
        {
            get { return feature; }
            set { feature = value; }
        }
    }
}
