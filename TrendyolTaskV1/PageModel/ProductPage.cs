using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TrendyolTaskV1.PageModel
{
    public class ProductPage : BasePage
    {
        public ProductPage(IWebDriver webDriver) : base (webDriver)
        {

        }

        [FindsBy(How = How.XPath, Using = "//button[@class='pr-in-btn add-to-bs']")]
        public IWebElement BtnAddToBasket { get; set; }

        public void ClickAddToBasket()
        {
            Click(BtnAddToBasket);
        }        
    }
}
