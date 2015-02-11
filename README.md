# PageObjectFramework
A Framework for Selenium and PageObjects, with built-in logging support and 
screenshots.

To use this Framework, you'll have to have Visual Studio installed. I used 
VS2013 Community Edition, which is free to download here: 
http://www.visualstudio.com/en-us/products/visual-studio-community-vs.aspx


Once you get that installed, you can use VS2013's built-in git support to pull 
this project.
1. Go to the Team Explorer window. View -> Team Explorer
2. Click the outlet plug at the top (Connect to Team Projects)
3. Click 'Clone'
4. Use URL: https://github.com/jrmsweitzer/PageObjectFramework


If you have VS2012 instead, you can go to the URL listed above, and click 
"Clone to desktop". From there, you can open it with your IDE.


To build a Page Object, just start with the TemplateModel found in the Models 
folder, and edit it to your specific page. It's really simple once you get going.


You can find an XPath Cheat Sheet in the Resources folder, which can help you 
create custom XPaths for your elements. Always use XPath as a last resort, as 
an XPath address can change with a DOM update.


I've also created and included a ByFormatter. That is, in effect, a By combined
with a string formatter. You can find an example of one of those in use in the
YouTube model, in the method ClickVideoAtIndex(int n);


The Logger that's included will log actions (all the selenium button clicks and
such), as well as test runs and their results (pass/fail). The default location
is C:/Selenium/Logs, but can be changed in App.config, with the logDirectory
value. You can also turn off the action logging with the logAllActions value.
The test suite also takes screenshots of the browser when the test finishes, so
you can see where it passed/failed. The default location of that is 
C:\Selenium\Results\Screenshots\, but again, can be edited in App.Config.


The WindowHandler is also a custom creation of mine. Its purpose is to help you
keep track of open windows, and to send control of your selenium driver back
and forth between the open windows. You can see an example of this in action in
the Email Model, in the method 
ComposeNewEmail(string to, string subject, string message).


Every Selenium Test should implement PageObjectTestBase, to gain access to the
logger and screenshots, without any extra coding on your part. Similarly, every
PageObject Model you create should implement PageObjectModelBase. By doing
that, you'll gain access to all of the common methods such as Click(by) and
SendKeys(by, value).


The last thing to touch up on is which browser the tests run in. By default,
the tests will run in Chrome. However, that can be changed in the App.config
file.


I hope you enjoy using this Framework! If you have any 
questions/comments/concerns, feel free to email me at
jsweitzer@catalystitservices.com! 

Thanks,
~Jeremy Sweitzer
Developer II - Catalyst IT Services.