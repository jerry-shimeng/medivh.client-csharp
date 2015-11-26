using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medivh.Models;

namespace Medivh.Config
{
    public class MedivhConfig
    {
        public ClientInfo Client { get; set; }
        public string ServerIp { get; set; }
        public int ServerPort { get; set; }
    }
}
