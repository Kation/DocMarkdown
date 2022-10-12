using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class ListBlockRenderer : MarkdownRenderer<ListBlock>
    {
        protected override object Render(IMarkdownRenderContext context, ListBlock markdown)
        {
            return new RenderFragment(builder =>
            {
                if (markdown.BulletType == '1')
                {
                    builder.OpenComponent<OrderedListBlockView>(0);
                    builder.AddAttribute(1, nameof(OrderedListBlockView.Start), markdown.OrderedStart);
                    builder.AddAttribute(2, nameof(BlockView.Content), context.RenderBlock(markdown));
                    builder.CloseComponent();
                }
                else
                {
                    builder.OpenComponent<UnorderedListBlockView>(0);
                    builder.AddAttribute(1, nameof(BlockView.Content), context.RenderBlock(markdown));
                    builder.CloseComponent();
                }
            });
        }
    }
}
