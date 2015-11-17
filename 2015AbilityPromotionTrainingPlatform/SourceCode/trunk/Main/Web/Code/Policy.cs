using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCS.Model
{
    [Serializable]
    public class Policy
    {
        public string scope;
        public string deadline;
        public string returnBody;
        public int overwrite;
        public long fsizeLimit;
        public string returnUrl;
    }
}
