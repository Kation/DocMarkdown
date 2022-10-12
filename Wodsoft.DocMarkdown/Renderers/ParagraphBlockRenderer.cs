using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class ParagraphBlockRenderer : MarkdownRenderer<ParagraphBlock>
    {
        protected override object Render(IMarkdownRenderContext context, ParagraphBlock markdown)
        {
            if (markdown.Inline == null)
                return new RenderFragment(builder => builder.AddContent(1, string.Empty));
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<ParagraphBlockView>(0);
                builder.AddAttribute(1, nameof(BlockView.Content), context.RenderInline(markdown.Inline));
                builder.CloseComponent();
            });
        }
    }
}
