using System;
using NUnit.Framework;
using OpenQA.Selenium;

namespace PhotoSearchTest
{
    public class Tests
    {
        private IWebDriver driver;

        private readonly By searchInputField = By.XPath("//input[@name='q']");
        private readonly By searchButton = By.XPath("//div[@class='lJ9FBc']//input[@class='gNO89b'][@name='btnK']");
        private readonly By imageButton = By.XPath("//a[@data-hveid='CAEQAw']");
        private readonly By imageBoxes = By.XPath("//div[@class='islrc']");

        private const string searchText = "christmas pug";
        private const int expectedImageBoxes = 1;

        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("https://www.google.com/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            var search = driver.FindElement(searchInputField);
            search.SendKeys(searchText);


            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            var searchBtn = driver.FindElement(searchButton);
            searchBtn.Click();

            var image = driver.FindElement(imageButton);
            image.Click();

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(50);
            var actualImageBoxes = driver.FindElements(imageBoxes).Count;
            Assert.AreEqual(expectedImageBoxes, actualImageBoxes, "window doesn't upload all images");

            ((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("photoTest.png", ScreenshotImageFormat.Png);
        }
        [TearDown]
        public void CloseBrowser()
        {
            driver.Quit();
        }
    }
}
