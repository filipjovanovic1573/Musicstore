using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Musicstore.Other {
    public class ApiKey {
        public static string GetKey() {
            string s = Path.GetRandomFileName();
            s = s.Replace(".", "");
            return s;
        }
    }
}