using NUnit.Framework;
using PageObjectFramework.Framework;
using PageObjectFramework.Models;
using PageObjectFramework.Models.Heroku;
using System;
using System.Media;
using System.Speech.Synthesis;
using System.Threading;

namespace PageObjectFramework.Tests
{
    [TestFixture]
    public class Presentation : PageObjectTestBase
    {
        [Test]
        public void GetInstalledVoices()
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();

            // Output information about all of the installed voices. 
            foreach (InstalledVoice voice in synth.GetInstalledVoices())
            {
                VoiceInfo info = voice.VoiceInfo;

                Console.WriteLine(" Name:          " + info.Name);
                Console.WriteLine(" Culture:       " + info.Culture);
                Console.WriteLine(" Age:           " + info.Age);
                Console.WriteLine(" Gender:        " + info.Gender);
                Console.WriteLine(" Description:   " + info.Description);
                Console.WriteLine(" ID:            " + info.Id);
            }

            synth.Rate = 2;

            synth.SelectVoice("Microsoft Hazel Desktop");
            synth.Speak("My name is Hazel.");

            synth.SelectVoice("Microsoft David Desktop");
            synth.Speak("David speaking here.");

            synth.SelectVoice("Microsoft Zira Desktop");
            synth.Speak("And this is Zira!");
        }

        [Test]
        public void PlayAudioFile()
        {
            var player = new SoundPlayer();
            player.SoundLocation = ""; //location of .wav file
            player.Play();
        }

        [Test]
        public void EmailLoginTest()
        {
            var email = new Email(Driver);

            email
                .SignInWithCredentials(@"catalystsolves\jsweitzer", "password")
                //.ComposeNewEmail("AllEmployees@catalystitservices.com",
                //    "Selenium PageObject Framework SourceCode with C#",
                //    "Message Goes Here...");                
                .ComposeNewEmail("jsweitzer@catalystitservices.com", 
                    "Selenium PageObject Framework SourceCode with C#", 
                    "Message Goes Here...");
            Thread.Sleep(5000);
        }

        [Test]
        public void TimeTest()
        {
            var google = new Google(Driver);

            PauseUntilTime("5:20:00");
            google.EnterSearchText("10 minutes until the presentation...");
            PauseUntilTime("5:20:30");
            google.EnterSearchText("Did you know that a quarter of all people make up 25% of the population?");
            PauseUntilTime("5:21:00");
            google.EnterSearchText("Nine minutes to go...");
            PauseUntilTime("5:21:30");
            google.EnterSearchText("Did you know the center of a donut is 100% fat-free?");
            PauseUntilTime("5:22:00");
            google.EnterSearchText("Just eight minutes until the sun explodes! Just kidding.");
            PauseUntilTime("5:22:20");
            google.EnterSearchText("Build a man a fire, and he'll be warm for a day...");
            PauseUntilTime("5:22:40");
            google.EnterSearchText("Light a man on fire, and he'll be warm for life.");
            PauseUntilTime("5:23:00");
            google.EnterSearchText("Only seven minutes left! Can you feel the energy?");
            PauseUntilTime("5:23:20");
            google.EnterSearchText("There are 10 types of people in the world...");
            PauseUntilTime("5:23:40");
            google.EnterSearchText("Those who understand binary, and those who don't");
            PauseUntilTime("5:24:00");
            google.EnterSearchText("Six minutes...");
            PauseUntilTime("5:24:20");
            google.EnterSearchText("If at first you don't succeed... ");
            PauseUntilTime("5:24:40");
            google.AppendSearchText("Call it Version 1.0");
            PauseUntilTime("5:25:00");
            google.EnterSearchText("Five minutes remaining!");
            PauseUntilTime("5:25:30");
            google.EnterSearchText("Keyboard not found. Press F1 to continue...");
            PauseUntilTime("5:26:00");
            google.EnterSearchText("Four minutes remaining! Go get yourself a drink.");
            PauseUntilTime("5:26:30");
            google.EnterSearchText("Three and a half minutes...");
            PauseUntilTime("5:27:00");
            google.EnterSearchText("Three minutes...");
            PauseUntilTime("5:27:30");
            google.EnterSearchText("Two and a half...");
            PauseUntilTime("5:28:00");
            google.EnterSearchText("Two minutes...");
            PauseUntilTime("5:28:30");
            google.EnterSearchText("A minute and a half until showtime!");
            PauseUntilTime("5:29:00");

            // POTENTIAL JOKES
            // REALITY.SYS corrupted. Reboot UNIVERSE [Y,n] ?
            // Earth is shutting down in five minutes - please save all files and log out
            // The Internet: where men are men, women are men, and children are FBI agents.
            // Programming today is a race between software engineers striving to build bigger and better idiot-proof programs, 
            // and the Universe trying to produce bigger and better idiots. So far, the Universe is winning.

            var currentSeconds = Int32.Parse(DateTime.Now.ToString("ss"));
            while (currentSeconds != 59)
            {
                var secondsRemaining = 60 - Int32.Parse(DateTime.Now.ToString("ss"));
                while (secondsRemaining + currentSeconds == 60)
                {
                    // stall for a second
                    secondsRemaining = 60 - Int32.Parse(DateTime.Now.ToString("ss"));
                }
                google.EnterSearchText(secondsRemaining +  " seconds remaining.");
                currentSeconds = Int32.Parse(DateTime.Now.ToString("ss"));
            }


        }

        private void PauseUntilTime(string time)
        {
            while (!DateTime.Now.ToString("h:mm:ss").Contains(time))
            {
                // Do nothing.
                // Wait until 5:20 to start the countdown
            }
        }

        [Test]
        public void MainPresentation()
        {
            var heroku = new HerokuMain(Driver);

            var herokuLogin =
                ((HerokuLogin)heroku
                .ClickLink(HerokuMain.FormAuthentication))
                .WriteInUsernameTextBox("Hello Catalyst, and welcome to Developer training.");
            Thread.Sleep(2000);
            herokuLogin.WriteInUsernameTextBox("My name is Jeremy Sweitzer, and I've been with Catalyst since August 2013");
            Thread.Sleep(2000);
            herokuLogin.WriteInUsernameTextBox("Tonight's topic is going to be Selenium. What is Selenium?");
            Thread.Sleep(2000);
            herokuLogin.WriteInUsernameTextBox("Selenium is a multi-language tool used to automate things on the Internet.");
            Thread.Sleep(2000);
            herokuLogin.WriteInUsernameTextBox("I've been using Selenium for almost a year now, for many different tasks.");
            Thread.Sleep(2000);
            herokuLogin.WriteInUsernameTextBox("I've used it to test web pages here at the office.");
            Thread.Sleep(2000);
            herokuLogin.WriteInUsernameTextBox("I've taught it to play games for me, and to simply gather data for me.");
            Thread.Sleep(2000);
            herokuLogin.WriteInUsernameTextBox("It's uses are many, limited only by your imagination.");
            Thread.Sleep(2000);
            herokuLogin.WriteInUsernameTextBox("As you can see, Selenium can fill in text boxes for you...");
            Thread.Sleep(2000);
            herokuLogin.WriteInUsernameTextBox("And can pause play for any amount of time. ");
            Thread.Sleep(1500);
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
            Thread.Sleep(3000);
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

            Thread.Sleep(52000);

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

            var weather = new Weather(Driver);
            string weatherForTomorrow =
                weather
                    .EnterLocation("21201")
                    .GetTomorrowsWeather();

            var textToSpeech = new SpeechSynthesizer();
            textToSpeech.SelectVoiceByHints(VoiceGender.Neutral);
            textToSpeech.Speak(weatherForTomorrow);

            var email = new Email(Driver);
            email
                .SignInWithCredentials(@"catalystsolves\jsweitzer", "password")
                .ComposeNewEmail("AllEmployees@catalystitservices.com",
                    "Selenium PageObject Framework SourceCode with C#",
                    "https://github.com/jrmsweitzer/PageObjectFramework");
            Thread.Sleep(5000);

            var yammer = new Yammer(Driver);
            yammer
                .LogInWithCredentials("jsweitzer@catalystitservices.com", "password")
                .PostNewMessage("Hello, Catalyst, from Selenium! I hope you enjoyed my presentation!");
            Thread.Sleep(2500);

            herokuLogin
                .ReturnToLogin()
                .WriteInUsernameTextBox("Now let's look at some Code!");
        
        }
    }
}
