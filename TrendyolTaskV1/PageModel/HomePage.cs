using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;

namespace TrendyolTaskV1.PageModel
{
    public class HomePage : BasePage
    {       
        public CategoryPage CategoryPage;
        public HomePage(IWebDriver webDriver) : base(webDriver)
        {
            CategoryPage = new CategoryPage(webDriver);
        }

        [FindsBy(How = How.XPath, Using = "//a[@title='Close']")]
        public IWebElement BtnClosePopup { get; set; }

        [FindsBy(How = How.Id, Using = "accountBtn")]
        public IWebElement BtnLogin { get; set; }

        [FindsBy(How = How.Id, Using = "email")]
        public IWebElement TxtEmail { get; set; }

        [FindsBy(How = How.Id, Using = "password")]
        public IWebElement TxtPassword { get; set; }

        [FindsBy(How = How.Id, Using = "loginSubmit")]
        public IWebElement BtnLoginSubmit { get; set; }

        [FindsBy(How = How.XPath, Using = "//ul[@class='main-nav']/li")]
        public IList<IWebElement> BtnHeadersList { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//*[@class='modal-close']")]
        public IWebElement BtnCloseNotificationPopup { get; set; }

        [FindsBy(How = How.XPath, Using = "//article[@class='component-item']")]
        public IList<IWebElement> LblBoutiquesList { get; set; }

        [FindsBy(How = How.XPath, Using = "//*[@class='campaign-summary']/span[@class='name']")]
        public IList<IWebElement> LblBoutiqueNameList { get; set; }


        public void ClosePopUpWindow()
        {            
            Click(BtnClosePopup);                      
        }

        public void ClickLoginButton()
        {
            Click(BtnLogin);
        }

        public void SetEmailAddress(string emailAddress)
        {
            SetText(TxtEmail, emailAddress);
        }

        public void SetPassword(string password)
        {
            SetText(TxtPassword, password);
        }

        public void ClickLoginSubmit()
        {
            Click(BtnLoginSubmit);
        }

        public void ClickCategoryHeader(string category)
        {
            Click(Find(By.XPath("//a[text()='" + category + "']")));
        }

        public void CheckBoutiques()
        {
            for(int index=1; index<= BtnHeadersList.Count; index++){                                        
                Click(Find(By.XPath("(//ul[@class='main-nav']/li)[" + index + "]")));               
                CheckBoutique();                
            }
        }


        public void CheckBoutique()
        {
            for (int index = 0; index < LblBoutiquesList.Count; index++)
            {
                IWebElement boutique = LblBoutiquesList[index];
                if (!boutique.Displayed)
                {
                    string butiqueName = LblBoutiqueNameList[index].Text;
                    Console.WriteLine(butiqueName + " butigi için butik resmi yuklenmemistir! ");
                }
            }
        }


        public void CloseNotificationPopup()
        {
            try
            {
                Click(BtnCloseNotificationPopup);
            }
            catch
            {

            }
        }

      

        
    }
}
