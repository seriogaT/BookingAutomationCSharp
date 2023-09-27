using System.Linq;
using DemoQA.BookingCom;

namespace Booking.PageObjects;

public class AdvertismentPage : BasePage
{

    public AdvertismentPage(Driver driver) : base(driver)
    {
    }

    private Element modal => driver.FindElementsByXpath("//div[@aria-modal='true']").Where(x => x.isDisplayed()).FirstOrDefault();
    private Element btnCloseModal => driver.FindElementByXpath("//div[@aria-modal='true']//button");

    private bool IsModalDisplayed()
    {
        return modal != null;
    }

    public void CloseModalIfExists()
    {
        if (IsModalDisplayed())
        {
            Helper.Wait(5);
            btnCloseModal.Click();

        }

    }

}