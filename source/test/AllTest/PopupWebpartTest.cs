using NUnit.Framework;
using SPPopupWithPreferences;
namespace Phresco.UnitTesting
{
    /// <summary>
    /// Summary description for PopupWebpartTest
    /// </summary>
    [TestFixture]
    public class PopupWebpartTest
    {
        public PopupWebpartTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }
        [Test]
        public void PopupWebpart_TestMethod()
        {
            UserPopupWebPart userPopupWebpart = new UserPopupWebPart();

            Assert.AreEqual("Untitled", userPopupWebpart.DisplayTitle);
        }
    }
}
