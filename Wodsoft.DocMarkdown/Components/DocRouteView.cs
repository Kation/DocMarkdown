using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Wodsoft.DocMarkdown.Services;

namespace Wodsoft.DocMarkdown.Components
{
#nullable disable warnings
    public class DocRouteView : IComponent
    {
        private RenderHandle _renderHandle;

        [Inject]
        private NavManager NavManaget { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private LanguageManager LanguageManager { get; set; }

        [Inject]
        private VersionManager VersionManager { get; set; }

        [Inject]
        private DocEngine DocEngine { get; set; }

        public void Attach(RenderHandle renderHandle)
        {
            _renderHandle = renderHandle;
            NavigationManager.LocationChanged += NavigationManager_LocationChanged;
        }

        private string _path;
        private async void NavigationManager_LocationChanged(object sender, LocationChangedEventArgs e)
        {
            var path = NavigationManager.ToBaseRelativePath(NavigationManager.Uri).Split('#')[0];
            if (path != _path)
            {
                _path = path;
                if (LanguageManager.IsEnabled)
                {
                    DocLanguage lang;
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        lang = LanguageManager.Languages[0];
                        _path = lang.Value;
                        NavigationManager.NavigateTo(lang.Value, false, true);
                    }
                    else
                    {
                        var langPath = path.Substring(0, path.IndexOf('/'));
                        if (langPath.Length == path.Length)
                            path = string.Empty;
                        else
                            path = path.Substring(langPath.Length + 1);
                        lang = LanguageManager.Languages.FirstOrDefault(t => t.Value.Equals(langPath, StringComparison.OrdinalIgnoreCase)) ?? LanguageManager.Languages[0];
                    }
                    if (lang != LanguageManager.Current)
                        LanguageManager.Current = lang;
                }
                path = path.Split('?')[0];
                await DocEngine.NavigateToAsync(_renderHandle, path.AsMemory());
            }
        }

        public Task SetParametersAsync(ParameterView parameters)
        {
            var path = NavigationManager.ToBaseRelativePath(NavigationManager.Uri).Split('#')[0];
            _path = path;
            path = path.Split('?')[0];
            //if (!path.StartsWith("/"))
            //    path = "/" + path;
            var pathMemory = path.AsMemory();
            if (LanguageManager.IsEnabled)
            {
                DocLanguage lang;
                if (string.IsNullOrWhiteSpace(path))
                {
                    lang = LanguageManager.Languages[0];
                    _path = lang.Value;
                    NavigationManager.NavigateTo(lang.Value, false, true);
                }
                else
                {
                    string langPath;
                    if (path.Contains('/'))
                    {
                        langPath = new string(pathMemory.Slice(0, path.IndexOf('/')).Span);
                        pathMemory = pathMemory.Slice(langPath.Length + 1);
                    }
                    else
                    {
                        langPath = path;
                        pathMemory = ReadOnlyMemory<char>.Empty;
                    }
                    lang = LanguageManager.Languages.FirstOrDefault(t => t.Value.Equals(langPath, StringComparison.OrdinalIgnoreCase)) ?? LanguageManager.Languages[0];
                }
                LanguageManager.Current = lang;
            }
            DocVersion version;
            if (VersionManager.IsEnabled)
            {
                string versionValue;
                if (NavigationManager.Uri.Contains("?"))
                {
                    var values = System.Web.HttpUtility.ParseQueryString(NavigationManager.Uri.Split('?')[1]);
                    versionValue = values["version"];
                }
                else
                    versionValue = null;
                if (string.IsNullOrWhiteSpace(versionValue))
                {
                    version = VersionManager.Versions[0];
                }
                else
                {
                    var item = VersionManager.Versions.FirstOrDefault(t => t.Value.Equals(versionValue, StringComparison.OrdinalIgnoreCase));
                    version = item ?? VersionManager.Versions[0];
                }
            }
            else
            {
                version = null;
            }
            VersionManager.Current = version;
            return DocEngine.RenderAsync(_renderHandle, pathMemory);
        }
    }
}
