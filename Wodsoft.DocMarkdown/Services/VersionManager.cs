using System.Collections.ObjectModel;

namespace Wodsoft.DocMarkdown.Services
{
    public class VersionManager
    {
        public VersionManager(DocVersion[] versions)
        {
            if (versions == null || versions.Length == 0)
            {
                IsEnabled = false;
            }
            else
            {
                Versions = new ReadOnlyCollection<DocVersion>(versions);
                IsEnabled = true;
            }
        }

        public bool IsEnabled { get; set; }

        public IReadOnlyList<DocVersion> Versions { get; set; }

        public DocVersion Current { get; set; }
    }
}
