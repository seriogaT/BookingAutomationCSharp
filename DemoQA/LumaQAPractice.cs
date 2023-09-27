using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools;

namespace DemoQA;

public class LumaQAPractice

{
    private static IWebDriver _driver = new ChromeDriver();

    [OneTimeSetUp]
    public void Setup()
    {
        _driver.Navigate().GoToUrl("https://magento.softwaretestingboard.com/");
        _driver.Manage().Window.Maximize();
    }

    [Test]
    [Ignore("ignore")]
    public void TestByClassName()
    {
        var element = _driver.FindElement(By.ClassName("text"));
        string text = element.Text;
        Console.WriteLine($"{text}");
        Thread.Sleep(5000);
    }

    [Test]
    [Ignore("ignore")]
    public void TestByID()
    {
        var id = _driver.FindElement(By.Id("ui-id-3"));
        Console.WriteLine($"{id.Text}");
        id.Click();
        Thread.Sleep(5000);
    }
    
    [Test]
    [Ignore("ignore")]
    public void TestByName()
    {
        var element = _driver.FindElement(By.Name("q"));
        var text = element.GetAttribute("id");
        Console.WriteLine(text);
        Thread.Sleep(5000);
    }
    
    [Test]
    [Ignore("ignore")]
    public void TestByTagName()
    {
        var element = _driver.FindElement(By.TagName("h1"));
        var text = element.GetAttribute("class");
        Console.WriteLine(text);
        Thread.Sleep(5000);
    }
    
    [Test]
    [Ignore("ignore")]
    public void TestByXPath()
    {
        var element = _driver.FindElement(By.XPath("//h1"));
        var text = element.GetAttribute("class");
        Console.WriteLine(text);
        Thread.Sleep(5000);
    }

    // [Test]
    // [Ignore("ignore")]
    // public void SearchListOfElements()
    // {
    //     List<IWebElement> products = _driver.FindElements(By.XPath("//li[@class='product-item']")).ToList();
    //
    //     foreach (IWebElement product in products)
    //     {
    //         IWebElement productName = product.FindElement(By.XPath(".//img"));
    //         string nameOfTheProduct = productName.GetAttribute("alt");
    //         // string nameOfTheProduct = productName.Text;
    //         Console.WriteLine($"Name of product - {nameOfTheProduct}");
    //     }
    //     
    // }

    [Test]
    public void ClickAndSendKeys()
    {
        var searchField = _driver.FindElement(By.Name("q"));
        searchField.SendKeys("pants");
        searchField.SendKeys(Keys.Enter);
        Thread.Sleep(5000);
        
        List<IWebElement> products = _driver.FindElements(By.XPath("//li[@class='item product product-item']")).ToList();

        foreach (IWebElement product in products)
        {
            IWebElement productName = product.FindElement(By.XPath(".//img"));
            string nameOfTheProduct = productName.GetAttribute("alt");
            Console.WriteLine($"Name of product - {nameOfTheProduct}");

        }
    }



    [OneTimeTearDown]
    public void CloseBrowser()
    {
        _driver.Quit();
    }
}