using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class ListItemBlockRenderer : MarkdownRenderer<ListItemBlock>
    {
        protected override object Render(IMarkdownRenderContext context, ListItemBlock markdown)
        {
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<ListItemBlockView>(0);
                builder.AddAttribute(1, nameof(BlockView.Content), context.RenderBlock(markdown));
                builder.CloseComponent();
            });
        }
    }
}
