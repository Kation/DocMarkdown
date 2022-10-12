# 扩展
`DocMarkdown`使用[`Markdig`](https://github.com/xoofx/markdig)解析`Markdown`文本内容。
仅启用默认功能与表格功能。

## 修改支持的Markdown功能
修改位于`Services/DocEngine.cs`内的`NavigateToAsync`方法，对`pipelineBuilder`添加、删除支持功能。  
增加新的功能后，解析`Markdown`文本内容会返回对应的`MarkdownObject`派生类型，需要添加对应的`Markdown渲染器`以渲染对应的Html内容。

## Markdown渲染器
要渲染对应的`MarkdownObject`派生类型，需要继承`MarkdownRenderer`基类并实现抽象方法。

### 是否支持渲染
`bool CanRender(MarkdownObject markdown)`方法应返回是否支持渲染此`MarkdownObject`对象，`true`为支持，`false`为不支持。

### 渲染
`object Render(IMarkdownRenderContext context, MarkdownObject markdown)`方法应返回此`MarkdownObject`对象渲染内容。  
一般而言，应返回`RenderFragment`委托对象，`DocMarkdown`将直接作为`Razor`渲染内容展现。  
同时也可以返回`MarkupString`标记文本对象，可以直接展示Html内容。但是其受限于`Razor`，`MarkupString`必须拥有完整的Html元素，不能只包含元素头部（例如\<a>）或元素结尾（例如\</a>）。  
如果需要将多个`MarkdownObject`组合为一个元素（例如[Markdown内联Html](https://spec.commonmark.org/0.30/#raw-html)），可以返回`HtmlElement`对象，可以参考`HtmlInlineRenderer`。

## 管理Markdown渲染器
每一个要启动的`Markdown渲染器`都应在`Program.cs`内添加到`DocEngine`内。  
为了提高性能，应首先添加常用的`Markdown渲染器`。

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

## 内置Markdown渲染器
`DocMarkdown`内置一批`Markdown渲染器`用于渲染基础`Markdown`内容，代码位于`Renderers`下。  

### 段落
`ParagraphBlockRenderer`用于展示[Markdown段落](https://spec.commonmark.org/0.30/#paragraphs)。

### 代码
`FencedCodeBlockRenderer`用于展示[Markdown代码](https://spec.commonmark.org/0.30/#fenced-code-blocks)。

### 标题
`HeadingBlockRenderer`用于展示[Markdown标题](https://spec.commonmark.org/0.30/#atx-headings)。

### HTML代码
`HtmlBlockRenderer`用于展示[Markdown Html](https://spec.commonmark.org/0.30/#html-blocks)。  
`HtmlInlineRenderer`用于展示[Markdown内联Html](https://spec.commonmark.org/0.30/#raw-html)。  
`HtmlEntityInlineRenderer`用于展示[Markdown Html实体](https://spec.commonmark.org/0.30/#entity-and-numeric-character-references)。

### 引用
`QuoteBlockRenderer`用于展示[Markdown引用](https://spec.commonmark.org/0.30/#block-quotes)。

### 分隔符
`ThematicBreakBlockRenderer`用于展示[Markdown分隔符](https://spec.commonmark.org/0.30/#thematic-breaks)。

### 列表
`ListBlockRenderer`以及
`ListItemBlockRenderer`用于展示[Markdown列表](https://spec.commonmark.org/0.30/#lists)。

### 段落内代码
`CodeInlineRenderer`用于展示[Markdown内联代码](https://spec.commonmark.org/0.30/#code-spans)。

### 段落内强调
`EmphasisInlineRenderer`用于展示[Markdown强调](https://spec.commonmark.org/0.30/#emphasis-and-strong-emphasis)。

### 段落内链接
`LinkInlineRenderer`用于展示[Markdown链接](https://spec.commonmark.org/0.30/#links)。
`AutolinkInlineRenderer`用于展示[Markdown自动链接](https://spec.commonmark.org/0.30/#autolinks)。

### 段落内文本
`LiteralInlineRenderer`用于展示普通文本。