using System;
using System.Collections.Generic;
using System.Text;

namespace Flotix2021.Helpers
{
    public class OauthToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public string scope { get; set; }
    }
}
