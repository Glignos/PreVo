using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreVo.Model
{
    public class Presentation
    {
        public Dictionary<int, Slide> Slides { get; set; }

        public Presentation()
        {
            Slides = new Dictionary<int, Slide>();
        }
    }
}
