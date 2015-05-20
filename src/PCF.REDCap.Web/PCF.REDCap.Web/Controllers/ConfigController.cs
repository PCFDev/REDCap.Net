using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using PCF.REDCap.Web.Mvc;

namespace PCF.REDCap.Web.Controllers
{
    [Authorize]
    public class ConfigController : Controller
    {
        [ClaimAuthorize(ClaimTypes.GroupSid, "S-1-5-21-620581126-1294811165-624655392-31026")]
        public ActionResult Index()
        {
            return View();
        }
    }
}