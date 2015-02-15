using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace PageObjectFramework.Framework
{
    /// <summary>
    /// A mechanism for controlling the window handles.
    /// </summary>
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

        /// <summary>
        /// Adds a window handle to the private dictionary _windowHandles, to allow you
        /// to switch to the window by it's new name. Call this method every
        /// time a new window appears in your code.
        /// <para>@param handleName - the name of the newly added window handle. Can be anything you like.</para>
        /// </summary>
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
                    currentWindowHandleCount++;
                }
            }
        }

        /// <summary>
        /// Gets the current window handle count
        /// </summary>
        public int GetWindowHandleCount()
        {
            return Driver.WindowHandles.Count;
        }

        /// <summary>
        /// This is a private method called from the constructor of a new 
        /// WindowHandle object. This creates a new _windowHandle Dictionary,
        /// and adds the first handle into it.
        /// </summary>
        private void SetStartingWindowHandle()
        {
            _windowHandles = new Dictionary<string, string>();
            if (GetWindowHandleCount() > 1)
            {
                Assert.Fail(string.Format(
                    "You must call this method before opening other windows! There are currently {0} window handles!",
                    GetWindowHandleCount()));
            }

            _windowHandles.Add(MainWindowHandle, Driver.CurrentWindowHandle);
            currentWindowHandleCount = 1;
        }

        /// <summary>
        /// Switches control to the specified window handle.
        /// Must call AddWindowHandle(handleName) to add a handle with
        /// the given name first.
        /// <para>@param handleName - the name of the window handle</para>
        /// </summary>
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
