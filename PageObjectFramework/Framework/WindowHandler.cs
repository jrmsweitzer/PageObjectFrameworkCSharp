using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace PageObjectFramework.Framework
{
    public class WindowHandler
    {
        // This class is designed to help you control multiple windows with Selenium.
        private IWebDriver Driver { get; set; }
        private Dictionary<string, string> _windowHandles { get; set; }

        private int currentWindowHandleCount = 0;
        public static readonly string MainWindowHandle = "Main Window";

        public WindowHandler(IWebDriver driver)
        {
            Driver = driver;
            SetStartingWindowHandle();
        }

        /** AddNewWindowHandle(string handleName)
         * 
         *  Adds a window handle to the dictionary _windowHandles, to allow you
         *  to switch to the window by it's new name. Call this method every
         *  time a new window appears in your code.
         *  
         * @param handleName - the name of the newly added window handle
         * 
         * @returns void
         */
        public void AddNewWindowHandle(string handleName)
        {
            int windowCount = GetWindowHandleCount();

            if (windowCount > currentWindowHandleCount + 1)
            {
                Assert.Fail(string.Format("Error: Unmatched window counts. Expected there to be {0} handles, but there are currently {1} handles.",
                    currentWindowHandleCount + 1, windowCount));
            }

            for (int i = 0; i < windowCount; i++)
            {
                Driver.SwitchTo().Window(Driver.WindowHandles[i]);
                if(!_windowHandles.ContainsValue(Driver.CurrentWindowHandle))
                {
                    _windowHandles.Add(handleName, Driver.CurrentWindowHandle);
                    currentWindowHandleCount += 1;
                }
            }
        }

        /** GetWindowHandleCount
         * 
         *  Gets the current window handle count
         *  
         *  @return int - the window handle count
         */
        public int GetWindowHandleCount()
        {
            return Driver.WindowHandles.Count;
        }

        /** SetStartingWindowHandle()
         * 
         *  This is a private method called from the constructor of a new 
         *  WindowHandle object. This creates a new _windowHandle Dictionary,
         *  and adds the first handle into it.
         *  
         * @returns void
         */
        private void SetStartingWindowHandle()
        {
            _windowHandles = new Dictionary<string, string>();
            if (GetWindowHandleCount() > 1)
            {
                Assert.Fail(string.Format(
                    "You must call this method before opening other windows! There are currently {0} window handles!",
                    Driver.WindowHandles.Count));
            }

            _windowHandles.Add(MainWindowHandle, Driver.CurrentWindowHandle);
            currentWindowHandleCount = 1;
        }

        /** SwitchToHandle(string handleName)
         * 
         *  Switches control to the specified window handle.
         *  
         *  Must call AddWindowHandle(handleName) to add a handle with
         *  the given name first.
         *  
         * @param handleName - the name of the window handle; Can be anything you like.
         * 
         * @returns void 
         */
        public void SwitchToHandle(string handleName)
        {
            if (!_windowHandles.ContainsKey(handleName))
            {
                Assert.Fail(string.Format(
                    "Window Handle with name {0} has not been saved. Call AddNewWindowHandle(handleName) to add the handle.",
                    handleName));
            }

            int windowCount = GetWindowHandleCount();

            for (int i = 0; i < windowCount; i++)
            {
                Driver.SwitchTo().Window(Driver.WindowHandles[i]);
                if (Driver.CurrentWindowHandle == _windowHandles[handleName])
                {
                    break;
                }
            }
        }
    }
}
