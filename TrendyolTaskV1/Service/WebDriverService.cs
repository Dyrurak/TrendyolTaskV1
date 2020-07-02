using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace TrendyolTaskV1.Service
{
    public class WebDriverService
    {
        public IWebDriver webDriver;
        public IWebDriver SetWebDriverAsChrome(string driverPath)
        {            
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("start-maximized");
            chromeOptions.AddArguments("test-type");
            chromeOptions.AddArguments("disable-popup-blocking");
            chromeOptions.AddArguments("ignore-certificate-errors");
            chromeOptions.AddArguments("disable-translate");
            chromeOptions.AddArguments("disable-automatic-password-saving");
            chromeOptions.AddArguments("allow-silent-push");
            chromeOptions.AddArguments("disable-infobars");
            chromeOptions.AddArguments("disable-notifications");
            chromeOptions.AddAdditionalCapability("useAutomationExtension", false);            
            webDriver = new ChromeDriver(driverPath, chromeOptions);            
            return webDriver;
        }

        public IWebDriver SetWebDriverAsFirefox(string driverPath)
        {   
            webDriver = new FirefoxDriver(driverPath);
            return webDriver;
        }

        public IWebDriver SetWebDriverAsInternetExplorer(string driverPath)
        {
            webDriver = new InternetExplorerDriver(driverPath);
            return webDriver;
        }
    }
}