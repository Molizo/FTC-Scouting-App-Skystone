using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkystoneScouting.Services
{
    public class AuthMessageSenderOptions
    {
        #region Public Properties

        public string SendGridKey { get; set; }
        public string SendGridUser { get; set; }

        #endregion Public Properties
    }
}