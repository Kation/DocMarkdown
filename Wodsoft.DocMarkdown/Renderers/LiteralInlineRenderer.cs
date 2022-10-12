using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class LiteralInlineRenderer : MarkdownRenderer<LiteralInline>
    {
        protected override object Render(IMarkdownRenderContext context, LiteralInline markdown)
        {
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<LiteralInlineView>(0);
                builder.AddAttribute(1, nameof(InlineView.Content), markdown.Content.ToString());
                builder.CloseComponent();
            });
        }
    }
}
