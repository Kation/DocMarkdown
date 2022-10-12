using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class AutolinkInlineRenderer : MarkdownRenderer<AutolinkInline>
    {
        protected override object Render(IMarkdownRenderContext context, AutolinkInline markdown)
        {
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<CodeBlockView>(0);
                builder.AddAttribute(1, nameof(LinkInlineView.Url), markdown.Url);
                builder.AddAttribute(2, nameof(LinkInlineView.Title), markdown.Url);
                builder.AddAttribute(3, nameof(InlineView.Content), new RenderFragment(content =>
                {
                    builder.AddContent(0, markdown.Url);
                }));
                builder.CloseComponent();
            });
        }
    }
}
