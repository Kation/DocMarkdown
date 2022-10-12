using Markdig.Syntax.Inlines;
using Microsoft.AspNetCore.Components;

namespace Wodsoft.DocMarkdown.Renderers
{
    public class HtmlInlineRenderer : MarkdownRenderer<HtmlInline>
    {
        protected override object Render(IMarkdownRenderContext context, HtmlInline markdown)
        {
            if (context.DocConfig.IsRawHtmlEnabled)
            {
                if (markdown.Tag.StartsWith("</"))
                {
                    return new HtmlElement { Tag = markdown.Tag.Substring(2, markdown.Tag.Length - 3), IsEnd = true };
                }
                else
                {
                    bool isSelfClose = markdown.Tag.EndsWith("/>");
                    var attrStart = markdown.Tag.IndexOf(' ');
                    Dictionary<string, string> attrs;
                    string tag;
                    if (attrStart == -1 || attrStart == markdown.Tag.Length - (isSelfClose ? 2 : 1))
                    {
                        attrs = null;
                        tag = markdown.Tag.Substring(1, markdown.Tag.Length - (isSelfClose ? 2 : 1) - 1).TrimEnd();
                    }
                    else
                    {
                        attrs = new Dictionary<string, string>(markdown.Tag.Substring(attrStart + 1, markdown.Tag.Length - (isSelfClose ? 2 : 1) - 1 - attrStart).Split(' ', StringSplitOptions.RemoveEmptyEntries)
                            .Select(t =>
                            {
                                var i = t.IndexOf('=');
                                if (i == -1)
                                    return new KeyValuePair<string, string>(t, null);
                                else
                                {
                                    var key = t.Substring(0, i);
                                    var value = t.Substring(i + 1, t.Length - i - 1).Trim('"');
                                    return new KeyValuePair<string, string>(key, value);
                                }
                            }));
                        tag = markdown.Tag.Substring(1, attrStart - 1);
                    }
                    return new HtmlElement { Tag = tag, IsSelfClose = isSelfClose, Attributes = attrs };
                }
            }
            else
                return markdown.Tag;
        }
    }
}
