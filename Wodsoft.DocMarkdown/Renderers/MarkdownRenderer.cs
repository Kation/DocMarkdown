using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public abstract class MarkdownRenderer
    {
        public abstract bool CanRender(MarkdownObject markdown);

        public abstract object Render(IMarkdownRenderContext context, MarkdownObject markdown);
    }

    public abstract class MarkdownRenderer<T> : MarkdownRenderer
        where T : MarkdownObject
    {
        public override bool CanRender(MarkdownObject markdown)
        {
            return markdown is T;
        }

        public override object Render(IMarkdownRenderContext context, MarkdownObject markdown)
        {
            return Render(context, (T)markdown);
        }

        protected abstract object Render(IMarkdownRenderContext context, T markdown);
    }
}
