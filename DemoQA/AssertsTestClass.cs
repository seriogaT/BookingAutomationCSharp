using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DemoQA;

public class AssertsTestClass
{
    private IWebDriver _driver = new ChromeDriver();

    [OneTimeSetUp]
    public void Setup()
    {
        _driver.Navigate().GoToUrl("https://mta.ua");
        _driver.Manage().Window.Maximize();
        // IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
        // js.ExecuteScript("window.scrollBy(0,2000)");
    }

    [Test]
    public void RegularAssert()
    {
        string inputText = "apple";
        IWebElement txtSearch = _driver.FindElement(By.XPath("//input[@name='search']"));
        txtSearch.SendKeys(inputText);
        txtSearch.SendKeys(Keys.Enter);

        Thread.Sleep(3000);

        IWebElement reseults = _driver.FindElement(By.TagName("h1"));
        string actualResultSearchText = reseults.Text.Split(" ")[^1].ToLower();
        Assert.AreEqual(inputText, actualResultSearchText,$" '{inputText}' is not equal to '{actualResultSearchText}' ");
    }


    [OneTimeTearDown]
    public void CloseBrowser()
    {
        _driver.Quit();
    }
}