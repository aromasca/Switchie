using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Switchie.Data;
using Switchie.Example.Models;

namespace Switchie.Example.Controllers
{
    public class FeatureController : Controller
    {
        public ActionResult Fifty()
        {
            Guid userGeneratedId = Guid.NewGuid();
            var res = MvcApplication._Switchie.User_Is_User_In_Percntage("featuretest", userGeneratedId);
            var toView = new FeatureModel{Id = userGeneratedId, IsOn = res};
            return View(toView);
        }

        public ActionResult Group(string groupName)
        {
            var res = MvcApplication._Switchie.Is_In_Groups("featuretest", groupName);
            var toView = new FeatureModel { IsOn = res, GroupName =  groupName};
            return View(toView);
        }


        public ActionResult Global()
        {
            var res = MvcApplication._Switchie.Is_Feature_Enabled("featuretest");
            var toView = new FeatureModel { IsOn = res, };
            return View(toView);
        }

    }
}
