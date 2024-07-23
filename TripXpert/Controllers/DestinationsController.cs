using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TripXpert.DAL;
using TripXpert.Models;

namespace TripXpert.Controllers
{
    public class DestinationsController : Controller
    {
        public ActionResult Destinations(bool? isSpecial, int? priceFrom, int? priceTo)
        {
            int[] model = null;
            var items = TripXpertDAL.GetAllDestinations();
            if (isSpecial != null)
            {
                items = items.Where(x => x.IsSpecial == isSpecial).ToList();
            }

            if (priceFrom != null)
            {
                items = items.Where(x => TripXpertDAL.GetLowestPriceForDestination(x.DestinationID, null, null) >= priceFrom).ToList();
            }

            if (priceTo != null)
            {
                items = items.Where(x => TripXpertDAL.GetLowestPriceForDestination(x.DestinationID, null, null) <= priceTo).ToList();
            }

            if (isSpecial != null || priceFrom != null || priceTo != null)
            {
                model = items.Select(x => x.DestinationID).ToArray();
            }


            return View(model);
        }

        public ActionResult DestinationDetails(int id)
        {
            DestinationViewModel destination = TripXpertDAL.GetAllDestinations().Where(x => x.DestinationID == id).Select(s => new DestinationViewModel()
            {
                DestinationID = s.DestinationID,
                DefaultImage = TripXpertDAL.GetDestinationDefaultImage(s.DestinationID),
                DetailImage = TripXpertDAL.GetDestinationDetailImage(s.DestinationID),
                LowestPrice = TripXpertDAL.GetLowestPriceForDestination(s.DestinationID, null, null),
                TestimonialID = s.TestimonialID,
                IsSpecial = s.IsSpecial,
                Title = s.Title,
                ShortDescription = s.ShortDescription.Split('|'),
                FullDescription = s.FullDescription,
                Duration = s.Duration,
                VideoURL = s.VideoURL,
                TourInfos = TripXpertDAL.GetTourInfos(s.DestinationID).Select(t => new Models.TourInfo()
                {
                    InfoID = t.InfoID,
                    DestinationID = t.DestinationID,
                    StartDate = t.StartDate,
                    EndDate = t.EndDate,
                    PerPersonSharing = t.PerPersonSharing,
                    PerSingleOccupancy = t.PerSingleOccupancy,
                    PerChild = t.PerChild,
                    Duration = t.Duration,
                    TourDates = GetTourDates((DateTime)t.StartDate, (DateTime)t.EndDate)
                }),
                Testimonial = new Models.Testimonial(TripXpertDAL.GetTestimonial(s.TestimonialID)),
                Attractions = TripXpertDAL.GetAttactionsForDestionation(s.DestinationID).Select(a => new Models.Attraction()
                {
                    AttractionID = a.AttractionID,
                    DestinationID = a.DestinationID,
                    Title = a.Title,
                    Location = a.Location,
                    Description = a.Description,
                    Image = new Models.Image(TripXpertDAL.GetAttractionImage(a.AttractionID))
                }),
                MapInfo = new MapInfo()
                {
                    ZoomSettings = TripXpertDAL.GetMapZoomSettings(s.DestinationID, 320, 350),
                    Markers = TripXpertDAL.GetMarkers(s.DestinationID).Select(m => new Marker((double)m.Latitude, (double)m.Longitude, s.Title))
                }
            }).FirstOrDefault();


            return View(destination);
        }


        private List<DateTime> GetTourDates(DateTime startDate, DateTime endDate)
        {
            List<DateTime> dates = new List<DateTime>();

            TimeSpan dayDiff = endDate.Subtract(startDate);

            for (int i = 0; i < dayDiff.Days; i++)
            {
                dates.Add(startDate.AddDays(i));
            }

            return dates;
        }

        public JsonResult GetCountries()
        {
            var countries = TripXpertDAL.GetCountries();

            return Json(countries, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Destinations_Read([DataSourceRequest] DataSourceRequest request, int[] ids)
        {
            IEnumerable<DestinationViewModel> data = TripXpertDAL.GetAllDestinations().Select(s => new DestinationViewModel()
            {
                DestinationID = s.DestinationID,
                DefaultImage = TripXpertDAL.GetDestinationDefaultImage(s.DestinationID),
                LowestPrice = TripXpertDAL.GetLowestPriceForDestination(s.DestinationID, null, null),
                TestimonialID = s.TestimonialID,
                IsSpecial = s.IsSpecial,
                Title = s.Title,
                ShortDescription = s.ShortDescription.Split('|'),
                FullDescription = s.FullDescription,
                Duration = s.Duration,
                VideoURL = s.VideoURL,
                Rating = s.Rating,
                DetailImage = TripXpertDAL.GetDestinationDefaultImage(s.DestinationID),
            });

            if (ids != null)
            {
                data = data.Where(d => ids.Contains(d.DestinationID));
            }

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Destinations_First()
        {
            IEnumerable<DestinationViewModel> data = TripXpertDAL.GetFirstDestinations().Select(s => new DestinationViewModel()
            {
                DestinationID = s.DestinationID,
                DefaultImage = TripXpertDAL.GetDestinationDefaultImage(s.DestinationID),
                LowestPrice = TripXpertDAL.GetLowestPriceForDestination(s.DestinationID, null, null),
                TestimonialID = s.TestimonialID,
                IsSpecial = s.IsSpecial,
                Title = s.Title,
                ShortDescription = s.ShortDescription.Split('|'),
                FullDescription = s.FullDescription,
                Duration = s.Duration,
                VideoURL = s.VideoURL,
                Rating = s.Rating,
                DetailImage = TripXpertDAL.GetDestinationDefaultImage(s.DestinationID),
            });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDestinationImages(int destinationId)
        {
            var data = TripXpertDAL.GetAllDestinationImages(destinationId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PriceInfo_Read([DataSourceRequest] DataSourceRequest request)
        {
            var data = TripXpertDAL.GetAllDestinations().Select(s => new DestinationViewModel()
            {
                DestinationID = s.DestinationID,
                DefaultImage = TripXpertDAL.GetDestinationDefaultImage(s.DestinationID),
                LowestPrice = TripXpertDAL.GetLowestPriceForDestination(s.DestinationID, null, null),
                TestimonialID = s.TestimonialID,
                IsSpecial = s.IsSpecial,
                Title = s.Title,
                ShortDescription = s.ShortDescription.Split('|'),
                FullDescription = s.FullDescription,
                Duration = s.Duration,
                VideoURL = s.VideoURL,
            });

            return Json(data.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


    }
}