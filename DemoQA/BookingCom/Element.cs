using OpenQA.Selenium;

namespace DemoQA.BookingCom;

public class Element
{
    public IWebElement element;

    public Element(IWebElement element)
    {
        this.element = element;
    }

    public void Click()
    {
        element.Click();
    }
    
    public void Clear()
    {
        element.Clear();
    }

    public bool isDisplayed()
    {
        return element.Displayed;
    }

    public void SendText(string text)
    {
        element.SendKeys(text);
    }

    public Element FindElementByXpath(string xpath)
    {
        return new Element(element.FindElement(By.XPath(xpath)));
    }

    public string GetText()
    {
        return element.Text;
    }

}