using DemoQA.BookingCom;
using NUnit.Framework;

namespace Booking.PageObjects;

public class SearchResultsPage : BasePage
{

    public SearchResultsPage(Driver driver) : base(driver)
    {
    }

   private Element actualStartDate => driver.FindElementByXpath("//button[@data-testid='date-display-field-start']");
   private Element actualEndDate => driver.FindElementByXpath("//button[@data-testid='date-display-field-end']");

   public string GetActualStartDateText() => actualStartDate.GetText();
   public string GetActualEndDateText() => actualEndDate.GetText();

   private static List<Element> hotelsList => driver.FindElementsByXpath("//div[@data-testid='property-card']");

    public List<string> GetHotelsAdresses()
    {
        List<string> result = new List<string>();
        foreach (var hotel in hotelsList)
        {
            Element hotelAddress = hotel.FindElementByXpath(".//span[@data-testid='address']");
            string hotelAddressText = hotelAddress.GetText();
            result.Add(hotelAddressText);
        }
        return result;
    }

}

