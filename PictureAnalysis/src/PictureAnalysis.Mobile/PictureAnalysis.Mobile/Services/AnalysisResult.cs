using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureAnalysis.Mobile.Services
{

    public class Metadata
    {
        public int height { get; set; }
        public int width { get; set; }
        public string format { get; set; }
    }

    public class ImageType
    {
        public int clipArtType { get; set; }
        public int lineDrawingType { get; set; }
    }

    public class Color
    {
        public string AccentColor { get; set; }
        public string DominantColorForeground { get; set; }
        public string DominantColorBackground { get; set; }
        public IList<string> DominantColors { get; set; }
        public bool IsBWImg { get; set; }
    }

    public class Adult
    {
        public bool IsAdultContent { get; set; }
        public bool IsRacyContent { get; set; }
        public double AdultScore { get; set; }
        public double RacyScore { get; set; }
    }

    public class Category
    {
        public object Detail { get; set; }
        public string Name { get; set; }
        public double Score { get; set; }
    }

    public class Tag
    {
        public string Name { get; set; }
        public double Confidence { get; set; }
        public object Hint { get; set; }
    }

    public class Caption
    {
        public string Text { get; set; }
        public double Confidence { get; set; }
    }

    public class Description
    {
        public IList<string> Tags { get; set; }
        public IList<Caption> Captions { get; set; }
    }

    public class AnalysisResult
    {
        public string RequestId { get; set; }
        public Metadata Metadata { get; set; }
        public ImageType ImageType { get; set; }
        public Color Color { get; set; }
        public Adult Adult { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<object> Faces { get; set; }
        public IList<Tag> Tags { get; set; }
        public Description Description { get; set; }
    }

}
