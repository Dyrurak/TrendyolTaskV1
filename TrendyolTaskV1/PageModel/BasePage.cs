using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;


namespace TrendyolTaskV1.PageModel
{
    public class BasePage
    {
        private IWebDriver webDriver;
        public BasePage(IWebDriver driver)
        {
            webDriver = driver;
            PageFactory.InitElements(webDriver, this);
        }

        public IWebElement Find(By by)
        {
            return webDriver.FindElement(by);
        }

        public void Click(IWebElement btn)
        {            
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(7));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(btn));
            btn.Click();
        }

        public void RefreshPage()
        {
            webDriver.Navigate().Refresh();
        }

        public void Hover(IWebElement btn)
        {
            Actions action = new Actions(webDriver);
            action.MoveToElement(btn).Build().Perform();
        }

        public void SetText(IWebElement txt, string text)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(txt));
            txt.SendKeys(text);
        }

        public void SelectOptionByText(IWebElement slct, string text)
        {
            SelectElement selectElement = new SelectElement(slct);
            selectElement.SelectByText(text);
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(5));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TextToBePresentInElement(slct, text));
        }

        public void ScrollTo(IWebElement el)
        {
            string jsStmt = String.Format("window.scrollTo({0},{1})", el.Location.X, el.Location.Y);
            GetScriptExecutor().ExecuteScript(jsStmt, true);
        }

        public IJavaScriptExecutor GetScriptExecutor()
        {
            return (IJavaScriptExecutor)webDriver;
        }

        public void SendKey(string keys){
            Actions action = new Actions(webDriver);            
            action.SendKeys(keys).Build().Perform();
        }

        public void DismissModalAlert(){
            IAlert alert = webDriver.SwitchTo().Alert();
            alert.Dismiss();
        }

        public void ClosePopupWindow()
        {
            string closeButtonScript = "document.getElementsByClassName('fancybox-item fancybox-close')[0].click()";
            GetScriptExecutor().ExecuteScript(closeButtonScript);
        }  
        
        public void InitElements(object elementLocator)
        {
            PageFactory.InitElements(webDriver, elementLocator);
        }

        public void CloseBrowser()
        {
            webDriver.Quit();
        }
    }
}