using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Twilio;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;

namespace MFA_PoC
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl("https://www.amazon.in");

            #region CreateAccount
            //IWebElement signInButton = _driver.FindElement(By.CssSelector("#nav-link-accountList > span.nav-line-1"));
            //signInButton.Click();

            //IWebElement createAccount = _driver.FindElement(By.CssSelector("#createAccountSubmit"));
            //createAccount.Click();

            //IWebElement yourName = _driver.FindElement(By.CssSelector("#ap_customer_name"));
            //yourName.SendKeys("Test");

            //IWebElement ccDropDown = _driver.FindElement(By.CssSelector("#auth-country-picker"));
            //SelectElement countryCode = new SelectElement(ccDropDown);
            //countryCode.SelectByText("US +1");

            //IWebElement mobileNumber = _driver.FindElement(By.CssSelector("#ap_phone_number"));
            //mobileNumber.SendKeys("8635937563");
            //IWebElement passWord = _driver.FindElement(By.CssSelector("#ap_password"));
            //passWord.SendKeys("Test2345");

            //to avoid the apperance of capTCHA static wait is used
            //Thread.Sleep(700);
            //IWebElement continueButton = _driver.FindElement(By.CssSelector("#continue"));
            //continueButton.Click();


            //IWebElement oTP = _driver.FindElement(By.CssSelector("#auth-pv-enter-code"));
            //oTP.SendKeys(otp);


            //IWebElement createAmazonAccount = _driver.FindElement(By.CssSelector("#auth-verify-button"));
            //createAmazonAccount.Click();
            #endregion

            IWebElement signInButton = _driver.FindElement(By.CssSelector("#nav-link-accountList > span.nav-line-1"));
            signInButton.Click();

            //enter mobile number and password to login
            IWebElement enterMobileNumber = _driver.FindElement(By.CssSelector("#ap_email"));
            enterMobileNumber.SendKeys("8635937563");
            IWebElement continueButton = _driver.FindElement(By.CssSelector("#continue"));
            continueButton.Click();

            IWebElement password = _driver.FindElement(By.CssSelector("#ap_password"));
            password.SendKeys("Test2345");
            IWebElement loginButton = _driver.FindElement(By.CssSelector("#signInSubmit"));
            loginButton.Click();

            IWebElement sendOtpButton = _driver.FindElement(By.CssSelector("#continue"));
            sendOtpButton.Click();

            

            //Get the OTP using Twilio Account
            const string accountSid = "AC56f4e0ec37b7cc42807ad75369747e4a";
            const string authToken = "2ae8777060d091980245514beacf50d9";           
            TwilioClient.Init(accountSid, authToken);
           
            var message = MessageResource.Read().First();
            string smsBody= message.Body;
            string otp = Regex.Replace(smsBody,"[^-0-9]+"," ");
            Console.WriteLine(otp);

            IWebElement enterOtpButton = _driver.FindElement(By.CssSelector("div:nth-child(2) > input"));
            enterOtpButton.SendKeys(otp);


            IWebElement conButton=_driver.FindElement(By.CssSelector("#a-autoid-0>span>input"));
            conButton.Click();


            _driver.Close();
            _driver.Quit();

        }


    }
}
