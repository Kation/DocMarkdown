using Blazorise;
using Blazorise.Extensions;
using Markdig;
using Markdig.Extensions.Tables;
using Markdig.Helpers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Globalization;
using System.Net.Http.Json;
using System.Reflection;
using System.Reflection.Metadata;
using Wodsoft.DocMarkdown.Components;
using Wodsoft.DocMarkdown.Pages;
using Wodsoft.DocMarkdown.Renderers;
using Wodsoft.DocMarkdown.Shared;
using Index = Wodsoft.DocMarkdown.Pages.Index;

namespace Wodsoft.DocMarkdown.Services
{
    public class DocEngine : IMarkdownRenderContext
    {
        private readonly NavManager _navManager;
        private readonly LanguageManager _languageManager;
        private readonly VersionManager _versionManager;
        private readonly CatalogManager _catelogManager;
        private readonly DocConfig _config;
        private readonly HttpClient _httpClient;
        private string _currentPath;

        public DocEngine(NavManager navManager, LanguageManager languageManager, VersionManager versionManager, CatalogManager catalogManager, DocConfig config, HttpClient httpClient)
        {
            _navManager = navManager;
            _languageManager = languageManager;
            _versionManager = versionManager;
            _catelogManager = catalogManager;
            _config = config;
            _httpClient = httpClient;
            Title = config.Title;
            Icon = config.Icon;

        }

        public string Title { get; }

        public string Icon { get; }

        public CatalogManager CatalogManager => _catelogManager;

        public DocConfig DocConfig => _config;

        public async Task RenderAsync(RenderHandle renderHandle, ReadOnlyMemory<char> path)
        {
            try
            {
                await _navManager.LoadAsync(_languageManager.Current?.Value.AsMemory() ?? ReadOnlyMemory<char>.Empty, _versionManager.Current?.Path.AsMemory() ?? ReadOnlyMemory<char>.Empty);
            }
            catch (Exception ex)
            {
                RenderError(renderHandle, ex.Message);
                return;
            }
            await NavigateToAsync(renderHandle, path);
        }

        public async Task NavigateToAsync(RenderHandle renderHandle, ReadOnlyMemory<char> path)
        {
            string realPath;
            if (path.IsEmpty)
                realPath = "index";
            else if (path.Span[path.Length - 1] == '/')
                realPath = new string(path.Span) + "index";
            else
                realPath = new string(path.Span);
            if (!_navManager.NavigateTo(realPath))
            {
                RenderError(renderHandle, "页面不存在。");
                return;
            }
            if (_languageManager.Current != null)
                realPath += "." + _languageManager.Current.Value;
            realPath += ".md";
            string md;
            try
            {
                if (!string.IsNullOrEmpty(_config.Path))
                    realPath = _config.Path + "/" + realPath;
                if (_versionManager.IsEnabled)
                    realPath = _versionManager.Current.Path + "/" + realPath;
                _currentPath = realPath;
                var mdResponse = await _httpClient.GetAsync(realPath);
                if (mdResponse.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    RenderError(renderHandle, "页面不存在。");
                    return;
                }
                md = await mdResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                RenderError(renderHandle, ex.Message);
                return;
            }
            _catelogManager.Clear();
            var pipelineBuilder = new MarkdownPipelineBuilder();
            pipelineBuilder.EnableTrackTrivia().UsePipeTables().UseCustomContainers();
            var pipeline = pipelineBuilder.Build();
            RenderMarkdown(renderHandle, Markdown.Parse(md, pipeline));
        }

        private void RenderError(RenderHandle renderHandle, string message)
        {
            renderHandle.Render(builder =>
            {
                builder.OpenComponent<LayoutView>(0);
                builder.AddAttribute(1, nameof(LayoutView.Layout), typeof(MainLayout));
                builder.AddAttribute(2, nameof(LayoutView.ChildContent), (RenderFragment)(child =>
                {
                    child.OpenComponent<Error>(0);
                    child.AddAttribute(1, "Message", message);
                    child.CloseComponent();
                }));
                builder.CloseComponent();
            });
        }

        private void RenderMarkdown(RenderHandle renderHandle, MarkdownDocument document)
        {
            var content = RenderBlock(document);
            renderHandle.Render(builder =>
            {
                builder.OpenComponent<LayoutView>(0);
                builder.AddAttribute(1, nameof(LayoutView.Layout), typeof(MainLayout));
                builder.AddAttribute(2, nameof(LayoutView.ChildContent), (RenderFragment)(child =>
                {
                    child.OpenComponent<Index>(0);
                    child.AddAttribute(1, "Content", content);
                    child.CloseComponent();
                }));
                builder.CloseComponent();
            });
        }

        public RenderFragment RenderBlock(ContainerBlock containerBlock)
        {
            return new RenderFragment(builder =>
            {
                int i = 0;
                foreach (var block in containerBlock)
                {
                    var obj = Render(block);
                    if (obj is RenderFragment fragment)
                        builder.AddContent(i, fragment);
                    else if (obj is MarkupString markup)
                        builder.AddContent(i, markup);
                    else if (obj is HtmlElement html)
                    {
                        if (html.IsEnd)
                            builder.CloseComponent();
                        else
                        {
                            builder.OpenElement(i, html.Tag);
                            i++;
                            if (html.Attributes != null)
                            {
                                foreach (var attr in html.Attributes)
                                {
                                    if (attr.Value == null)
                                        builder.AddAttribute(i, attr.Key);
                                    else
                                        builder.AddAttribute(i, attr.Key, attr.Value);
                                    i++;
                                }
                            }
                            if (html.IsSelfClose)
                                builder.CloseElement();
                        }
                    }
                    else
                        builder.AddContent(i, obj);
                    i++;
                }
            });
        }

        private List<MarkdownRenderer> _renderers = new List<MarkdownRenderer>();
        private object Render(MarkdownObject markdownObject)
        {
            foreach(var renderer in _renderers)
            {
                if (renderer.CanRender(markdownObject))
                {
                    return renderer.Render(this, markdownObject);
                }
            }
            return new RenderFragment(builder => builder.AddContent(1, $"不支持的Markdown类型“{markdownObject.GetType().Name}”。"));
        }

        public RenderFragment RenderInline(ContainerInline containerInline)
        {
            return new RenderFragment(content =>
            {
                var inline = containerInline.FirstChild;
                int i = 0;
                while (inline != null)
                {
                    var obj = Render(inline);
                    if (obj is RenderFragment fragment)
                        content.AddContent(i, fragment);
                    else if (obj is MarkupString markup)
                        content.AddContent(i, markup);
                    else if (obj is HtmlElement html)
                    {
                        if (html.IsEnd)
                            content.CloseComponent();
                        else
                        {
                            content.OpenElement(i, html.Tag);
                            i++;
                            if (html.Attributes != null)
                            {
                                foreach (var attr in html.Attributes)
                                {
                                    if (attr.Value == null)
                                        content.AddAttribute(i, attr.Key);
                                    else
                                        content.AddAttribute(i, attr.Key, attr.Value);
                                    i++;
                                }
                            }
                            if (html.IsSelfClose)
                                content.CloseElement();
                        }
                    }
                    else
                        content.AddContent(i, obj);
                    inline = inline.NextSibling;
                    i++;
                }
            });
        }

        public void AddRenderer(MarkdownRenderer renderer)
        {
            _renderers.Add(renderer);
        }

        public string GetRelativePath(string path)
        {
            string prefix;
            List<string> paths;
            if (_currentPath.StartsWith('/'))
            {
                prefix = string.Empty;
                paths = new List<string>(_currentPath.Split('/', StringSplitOptions.RemoveEmptyEntries));
            }
            else
            {
                var uri = new Uri(path);
                prefix = uri.Scheme + "://" +  uri.Host;
                if (!uri.IsDefaultPort)
                    prefix += ":" + uri.Port;
                paths = new List<string>(uri.PathAndQuery.Split('/', StringSplitOptions.RemoveEmptyEntries));
            }
            var targetPaths = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < targetPaths.Length; i++)
            {
                if (targetPaths[i] == "..")
                {
                    if (paths.Count > 0)
                        paths.RemoveAt(paths.Count - 1);
                }
                paths.Add(targetPaths[i]);
            }
            return prefix + string.Join('/', paths);
        }
    }
}
