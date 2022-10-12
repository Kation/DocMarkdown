using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;
using Wodsoft.DocMarkdown.Components;
using Wodsoft.DocMarkdown.Services;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class HeadingBlockRenderer : MarkdownRenderer<HeadingBlock>
    {
        protected override object Render(IMarkdownRenderContext context, HeadingBlock markdown)
        {
            var name = ((LiteralInline)markdown.Inline.FirstOrDefault(t => t is LiteralInline))?.Content.ToString();
            var anchor = name?.Replace(' ', '-');
            if (!string.IsNullOrEmpty(name))
            {
                if (markdown.Level == 2 || markdown.Level == 3)
                {
                    context.CatalogManager.Add(new Catalog { Anchor = anchor, Name = name, Level = markdown.Level - 1 });
                }
            }
            return new RenderFragment(builder =>
            {
                builder.OpenComponent<HeadingBlockView>(0);
                builder.AddAttribute(1, nameof(HeadingBlockView.Level), markdown.Level);
                builder.AddAttribute(2, nameof(HeadingBlockView.Name), name);
                builder.AddAttribute(2, nameof(HeadingBlockView.Anchor), anchor);
                builder.AddAttribute(3, nameof(BlockView.Content), context.RenderInline(markdown.Inline));
                builder.CloseComponent();
            });
        }
    }
}
