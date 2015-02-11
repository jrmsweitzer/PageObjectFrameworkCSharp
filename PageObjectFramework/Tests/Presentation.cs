using NUnit.Framework;
using OpenQA.Selenium;
using PageObjectFramework.Framework;
using PageObjectFramework.Models;
using PageObjectFramework.Models.Heroku;
using PageObjectFramework.Resources;
using System;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Reflection;
using System.Speech.Synthesis;
using System.Threading;

namespace PageObjectFramework.Tests
{
    [TestFixture]
    public class Presentation : PageObjectTestBase
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
            Thread.Sleep(2000);
            herokuLogin.WriteInUsernameTextBox("My name is Jeremy Sweitzer, and I've been with Catalyst since August 2013");
            Thread.Sleep(3000);
            herokuLogin.WriteInUsernameTextBox("Tonight's topic is going to be Selenium. What is Selenium?");
            Thread.Sleep(2800);
            herokuLogin.WriteInUsernameTextBox("Selenium is a multi-language tool used to automate things on the Internet.");
            Thread.Sleep(2500);
            herokuLogin.WriteInUsernameTextBox("I've been using Selenium for almost a year now, for many different tasks.");
            Thread.Sleep(3500);
            herokuLogin.WriteInUsernameTextBox("I've used it to test web pages here at the office.");
            Thread.Sleep(2000);
            herokuLogin.WriteInUsernameTextBox("I've taught it to play games, and to simply gather data.");
            Thread.Sleep(2500);
            herokuLogin.WriteInUsernameTextBox("It's uses are many, limited only by your imagination.");
            Thread.Sleep(2500);
            herokuLogin.WriteInUsernameTextBox("As you can see, Selenium can fill in text boxes for you...");
            Thread.Sleep(2500);
            herokuLogin.WriteInUsernameTextBox("And can pause play for any amount of time. ");
            Thread.Sleep(1700);
            herokuLogin.AppendToUsernameTextBox("1 ");
            Thread.Sleep(1000);
            herokuLogin.AppendToUsernameTextBox("2 ");
            Thread.Sleep(1000);
            herokuLogin.AppendToUsernameTextBox("3 ");
            Thread.Sleep(1000);
            herokuLogin.AppendToUsernameTextBox("4 ");
            Thread.Sleep(1000);
            herokuLogin.AppendToUsernameTextBox("5 ");
            Thread.Sleep(1000);
            herokuLogin.WriteInUsernameTextBox("You can check and uncheck checkboxes...");
            Thread.Sleep(2000);

            ((HerokuCheckboxes)heroku
                .ReturnToHeroku()
                .ClickLink(HerokuMain.Checkboxes))
                .ToggleUncheckedBox(3)
                .ToggleCheckedBox(3);

            herokuLogin
                .ReturnToLogin()
                .WriteInUsernameTextBox("As you had just seen, we can jump to different pages...");
            Thread.Sleep(2200);
            herokuLogin.WriteInUsernameTextBox("We can also select options from select dropdowns.");
            Thread.Sleep(3000);

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
            Thread.Sleep(3000);

            var youtube = new YouTube(Driver);
            youtube
                .SearchYouTube("dQw4w9WgXcQ")
                .ClickVideoAtIndex(1);

            Thread.Sleep(5000);

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
                var toEmail = "AllEmployees&Associates@catalystitservices.com";
                //var toEmail = "jsweitzer@catalystitservices.com";
                var subject = "Selenium PageObject Framework Source Code with C#";
                var message = "Check out this neat Source Code! It's an entire framework just for Selenium," +
                        " using PageObjects! https://github.com/jrmsweitzer/PageObjectFramework.";

                var email = new Email(Driver);
                email
                    .LogInWithCredentials(emailUsername, emailPassword)
                    .ComposeNewEmail(toEmail, subject, message);
                Thread.Sleep(5000);
            }

            if (yammerEmail != NOTSET && yammerPassword != NOTSET)
            {
                var message = "Hello, Catalyst, from Selenium! I hope you enjoyed my presentation! " +
                    "You can find the source code for the presentation here: https://github.com/jrmsweitzer/PageObjectFramework";

                var yammer = new Yammer(Driver);
                yammer
                    .LogInWithCredentials(yammerEmail, yammerPassword)
                    .PostNewMessage(message);
                Thread.Sleep(2500);
            }

            herokuLogin
                .ReturnToLogin()
                .WriteInUsernameTextBox("Now let's look at some Code!");
            Thread.Sleep(5000);
            herokuLogin
                .Login();
            Thread.Sleep(5000);
        }


        [Test]
        public void TimeTest()
        {
            var heroku = new HerokuMain(Driver);

            //PauseUntilTime("5:20:00");
            var herokuLogin =
                ((HerokuLogin)heroku
                .ClickLink(HerokuMain.FormAuthentication))
                .WriteInUsernameTextBox("10 minutes until the presentation...");
            //PauseUntilTime("5:20:30");
            //herokuLogin.WriteInUsernameTextBox("Did you know that a quarter of all people make up 25% of the population?");
            //PauseUntilTime("5:21:00");
            //herokuLogin.WriteInUsernameTextBox("Nine minutes to go...");
            //PauseUntilTime("5:21:30");
            //herokuLogin.WriteInUsernameTextBox("Did you know the center of a donut is 100% fat-free?");
            //PauseUntilTime("5:22:00");
            //herokuLogin.WriteInUsernameTextBox("Just eight minutes until the sun explodes! Just kidding.");
            //PauseUntilTime("5:22:20");
            //herokuLogin.WriteInUsernameTextBox("Build a man a fire, and he'll be warm for a day...");
            //PauseUntilTime("5:22:40");
            //herokuLogin.WriteInUsernameTextBox("Light a man on fire, and he'll be warm for life.");
            //PauseUntilTime("5:23:00");
            //herokuLogin.WriteInUsernameTextBox("Only seven minutes left! Can you feel the energy?");
            //PauseUntilTime("5:23:20");
            //herokuLogin.WriteInUsernameTextBox("There are 10 types of people in the world...");
            //PauseUntilTime("5:23:40");
            //herokuLogin.WriteInUsernameTextBox("Those who understand binary, and those who don't");
            //PauseUntilTime("5:24:00");
            //herokuLogin.WriteInUsernameTextBox("Six minutes...");
            //PauseUntilTime("5:24:20");
            //herokuLogin.WriteInUsernameTextBox("If at first you don't succeed... ");
            //PauseUntilTime("5:24:40");
            //herokuLogin.AppendToUsernameTextBox("Call it Version 1.0");
            //PauseUntilTime("5:25:00");
            //herokuLogin.WriteInUsernameTextBox("Earth is shutting down in five minutes - please save all files and log out.");
            //PauseUntilTime("5:25:30");
            //herokuLogin.WriteInUsernameTextBox("Keyboard not found. Press F1 to continue...");
            //PauseUntilTime("5:26:00");
            //herokuLogin.WriteInUsernameTextBox("Four minutes remaining! Go get yourself a drink.");
            //PauseUntilTime("5:26:30");
            //herokuLogin.WriteInUsernameTextBox("Three and a half minutes...");
            //PauseUntilTime("5:27:00");
            //herokuLogin.WriteInUsernameTextBox("Three minutes...");
            //PauseUntilTime("5:27:30");
            //herokuLogin.WriteInUsernameTextBox("Two and a half...");
            //PauseUntilTime("5:28:00");
            //herokuLogin.WriteInUsernameTextBox("Two minutes...");
            //PauseUntilTime("5:28:30");
            //herokuLogin.WriteInUsernameTextBox("A minute and a half until showtime!");
            //PauseUntilTime("5:29:00");

            herokuLogin.WriteInUsernameTextBox("Seconds remaining:   ");
            var currentSeconds = Int32.Parse(DateTime.Now.ToString("ss"));
            while (currentSeconds != 59)
            {
                var secondsRemaining = 60 - Int32.Parse(DateTime.Now.ToString("ss"));
                while (secondsRemaining + currentSeconds == 60)
                {
                    // stall for a second
                    secondsRemaining = 60 - Int32.Parse(DateTime.Now.ToString("ss"));
                }
                herokuLogin.Backspace();
                if (secondsRemaining >= 9)
                {
                    herokuLogin.Backspace();
                }
                herokuLogin.AppendToUsernameTextBox(secondsRemaining.ToString());
                currentSeconds = Int32.Parse(DateTime.Now.ToString("ss"));
            }


        }

        /// <summary>
        /// Pauses until the given time
        /// <para> @param time - the time to pause until, following h:mm:ss format</para>
        /// </summary>
        private void PauseUntilTime(string time)
        {
            while (!DateTime.Now.ToString("h:mm:ss").Contains(time))
            {
                // Do nothing except wait.
            }
        }
    }
}
