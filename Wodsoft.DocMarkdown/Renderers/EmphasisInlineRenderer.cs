using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class EmphasisInlineRenderer : MarkdownRenderer<EmphasisInline>
    {
        protected override object Render(IMarkdownRenderContext context, EmphasisInline markdown)
        {
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<EmphasisInlineView>(0);
                builder.AddAttribute(1, nameof(EmphasisInlineView.IsDouble), markdown.DelimiterCount == 2);
                builder.AddAttribute(2, nameof(BlockView.Content), context.RenderInline(markdown));
                builder.CloseComponent();
            });
        }
    }
}
