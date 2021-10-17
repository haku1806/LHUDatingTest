using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestLHUDatingCore.Ulti
{
    public class Constrants
    {
        public class JwtConfig
        {
            public const string SecretKey = "40om7p4gxqoctr4fgdsir6u3ec1tpf33vzr0ojegkdkllmq1cn36k571e2n7modjy";
            public const int ExpirationInMinutes = 14400;
        }

        public class Encrypt
        {
            public const string DefautEncrypt = "LHUDating_";
        }
    }
}
