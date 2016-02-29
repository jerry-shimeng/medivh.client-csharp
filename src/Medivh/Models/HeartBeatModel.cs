using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medivh.Models
{
    public class HeartBeatModel
    {
        /// <summary>
        /// 标示
        /// </summary>
        public string Mark { get; set; }

        public Func<string> Action { get; set; }

        public int Level { get; set; }

        public string Alias { get; set; }
        public List<string> Other { get; set; }
    }
}
