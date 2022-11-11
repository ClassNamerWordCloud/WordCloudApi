namespace WordCloudApi.Models
{
    public class Filter
    {
        public Filter(string tag, string attribute, string value, string splitTag)
        {
            Tag = tag;
            Attribute = attribute;
            Value = value;
            SplitTag = splitTag;
        }

        public string Tag { get; set; }
        public string Attribute { get; set; }
        public string Value { get; set; }
        public string SplitTag { get; set; }

    }
}
