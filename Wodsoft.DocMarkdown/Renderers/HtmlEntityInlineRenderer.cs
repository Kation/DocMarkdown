using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class HtmlEntityInlineRenderer : MarkdownRenderer<HtmlEntityInline>
    {
        protected override object Render(IMarkdownRenderContext context, HtmlEntityInline markdown)
        {
            return markdown.Transcoded.ToString();
        }
    }
}
