using DemoQA.BookingCom;
using NUnit.Framework;

namespace Booking.Tests;

public class BaseTest
{
    protected Driver driver;


    [OneTimeSetUp]
    public void Setup()
    {
        driver = new DriverFactory().GetDriverByName("chrome");
        driver.GoToUrl("https://www.booking.com");
        driver.MaximizeWindow();
    }
    
    [OneTimeTearDown]
    public void CloseBrowser()
    {
        driver.QuitDriver();
    }
}