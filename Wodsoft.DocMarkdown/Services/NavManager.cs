using System.Collections.ObjectModel;
using System.Net.Http.Json;

namespace Wodsoft.DocMarkdown.Services
{
    public class NavManager
    {
        private readonly HttpClient _httpClient;
        private readonly string _path;

        public NavManager(HttpClient httpClient, string path)
        {
            _httpClient = httpClient;
            _path = path;
        }

        public NavModel Navs { get; private set; }

        public NavItem Current { get; private set; }

        public async Task LoadAsync(ReadOnlyMemory<char> lang, ReadOnlyMemory<char> version)
        {
            string path;
            if (version.Length == 0)
            {
                path = string.Empty;
            }
            else
            {
                path = new string(version.Span) + "/";
            }
            if (!string.IsNullOrEmpty(path))
                path += _path + "/";
            if (lang.Length == 0)
                path += "nav.json";
            else
                path += "nav." + new string(lang.Span) + ".json";
            var navResponse = await _httpClient.GetAsync(path);
            if (navResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Current = null;
                Navs = null;
                return;
            }
            var navs = await navResponse.Content.ReadFromJsonAsync<Dictionary<string, NavConfig>>(new System.Text.Json.JsonSerializerOptions
            {
                TypeInfoResolver = SourceGenerationContext.Default
            });
            List<NavItem> allItems = new List<NavItem>();
            List<NavItem> items = new List<NavItem>();
            foreach (var nav in navs!)
            {
                items.Add(Convert(nav.Key, nav.Value, allItems, null));
            }
            Navs = new NavModel(new ReadOnlyCollection<NavItem>(items), new ReadOnlyCollection<NavItem>(allItems));
            Current = null;
        }

        public bool NavigateTo(string path)
        {
            if (Navs == null)
                return false;
            var nav = Navs.AllItems.FirstOrDefault(t => t.Path != null && t.Path.Equals(path, StringComparison.OrdinalIgnoreCase));
            if (nav == null)
                return false;
            Current = nav;
            return true;
        }

        private NavItem Convert(string name, NavConfig config, List<NavItem> items, NavItem parent)
        {
            var item = new NavItem(name, config.Path, parent);
            items.Add(item);
            if (config.Children != null && config.Children.Count > 0)
                foreach (var child in config.Children)
                    Convert(child.Key, child.Value, items, item);
            return item;
        }
    }
}
