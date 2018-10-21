using System;
using System.Linq;
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

            IList<string> args = BeeOpts.Parse(
                new string[] { "a", "b" }, 
                opts, 
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

            IList<string> args = BeeOpts.Parse(
                new string[] { "-a", "b" },
                opts,
                (optname) => throw new Exception($"unknow {optname}"));

            Assert.AreEqual("b", a);
        }
        [TestMethod]
        public void OneOptionWithoutSpace()
        {
            string a = null;

            var opts = new BeeOptsBuilder()
                .Add('a', "add", OPTTYPE.VALUE, "add more args", (v) => a = v)
                .GetOpts();

            IList<string> args = BeeOpts.Parse(
                new string[] { "-ab" },
                opts,
                (optname) => throw new Exception($"unknow {optname}"));

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

            IList<string> args = BeeOpts.Parse(
                new string[] { "-r", "r", "-o" },
                opts,
                (optname) => throw new Exception($"unknow {optname}"));

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

            IList<string> args = BeeOpts.Parse(
                new string[] { "-o", "-r", "r" },
                opts,
                (optname) => throw new Exception($"unknow {optname}"));

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

            IList<string> args = BeeOpts.Parse(
                new string[] { "-o", "-r", "r" },
                opts,
                (optname) => throw new Exception($"unknow {optname}"));

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

            IList<string> args = BeeOpts.Parse(
                new string[] { "-brR" },
                opts,
                (optname) => throw new Exception($"unknow {optname}"));

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

            IList<string> args = BeeOpts.Parse(
                new string[] { "--boo", "-rR" },
                opts,
                (optname) => throw new Exception($"unknow option [{optname}]"));

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

            IList<string> args = BeeOpts.Parse(
                new string[] { "--boo=huu", "-r", "R" },
                opts,
                (optname) => throw new Exception($"unknow option [{optname}]"));

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

            IList<string> args = BeeOpts.Parse(
                new string[] { "--boo", "huu", "-r", "R" },
                opts,
                (optname) => throw new Exception($"unknow option [{optname}]"));

            Assert.AreEqual("R", r);
            Assert.AreEqual("huu", b);
        }
        [TestMethod]
        public void TwoOptions_long_short_with_value_and_one_argument()
        {
            string r = null;
            string b = null;

            var opts = new BeeOptsBuilder()
                .Add('r', "req", OPTTYPE.VALUE, "desc", (v) => r = v)
                .Add('b', "boo", OPTTYPE.VALUE, "desc", (v) => b = v)
                .GetOpts();

            IList<string> args = BeeOpts.Parse(
                new string[] { "--boo", "huu", "und", "-r", "R" },
                opts,
                (optname) => throw new Exception($"unknow option [{optname}]"));

            Assert.AreEqual("R", r);
            Assert.AreEqual("huu", b);
            Assert.AreEqual("und", args.First());
        }
        [TestMethod]
        public void TwoOptions_long_short_with_value_and_one_argument_3()
        {
            string r = "n/a";
            string b = null;

            var opts = new BeeOptsBuilder()
                .Add('r', "req", OPTTYPE.VALUE, "desc", (v) => r = v)
                .Add('b', "boo", OPTTYPE.VALUE, "desc", (v) => b = v)
                .GetOpts();

            IList<string> args = BeeOpts.Parse(
                new string[] { "--boo", "huu", "und", "-r" },
                opts,
                (optname) => throw new Exception($"unknow option [{optname}]"));

            Assert.IsNull(r);
            Assert.AreEqual("huu", b);
            Assert.AreEqual("und", args.First());
        }
    }
}
