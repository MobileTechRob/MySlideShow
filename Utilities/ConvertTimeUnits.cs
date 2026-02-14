using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySlideShow.Utilities
{
    public enum SortOrder
    {
        Ascending,
        Descending
    }

    public enum TimeUnit
    {
        Minute,
        Hour,
        Day,
        Week,
        Other
    }

    public class ConvertTimeUnits
    {

        public static TimeUnit UI_To_GenericTimeUnit(bool radioButtonMinuteIsChecked , 
                                                        bool radioButtonHourIsChecked, 
                                                        bool radioButtonDayIsChecked,
                                                        bool radioButtonWeekIsChecked)
        {
            if (radioButtonMinuteIsChecked)
            {
                return TimeUnit.Minute;
            }
            else if (radioButtonHourIsChecked)
            {
                return TimeUnit.Hour;
            }
            else if (radioButtonDayIsChecked)
            {
                return TimeUnit.Day;
            }
            else if (radioButtonWeekIsChecked)
            {
                return TimeUnit.Week;
            }

            return TimeUnit.Other;
        }
    }
}
