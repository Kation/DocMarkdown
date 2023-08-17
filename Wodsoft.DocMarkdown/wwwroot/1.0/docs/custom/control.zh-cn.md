# 控件
`DocMarkdown`使用`Razor`与[`Blazorise`](https://blazorise.com)组件来渲染Markdown页面。  
在修改前应首先了解[ASP.NET Core Razor组件](https://docs.microsoft.com/zh-cn/aspnet/core/blazor/components)相关知识。

## 段落
`Components/ParagraphBlockView.razor`用于展示[`Markdown段落`](https://spec.commonmark.org/0.30/#paragraphs)。

使用`<p class="paragraph"></p>`元素包裹段落内容。

## 代码
`Components/HighlightCodeBlockView.razor`用于展示[`Markdown代码`](https://spec.commonmark.org/0.30/#fenced-code-blocks)。

使用`<pre><code></pre></code>`元素包裹代码纯文本，并使用[`highlight.js`](https://highlightjs.org)高亮代码。

示例：
```
代码内容
```

## 标题
`Components/HeadingCodeBlockView.razor`用于展示[`Markdown标题`](https://spec.commonmark.org/0.30/#atx-headings)。

使用`<Heading></Heading>`元素包裹标题内容。并添加锚点，一级标题将额外设置为页面标题。

## HTML内容
`Components/HtmlBlockView.razor`用于展示[`Markdown Html`](https://spec.commonmark.org/0.30/#html-blocks)。

使用`<div class="html"></div>`元素包裹Html内容。

## 引用
`Components/QuoteBlockView.razor`用于展示[`Markdown引用`](https://spec.commonmark.org/0.30/#block-quotes)。

使用`<Blockquote></Blockquote>`元素包裹引用内容。

示例：  
> 1  
> 2

## 分隔符
`Components/ThematicBreakBlockView.razor`用于展示[`Markdown分隔符`](https://spec.commonmark.org/0.30/#thematic-breaks)。

使用`<Divider />`作为分隔符。

示例：

----

## 列表
`Components/OrderedListBlockView.razor`以及
`Components/UnorderedListBlockView.razor`用于展示[`Markdown列表`](https://spec.commonmark.org/0.30/#lists)。

使用`<OrderedList></OrderedList>`或`<UnorderedList></UnorderedList>`包裹列表项。

`Components/ListItemBlockView.razor`用于展示列表项。

使用`<UnorderedListItem></UnorderedListItem>`包裹列表项内容。

示例：
* 1
* 2
* 3

## 段落内代码
`Components/CodeInlineView.razor`用于展示[`Markdown内联代码`](https://spec.commonmark.org/0.30/#code-spans)。

使用`<code></code>`包裹段落内代码内容。

示例：  
这是一段文本，其中`abc`为代码内容。

## 段落内强调
`Components/EmphasisInlineView.razor`用于展示[`Markdown强调`](https://spec.commonmark.org/0.30/#emphasis-and-strong-emphasis)。

使用`<em></em>`或`<strong></strong>`包裹段落内强调内容。

## 段落内链接
`Components/LinkInlineView.razor`用于展示[`Markdown链接`](https://spec.commonmark.org/0.30/#links)。

使用`<Link></Link>`包裹段落内链接内容。

支持站内导航，以`/`开头自动导航到相关文档。

示例：[使用Github Pages](/deploy/githubpages)

支持锚点导航，以`#`开头导航到页内锚点。

示例：[代码](#代码)

## 段落内文本
`Components/LiteralInlineView.razor`用于展示普通文本。

使用`<span></span>`包裹段落内文本内容。

## 自定义容器
`Components/CustomContainerView.razor`用于展示[`Markdown自定义容器`](https://talk.commonmark.org/t/custom-container-for-block-and-inline/2051)。

使用`<div class="alert alert-{type}"></div>`包裹段落内容。

可用类型：
* info
* success
* warning
* danger
* light
* dark
* primary
* secondary
* default

示例：
:::info
一般用于显示提示信息。
:::