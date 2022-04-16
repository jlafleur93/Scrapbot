using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Net;
using System.IO;



class HelloSelenium
{

    static void Main()
    {
        var listOfStrings = new List<string>();

        new DriverManager().SetUpDriver(new ChromeConfig());
        IWebDriver driver = new ChromeDriver();
        try
        {
            // Navigate to Url
            driver.Navigate().GoToUrl("https://imgur.com/");
            // Store 'google search' button web element
           var searchBox = driver.FindElement(By.Name("q"));
           var searchButton = driver.FindElement(By.ClassName("Searchbar-submitInput"));

           searchBox.SendKeys("Cats");
           driver.FindElement(By.Name("q")).GetAttribute("value"); // => "Selenium"
                                                                    //Give wait time after
           driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);
           searchButton.Click();
            Actions actionProvider = new Actions(driver);
            // Perform click action on the element
           driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2000);
            IWebElement element = driver.FindElement(By.ClassName("cards"));
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2500);
            IList<IWebElement> elements = element.FindElements(By.TagName("img"));
            foreach (IWebElement e in elements)
            {
                listOfStrings.Add(e.GetAttribute("src"));
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(2500);

            }

            

        }
        finally

        {


            var t = new StringHandler();
            t.DownloadImage(listOfStrings);
            driver.Quit();
        }
    }
}

//https://social.msdn.microsoft.com/Forums/vstudio/en-US/cb291169-aacc-407a-ae33-0d06783ed56f/download-a-jpeg-file-and-store-it-in-the-localfolder?forum=winappswithcsharp
//for async func and following for idea
class StringHandler
{
    public void DownloadImage(List<string> strings)
    {
        string remoteUri = "http://www.contoso.com/library/homepage/images/";
string fileName = "test.jpg", myStringWebResource = null;
// Create a new WebClient instance.
WebClient myWebClient = new WebClient();
// Concatenate the domain with the Web resource filename.
myStringWebResource = remoteUri + fileName;
            foreach (var ele in strings)
        {
            myWebClient.DownloadFile(ele, fileName);


        }
        Console.WriteLine(strings.Count);
    }
}