using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class CodeInlineRenderer : MarkdownRenderer<CodeInline>
    {
        protected override object Render(IMarkdownRenderContext context, CodeInline markdown)
        {
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<CodeInlineView>(0);
                builder.AddAttribute(1, nameof(InlineView.Content), markdown.Content);
                builder.CloseComponent();
            });
        }
    }
}
