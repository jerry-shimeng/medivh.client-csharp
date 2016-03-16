using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medivh.Config;

namespace Medivh.Common
{
    internal class ConfigHelper
    {
        private static MedivhConfig config = null;
        internal static void SetMedivhConfig(MedivhConfig c)
        {
            config = c;
        }

        internal static MedivhConfig GetMedivhConfig()
        {
            return config;
        }
    }
}
