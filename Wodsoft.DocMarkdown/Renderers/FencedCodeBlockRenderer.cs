using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class FencedCodeBlockRenderer : MarkdownRenderer<FencedCodeBlock>
    {
        protected override object Render(IMarkdownRenderContext context, FencedCodeBlock markdown)
        {
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<HighlightCodeBlockView>(0);
                builder.AddAttribute(1, nameof(HighlightCodeBlockView.Language), markdown.Info);
                builder.AddAttribute(2, nameof(InlineView.Content), string.Join('\n', markdown.Lines.Lines.Select(t => t.Slice.ToString())));
                builder.CloseComponent();
            });
        }
    }
}
