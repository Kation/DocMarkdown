using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class LinkInlineRenderer : MarkdownRenderer<LinkInline>
    {
        protected override object Render(IMarkdownRenderContext context, LinkInline markdown)
        {
            return new RenderFragment(builder =>
            {
                if (markdown.IsImage)
                {
                    builder.OpenComponent<ImageInlineView>(0);
                    builder.AddAttribute(1, nameof(LinkInlineView.Url), context.GetRelativePath(markdown.Url));
                    builder.AddAttribute(2, nameof(LinkInlineView.Title), markdown.Title);
                    builder.CloseComponent();
                }
                else
                {
                    builder.OpenComponent<LinkInlineView>(0);
                    builder.AddAttribute(1, nameof(LinkInlineView.Url), markdown.Url);
                    builder.AddAttribute(2, nameof(LinkInlineView.Title), markdown.Title);
                    builder.AddAttribute(3, nameof(BlockView.Content), context.RenderInline(markdown));
                    builder.CloseComponent();
                }
            });
        }
    }
}
