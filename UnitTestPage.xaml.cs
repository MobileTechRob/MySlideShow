
using System.Globalization;

namespace MySlideShow;

public partial class UnitTestPage : ContentPage
{

    public class MyAssert
    {
        public static bool IsEqual (int a, int b)
        {
            return a == b;
        }   
    }

	public UnitTestPage()
	{
		InitializeComponent();
    }


    private void SpecificFromAndToDate_Clicked(object sender, EventArgs e)
    {
        Dictionary<string, DateTime> photoDateTimeDict = new Dictionary<string, DateTime>();
        DateTime laterDate = DateTime.Now;
        DateTime earlierDate = DateTime.Now.AddHours(-5);
        Utilities.TimeUnit timeUnit = Utilities.TimeUnit.Other;
        int qtyTimeUnit = 0;

        photoDateTimeDict.Add("Photo1.jpg", DateTime.Now.AddHours(-1));
        photoDateTimeDict.Add("Photo2.jpg", DateTime.Now.AddHours(-2));
        photoDateTimeDict.Add("Photo3.jpg", DateTime.Now.AddHours(-7));

        List<string> list = Utilities.CollectFiles.SortPhotosByTimeFrame(photoDateTimeDict,
            laterDate,
            earlierDate,
            Utilities.TimeUnit.Other,
            1);

        bool succceed = MyAssert.IsEqual(list.Count, 2);

        if (succceed == false)
            throw new System.Exception("Unit Test 1 Failed");

        photoDateTimeDict.Clear();

        IFormatProvider culture = new CultureInfo("en-US");

        laterDate = DateTime.ParseExact("2026-02-06 13:05:11", "yyyy-MM-dd HH:mm:ss", culture);
        earlierDate = DateTime.ParseExact("2026-02-06 13:05:04", "yyyy-MM-dd HH:mm:ss", culture);

        photoDateTimeDict.Add("Photo1.jpg", DateTime.ParseExact("2026-02-06 13:05:01", "yyyy-MM-dd HH:mm:ss", culture));
        photoDateTimeDict.Add("Photo2.jpg", DateTime.ParseExact("2026-02-06 13:05:04", "yyyy-MM-dd HH:mm:ss", culture));
        photoDateTimeDict.Add("Photo3.jpg", DateTime.ParseExact("2026-02-06 13:05:07", "yyyy-MM-dd HH:mm:ss", culture));
        photoDateTimeDict.Add("Photo4.jpg", DateTime.ParseExact("2026-02-06 13:05:09", "yyyy-MM-dd HH:mm:ss", culture));
        photoDateTimeDict.Add("Photo5.jpg", DateTime.ParseExact("2026-02-06 13:05:11", "yyyy-MM-dd HH:mm:ss", culture));
        photoDateTimeDict.Add("Photo6.jpg", DateTime.ParseExact("2026-02-06 13:05:13", "yyyy-MM-dd HH:mm:ss", culture));

        list = Utilities.CollectFiles.SortPhotosByTimeFrame(photoDateTimeDict,
            laterDate,
            earlierDate,
            Utilities.TimeUnit.Other,
            -5);

        succceed = MyAssert.IsEqual(list.Count, 4);

        if (succceed == false)
            throw new System.Exception("Unit Test 2 Failed");

        succceed = list.Contains("Photo2.jpg") && list.Contains("Photo3.jpg")
            && list.Contains("Photo4.jpg") && list.Contains("Photo5.jpg");

        if (succceed == false)
            throw new System.Exception("Unit Test 3 Failed");

        photoDateTimeDict.Clear();

        photoDateTimeDict.Add("Photo2.jpg", DateTime.ParseExact("2026-02-06 13:05:04", "yyyy-MM-dd HH:mm:ss", culture));
        photoDateTimeDict.Add("Photo1.jpg", DateTime.ParseExact("2026-02-06 13:05:01", "yyyy-MM-dd HH:mm:ss", culture));
        photoDateTimeDict.Add("Photo4.jpg", DateTime.ParseExact("2026-02-06 13:05:09", "yyyy-MM-dd HH:mm:ss", culture));
        photoDateTimeDict.Add("Photo3.jpg", DateTime.ParseExact("2026-02-06 13:05:07", "yyyy-MM-dd HH:mm:ss", culture));
        photoDateTimeDict.Add("Photo6.jpg", DateTime.ParseExact("2026-02-06 13:05:13", "yyyy-MM-dd HH:mm:ss", culture));
        photoDateTimeDict.Add("Photo5.jpg", DateTime.ParseExact("2026-02-06 13:05:11", "yyyy-MM-dd HH:mm:ss", culture));

        list = Utilities.CollectFiles.SortPhotosByTimeFrame(photoDateTimeDict,
            laterDate,
            earlierDate,
            Utilities.TimeUnit.Other,
            -5);

        succceed = MyAssert.IsEqual(list.Count, 4);

        if (succceed == false)
            throw new System.Exception("Unit Test 4 Failed");

        succceed = list.Contains("Photo2.jpg") && list.Contains("Photo3.jpg")
            && list.Contains("Photo4.jpg") && list.Contains("Photo5.jpg");

        if (succceed == false)
            throw new System.Exception("Unit Test 5 Failed");
    }

    private void MinutesTest_Clicked(object sender, EventArgs e)
    {
        Dictionary<string, DateTime> photoDateTimeDict = new Dictionary<string, DateTime>();
        DateTime laterDate = DateTime.Now;
        DateTime earlierDate = laterDate.AddMinutes(-5);
        Utilities.TimeUnit timeUnit = Utilities.TimeUnit.Minute;
        int qtyTimeUnit = 5;

        photoDateTimeDict.Add("Photo1.jpg", laterDate.AddMinutes(-1));
        photoDateTimeDict.Add("Photo4.jpg", laterDate.AddMinutes(-7));
        photoDateTimeDict.Add("Photo3.jpg", laterDate.AddMinutes(-5));
        photoDateTimeDict.Add("Photo2.jpg", laterDate.AddMinutes(-2));

        List<string> list = Utilities.CollectFiles.SortPhotosByTimeFrame(photoDateTimeDict,
            laterDate,
            earlierDate,
            timeUnit,
            qtyTimeUnit);

        bool succceed = MyAssert.IsEqual(list.Count, 3);

        if (succceed == false)
            throw new System.Exception("MinutesTest_Clicked");

        succceed = list.Contains("Photo1.jpg") && list.Contains("Photo2.jpg")
            && list.Contains("Photo3.jpg");

        if (succceed == false)
            throw new System.Exception("MinutesTest_Clicked");
    }

    private void HoursTest_Clicked(object sender, EventArgs e)
    {
        Dictionary<string, DateTime> photoDateTimeDict = new Dictionary<string, DateTime>();
        DateTime laterDate = DateTime.Now;
        DateTime earlierDate = laterDate.AddHours(-5);
        Utilities.TimeUnit timeUnit = Utilities.TimeUnit.Hour;
        int qtyTimeUnit = 5;

        photoDateTimeDict.Add("Photo1.jpg", laterDate.AddHours(-1));
        photoDateTimeDict.Add("Photo4.jpg", laterDate.AddHours(-7));
        photoDateTimeDict.Add("Photo3.jpg", laterDate.AddHours(-5));
        photoDateTimeDict.Add("Photo2.jpg", laterDate.AddHours(-2));

        List<string> list = Utilities.CollectFiles.SortPhotosByTimeFrame(photoDateTimeDict,
            laterDate,
            earlierDate,
            timeUnit,
            qtyTimeUnit);

        bool succceed = MyAssert.IsEqual(list.Count, 3);

        if (succceed == false)
            throw new System.Exception("HoursTest_Clicked");

        succceed = list.Contains("Photo1.jpg") && list.Contains("Photo2.jpg")
            && list.Contains("Photo3.jpg");

        if (succceed == false)
            throw new System.Exception("HoursTest_Clicked");
    }

    private void DaysTest_Clicked(object sender, EventArgs e)
    {
        Dictionary<string, DateTime> photoDateTimeDict = new Dictionary<string, DateTime>();
        DateTime laterDate = DateTime.Now;
        DateTime earlierDate = laterDate.AddDays(-5);
        Utilities.TimeUnit timeUnit = Utilities.TimeUnit.Day;
        int qtyTimeUnit = 5;

        photoDateTimeDict.Add("Photo1.jpg", laterDate.AddDays(-1));
        photoDateTimeDict.Add("Photo4.jpg", laterDate.AddDays(-7));
        photoDateTimeDict.Add("Photo3.jpg", laterDate.AddDays(-5));
        photoDateTimeDict.Add("Photo2.jpg", laterDate.AddDays(-2));

        List<string> list = Utilities.CollectFiles.SortPhotosByTimeFrame(photoDateTimeDict,
            laterDate,
            earlierDate,
            timeUnit,
            qtyTimeUnit);

        bool succceed = MyAssert.IsEqual(list.Count, 3);

        if (succceed == false)
            throw new System.Exception("DaysTest_Clicked");

        succceed = list.Contains("Photo1.jpg") && list.Contains("Photo2.jpg")
            && list.Contains("Photo3.jpg");
    
        if (succceed == false)
            throw new System.Exception("DaysTest_Clicked");
    }

    private void WeeksTest_Clicked(object sender, EventArgs e)
    {
        Dictionary<string, DateTime> photoDateTimeDict = new Dictionary<string, DateTime>();
        DateTime laterDate = DateTime.Now;
        DateTime earlierDate = laterDate.AddDays(-5 * 7);
        Utilities.TimeUnit timeUnit = Utilities.TimeUnit.Week;
        int qtyTimeUnit = 5;

        photoDateTimeDict.Add("Photo1.jpg", laterDate.AddDays(-1 * 7));
        photoDateTimeDict.Add("Photo4.jpg", laterDate.AddDays(-7 * 7));
        photoDateTimeDict.Add("Photo3.jpg", laterDate.AddDays(-5 * 7));
        photoDateTimeDict.Add("Photo2.jpg", laterDate.AddDays(-2 * 7));

        List<string> list = Utilities.CollectFiles.SortPhotosByTimeFrame(photoDateTimeDict,
            laterDate,
            earlierDate,
            timeUnit,
            qtyTimeUnit);

        bool succceed = MyAssert.IsEqual(list.Count, 3);

        if (succceed == false)
            throw new System.Exception("WeeksTest_Clicked");

        succceed = list.Contains("Photo1.jpg") && list.Contains("Photo2.jpg")
            && list.Contains("Photo3.jpg");

        if (succceed == false)
            throw new System.Exception("WeeksTest_Clicked");
    }
}