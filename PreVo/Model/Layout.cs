using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreVo.Model
{
    public abstract class Layout
    {
        public abstract IEnumerable<Content> GetContents();
    }

    public class SingleLayout : Layout
    {
        public Content Main { get; set; }

        public override IEnumerable<Content> GetContents()
        {
            throw new NotImplementedException();
        }
    }

    public class VerticalDoubleLayout : Layout
    {
        public Content Top { get; set; }
        public Content Bottom { get; set; }

        public override IEnumerable<Content> GetContents()
        {
            throw new NotImplementedException();
        }
    }

    public class HorizontalDoubleLayout : Layout
    {
        public Content Left { get; set; }
        public Content Right { get; set; }

        public override IEnumerable<Content> GetContents()
        {
            throw new NotImplementedException();
        }
    }
}
