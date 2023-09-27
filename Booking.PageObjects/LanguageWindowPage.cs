using DemoQA.BookingCom;

namespace Booking.PageObjects;

public class LanguageWindowPage : BasePage
{

    public LanguageWindowPage(Driver driver) : base(driver)
    {
    }

    private Element btnLanguage(string language) => driver.FindElementByXpath($"//span[text()='{language}'][1]/ancestor::button");

    public void SelectLanguage(string language)
    {
        btnLanguage(language).Click();
    }

}