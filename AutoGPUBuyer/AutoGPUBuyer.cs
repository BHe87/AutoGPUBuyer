using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutoGPUBuyer
{
    public class AutoGPUBuyer
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver(@"C:\AutoGPUBuyer\AutoGPUBuyer\AutoGPUBuyer\Drivers\");
            String url = "https://www.bestbuy.com/site/gigabyte-amd-radeon-rx-6700-xt-gaming-oc-12gb-gddr6-pci-express-4-0-gaming-graphics-card/6457993.p?skuId=6457993";
            String xpath = "//*[text()='Add to Cart']";

            driver.Url = (url);
            if (isInStock(driver, xpath))
            {
                if(buy(driver))
                {
                    driver.Close();
                    //System.exit(0);
                }
            } else
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            }
        }

        static bool isInStock(IWebDriver driver, String xpath)
        {
            return driver.FindElements(By.XPath(xpath)).Count >= 1;
        }

        static bool buy(IWebDriver driver)
        {
            addToCart(driver);
            checkout(driver);
            inputShippingInfo(driver);
            inputPaymentInfo(driver);
            placeOrder(driver);
            return false;
        }

        static void addToCart(IWebDriver driver)
        {
            String xpath = "//*[text()='Add to Cart']";
            IWebElement element = driver.FindElement(By.XPath(xpath));
            element.Click();

            driver.Navigate().Refresh();

            xpath = "//*[@title='Cart']";
            element = driver.FindElement(By.XPath(xpath));
            element.Click();

            //radio button to opt into shipping instead of pickup
            xpath = "/html/body/div[1]/main/div/div[2]/div[1]/div/div[1]/div[1]/section[1]/div[4]/ul/li/section/div[2]/div[2]/form/div[2]/fieldset/div[2]/div[1]/div/div/div/input";
            element = driver.FindElement(By.XPath(xpath));
            element.Click();
        }

        static void checkout(IWebDriver driver)
        {
            String xpath = "//*[text()='Checkout']";
            IWebElement element = driver.FindElement(By.XPath(xpath));
            element.Click();
        }

        static void inputShippingInfo(IWebDriver driver)
        {
            IWebElement element = driver.FindElement(By.Id("consolidatedAddresses.ui_address_2.firstName"));
            element.SendKeys("Brandon");

            element = driver.FindElement(By.Id("consolidatedAddresses.ui_address_2.lastName"));
            element.SendKeys("He");

            element = driver.FindElement(By.Id("consolidatedAddresses.ui_address_2.street"));
            element.SendKeys("Address");

            element = driver.FindElement(By.Id("consolidatedAddresses.ui_address_2.city"));
            element.SendKeys("City");

            SelectElement menu = new SelectElement(driver.FindElement(By.Id("consolidatedAddresses.ui_address_2.state")));
            element.SendKeys("Brandon");

            element = driver.FindElement(By.Id("consolidatedAddresses.ui_address_2.zipcode"));
            element.SendKeys("Zip");
        }

        static void inputPaymentInfo(IWebDriver driver)
        {

        }

        static void placeOrder(IWebDriver driver)
        {

        }
    }
}
