using System.Collections.Generic;

namespace PreVo.Model
{
    public class VocabularyBuilder
    {
        public static Dictionary<string, IPresentable> BuildVocabulary(Presentation presentation)
        {
            Dictionary<string, IPresentable> res = new Dictionary<string, IPresentable>();
            foreach (var slide in presentation.Slides.Values)
            {
                res.Add(slide.Description, slide);
                foreach (var content in slide.SlideLayout.GetContents())
                {
                    res.Add(content.Description, content);
                }
            }
            return res;
        }
    }
}
