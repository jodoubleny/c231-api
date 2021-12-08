using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace c231_qrder.Services
{
    public class ServiceBase
    {
        public string GetGuidAsStr()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

    }
}
