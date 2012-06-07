using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
namespace Phresco.UnitTesting
{
    /// <summary>
    /// Summary description for RegularExpressionTest
    /// </summary>
    [TestFixture]
    public class RegularExpressionTest
    {
        public RegularExpressionTest()
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
        public void RegularExpressionTestMethod()
        {
            //
            // TODO: Add test logic	here
            //
            RegularExpressionTest RegularExpression = new RegularExpressionTest();
        }
    }
}
