using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Services;

namespace Wodsoft.DocMarkdown.Renderers
{
    public interface IMarkdownRenderContext
    {
        CatalogManager CatalogManager { get; }

        DocConfig DocConfig { get; }

        RenderFragment RenderInline(ContainerInline containerInline);

        RenderFragment RenderBlock(ContainerBlock containerBlock);

        string GetRelativePath(string path);
    }
}
