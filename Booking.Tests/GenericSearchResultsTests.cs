 using System.Globalization;
using Booking.PageObjects;
using NUnit.Framework;

namespace Booking.Tests;

public class GenericSearchResultsTests : BaseTest

{
    [Test]
    public void VerifyGenericSearchResults()
    {
        string city = "New York";
        string language = "English (UK)";
        
        InitialPage initialPage = new InitialPage(driver);
        Console.WriteLine("Close modal if exists");
        initialPage.AdvertismentPage.CloseModalIfExists();

        Console.WriteLine("Change language to English - UK");
        initialPage.ClickLanguageSwitcher();
        initialPage.LanguageWindowPage.SelectLanguage(language);
        
        Console.WriteLine($"Selecting {city}");
        initialPage.FillDestination(city);
        initialPage.CloseDestinationDropDownIfExists();

        Console.WriteLine("Click on CheckIn-CheckOut button");
        initialPage.ClickDate();

        DateTime checkInDate = DateTime.Now.AddDays(1);
        string checkInMonth = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(checkInDate.Month);
        string checkInDay = checkInDate.Day.ToString();

        DateTime checkOutDate = checkInDate.AddDays(7);
        string checkOutMonth = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(checkOutDate.Month);
        string checkOutDay = checkOutDate.Day.ToString();
        
        Console.WriteLine("Selecting Month and Day");
        initialPage.SelectMonthAndDay(checkInMonth,checkOutMonth,checkInDay,checkOutDay);
        
        Console.WriteLine("Clicking Search button");
        initialPage.ClickSearch();
        
        Console.WriteLine("Getting list of hotels");
        SearchResultsPage searchResultsPage = new SearchResultsPage(driver);
        var hotelsAdresses = searchResultsPage.GetHotelsAdresses();

        foreach (var hotelAdressText in hotelsAdresses)
        {
            StringAssert.Contains(city,hotelAdressText,$"Actual hotel address does not contain {city}");
        }

        string actualStartDate = searchResultsPage.GetActualStartDateText();
        string actualEndDate = searchResultsPage.GetActualEndDateText();
        


      
        
        VerifyThatDateIsDisplayedInSearch(actualStartDate, checkInDay, checkInMonth);
        VerifyThatDateIsDisplayedInSearch(actualEndDate, checkOutDay, checkOutMonth);

    }


//----------------------------------------------------------------------------------------------------------------------------------------


 

    private static void VerifyThatDateIsDisplayedInSearch(string actualDate, string expectedDay, string expectedMonth)
    {
        Console.WriteLine($"Verify that date {actualDate} is displayed in Search");
        string actualDay = actualDate.Split(' ')[1];
        string actualMonth = actualDate.Split(' ')[2];
        Assert.AreEqual(expectedDay, actualDay, "Check in DAY is not equal to expected");
        StringAssert.Contains(actualMonth, expectedMonth, "Check in MONTH is not equal to expected");
        Console.WriteLine("Date is successfully displayed");
    }

   
}
