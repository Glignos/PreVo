using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreVo.Model
{
    public class SampleDataProvider
    {
        public static Dictionary<String, Presentation> Repo;

        public static void GeneratePresentations()
        {
            if (Repo != null) return;

            Repo = new Dictionary<string, Presentation>();



            // Presentation 1
            Presentation presentation1 = new Presentation();

            // Slide 1 - Single
            var layout1 = new SingleLayout();
            var text1 = new TextContent();
            text1.Text = "Founded by Enzo Ferrari in 1929, as Scuderia Ferrari, the company sponsored drivers";
            text1.Description = "Text1";
            
            layout1.Main = text1;
            var slide1 = new Slide(layout1);

            slide1.Description = "introduction";
            slide1.SlidePage = typeof(SamplePresentations.A1);
            presentation1.Slides.Add(1, slide1);


            // Slide 2 - Double Horizontal
            var layout2 = new HorizontalDoubleLayout();
            var text2 = new TextContent();
            text2.Text = "Before moving into production of street-legal vehicles in 1947. Fiat acquired 50% of Ferrari in 1969 and expanded its stake to 90% in 1988.[4] Ferrari is the world's most powerful brand according to Brand Finance.[5] In May 2012 the 1962 Ferrari 250 GTO became the most expensive car in history,";
            text2.Description = "Text2";
            layout2.Left = text2;

            var text3 = new TextContent();
            text3.Text = "Selling in a private transaction for $38,115,000 to American communications magnate Craig McCaw.[6] In 2014 Fiat announced its intentions to sell a portion of its share in Ferrari; as of the announcement Fiat owned 90% of Ferrari.[7][8][9] In July 2015, it was announced that 10% of the company would be offered up ";
            text3.Description = "Text3";
            layout2.Right = text3;

            var slide2 = new Slide(layout2);
            slide2.Description = "sales data";
            slide2.SlidePage = typeof(SamplePresentations.A2);
            presentation1.Slides.Add(2, slide2);

            
            // Slide 3 - Double Vertical
            var layout3 = new VerticalDoubleLayout();
            var text4 = new TextContent();
            text4.Text = "Before moving into production of street-legal vehicles in 1947. Fiat acquired 50% of Ferrari in 1969 and expanded its stake to 90% in 1988.[4] Ferrari is the world's most powerful brand according to Brand Finance.[5] In May 2012 the 1962 Ferrari 250 GTO became the most expensive car in history,";
            text4.Description = "Text4";
            layout3.Top = text4;

            var text5 = new TextContent();
            text5.Text = "Selling in a private transaction for $38,115,000 to American communications magnate Craig McCaw.[6] In 2014 Fiat announced its intentions to sell a portion of its share in Ferrari; as of the announcement Fiat owned 90% of Ferrari.[7][8][9] In July 2015, it was announced that 10% of the company would be offered up ";
            text5.Description = "Text5";
            layout3.Bottom = text5;

            var slide3 = new Slide(layout3);
            slide3.Description = "client status";
            slide3.SlidePage = typeof(SamplePresentations.A3);
            presentation1.Slides.Add(3, slide3);

            Repo.Add("A", presentation1);





            // Presentation 2
            Presentation p2 = new Presentation();

            ImageContent i1 = new ImageContent();
            i1.Path = "Photo1";
            i1.Description = "First Image";
            TextContent c6 = new TextContent();
            c6.Text = "Before moving into production of street-legal vehicles in 1947. Fiat acquired 50% of Ferrari in 1969 and expanded its stake to 90% in 1988.[4] Ferrari is the world's most powerful brand according to Brand Finance.[5] In May 2012 the 1962 Ferrari 250 GTO became the most expensive car in history,";
            c6.Description = "Tex6";
            var layout4 = new SingleLayout();
            layout4.Main = c6;
            var slide4 = new Slide(layout4);
            slide4.Description = "first picture";
            slide4.SlidePage = typeof(SamplePresentations.B1);
            p2.Slides.Add(1, slide4);

            ImageContent i2 = new ImageContent();
            i2.Path = "Photo2";
            i2.Description = "Second Image";
            TextContent c7 = new TextContent();
            c7.Text = "Selling in a private transaction for $38,115,000 to American communications magnate Craig McCaw.[6] In 2014 Fiat announced its intentions to sell a portion of its share in Ferrari; as of the announcement Fiat owned 90% of Ferrari.[7][8][9] In July 2015, it was announced that 10% of the company would be offered up ";
            c7.Description = "Text5";
            var layout5 = new HorizontalDoubleLayout();
            layout5.Left = i2;
            layout5.Right = c7;
            var slide5 = new Slide(layout5);
            slide5.Description = "second picture and comments";
            slide5.SlidePage = typeof(SamplePresentations.B2);
            p2.Slides.Add(2, slide5);


            ImageContent i3 = new ImageContent();
            i3.Path = "Photo2";
            i3.Description = "Second Image";
            TextContent c8 = new TextContent();
            c8.Text = "Selling in a private transaction for $38,115,000 to American communications magnate Craig McCaw.[6] In 2014 Fiat announced its intentions to sell a portion of its share in Ferrari; as of the announcement Fiat owned 90% of Ferrari.[7][8][9] In July 2015, it was announced that 10% of the company would be offered up ";
            c8.Description = "Text8";
            var layout6 = new VerticalDoubleLayout();
            layout3.Bottom = i3;
            layout3.Top = c8;
            var slide6 = new Slide(layout6);
            slide6.Description = "third picture and conclusion";
            slide6.SlidePage = typeof(SamplePresentations.B3);
            p2.Slides.Add(3, slide6);

            Repo.Add("B", p2);
        }
    }
}
