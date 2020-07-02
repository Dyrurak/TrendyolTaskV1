using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace TrendyolTaskV1.PageModel
{
    public class CategoryPage : BasePage
    {
        public CategoryPage(IWebDriver webDriver) : base(webDriver)
        {

        }

        [FindsBy(How = How.XPath, Using = "(//article[@class='component-item'])[1]")]
        public IWebElement LblFirstBoutique { get; set; }


        public void ClickFirstBoutique()
        {
            Click(LblFirstBoutique);
        }

    }
}
