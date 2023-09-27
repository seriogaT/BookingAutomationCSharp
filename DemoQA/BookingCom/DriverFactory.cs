using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace DemoQA.BookingCom;

public class DriverFactory
{
    public Driver GetDriverByName(string browserName)
    {
        IWebDriver driver = null;

        switch (browserName)
        {
            case "headless":
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.AddArguments("--headless");
                driver = new ChromeDriver(chromeOptions);
                break;
            case "chrome":
                driver = new ChromeDriver();
                break;

            case "firefox":
                driver = new FirefoxDriver();
                break;
            default:
                throw new Exception("Wrong browser selected");
        }

        return new Driver(driver);
    }
}