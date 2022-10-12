namespace Wodsoft.DocMarkdown
{
    public class DocConfig
    {
        public string Title { get; set; }

        public string Icon { get; set; }

        public string BaseUrl { get; set; }

        public string Path { get; set; }

        public bool IsRawHtmlEnabled { get; set; }

        public DocLanguage[] Languages { get; set; }

        public DocVersion[] Versions { get; set; }
    }

    public class DocVersion
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string Path { get; set; }
    }

    public class DocLanguage
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string CatalogText { get; set; }
    }
}
