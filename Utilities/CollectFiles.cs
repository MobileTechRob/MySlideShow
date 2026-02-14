using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySlideShow.Utilities
{
    public  class CollectFiles
    {
        public static List<string> SortPhotosByTimeFrame(Dictionary<string, DateTime> photoDateTimeDict, DateTime laterDate, DateTime earlierDate, TimeUnit timeUnit, int qtyTimeUnit, SortOrder sortOrder = SortOrder.Descending)
        {
            const int DaysInWeek = 7;
            //  sort list descending by time because the app will start with the most current first

            if ((photoDateTimeDict == null) || (photoDateTimeDict.Count == 0))
                return null!;

            if (timeUnit == TimeUnit.Minute)
            {
                earlierDate = laterDate.AddMinutes(0 - qtyTimeUnit);
            }
            else if (timeUnit == TimeUnit.Hour)
            {
                earlierDate = laterDate.AddHours(0 - qtyTimeUnit);
            }
            else if (timeUnit == TimeUnit.Day)
            {
                earlierDate = laterDate.AddDays(0 - qtyTimeUnit);
            }
            else if (timeUnit == TimeUnit.Week)
            {
                earlierDate = laterDate.AddDays(0 - (qtyTimeUnit * DaysInWeek));
            }

            List<KeyValuePair<string, DateTime>> listOfKVPSortedPhotos = new List<KeyValuePair<string, DateTime>>();
            List<string> listOfSortedPhotos = new List<string>();

            if (sortOrder == SortOrder.Ascending)
            {
                listOfKVPSortedPhotos = photoDateTimeDict.Where(kvp => (kvp.Value <= laterDate && kvp.Value >= earlierDate))
                                                            .OrderBy(kvp => kvp.Value)
                                                            .ToList();
            }
            else if (sortOrder == SortOrder.Descending) 
            {
                listOfKVPSortedPhotos = photoDateTimeDict.Where(kvp => (kvp.Value <= laterDate && kvp.Value >= earlierDate))
                                                                .OrderByDescending(kvp => kvp.Value)
                                                                .ToList();
            }

            foreach (KeyValuePair<string, DateTime> item in listOfKVPSortedPhotos)
                listOfSortedPhotos.Add(item.Key);

            return listOfSortedPhotos;
        }

    }
}
