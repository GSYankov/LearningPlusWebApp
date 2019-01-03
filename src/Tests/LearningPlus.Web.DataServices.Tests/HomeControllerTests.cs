using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;
using Xunit;
using System;

namespace LearningPlus.Web.DataServices.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void IndexView_GoogleMapsIframeExists()
        {
            var driver = new ChromeDriver(".");
            driver.Navigate().GoToUrl("https://localhost:44310");
            driver.Manage().Window.Minimize();
            driver.SwitchTo().Frame(driver.FindElement(By.TagName("iframe")));
            IWebElement iframe = null;
            try
            {
                iframe = driver.FindElement(By.TagName("pre"));
            }
            catch (Exception)
            {

            }

            iframe.ShouldBeNull();
        }
    }
}
