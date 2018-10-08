using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BeeOptLong;

namespace TestGetOptLong
{
    [TestClass]
    public class TestBeeOptLong
    {
        [TestMethod]
        public void NoOptions()
        {
            IList<BeeOpts> opts = new List<BeeOpts>();
            opts.Add()

            IList<string> args = BeeOpt.Parse(new string[] { "a", "b"}, )
        }
    }
}
