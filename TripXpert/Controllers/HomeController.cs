using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using TripXpert.DAL;

namespace TripXpert.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetDestinations()
        {
            //TripXpertDAL.GetSpecialDestinations()
            List<Destination> data = TripXpertDAL.GetAllDestinations();
            foreach (Destination item in data)
            {
                item.DefaultImage = TripXpertDAL.GetDestinationDefaultImage(item.DestinationID, 'S');
                item.LowestPrice = TripXpertDAL.GetLowestPriceForDestination(item.DestinationID, null, null);
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSpecialDestinations()
        {
            return Json(TripXpertDAL.GetSpecialDestinations(), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult GetDestinationsQuery(string priceRange, string offerType)
        {
            var result = TripXpertDAL.GetDestinationsQueryString(String.Empty, offerType == "" ? "All" : offerType, priceRange);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public void GetPhotoInCertainSize(string path, int width, int height)
        {
            var rootDirecory = System.AppDomain.CurrentDomain.BaseDirectory;

                new WebImage(rootDirecory+path)
                    .Resize(width, height, false, true) 
                    .Crop(1, 1)
                    .Write();
        }
    }
}