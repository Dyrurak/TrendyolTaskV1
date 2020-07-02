using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;
using System.Reflection;
using TechTalk.SpecFlow;
using TrendyolTaskV1.PageModel;
using TrendyolTaskV1.Service;

namespace TrendyolTaskV1.Test
{
    
    [TestFixture]
    [Binding, Scope(Feature = "Trendyol")]
    public class TrendyolTest
    {
        public IWebDriver webDriver;
        public WebDriverService webDriverService;
        public HomePage homePage;
        public CategoryPage categoryPage;
        public BoutiquePage boutiquePage;
        public ProductPage productPage;
        string driverPath = string.Empty;       

        public TrendyolTest()
        {
            driverPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            webDriverService = new WebDriverService();                       
        }

        [AfterScenario]
        public void AfterScenario()
        {
            webDriver.Quit();
        }

        [StepDefinition(@"'(.*)' driver ile browser acilir")]
        public void OpenBrowser(string requestedDriver){
            
            switch (requestedDriver) {
                case "Chrome": { webDriver = webDriverService.SetWebDriverAsChrome(driverPath);  break; }
                case "Firefox": { webDriver = webDriverService.SetWebDriverAsFirefox(driverPath); break; }
                case "InternetExplorer": { webDriver = webDriverService.SetWebDriverAsInternetExplorer(driverPath); break; }
            }
            homePage = new HomePage(webDriver);
            categoryPage = new CategoryPage(webDriver);
            boutiquePage = new BoutiquePage(webDriver);
            productPage = new ProductPage(webDriver);
        }

        [StepDefinition(@"Trendyol sitesine gidilir")]
        public void OpenTrendyol()
        {
            webDriver.Navigate().GoToUrl("https://www.trendyol.com/");
        }

        [StepDefinition(@"Gelen popup ekrani kapatilir")]
        public void ClosePopUpWindow()
        {           
            homePage.ClosePopUpWindow();
        }

        [StepDefinition(@"Giris yap butonuna tiklanir")]
        public void ClickLoginButton()
        {
            homePage.ClickLoginButton();
        }

        [StepDefinition(@"Eposta adresi '(.*)' olarak girilir")]
        public void SetEmailAddress(string emailAddress)
        {
            homePage.SetEmailAddress(emailAddress);
        }

        [StepDefinition(@"Sifre alanina '(.*)' girilir")]
        public void SetPassword(string password)
        {
            homePage.SetPassword(password);
        }

        [StepDefinition(@"Giris yap butonuna basilarak giris yapilir")]
        public void ClickLoginSubmit()
        {
            homePage.ClickLoginSubmit();
        }

        [StepDefinition(@"Kategorilere tiklanarak içindeki butikler kontrol edilir")]
        public void CheckBoutiques()
        {
            homePage.CheckBoutiques();
        }

        [StepDefinition(@"Bildirim popupı gelirse kapatılır")]
        public void CloseNotificationPopup()
        {
            homePage.CloseNotificationPopup();
        }

        [StepDefinition(@"Ilk butige tiklanarak içindeki ürünlerin görsellerinin yüklendiği kontrol edilir")]
        public void ClickFirstBoutique()
        {
            categoryPage.ClickFirstBoutique();
            boutiquePage.CheckProducts();
        }

        [StepDefinition(@"Ilk urune tiklanir")]
        public void ClickFirstProduct()
        {
            boutiquePage.ClickFirstProduct();
        }

        [StepDefinition(@"Sepete ekle butonuna tiklanarak urun sepete eklenir")]
        public void ClickAddToBasket()
        {
            productPage.ClickAddToBasket();            
        }
    }
}
