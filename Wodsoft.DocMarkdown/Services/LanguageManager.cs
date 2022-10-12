using System.Collections.ObjectModel;

namespace Wodsoft.DocMarkdown.Services
{
    public class LanguageManager
    {
        public LanguageManager(DocLanguage[] languages)
        {
            if (languages == null || languages.Length == 0)
            {
                IsEnabled = false;
            }
            else
            {
                IsEnabled = true;
                Languages = new ReadOnlyCollection<DocLanguage>(languages);
            }
        }

        public bool IsEnabled { get; }

        public IReadOnlyList<DocLanguage> Languages { get; }

        public DocLanguage Current { get; set; }
    }
}
