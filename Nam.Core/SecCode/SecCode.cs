using System;
using System.Collections.Generic;
using System.Text;

namespace Nam.Core.SecCode
{
    public class SecCode
    {
        public int Id { get; set; }
        public string Controller { get; set; }
        public string Function { get; set; }
        public string Code { get; set; }

        public class ListSecCode
        {
            public SecCode SecCode { get; set; }
        }
    }
}
