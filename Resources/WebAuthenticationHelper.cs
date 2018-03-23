using NSerio.Relativity.WebAuthentication;
using Relativity.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resources
{
    public class WebAuthenticationHelper : IWebAuthenticationHelper
    {
        public Func<int, IDBContext> GetDBContext { get; set; }

        public Uri RelativityUrl { get; set; }

        public int UserArtifactID { get; set; }
    }
}
