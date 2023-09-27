using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace DemoQA;

public class TestClass3
{
    private IWebDriver _driver = new ChromeDriver();

    [OneTimeSetUp]
    public void Setup()
    {
        _driver.Navigate().GoToUrl("https://ultimateqa.com/simple-html-elements-for-automation");
        _driver.Manage().Window.Maximize();
        IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
        js.ExecuteScript("window.scrollBy(0,2000)");
    }

    [Test]
    public void TestRadioButton()
    {
        IWebElement rdoOther = _driver.FindElement(By.XPath("//input[@value='other']"));
        IWebElement rdoMale = _driver.FindElement(By.XPath("//input[@value='male']"));
        IWebElement rdoFemale = _driver.FindElement(By.XPath("//input[@value='female']"));


        rdoFemale.Click();
        bool rdoFemaleSelected = rdoFemale.Selected;
        Thread.Sleep(5000);
        rdoMale.Click();
        bool rdoMaleSelected = rdoMale.Selected;
        rdoOther.Click();
        bool rdoOtherSelected = rdoOther.Selected;

        Console.WriteLine("Female: " + rdoFemaleSelected);
        Console.WriteLine("Male: " + rdoMaleSelected);
        Console.WriteLine("Female: " + rdoOtherSelected);
        Console.WriteLine("+++++++++++++++++++++++++++++++++++");
        rdoFemaleSelected = rdoFemale.Selected;
        rdoMaleSelected = rdoMale.Selected;
        rdoOtherSelected = rdoOther.Selected;
        Console.WriteLine("Female: " + rdoFemaleSelected);
        Console.WriteLine("Male: " + rdoMaleSelected);
        Console.WriteLine("Female: " + rdoOtherSelected);
    }

    [Test]
    public void CheckBoxTest()
    {
        IWebElement checkCar = _driver.FindElement(By.XPath("//input[@value = 'Car']"));
        IWebElement checkBike = _driver.FindElement(By.XPath("//input[@value = 'Bike']"));

        bool isCarChecked = checkCar.Selected;
        bool isBikeChecked = checkBike.Selected;

        Console.WriteLine("Car is checked: " + isCarChecked);
        Console.WriteLine("Bike is checked: " + isBikeChecked);

        checkBike.Click();
        checkCar.Click();

        isCarChecked = checkCar.Selected;
        isBikeChecked = checkBike.Selected;

        Thread.Sleep(5000);

        Console.WriteLine("+++++++++++++++++++++++ ");
        Console.WriteLine("Car is checked: " + isCarChecked);
        Console.WriteLine("Bike is checked: " + isBikeChecked);
    }


    [Test]
    public void DropDownTest()
    {
        IWebElement ddlVehicle = _driver.FindElement(By.TagName("select"));
        SelectElement selectedDdlVehicle = new SelectElement(ddlVehicle);

        var allSelectedOptions = selectedDdlVehicle.AllSelectedOptions.ToList();
        allSelectedOptions.ForEach(x => Console.WriteLine("All selected options: " + x.Text));

        bool isMultiple = selectedDdlVehicle.IsMultiple;
        Console.WriteLine("Is Multiple: " + isMultiple);

        var optionsList = selectedDdlVehicle.Options.ToList();
        optionsList.ForEach(x => Console.WriteLine("All options: " + x.Text));

        var wrappedElement = selectedDdlVehicle.WrappedElement;
        Console.WriteLine(wrappedElement);
        Console.WriteLine(wrappedElement.Text);
        Console.WriteLine(wrappedElement.TagName);

        selectedDdlVehicle.SelectByText("Volvo");
        var selectedOption = selectedDdlVehicle.SelectedOption.Text;
        Console.WriteLine("First selected option: " + selectedOption);

        selectedDdlVehicle.SelectByValue("saab");
        selectedOption = selectedDdlVehicle.SelectedOption.Text;
        Console.WriteLine("Second selected option: " + selectedOption);

        selectedDdlVehicle.SelectByIndex(3);
        selectedOption = selectedDdlVehicle.SelectedOption.Text;
        Console.WriteLine("Second selected option: " + selectedOption);
    }

    [Test]
    public void TestUL()
    {
        IWebElement liTab1 = _driver.FindElement(By.XPath("//li[contains(@class,'et_pb_tab_0')]"));
        IWebElement liTab2 = _driver.FindElement(By.XPath("//li[contains(@class,'et_pb_tab_1')]"));
        IWebElement displayedTab = _driver.FindElement(By.XPath("//div[not(contains(@style, 'none'))]/div[@class='et_pb_tab_content']"));

        Console.WriteLine("Active tab: " + displayedTab.Text);
        
        liTab2.Click();
        Thread.Sleep(5000);        
        displayedTab = _driver.FindElement(By.XPath("//div[not(contains(@style, 'none'))]/div[@class='et_pb_tab_content']"));
        Console.WriteLine("Active tab: " + displayedTab.Text);
        
        liTab1.Click();
        Thread.Sleep(5000);        
        displayedTab = _driver.FindElement(By.XPath("//div[not(contains(@style, 'none'))]/div[@class='et_pb_tab_content']"));
        Console.WriteLine("Active tab: " + displayedTab.Text);


    }

    [OneTimeTearDown]
    public void CloseBrowser()
    {
        _driver.Quit();
    }
}