using OpenQA.Selenium;

namespace PageObjectFramework.Framework
{
    #region Helpful Hints
    /**
     * Class ByFormatter extends Class By.
     * 
     * This class combines a By with a string formatter. You can use this class
     * to construct locators such as:
     * 
     * By link = ByFormatter.XPath("//a[contains(.,'{0}')]");
     * 
     * In this case, the XPath value is looking for a link containing text {0},
     * where {0} is the value that needs to be replaced. To use that locator,
     * you would format it, like so:
     * 
     * Click(link.Format("Cancel"));
     * 
     * That would click a link containing text "Cancel".
     */
    #endregion
    public class ByFormatter : By
    {
        // CONSTRUCTOR
        private ByFormatter(string locator, string formatter)
        {
            _locator = locator;
            _formatter = formatter;
        }
        // Private Members
        private string _locator;
        private string _formatter;

        #region Constants
        private const string FORMATTER_CLASSNAME = "class";
        private const string FORMATTER_CSSSELECTOR = "css";
        private const string FORMATTER_ID = "id";
        private const string FORMATTER_LINKTEXT = "link";
        private const string FORMATTER_PARTIALLINKTEXT = "partiallink";
        private const string FORMATTER_NAME = "name";
        private const string FORMATTER_TAGNAME = "tag";
        private const string FORMATTER_XPATH = "xpath";
        #endregion

        public static new ByFormatter ClassName(string locator)
        {
            return new ByFormatter(locator, ByFormatter.FORMATTER_CLASSNAME);
        }
        public static new ByFormatter CssSelector(string locator)
        {
            return new ByFormatter(locator, ByFormatter.FORMATTER_CSSSELECTOR);
        }
        public static new ByFormatter Id(string locator)
        {
            return new ByFormatter(locator, ByFormatter.FORMATTER_ID);
        }
        public static new ByFormatter LinkText(string locator)
        {
            return new ByFormatter(locator, ByFormatter.FORMATTER_LINKTEXT);
        }
        public static new ByFormatter PartialLinkText(string locator)
        {
            return new ByFormatter(locator, ByFormatter.FORMATTER_PARTIALLINKTEXT);
        }
        public static new ByFormatter Name(string locator)
        {
            return new ByFormatter(locator, ByFormatter.FORMATTER_NAME);
        }
        public static new ByFormatter TagName(string locator)
        {
            return new ByFormatter(locator, ByFormatter.FORMATTER_TAGNAME);
        }
        public static new ByFormatter XPath(string locator)
        {
            return new ByFormatter(locator, ByFormatter.FORMATTER_XPATH);
        }

        public By Format(params object[] vars)
        {
            int numVars = vars.Length;
            var by = _locator;
            for (int i = 0; i < numVars; i++)
            {
                string replacement = "{" + (i) + "}";
                by = by.Replace(replacement, vars[i].ToString());
            }
            switch(_formatter)
            {
                case FORMATTER_CLASSNAME:
                    return By.ClassName(by);
                case FORMATTER_CSSSELECTOR:
                    return By.CssSelector(by);
                case FORMATTER_ID:
                    return By.Id(by);
                case FORMATTER_LINKTEXT:
                    return By.LinkText(by);
                case FORMATTER_PARTIALLINKTEXT:
                    return By.Name(by);
                case FORMATTER_NAME:
                    return By.PartialLinkText(by);
                case FORMATTER_TAGNAME:
                    return By.TagName(by);
                case FORMATTER_XPATH:
                    return By.XPath(by);
                default:
                    return null;
            }
        }
    }
}
