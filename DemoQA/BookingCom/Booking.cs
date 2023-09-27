using System.Globalization;
using NUnit.Framework;
using OpenQA.Selenium;

namespace DemoQA.BookingCom;

public class Booking
{
    private Driver _driver = new DriverFactory().GetDriverByName("chrome");


    [OneTimeSetUp]
    public void Setup()
    {
        _driver.GoToUrl("https://www.booking.com");
        _driver.MaximizeWindow();
    }


    [Test]
    public void Test()
    {
        string city = "New York";

        ChangeLanguageToEnglishUK(_driver);
        SelectDestination(_driver, city);
        FindAndClickCheckInCheckOut(_driver);

        DateTime checkInDate = DateTime.Now.AddDays(1);
        string checkInMonth = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(checkInDate.Month);
        string checkInDay = checkInDate.Day.ToString();

        DateTime checkOutDate = checkInDate.AddDays(7);
        string checkOutMonth = CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(checkOutDate.Month);
        string checkOutDay = checkOutDate.Day.ToString();

        SelectMonthAndDay(_driver, checkInMonth, checkOutMonth, checkInDay, checkOutDay);
        ClickSearchButton(_driver);
        VerifyThatEveryHotelContainsCity(_driver, city);

        string actualStartDate = _driver.FindElementByXpath("//button[@data-testid='date-display-field-start']").GetText();
        VerifyThatDateIsDisplayedInSearch(actualStartDate, checkInDay, checkInMonth);
        string actualEndDate = _driver.FindElementByXpath("//button[@data-testid='date-display-field-end']").GetText();
        VerifyThatDateIsDisplayedInSearch(actualEndDate, checkOutDay, checkOutMonth);

    }


//----------------------------------------------------------------------------------------------------------------------------------------


 

    private static void ChangeLanguageToEnglishUK(Driver _driver)
    {
        Console.WriteLine("Change language to English UK");
        Element btnLanguageSwitcher =
            _driver.FindElementByXpath("//button[@data-testid = 'header-language-picker-trigger']");
        btnLanguageSwitcher.Click();

        Element btnEnglishUK = _driver.FindElementByXpath("//span[text()='English (UK)'][1]/ancestor::button");
        btnEnglishUK.Click();
    }

    private static void SelectDestination(Driver _driver, string destination)
    {
        Console.WriteLine($"Selecting {destination}");
        Element txtDestination = _driver.FindElementByXpath("//input[@name='ss']");
        txtDestination.Clear();
        txtDestination.SendText(destination);

        try
        {
            Thread.Sleep(1000);
            Element liDestination = _driver.FindElementByXpath("//ul[contains(@aria-label, 'destination')]/li[1]");
            liDestination.Click();

        }
        catch (Exception)
        {
            Console.WriteLine("Destination dropdown did not appear");
        }
    }

    private static void FindAndClickCheckInCheckOut(Driver _driver)
    {
        Console.WriteLine("Click on CheckIn-CheckOut button");
        Element buttonDate = null;
        try
        {
            buttonDate = _driver.FindElementByXpath("//div[@data-testid='searchbox-dates-container']//button[1]");

        }
        catch (NoSuchElementException)
        {
            buttonDate = _driver.FindElementByXpath("//div[@data-placeholder='Check-in']");
        }
        catch (Exception)
        {
            Console.WriteLine("damn it");
        }

        buttonDate.Click();
        Thread.Sleep(2000);
    }

    private static void SelectMonthAndDay(Driver _driver, string checkInMonth, string checkOutMonth, string checkInDay,
        string checkOutDay)
    {
        Console.WriteLine("Selecting Month and Day");
        try
        {
            Element leftWindow =
                _driver.FindElementByXpath($"//div[contains(@class,'calendar')]//*[contains(text()='{checkInMonth}')]/..");
            Element checkInDateToPick =
                leftWindow.FindElementByXpath($".following-sibling::table//*[text()='{checkInDay}'");

            checkInDateToPick.Click();

            Element checkOutDateToPick;
            if (checkOutMonth != checkInMonth)
            {
                Element rightWindow =
                    _driver.FindElementByXpath($"//div[contains(@class,'calendar')]//*[contains(text()='{checkOutMonth}')]/..");
                checkOutDateToPick = rightWindow.FindElementByXpath($".following-sibling::table//*[text()='{checkOutDay}']");
            }
            else
            {
                checkOutDateToPick = leftWindow.FindElementByXpath($".following-sibling::table//*[text()='{checkOutDay}']");
            }

            Thread.Sleep(1000);
            checkOutDateToPick.Click();
            Console.WriteLine("Try block worked");

        }
        catch (Exception)
        {
            Element leftWindow = _driver.FindElementByXpath($"//h3[contains(text(),'{checkInMonth}')]/..");
            Element checkInDateToPick = leftWindow.FindElementByXpath($".//*[text()='{checkInDay}']");
            checkInDateToPick.Click();

            Element checkOutDateToPick;
            if (checkOutMonth != checkInMonth)
            {
                Element rightWindow = _driver.FindElementByXpath($"//h3[contains(text(),'{checkOutMonth}')]/..");
                checkOutDateToPick = rightWindow.FindElementByXpath($".//*[text()='{checkOutDay}']");
            }
            else
            {
                checkOutDateToPick = leftWindow.FindElementByXpath($".//*[text()='{checkOutDay}']");
            }

            checkOutDateToPick.Click();
            Console.WriteLine("Catch block worked");
        }
    }

    private static void ClickSearchButton(Driver _driver)
    {
        Console.WriteLine("Clicking Search button");
        Element buttonSearch = _driver.FindElementByXpath("//span[normalize-space()='Search']");
        buttonSearch.Click();

    }

    private static void VerifyThatEveryHotelContainsCity(Driver _driver, string city)
    {
        Console.WriteLine("Getting list of hotels");
        List<Element> hotels = _driver.FindElementsByXpath("//div[@data-testid='property-card']");
        int index = 0;
        foreach (var hotel in hotels)
        {
            Console.WriteLine($"\t Checking hotel number -> {++index}");
            Element hotelAddress = hotel.FindElementByXpath(".//span[@data-testid='address']");
            string hotelAddressText = hotelAddress.GetText();
            StringAssert.Contains(city, hotelAddressText, $"Actual hotel address does not contain {city}");
            Console.WriteLine($"Hotel number {index} - verification passed");
        }

    }

    private static void VerifyThatDateIsDisplayedInSearch(string actualDate, string expectedDay, string expectedMonth)
    {
        Console.WriteLine("Verify that date is displayed in Search");
        string actualStartDay = actualDate.Split(' ')[1];
        string actualStartMonth = actualDate.Split(' ')[2];
        Assert.AreEqual(expectedDay, actualStartDay, "Check in DAY is not equal to expected");
        StringAssert.Contains(actualStartMonth, expectedMonth, "Check in MONTH is not equal to expected");

    }

    [OneTimeTearDown]
    public void CloseBrowser()
    {
        _driver.QuitDriver();
    }
}