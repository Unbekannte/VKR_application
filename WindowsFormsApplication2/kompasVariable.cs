using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKR_FlangeCoupling
{
    class kompasVariable
    {
        public string displayName { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        public string value { get; set; }
    }

    class userVariable : kompasVariable
    {
        public string DisplayName { get; set; }
        private string Name;
        public string Note { get; set; }
        public double Value { get; set; }
    }
}
