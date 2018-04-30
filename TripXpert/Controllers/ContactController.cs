using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripXpert.DAL;

namespace TripXpert.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }

        public JsonResult GetCountries()
        {
            return Json(TripXpertDAL.GetCountries(), JsonRequestBehavior.AllowGet);
        }
    }
}