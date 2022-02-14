using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AssesmentTest1
{
    class Driver
    {

        IWebDriver driver;

        [SetUp]
        public void startBrowser()
        {
            driver = new ChromeDriver("C:\\Users\\Jahnavi\\Work\\lib\\chromedriver_win32");
        }

        [Test]
        public void test1()
        {
            driver.Url = "https://www.labcorp.com/";
            Thread.Sleep(5000);
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            IWebElement acceptCookies = driver.FindElement(By.Id("onetrust-accept-btn-handler"));
            if (acceptCookies.Displayed)
            {
                acceptCookies.Click();
            }

            driver.FindElement(By.XPath("//div[@class='footer-links']//a[text()='Careers']")).Click();
            driver.FindElement(By.Id("typehead")).SendKeys("QA Analyst");
            driver.FindElement(By.Id("ph-search-backdrop")).Submit();
            driver.FindElement(By.XPath("//div[@class='job-title']//span")).Click();

            IWebElement jobTitle = driver.FindElement(By.XPath("//h1[@class='job-title']"));
            String jobtitleText = jobTitle.Text;
            Assert.IsTrue(jobTitle.Displayed, "Job Title displayed");

            IWebElement jobLocation = driver.FindElement(By.XPath("//span[contains(@class,'job-location')]"));
            String jobLocationText = jobLocation.Text;
            Assert.IsTrue(jobLocation.Displayed, "Job Location element displayed");

            IWebElement jobId = driver.FindElement(By.XPath("//span[contains(@class,'jobId')]"));
            String jobIdText = jobId.Text;
            Assert.IsTrue(jobId.Displayed, "Job Id displayed");

            String jdSummary = "The Quality Analyst will be responsible for supporting projects and activities related to quality assurance";
            String jdRequirements = "Provide data and reports for quality teams and support all Quality Improvement efforts";
            String jdResp = "Knowledgeable of CAP proficiency testing";
            Assert.IsTrue(driver.FindElement(By.XPath("//div[@data-ph-at-id='jobdescription-text']/p/span/span[2]")).Text.Contains(jdSummary),"Job description matched");
            Assert.IsTrue(driver.FindElement(By.XPath("//div[@data-ph-at-id='jobdescription-text']//ul[1]/li[4]")).Text.Contains(jdRequirements),"Job Requirements matched");
            Assert.IsTrue(driver.FindElement(By.XPath("//div[@data-ph-at-id='jobdescription-text']//ul[2]/li[5]")).Text.Contains(jdResp), "Job Responsibilities matched");

            driver.FindElement(By.XPath("//a[@data-ph-at-id='apply-link']")).Click();
            Thread.Sleep(5000);
            Console.WriteLine("Window handle count: "+driver.WindowHandles.Count);
            String originalWindow = driver.CurrentWindowHandle;
            driver.SwitchTo().Window(driver.WindowHandles[1]);

            driver.FindElement(By.XPath("//div[@class='popover-content']//button[@type='button']")).Click();

            IWebElement jobDetailTitle = driver.FindElement(By.XPath("//span[@class='jobTitle job-detail-title']"));
            Assert.AreEqual(jobtitleText, jobDetailTitle.Text, "Same job title available") ;

            IWebElement jobDetailLocation = driver.FindElement(By.XPath("//span[contains(@label,'Location')]//span[@class='resultfootervalue']"));
            Assert.AreEqual(jobLocationText.Split("\r\n")[1].Split(',')[0], jobDetailLocation.Text.Split(',')[0], "Same job location available");

            IWebElement jobDetailId = driver.FindElement(By.XPath("//span[@class='jobnum']"));
            Assert.AreEqual(jobIdText.Split(": ")[1], jobDetailId.Text.Split('#')[1], "Same job id available");

            Assert.IsTrue(driver.FindElement(By.Id("ae-main-content")).Text.Contains("jdRequirements"));

            driver.FindElement(By.XPath("//button[@type='button']//span[text()='Return to Job Search']")).Click();

            driver.FindElement(By.XPath("//button[@type='button' and contains(text(),'Search for Jobs!')]"));
            driver.Close();
            driver.SwitchTo().Window(originalWindow);
        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
            driver.Quit();
        }

    }
}
