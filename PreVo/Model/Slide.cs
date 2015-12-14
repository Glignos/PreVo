using System;
using System.Collections.Generic;

namespace PreVo.Model
{
    public class Slide : IVocable, IPresentable
    {
        public Layout SlideLayout { get; set; }
        public String Description { get; set; }
        public Type SlidePage { get; set; }


        public Slide(Layout layout)
        {
            SlideLayout = layout;
        }
    }

}