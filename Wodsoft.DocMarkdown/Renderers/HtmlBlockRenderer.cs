using Markdig.Syntax;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class HtmlBlockRenderer : MarkdownRenderer<HtmlBlock>
    {
        protected override object Render(IMarkdownRenderContext context, HtmlBlock markdown)
        {
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<HtmlBlockView>(0);
                builder.AddAttribute(1, nameof(BlockView.Content), new RenderFragment(content =>
                {
                    int i = 0;
                    foreach (var line in markdown.Lines.Lines)
                    {
                        if (context.DocConfig.IsRawHtmlEnabled)
                            content.AddContent(i, new MarkupString(line.Slice.ToString()));
                        else
                            content.AddContent(i, line.Slice.ToString());
                        i++;
                    }
                }));
                builder.CloseComponent();
            });
        }
    }
}
