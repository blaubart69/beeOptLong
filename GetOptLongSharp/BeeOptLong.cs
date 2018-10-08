using System;
using System.Collections.Generic;

namespace BeeOptLong
{
    public class BeeOpts
    {
        public readonly char   opt;
        public readonly string optLong;
        public readonly string desc;
        public readonly Action<char,string> OnOption;

        public BeeOpts(char opt, string optLong, string desc, Action<char, string> OnOption)
        {
            this.opt = opt;
            this.optLong = optLong;
            this.desc = desc;
            this.OnOption = OnOption;
        }
    }
    public class BeeOptsBuilder
    {
        private IList<BeeOpts> _data = new List<BeeOpts>();

        public BeeOptsBuilder Add(char opt, string optLong, string desc, Action<char, string> OnOption)
        {
            _data.Add(new BeeOpts(opt, optLong, desc, OnOption));
            return this;
        }
        public IList<BeeOpts> GetOpts()
        {
            return _data;
        }
    }

    public class BeeOptsParser
    {
        public static IList<string> Parse(string[] args, IList<BeeOpts> opts)
        {
            IList<string> parsedArgs = new List<string>();

            int i = 0;
            while (i < args.Length )
            {
                if ( )

                i++;
            }


            return parsedArgs;
        }
    }
}