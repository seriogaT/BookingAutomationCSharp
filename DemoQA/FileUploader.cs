using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace DemoQA;

public class FileUploader
{
    private IWebDriver _driver = new ChromeDriver();


    [OneTimeSetUp]
    public void Setup()
    {
        _driver.Navigate().GoToUrl("https://the-internet.herokuapp.com/upload");
        _driver.Manage().Window.Maximize();
    }


    [Test]
    public void Test()
    {

        var element = _driver.FindElement(By.XPath("//input[@id='file-upload']"));
        element.SendKeys("C:\\QPP\\QPP_TAF\\2026_3_HD4.xlsx");
        var element2 = _driver.FindElement(By.XPath("//input[@id='file-submit']"));
        element2.Click();
        Thread.Sleep(5000);

    }


    [OneTimeTearDown]
    public void CloseBrowser()
    {
        _driver.Quit();
    }
}