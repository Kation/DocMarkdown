using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class LineBreakInlineRenderer : MarkdownRenderer<LineBreakInline>
    {
        protected override object Render(IMarkdownRenderContext context, LineBreakInline markdown)
        {
            return new MarkupString("<br/>");
        }
    }
}
