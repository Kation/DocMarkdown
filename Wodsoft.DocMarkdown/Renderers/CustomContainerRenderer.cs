using Markdig.Extensions.CustomContainers;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class CustomContainerRenderer : MarkdownRenderer<CustomContainer>
    {
        protected override object Render(IMarkdownRenderContext context, CustomContainer markdown)
        {
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<CustomContainerView>(0);
                builder.AddAttribute(1, nameof(CustomContainerView.Type), markdown.Info);
                builder.AddAttribute(2, nameof(CustomContainerView.Content), context.RenderBlock(markdown));
                builder.CloseComponent();
            });
        }
    }
}
