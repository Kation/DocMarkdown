using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class CodeBlockRenderer : MarkdownRenderer<CodeBlock>
    {
        protected override object Render(IMarkdownRenderContext context, CodeBlock markdown)
        {
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<CodeBlockView>(0);
                builder.AddAttribute(1, nameof(BlockView.Content), new RenderFragment(content =>
                {
                    int i = 0;
                    foreach (var line in markdown.Lines.Lines)
                    {
                        content.AddContent(i, line.Slice.ToString());
                        i++;
                    }
                }));
                builder.CloseComponent();
            });
        }
    }
}
