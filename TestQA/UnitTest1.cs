using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

namespace TestQA
{
	[TestClass]
	public class UnitTest1
	{
        [TestMethod]
        [Obsolete]
        public void TestMethod1()
		{
			IWebDriver driver = new FirefoxDriver();

			//launch first website
			driver.Url = "https://www.amazon.com/";
			//driver.Manage().Window.Maximize();

			//perform search
			driver.FindElement(By.Id("twotabsearchtextbox")).SendKeys("Iphone11");
			driver.FindElement(By.CssSelector("#nav-search-submit-text > input:nth-child(1)")).Click();

			WebDriverWait waitForElement = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
			try
			{
				waitForElement.Until(ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/div[1]/div[2]/div/span[3]/div[2]/div[1]/span/div/div/span[1]")));
				Console.WriteLine("Search result displayed");
			}
			catch(NoSuchElementException)
			{
				Console.WriteLine("Element was not found in current context page.");
				throw;
			}

			//open new tab
			Console.WriteLine("Open new tab");
			IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
			js.ExecuteScript("window.open('https://www.ebay.com','_blank');");

			//perform search
			driver.SwitchTo().Window(driver.WindowHandles.Last());
			driver.FindElement(By.CssSelector("#gh-ac")).SendKeys("Iphone11");
			driver.FindElement(By.CssSelector("#gh-btn")).Click();

			WebDriverWait waitForElement1 = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
			try
			{
				waitForElement1.Until(ExpectedConditions.ElementIsVisible(By.CssSelector(".srp-controls")));
				Console.WriteLine("Search result displayed");
			}
			catch (NoSuchElementException)
			{
				Console.WriteLine("Element was not found in current context page.");
				throw;
			}

			//back to first tab
			driver.SwitchTo().Window(driver.WindowHandles.First());
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			//copy test result
			IWebElement resultPanel = driver.FindElement(By.XPath("//h2[@class='a-size-mini a-spacing-none a-color-base s-line-clamp-2']"));
            IReadOnlyCollection<IWebElement> searchResults = resultPanel.FindElements(By.XPath("./a"));

			foreach (IWebElement result in searchResults)
            {
				String value = result.Text;
				Console.WriteLine(value);
            }

			driver.SwitchTo().Window(driver.WindowHandles.Last());
			driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

			IWebElement resultPanel1 = driver.FindElement(By.XPath("//h3[@class='s-item__title']"));
			IReadOnlyCollection<IWebElement> searchResults1 = resultPanel1.FindElements(By.XPath("./a"));

			foreach (IWebElement result1 in searchResults1)
			{
				String value1 = result1.Text;
				Console.WriteLine(value1);
			}

			//driver.Close();
		}
	}
}
