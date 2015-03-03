using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using PageObjectFramework.Framework;
using PageObjectFramework.Models;
using PageObjectFramework.Models.Heroku;
using System.Speech.Synthesis;
using System.Threading;

namespace PageObjectFramework.Tests
{
    //[TestFixture(typeof(ChromeDriver))]
    //[TestFixture(typeof(FirefoxDriver))]
    //[TestFixture(typeof(InternetExplorerDriver))]
    //[TestFixture(typeof(SafariDriver))]
    public class Presentation<TWebDriver> : PageObjectTest<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private const string NOTSET = "NOT_SET";

        [Test]
        public void MainPresentation()
        {

            var emailUsername = NOTSET;
            var emailPassword = NOTSET;
            var yammerEmail = NOTSET;
            var yammerPassword = NOTSET;

            var heroku = new HerokuMain(Driver);

            var herokuLogin =
                ((HerokuLogin)heroku
                .ClickLink(HerokuMain.FormAuthentication))
                .WriteInUsernameTextBox("Hello Catalyst, and welcome to Developer training.");
            herokuLogin.Sleep(2000);
            herokuLogin.WriteInUsernameTextBox("My name is Jeremy Sweitzer, and I've been with Catalyst since August 2013");
            herokuLogin.Sleep(3000);
            herokuLogin.WriteInUsernameTextBox("Tonight's topic is going to be Selenium. What is Selenium?");
            herokuLogin.Sleep(2800);
            herokuLogin.WriteInUsernameTextBox("Selenium is a multi-language tool used to automate things on the Internet.");
            herokuLogin.Sleep(2500);
            herokuLogin.WriteInUsernameTextBox("I've been using Selenium for almost a year now, for many different tasks.");
            herokuLogin.Sleep(3500);
            herokuLogin.WriteInUsernameTextBox("I've used it to test web pages here at the office.");
            herokuLogin.Sleep(2000);
            herokuLogin.WriteInUsernameTextBox("I've taught it to play games, and to simply gather data.");
            herokuLogin.Sleep(2500);
            herokuLogin.WriteInUsernameTextBox("It's uses are many, limited only by your imagination.");
            herokuLogin.Sleep(2500);
            herokuLogin.WriteInUsernameTextBox("As you can see, Selenium can fill in text boxes for you...");
            herokuLogin.Sleep(2500);
            herokuLogin.WriteInUsernameTextBox("And can pause play for any amount of time. ");
            herokuLogin.Sleep(1700);
            herokuLogin.AppendToUsernameTextBox("1 ");
            herokuLogin.Sleep(1000);
            herokuLogin.AppendToUsernameTextBox("2 ");
            herokuLogin.Sleep(1000);
            herokuLogin.AppendToUsernameTextBox("3 ");
            herokuLogin.Sleep(1000);
            herokuLogin.AppendToUsernameTextBox("4 ");
            herokuLogin.Sleep(1000);
            herokuLogin.AppendToUsernameTextBox("5 ");
            herokuLogin.Sleep(1000);
            herokuLogin.WriteInUsernameTextBox("You can check and uncheck checkboxes...");
            herokuLogin.Sleep(2000);

            ((HerokuCheckboxes)heroku
                .ReturnToHeroku()
                .ClickLink(HerokuMain.Checkboxes))
                .ToggleUncheckedBox(3)
                .ToggleCheckedBox(3);

            herokuLogin
                .ReturnToLogin()
                .WriteInUsernameTextBox("As you had just seen, we can jump to different pages...");
            herokuLogin.Sleep(2200);
            herokuLogin.WriteInUsernameTextBox("We can also select options from select dropdowns.");
            herokuLogin.Sleep(3000);

            ((HerokuDropdown)heroku
                .ReturnToHeroku()
                .ClickLink(HerokuMain.Dropdown))
                .SelectOption(HerokuDropdown.Option1)
                .SelectOption(HerokuDropdown.Option2)
                .SelectOption(HerokuDropdown.Option1)
                .SelectOption(HerokuDropdown.Option2);

            herokuLogin
                .ReturnToLogin()
                .WriteInUsernameTextBox("Basically, anything you can do using a browser, Selenium can too!");
            herokuLogin.Sleep(3000);

            var youtube = new YouTube(Driver);
            youtube
                .SearchYouTube("dQw4w9WgXcQ")
                .ClickVideoAtIndex(1);

            youtube.Sleep(5000);

            var cookieclicker = new CookieClicker(Driver);
            cookieclicker
                .ClickCookie(100)
                .BuyGrandma()
                .ClickCookie(115)
                .BuyGrandma()
                .ClickCookie(133)
                .BuyGrandma()
                .ClickCookie(153)
                .BuyGrandma()
                .ClickCookie(465)
                .BuyFarm()
                .ClickCookie(500);

            var textToSpeech = new SpeechSynthesizer();
            textToSpeech.SelectVoiceByHints(VoiceGender.Neutral);

            var weather = new Weather(Driver);
            string weatherForTomorrow =
                weather
                    .EnterLocation("21201")
                    .GetTomorrowsWeather();

            textToSpeech.Speak("Tomorrow's weather is");
            textToSpeech.Speak(weatherForTomorrow);

            if (emailUsername != NOTSET && emailPassword != NOTSET)
            {
                var toEmail = "jsweitzer@catalystitservices.com";
                var subject = "Selenium PageObject Framework Source Code with C#";
                var message = "Check out this neat Source Code! It's an entire framework just for Selenium," +
                        " using PageObjects! https://github.com/jrmsweitzer/PageObjectFramework.";

                var email = new Email(Driver);
                email
                    .LogInWithCredentials(emailUsername, emailPassword)
                    .ComposeNewEmail(toEmail, subject, message);
                email.Sleep(5000);
            }

            if (yammerEmail != NOTSET && yammerPassword != NOTSET)
            {
                var message = "Hello, Catalyst, from Selenium! I hope you enjoyed my presentation! " +
                    "You can find the source code for the presentation here: https://github.com/jrmsweitzer/PageObjectFramework";

                var yammer = new Yammer(Driver);
                yammer
                    .LogInWithCredentials(yammerEmail, yammerPassword)
                    .PostNewMessage(message);
                yammer.Sleep(2500);
            }

            herokuLogin
                .ReturnToLogin()
                .WriteInUsernameTextBox("Now let's look at some Code!");
            herokuLogin.Sleep(5000);
            herokuLogin
                .Login();
            herokuLogin.Sleep(5000);
        }
    }
}
