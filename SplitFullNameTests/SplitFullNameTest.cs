using Microsoft.VisualStudio.TestTools.UnitTesting;
using SplitFullNameCore.Interfaces;
using SplitFullNameCore.Modules;

namespace SplitFullNameTests
{
    [TestClass]
    public class SplitFullNameTest
    {
        [TestMethod]
        public void SplitFullName_3PartsFullMiddle()
        {
            //arrange
            string strFullName = "Matthew Ryan Hayes";

            //act
            ISplitFullName splitFullNameModule = new SplitFullNameModule();
            IName splitName = splitFullNameModule.SplitFullName(strFullName);

            //assert
            Assert.AreEqual("Matthew", splitName.FirstName);
            Assert.AreEqual("Ryan", splitName.MiddleName);
            Assert.AreEqual("Hayes", splitName.LastName);
        }

        [TestMethod]
        public void SplitFullName_3PartsMiddleInitial()
        {
            //arrange
            string strFullName = "Matthew R Hayes";

            //act
            ISplitFullName splitFullNameModule = new SplitFullNameModule();
            IName splitName = splitFullNameModule.SplitFullName(strFullName);

            //assert
            Assert.AreEqual("Matthew", splitName.FirstName);
            Assert.AreEqual("R", splitName.MiddleName);
            Assert.AreEqual("Hayes", splitName.LastName);
        }

        [TestMethod]
        public void SplitFullName_3PartsFullMiddleLMFformat()
        {
            //arrange
            string strFullName = "Hayes, Matthew Ryan";

            //act
            ISplitFullName splitFullNameModule = new SplitFullNameModule();
            IName splitName = splitFullNameModule.SplitFullName(strFullName);

            //assert
            Assert.AreEqual("Matthew", splitName.FirstName);
            Assert.AreEqual("Ryan", splitName.MiddleName);
            Assert.AreEqual("Hayes", splitName.LastName);
        }

        [TestMethod]
        public void SplitFullName_4PartsDoubleSpaceFullMiddleSuffix()
        {
            //arrange
            string strFullName = "Matthew  Ryan Hayes Jr.";

            //act
            ISplitFullName splitFullNameModule = new SplitFullNameModule();
            IName splitName = splitFullNameModule.SplitFullName(strFullName);

            //assert
            Assert.AreEqual("Matthew", splitName.FirstName);
            Assert.AreEqual("Ryan", splitName.MiddleName);
            Assert.AreEqual("Hayes Jr.", splitName.LastName);
        }

        [TestMethod]
        public void SplitFullName_5PartsMiddleInitialMultiplePartLastName()
        {
            //arrange
            string strFullName = "Lea M. De La Rosa";

            //act
            ISplitFullName splitFullNameModule = new SplitFullNameModule();
            IName splitName = splitFullNameModule.SplitFullName(strFullName);

            //assert
            Assert.AreEqual("Lea", splitName.FirstName);
            Assert.AreEqual("M", splitName.MiddleName);
            Assert.AreEqual("De La Rosa", splitName.LastName);
        }
    }
}
