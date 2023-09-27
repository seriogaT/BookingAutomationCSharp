using OpenQA.Selenium;

namespace DemoQA.BookingCom;

public class Driver
{
    public IWebDriver driver { get; set; }

    public Driver(IWebDriver driver)
    {
        this.driver = driver;
    }

    public void GoToUrl(string url)
    {
        driver.Navigate().GoToUrl(url);
    }

    public void MaximizeWindow()
    {
        driver.Manage().Window.Maximize();
    }

    public Element FindElementByXpath(string XPath)
    {
        return new Element(driver.FindElement(By.XPath(XPath)));
    }

    public List<Element> FindElementsByXpath(string XPath)
    {
        var elements = driver.FindElements(By.XPath(XPath));
        var result = elements.Select(x => new Element(x));
        return result.ToList();
    }

    public void QuitDriver()
    {
        driver.Quit();
    }
}