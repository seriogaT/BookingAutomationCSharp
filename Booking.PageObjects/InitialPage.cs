using DemoQA.BookingCom;
using OpenQA.Selenium;

namespace Booking.PageObjects;

public class InitialPage : BasePage
{

    public InitialPage(Driver driver) : base(driver)
    {
    }

    public AdvertismentPage AdvertismentPage => new(driver);
    public LanguageWindowPage LanguageWindowPage => new(driver);

    private Element btnLanguageSwitcher => driver.FindElementByXpath("//button[@data-testid='header-language-picker-trigger']");
    private Element txtDestination => driver.FindElementByXpath("//input[@name='ss']");
    private Element liDestination =>
        driver.FindElementByXpath("//ul[contains(@aria-label, 'destination') or (@data-testid='autocomplete-results')]/li[1]");

   private Element buttonSearch => driver.FindElementByXpath("//span[normalize-space()='Search']");

    public void SelectMonthAndDay(string checkInMonth, string checkOutMonth, string checkInDay, string checkOutDay)
    {
        Helper.Wait(10);
        try
        {
            DefineFirstUiXPathesAndSelectMonthAndDay(checkInMonth, checkOutMonth, checkInDay, checkOutDay);
            Console.WriteLine("\t Try block worked");
        }
        catch (Exception)
        {
            DefineSecondUiXPathesAndSelectMonthAndDay(checkInMonth, checkOutMonth, checkInDay, checkOutDay);
            Console.WriteLine("\t Catch block worked");
        }
    }

    private Element GetBtnDate()
    {
        Element buttonDate = null;
        try
        {
            buttonDate = driver.FindElementByXpath("//div[@data-testid='searchbox-dates-container']//button[1]");
        }
        catch (NoSuchElementException)
        {
            buttonDate = driver.FindElementByXpath("//div[@data-placeholder='Check-in']");
        }
        catch (Exception)
        {
            Console.WriteLine("damn it");
        }

        return buttonDate;
    }

    public void ClickDate()
    {
        GetBtnDate().Click();
        Helper.Wait(2);
    }

    public void ClickSearch()
    {
        buttonSearch.Click();
    }

    public void ClickLanguageSwitcher()
    {
        btnLanguageSwitcher.Click();
    }

    public void FillDestination(string destination)
    {
        txtDestination.Clear();
        txtDestination.SendText(destination);
    }

    public void CloseDestinationDropDownIfExists()
    {
        try
        {
            Helper.Wait(2);
            liDestination.Click();
        }
        catch (Exception)
        {
            Console.WriteLine("Destination dropdown did not appear");
        }
    }

#region Month and Day Selection method
    private void DefineFirstUiXPathesAndSelectMonthAndDay(string checkInMonth, string checkOutMonth, string checkInDay, string checkOutDay)
    {
        string leftWindowXPath = $"//div[contains(@class,'calendar')]//*[contains(text()='{checkInMonth}')]/..";
        string checkInDateToPickXPath = $".following-sibling::table//*[text()='{checkInDay}'";
        string rightWindowXPath = $"//div[contains(@class,'calendar')]//*[contains(text()='{checkOutMonth}')]/..";
        string checkOutDateToPickXPath = $".following-sibling::table//*[text()='{checkOutDay}']";

        SelectMonthAndDayAction(leftWindowXPath,rightWindowXPath,checkInDateToPickXPath,checkOutDateToPickXPath,checkInMonth,checkOutMonth);
    }

    private void DefineSecondUiXPathesAndSelectMonthAndDay(string checkInMonth, string checkOutMonth, string checkInDay, string checkOutDay)
    {
        string leftWindowXPath = $"//h3[contains(text(),'{checkInMonth}')]/..";
        string checkInDateToPickXPath = $".//*[text()='{checkInDay}']";
        string rightWindowXPath = $"//h3[contains(text(),'{checkOutMonth}')]/..";
        string checkOutDateToPickXPath = $".//*[text()='{checkOutDay}']";
        
        SelectMonthAndDayAction(leftWindowXPath,rightWindowXPath,checkInDateToPickXPath,checkOutDateToPickXPath,checkInMonth,checkOutMonth);
    }

    private void SelectMonthAndDayAction(string leftWindowXPath,string rightWindowXPath, string checkInDateToPickXPath, 
                                         string checkOutDateToPickXPath, string checkInMonth, string checkOutMonth)
    {
        Element leftWindow = driver.FindElementByXpath(leftWindowXPath);
        Element checkInDateToPick = leftWindow.FindElementByXpath(checkInDateToPickXPath);
        checkInDateToPick.Click();
        Element checkOutDateToPick;
        if (checkOutMonth != checkInMonth)
        {
            Element rightWindow = driver.FindElementByXpath(rightWindowXPath);
            checkOutDateToPick = rightWindow.FindElementByXpath(checkOutDateToPickXPath);
        }
        else
        {
            checkOutDateToPick = leftWindow.FindElementByXpath(checkOutDateToPickXPath);
        }
        Helper.Wait(1);
        checkOutDateToPick.Click();
    }
#endregion

}