using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TripXpert.DAL
{
    public class TripXpertDAL
    {
        #region MapRegion

        private static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private static double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        public static Hashtable GetMapZoomSettings(int destinationID, int mapWidth, int mapHeight)
        {
            double paddingFactor = 1.2;
            List<DestinationMarker> markers = GetMarkers(destinationID);
            double minY = markers.Min(x => (double)x.Latitude);
            double maxY = markers.Max(x => (double)x.Latitude);
            double minX = markers.Min(x => (double)x.Longitude);
            double maxX = markers.Max(x => (double)x.Longitude);
            double centerY = (minY + maxY) / 2;
            double centerX = (minX + maxX) / 2;

            double minRadianY = Math.Log((Math.Sin(DegreeToRadian(minY)) + 1) / Math.Cos(DegreeToRadian(minY)));
            double maxRadianY = Math.Log((Math.Sin(DegreeToRadian(maxY)) + 1) / Math.Cos(DegreeToRadian(maxY)));
            double centerRadianY = (minRadianY + maxRadianY) / 2;
            centerY = RadianToDegree(Math.Atan(Math.Sinh(centerRadianY)));

            double deltaX = maxX - minX;
            double resolutionX = deltaX / mapWidth;

            double vy0 = Math.Log(Math.Tan(Math.PI * (0.25 + centerY / 360)));
            double vy1 = Math.Log(Math.Tan(Math.PI * (0.25 + maxY / 360)));
            double viewHeightHalf = mapHeight / 2;
            double zoomFactorPowered = viewHeightHalf / (40.7436654315252 * (vy1 - vy0));
            double resolutionY = 360.0 / (zoomFactorPowered * 256);

            double resolution = Math.Max(resolutionX, resolutionY) * paddingFactor;
            double zoom = Math.Log(360 / (resolution * 256), 2);

            Hashtable mapValues = new Hashtable();
            mapValues["Zoom"] = Math.Floor(zoom);
            mapValues["Latitude"] = centerY;
            mapValues["Longitude"] = centerX;
            return mapValues;

        }

        public static List<DestinationMarker> GetMarkers(int destinationID)
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                return (from marker in entity.DestinationMarkers
                        where marker.DestinationID == destinationID
                        select marker).ToList();
            }
        }

        public static object GetMapData(int destinationID)
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                return (from marker in entity.DestinationMarkers
                        where marker.DestinationID == destinationID
                        select new
                        {
                            Longitude = marker.Longitude,
                            Latitude = marker.Latitude,
                            Shape = "pinTarget"
                        }).ToList();
            }
        }

        #endregion

        #region DestinationsImages
        public const string domainURL = @"Images/Gallery/";
        public static string GetDestinationsQueryString(string title, string typeOfOffer, string priceRange)
        {

            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                string[] prices = priceRange.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                int? id = null;
                if (title != string.Empty)
                {
                    id = (from destination in entity.Destinations
                          where destination.Title.Contains(title)
                          select destination.DestinationID).First();
                    return id.ToString();
                }

                IEnumerable<TourInfo> infos = entity.TourInfos.ToList();
                List<Destination> destinations = (from destination
                                                  in entity.Destinations
                                                  select destination).ToList();

                if (prices.Length > 1)
                {
                    int lowestPrice = int.Parse(prices[0]);
                    int highestPrice = int.Parse(prices[1]);
                    infos = infos.Where(i => i.PerSingleOccupancy > lowestPrice && i.PerSingleOccupancy < highestPrice);
                    destinations = GetDestinationsByIds(infos.Select(x => x.DestinationID).Distinct().ToList());
                }
                if(typeOfOffer!="All")
                {
                    bool isSpecial=(typeOfOffer=="Special")?true:false;
                    destinations=destinations.Where(x => x.IsSpecial == isSpecial).ToList();
                }
                StringBuilder sb = new StringBuilder();
                sb.Append(destinations[0].DestinationID.ToString());
                for (int i = 1; i < destinations.Count; i++)
                {
                    sb.Append(",");
                    sb.Append(destinations[i].DestinationID.ToString());
                }
                return sb.ToString();
            }
        }

        public static IEnumerable<Image> GetSpecialDestinations()
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                var imagesExtracted = (from destination in entity.Destinations.Where(x => x.IsSpecial == true)
                                       from image in entity.Images.Where(a => a.DestinationID == destination.DestinationID && a.Description != null)
                                       from attraction in entity.Attractions.Where(a => a.AttractionID == image.AttractionID)
                                       select new
                                       {
                                           Description = image.Description,
                                           DestinationTitle=destination.Title,
                                           AttractionTitle=attraction.Title,
                                           Author = image.Author,
                                           ImageURL = domainURL + image.FolderName.Trim() + "/2000x1125/" + image.ImageURL,
                                           DestinationID = image.DestinationID
                                       }).ToList().Take(5);

                var imagesToReturn = (from imageData in imagesExtracted
                                      select new Image()
                                      {
                                          Description = imageData.Description,
                                          ImageURL = imageData.ImageURL,
                                          Title = imageData.DestinationTitle + " - " + imageData.AttractionTitle,
                                          DestinationID = imageData.DestinationID
                                      }
                 ).AsEnumerable();

                return imagesToReturn;
            }
        }

        public static object GetAllDestinationImages(int id)
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {                
                return entity.Images.Where(x => x.DestinationID == id).ToList(); 
            }
        }

        public static string GetDestinationDefaultImage(int id)
        {
            TripXpertEntities entity = new TripXpertEntities();

            using (entity)
            {
                Image defaultImage = (from image in entity.Images
                                      where image.DestinationID == id
                                      select image).ToList().First();

                return String.Format("{0}{1}/{2}", domainURL,defaultImage.FolderName.Trim(), defaultImage.ImageURL);
            }
        }

        public static string GetDestinationDetailImage(int id)
        {
            TripXpertEntities entity = new TripXpertEntities();

            using (entity)
            {
                var rnd = new Random();
                var detailImages = (from image in entity.Images
                                      where image.DestinationID == id
                                      select image).ToList();
                int randomIndex = rnd.Next(detailImages.Count);
                var detailImage = detailImages[randomIndex];

                return String.Format("{0}{1}/{2}", domainURL, detailImage.FolderName.Trim(), detailImage.ImageURL);
            }
        }

        public static Destination GetDestinationById(string id)
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                int destinationID = int.Parse(id);
                Destination dest = (from destination in entity.Destinations
                                    where destination.DestinationID == destinationID
                                    select destination).First();
                return dest;
            }
        }

        public static List<Destination> GetDestinationsByIds(List<int> ids)
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                var destinations = (from destination in entity.Destinations
                                    where ids.Contains(destination.DestinationID)
                                    select destination).ToList();
                return destinations;
            }
        }

        public static List<Destination> GetAllDestinations()
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {              
                return entity.Destinations.ToList();
            }
        }

        public static List<Destination> GetFirstDestinations()
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                var destinations = (from destionation in entity.Destinations select destionation).Take(3).ToList();
                return destinations;
            }
        }

        public static object GetFullDestinationInfo()
        {
            Dictionary<string, List<object>> dataDictionary = new Dictionary<string, List<object>>();
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                List<Destination> destinations = GetAllDestinations();
                foreach (Destination destination in destinations)
                {
                    dataDictionary.Add(destination.Title,new List<object>());
                    dataDictionary[destination.Title].Add(GetLowestPriceForDestination(destination.DestinationID, null, null).ToString());
                    dataDictionary[destination.Title].Add(destination.IsSpecial);
                    dataDictionary[destination.Title].Add(destination.DestinationID);
                }
                return dataDictionary;
            }
        }

        #endregion

        #region AttractionImages

        public static object GetAllAttractionImages()
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                return (from image in entity.Images
                        where image.AttractionID != null
                        select new
                        {
                            Title = image.Title,
                            ImageID = image.ImageID,
                            ImageURL = domainURL + image.FolderName.Trim() + "/" + image.ImageURL
                        }).ToList();
            }
        }

        public static Image GetAttractionImage(int attractionID)
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                Image attractionImage = (from image in entity.Images
                                         where image.AttractionID == attractionID
                                         select image).ToList().First();
                attractionImage.ImageURL = String.Format("{0}{1}/{2}", domainURL, attractionImage.FolderName.Trim(), attractionImage.ImageURL);

                return attractionImage;
            }
        }

        public static List<Attraction> GetAttactionsForDestionation(int destinationID)
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                return (from attraction in entity.Attractions
                        where attraction.DestinationID == destinationID
                        select attraction).ToList();
            }
        }

        public static object GetFullAttractionInfo(string url)
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                Image attractionImage = (from image in entity.Images.Where(a => a.ImageURL == url) select image).First();
                var data = (from attraction in entity.Attractions.Where(x => x.AttractionID == attractionImage.AttractionID)
                            from destination in entity.Destinations.Where(a => a.DestinationID == attraction.DestinationID)
                            select new
                            {
                                DestinationTitle = destination.Title,
                                ImageURL =  domainURL+attractionImage.FolderName.Trim() + "/" + attractionImage.ImageURL,
                                ImageTitle = attractionImage.Title,
                                Location = attraction.Location,
                                Description = attraction.Description,
                                AttractionTitle = attraction.Title,
                                DestinationID = destination.DestinationID
                            }).ToList();
                return data;
            }
        }

        #endregion

        #region TourInfoMethods

        public static List<TourInfo> GetTourInfos(int id)
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                return (from tourInfo in entity.TourInfos
                        where tourInfo.DestinationID == id
                        select tourInfo).ToList();
            }
        }

        public static int GetLowestPriceForDestination(int id, int? min, int? max)
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                if (min.HasValue && max.HasValue)
                {
                    return (int)GetTourInfos(id).Where(t=> t.PerSingleOccupancy > min.Value && t.PerSingleOccupancy < max.Value).First().PerSingleOccupancy;
                }
                else
                {
                    return (int)GetTourInfos(id).Min(x => x.PerSingleOccupancy);
                }
            }
        }

        public static DateTime? GetLatestDate(int id)
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                return (DateTime?)GetTourInfos(id).Max(x => x.StartDate);
            }
        }

        #endregion

        public static Random rnd = new Random();

        public static Testimonial GetTestimonial(int testimonialID)
        {
            TripXpertEntities entity = new TripXpertEntities();
            using (entity)
            {
                return (from testimonial in entity.Testimonials
                        where testimonial.ID == testimonialID
                        select testimonial).First();
            }
        }

        public static List<string> GetCountries()
        {
            List<string> list = new List<string>();
            foreach (CultureInfo info in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
            {
                RegionInfo regionInfo = new RegionInfo(info.LCID);
                if (!list.Contains(regionInfo.EnglishName))
                {
                    list.Add(regionInfo.EnglishName);
                }
            }
            list.Sort();
            return list;
        }
    }
}
