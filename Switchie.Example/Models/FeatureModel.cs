using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Switchie.Example.Models
{
    public class FeatureModel
    {
        public Guid? Id { get; set; }
        public bool IsOn { get; set; }
        public string GroupName { get; set; }
    }
}