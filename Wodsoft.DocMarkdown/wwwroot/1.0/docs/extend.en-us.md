# Extension
`DocMarkdown` use [`Markdig`](https://github.com/xoofx/markdig) to parse `Markdown` contents.  
Only generic feature and table feature are enabled by default.

## Enable Markdown Feature
Adjust features to `pipelineBuilder` in method `NavigateToAsync` at `Services/DocEngine.cs`.  
Related `MarkdownObject` derived class need to render to html content while enable new features by add `Markdown Renderer`.

## Markdown Renderer
Inherit `MarkdownRenderer` and implement abstract method to render a `MarkdownObject` object.

### Can Render
Method `bool CanRender(MarkdownObject markdown)` should return value if renderer could render this `MarkdownObject` parameter object. `true` is support otherwise `false`.

### Render
Method `object Render(IMarkdownRenderContext context, MarkdownObject markdown)` should return present content to `MarkdownObject`.  
In general, it should return `RenderFragment` delegate value.
`DocMarkdown` will use it as `Razor` content directly.  
Or you can return `MarkupString` value to display HTML contents.
But it must have full html element limited by `Razor`.
`MarkupString` **NOT SUPPORT** split element like `<a>` or `</a>` etc.  
If there have a splitted HTML element from `MarkdownObject`(Example [`Markdown Raw Html`](https://spec.commonmark.org/0.30/#raw-html)),
it can return `HtmlElement` object value. Reference to `HtmlInlineRenderer`.

## Manage Markdown Renderer
Each `Markdown Renderer` that need to be enableed should add to `DocEngine` in `Program.cs` file.  
Frequently-used `Markdown Renderer` should add first to improve performance.

```csharp
engine.AddRenderer(new ParagraphBlockRenderer());
engine.AddRenderer(new LiteralInlineRenderer());
engine.AddRenderer(new LineBreakInlineRenderer());
engine.AddRenderer(new EmphasisInlineRenderer());
engine.AddRenderer(new CodeInlineRenderer());
engine.AddRenderer(new LinkInlineRenderer());
engine.AddRenderer(new AutolinkInlineRenderer());
engine.AddRenderer(new HeadingBlockRenderer());
engine.AddRenderer(new FencedCodeBlockRenderer());
engine.AddRenderer(new QuoteBlockRenderer());
engine.AddRenderer(new ListBlockRenderer());
engine.AddRenderer(new ListItemBlockRenderer());
engine.AddRenderer(new TableRenderer());
engine.AddRenderer(new CodeBlockRenderer());
engine.AddRenderer(new HtmlBlockRenderer());
engine.AddRenderer(new HtmlInlineRenderer());
engine.AddRenderer(new HtmlEntityInlineRenderer());
engine.AddRenderer(new ThematicBreakBlockRenderer());
```

## Built-in Markdown Renderer
`DocMarkdown` have some built-in `Markdown Renderer` to render generic `Markdown` contents in the folder `Renderers`.

### Paragraph
`ParagraphBlockRenderer` used to present [`Markdown Paragraph`](https://spec.commonmark.org/0.30/#paragraphs).

### Code
`FencedCodeBlockRenderer` used to present [`Markdown Fenced Code`](https://spec.commonmark.org/0.30/#fenced-code-blocks).

### Heading
`HeadingBlockRenderer` used to present [`Markdown Heading`](https://spec.commonmark.org/0.30/#atx-headings).

### HTML Content
`HtmlBlockRenderer` used to present [`Markdown Html`](https://spec.commonmark.org/0.30/#html-blocks).  
`HtmlInlineRenderer` used to present [`Markdown Raw Html`](https://spec.commonmark.org/0.30/#raw-html).  
`HtmlEntityInlineRenderer` used to present [`Markdown Entity`](https://spec.commonmark.org/0.30/#entity-and-numeric-character-references).

### Quote
`QuoteBlockRenderer` used to present [`Markdown Quote`](https://spec.commonmark.org/0.30/#block-quotes).

### Thematic Break
`ThematicBreakBlockRenderer` used to present [`Markdown Thematic Break`](https://spec.commonmark.org/0.30/#thematic-breaks).

### List
`ListBlockRenderer`以及
`ListItemBlockRenderer` used to present [`Markdown Lists`](https://spec.commonmark.org/0.30/#lists).

### Inline Code
`CodeInlineRenderer` used to present [`Markdown Code Spans`](https://spec.commonmark.org/0.30/#code-spans).

### Emphasis
`EmphasisInlineRenderer` used to present [`Markdown Emphasis`](https://spec.commonmark.org/0.30/#emphasis-and-strong-emphasis).

### Link
`LinkInlineRenderer` used to present [`Markdown Links`](https://spec.commonmark.org/0.30/#links).
`AutolinkInlineRenderer` used to present [`Markdown Autolinks`](https://spec.commonmark.org/0.30/#autolinks).

### Literal
`LiteralInlineRenderer` used to present normal literal.