using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;

namespace TrendyolTaskV1.PageModel
{
    public class BoutiquePage : BasePage
    {
        public BoutiquePage(IWebDriver webDriver) : base(webDriver)
        {

        }

        [FindsBy(How = How.XPath, Using = "//*[@class='boutique-product']")]
        public IList<IWebElement> LblProductsList { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='boutique-product']//span[@class='name']")]
        public IList<IWebElement> lblProductsNamesList { get; set; }

        

        public void CheckProducts()
        {
            for (int index = 0; index < LblProductsList.Count; index++)
            {
                IWebElement product = LblProductsList[index];
                if (!product.Displayed)
                {
                    string productName = lblProductsNamesList[index].Text;
                    Console.WriteLine(productName + " ürünü için ürün resmi yuklenmemistir! ");
                }
            }
        }

        public void ClickFirstProduct()
        {
            Click(lblProductsNamesList[0]);
        }

    }
}
