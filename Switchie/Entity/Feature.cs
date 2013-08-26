using System;
using System.Collections.Generic;
using Switchie.Data;

namespace Switchie.Entity
{
    public class Feature : IPollableItem
    {
        private Guid id;
        private DateTime lastModifiedDate;
        private int percentage;
        private string name;
        private bool isEnabled;
        private IList<FeatureGroup> groups;
        private IList<FeatureUser> users; 

        public virtual Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        public virtual DateTime LastModifiedDate
        {
            get { return lastModifiedDate; }
            set { lastModifiedDate = value; }
        }

        public virtual int Percentage
        {
            get { return percentage; }
            set { percentage = value; }
        }
        public virtual string Name
        {
            get { return name; }
            set { name = value; }
        }
        public virtual bool IsEnabled
        {
            get { return isEnabled; }
            set { isEnabled = value; }
        }
        public virtual IList<FeatureGroup> Groups
        {
            get { return groups; }
            set { groups = value; }
        }
        public virtual IList<FeatureUser> Users
        {
            get { return users; }
            set { users = value; }
        }
    }
}
