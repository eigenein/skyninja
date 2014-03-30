using System;

using NUnit.Framework;

namespace SkyNinja.Core.Test.Classes
{
    [TestFixture]
    public class Group
    {
        [TestCase("1", "2", false)]
        [TestCase("3", "3", true)]
        public void TestEquals(string group1, string group2, bool equals)
        {
            Assert.AreEqual(
                equals, 
                new Core.Classes.Group(group1).Equals(new Core.Classes.Group(group2)));
        }
    }
}
