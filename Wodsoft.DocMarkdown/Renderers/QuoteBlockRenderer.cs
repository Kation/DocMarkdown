using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class QuoteBlockRenderer : MarkdownRenderer<QuoteBlock>
    {
        protected override object Render(IMarkdownRenderContext context, QuoteBlock markdown)
        {
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<QuoteBlockView>(0);
                builder.AddAttribute(1, nameof(BlockView.Content), context.RenderBlock(markdown));
                builder.CloseComponent();
            });
        }
    }
}
