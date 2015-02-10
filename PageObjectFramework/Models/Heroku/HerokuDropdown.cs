﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PageObjectFramework.Framework;
using System.Threading;

namespace PageObjectFramework.Models.Heroku
{
    public class HerokuDropdown : PageObjectModelBase, IHerokuApp
    {
        private IWebDriver Driver { get; set; }

        public HerokuDropdown(IWebDriver driver) : base(driver)
        {
        }

        public static readonly By Dropdown = By.Id("dropdown");

        public static readonly string Option1 = "Option 1";
        public static readonly string Option2 = "Option 2";

        public HerokuDropdown SelectOption(string option)
        {
            SelectByText(Dropdown, option);
            Thread.Sleep(1000);
            return this;
        }
    }
}
