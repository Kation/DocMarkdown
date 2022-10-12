namespace Wodsoft.DocMarkdown.Renderers
{
    public class HtmlElement
    {
        public string Tag { get; set; }

        public bool IsEnd { get; set; }

        public bool IsSelfClose { get; set; }

        public Dictionary<string, string> Attributes { get; set; }
    }
}
