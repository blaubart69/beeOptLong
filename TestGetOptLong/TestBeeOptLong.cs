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
            var opts = new BeeOptsBuilder()
                .GetOpts();

            bool ok = BeeOpts.Parse(
                new string[] { "a", "b" }, 
                opts, 
                out IList<string> args,
                (optname) => throw new Exception($"unknow {optname}"));
            Assert.AreEqual(2, args.Count);
        }
        [TestMethod]
        public void OneOption()
        {
            string a = null;

            var opts = new BeeOptsBuilder()
                .Add('a', "add", OPTTYPE.VALUE, "add more args", (v) => a = v)
                .GetOpts();

            bool ok = BeeOpts.Parse(
                new string[] { "-a", "b" },
                opts,
                out IList<string> args,
                (optname) => throw new Exception($"unknow {optname}"));

            Assert.IsTrue(ok);
            Assert.AreEqual("b", a);
        }
        [TestMethod]
        public void TwoValueOptions_one_not_given()
        {
            string r = null;
            string o = null;

            var opts = new BeeOptsBuilder()
                .Add('r', "req", OPTTYPE.VALUE, "desc", (v) => r = v)
                .Add('o', "opt", OPTTYPE.VALUE, "desc", (v) => o = v)
                .GetOpts();

            bool ok = BeeOpts.Parse(
                new string[] { "-r", "r", "-o" },
                opts,
                out IList<string> args,
                (optname) => throw new Exception($"unknow {optname}"));

            Assert.IsTrue(ok);
            Assert.AreEqual("r", r);
            Assert.IsNull(o);
        }
        [TestMethod]
        public void TwoValueOptions_one_not_given_other_order()
        {
            string r = null;
            string o = null;

            var opts = new BeeOptsBuilder()
                .Add('r', "req", OPTTYPE.VALUE, "required", (v) => r = v)
                .Add('o', "opt", OPTTYPE.VALUE, "optional", (v) => o = v)
                .GetOpts();

            bool ok = BeeOpts.Parse(
                new string[] { "-o", "-r", "r" },
                opts,
                out IList<string> args,
                (optname) => throw new Exception($"unknow {optname}"));

            Assert.IsTrue(ok);
            Assert.AreEqual("r", r);
            Assert.IsNull(o);
        }
        [TestMethod]
        public void TwoValueOptions_one_has_no_value()
        {
            string r = null;
            string o = null;

            var opts = new BeeOptsBuilder()
                .Add('r', "req", OPTTYPE.VALUE, "desc", (v) => r = v)
                .Add('o', "opt", OPTTYPE.VALUE, "desc", (v) => o = v)
                .GetOpts();

            bool ok = BeeOpts.Parse(
                new string[] { "-o", "-r", "r" },
                opts,
                out IList<string> args,
                (optname) => throw new Exception($"unknow {optname}"));

            Assert.IsTrue(ok);
            Assert.AreEqual("r", r);
            Assert.IsNull(o);
        }
        [TestMethod]
        public void TwoOptions_bool_value()
        {
            string r = null;
            bool b = false;

            var opts = new BeeOptsBuilder()
                .Add('r', "req", OPTTYPE.VALUE, "desc", (v) => r = v)
                .Add('b', "boo", OPTTYPE.BOOL,  "desc", (v) => b = true)
                .GetOpts();

            bool ok = BeeOpts.Parse(
                new string[] { "-brR" },
                opts,
                out IList<string> args,
                (optname) => throw new Exception($"unknow {optname}"));

            Assert.IsTrue(ok);
            Assert.AreEqual("R", r);
            Assert.IsTrue(b);
        }
        [TestMethod]
        public void TwoOptions_bool_value_long_notation()
        {
            string r = null;
            bool b = false;

            var opts = new BeeOptsBuilder()
                .Add('r', "req", OPTTYPE.VALUE, "desc", (v) => r = v)
                .Add('b', "boo", OPTTYPE.BOOL, "desc", (v) => b = true)
                .GetOpts();

            bool ok = BeeOpts.Parse(
                new string[] { "--boo", "-rR" },
                opts,
                out IList<string> args,
                (optname) => throw new Exception($"unknow option [{optname}]"));

            Assert.IsTrue(ok);
            Assert.AreEqual("R", r);
            Assert.IsTrue(b);
        }
        [TestMethod]
        public void TwoOptions_long_short_with_value()
        {
            string r = null;
            string b = null;

            var opts = new BeeOptsBuilder()
                .Add('r', "req", OPTTYPE.VALUE, "desc", (v) => r = v)
                .Add('b', "boo", OPTTYPE.VALUE, "desc", (v) => b = v)
                .GetOpts();

            bool ok = BeeOpts.Parse(
                new string[] { "--boo=huu", "-r", "R" },
                opts,
                out IList<string> args,
                (optname) => throw new Exception($"unknow option [{optname}]"));

            Assert.IsTrue(ok);
            Assert.AreEqual("R", r);
            Assert.AreEqual("huu", b);
        }
        [TestMethod]
        public void TwoOptions_long_short_with_value_2()
        {
            string r = null;
            string b = null;

            var opts = new BeeOptsBuilder()
                .Add('r', "req", OPTTYPE.VALUE, "desc", (v) => r = v)
                .Add('b', "boo", OPTTYPE.VALUE, "desc", (v) => b = v)
                .GetOpts();

            bool ok = BeeOpts.Parse(
                new string[] { "--boo", "huu", "-r", "R" },
                opts,
                out IList<string> args,
                (optname) => throw new Exception($"unknow option [{optname}]"));

            Assert.IsTrue(ok);
            Assert.AreEqual("R", r);
            Assert.AreEqual("huu", b);
        }
    }
}
