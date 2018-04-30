using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripXpert.DAL;
using System.Collections;

namespace TripXpert.Controllers
{
    public class AttractionsController : Controller
    {
        // GET: Attractions
        public ActionResult Attractions()
        {
            return View();
        }

        public ActionResult AttractionImages_Read([DataSourceRequest] DataSourceRequest request)
        {
            object data = TripXpertDAL.GetAllAttractionImages();
            return Json(((IEnumerable)data).Cast<object>().ToList().ToDataSourceResult(request));
        }

        public ActionResult AttractionInfoListView_Read([DataSourceRequest] DataSourceRequest request, string imageUrl)
        {
            if (string.Empty != imageUrl)
            {
                string url = imageUrl.Split(new char[] { '/' }).Last();
                object data = TripXpertDAL.GetFullAttractionInfo(url);
                return Json(((IEnumerable)data).Cast<object>().ToList().ToDataSourceResult(request));
            }
            return Json(string.Empty);
        }
    }
}