using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class ThematicBreakBlockRenderer : MarkdownRenderer<ThematicBreakBlock>
    {
        protected override object Render(IMarkdownRenderContext context, ThematicBreakBlock markdown)
        {
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<ThematicBreakBlockView>(0);
                builder.AddAttribute(1, nameof(BlockView.Content), new RenderFragment(content =>
                {
                    content.AddContent(0, markdown.Content.ToString());
                }));
                builder.CloseComponent();
            });
        }
    }
}
