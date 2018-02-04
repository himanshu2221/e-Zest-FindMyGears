using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindMyGears.Model
{
    [Serializable]
    public class UserProfile
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public long Contact { get; set; }
    }
}