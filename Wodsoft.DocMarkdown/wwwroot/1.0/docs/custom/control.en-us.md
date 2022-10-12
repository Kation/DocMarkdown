# Controls
`DocMarkdown` use `Razor` and [`Blazorise`](https://blazorise.com) to render `Markdown` pages.  
You should learn [`ASP.NET Core Razor Component`](https://docs.microsoft.com/zh-cn/aspnet/core/blazor/components) before change it.

## Paragraph
`Components/ParagraphBlockView.razor` used to present [`Markdown Paragraph`](https://spec.commonmark.org/0.30/#paragraphs).

Use `<p class="paragraph"></p>` element to wrap paragraph contents.

## Code
`Components/HighlightCodeBlockView.razor` used to present [`Markdown Fenced Code`](https://spec.commonmark.org/0.30/#fenced-code-blocks).

Use `<pre><code></pre></code>` element to wrap codes and use [`highlight.js`](https://highlightjs.org) to high light codes.

## Heading
`Components/HeadingCodeBlockView.razor` used to present [`Markdown Heading`](https://spec.commonmark.org/0.30/#atx-headings).

Use `<Heading></Heading>` element to wrap heading contents and add anchor.
Top level will extra to set to page title.

## HTML Content
`Components/HtmlBlockView.razor` used to present [`Markdown Html`](https://spec.commonmark.org/0.30/#html-blocks).

Use `<div class="html"></div>` element to wrap Html contents.

## Quote
`Components/QuoteBlockView.razor` used to present [`Markdown Quote`](https://spec.commonmark.org/0.30/#block-quotes).

Use `<Blockquote></Blockquote>` element to wrap quote contents.

## Thematic Break
`Components/ThematicBreakBlockView.razor` used to present [`Markdown Thematic Break`](https://spec.commonmark.org/0.30/#thematic-breaks).

Use `<Divider />` as separator.

## List
`Components/OrderedListBlockView.razor` and 
`Components/UnorderedListBlockView.razor` used to present [`Markdown Lists`](https://spec.commonmark.org/0.30/#lists).

Use `<OrderedList></OrderedList>` or `<UnorderedList></UnorderedList>` to wrap list items.

`Components/ListItemBlockView.razor` used to present list item.

Use `<UnorderedListItem></UnorderedListItem>` to wrap list item contents.

## Inline Code
`Components/CodeInlineView.razor` used to present [`Markdown Code Spans`](https://spec.commonmark.org/0.30/#code-spans).

Use `<code></code>` to wrap inline codes.

## Emphasis
`Components/EmphasisInlineView.razor` used to present [`Markdown Emphasis`](https://spec.commonmark.org/0.30/#emphasis-and-strong-emphasis).

Use `<em></em>`或`<strong></strong>` to wrap inline emphasis contents.

## Link
`Components/LinkInlineView.razor` used to present [`Markdown Links`](https://spec.commonmark.org/0.30/#links).

Use `<Link></Link>` to wrap link content.

## Literal
`Components/LiteralInlineView.razor` used to present normal literal.

Use `<span></span>` to wrap normal literal.