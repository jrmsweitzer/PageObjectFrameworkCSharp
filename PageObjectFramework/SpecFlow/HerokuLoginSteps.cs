using System;
using TechTalk.SpecFlow;
using PageObjectFramework.Framework;
using OpenQA.Selenium;
using PageObjectFramework.Models.Heroku;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;

namespace PageObjectFramework.SpecFlow
{
    [Binding]
    public class HerokuLoginSteps : ScenarioRunner
    {
        HerokuLogin heroku;

        [Given(@"I am on on the HerokuLogin Page")]
        public void GivenIAmOnOnTheHerokuLoginPage()
        {
            heroku = new HerokuLogin(); 
        }

        [When(@"I input valid credentials")]
        public void WhenIInputValidCredentials()
        {
            heroku
                .WriteInUsernameTextBox("tomsmith")
                .WriteInPasswordTextBox("SuperSecretPassword!")
                .Login();
        }

        [Then(@"I should log in successfully")]
        public void ThenIShouldLogInSuccessfully()
        {
            var expectedURL = "http://the-internet.herokuapp.com/secure";
            var actualURL = heroku.GetUrl();

            Assert.AreEqual(expectedURL, actualURL);
        }
    }
}
