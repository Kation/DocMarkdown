# 布局
`DocMarkdown`使用`Razor`与[`Blazorise`](https://blazorise.com)组件来渲染Markdown页面。  
在修改前应首先了解[ASP.NET Core Razor组件](https://docs.microsoft.com/zh-cn/aspnet/core/blazor/components)相关知识。

## 母版页
`Shared/MainLayout.razor`用于处理页面布局结构。  
里面定义了导航菜单、侧边栏与页面内容结构关系。

使用其它母版页需要修改`Services/DocEngine.cs`以下代码。
```csharp
private void RenderError(RenderHandle renderHandle, string message)
{
    renderHandle.Render(builder =>
    {
        builder.OpenComponent<LayoutView>(0);//修改为其它母版页
        builder.AddAttribute(1, nameof(LayoutView.Layout), typeof(MainLayout));
        builder.AddAttribute(2, nameof(LayoutView.ChildContent), (RenderFragment)(child =>
        {
            child.OpenComponent<Error>(0);
            child.AddAttribute(1, "Message", message);
            child.CloseComponent();
        }));
        builder.CloseComponent();
    });
}
```
以及
```csharp
private void RenderMarkdown(RenderHandle renderHandle, MarkdownDocument document)
{
    var content = ConvertDocument(document);
    renderHandle.Render(builder =>
    {
        builder.OpenComponent<LayoutView>(0);//修改为其它母版页
        builder.AddAttribute(1, nameof(LayoutView.Layout), typeof(MainLayout));
        builder.AddAttribute(2, nameof(LayoutView.ChildContent), (RenderFragment)(child =>
        {
            child.OpenComponent<Index>(0);
            child.AddAttribute(1, "Content", content);
            child.CloseComponent();
        }));
        builder.CloseComponent();
    });
}
```

## 侧边栏
`Shared/NavMenu.razor`用于展示侧边栏内容，包括了版本、语言、文档大纲目录。  
切换文档的触发操作在此文件内。

## 内容展示页
`Pages/Index.razor`用于展示`Markdown`文档内容。  
页面除了`Markdown`文档外，还包括了页内目录导航。页内目录导航样式。

使用其它内容展示页需要修改`Services/DocEngine.cs`以下代码。
```csharp
private void RenderMarkdown(RenderHandle renderHandle, MarkdownDocument document)
{
    var content = ConvertDocument(document);
    renderHandle.Render(builder =>
    {
        builder.OpenComponent<LayoutView>(0);
        builder.AddAttribute(1, nameof(LayoutView.Layout), typeof(MainLayout));
        builder.AddAttribute(2, nameof(LayoutView.ChildContent), (RenderFragment)(child =>
        {
            child.OpenComponent<Index>(0);//修改为其它Razor页面
            child.AddAttribute(1, "Content", content);
            child.CloseComponent();
        }));
        builder.CloseComponent();
    });
}
```

## 错误页
`Pages/Error.razor`用于出现错误时显示错误信息，例如找不到`Markdown`文件。

使用其它错误页需要修改`Services/DocEngine.cs`以下代码。
```csharp
private void RenderError(RenderHandle renderHandle, string message)
{
    renderHandle.Render(builder =>
    {
        builder.OpenComponent<LayoutView>(0);
        builder.AddAttribute(1, nameof(LayoutView.Layout), typeof(MainLayout));
        builder.AddAttribute(2, nameof(LayoutView.ChildContent), (RenderFragment)(child =>
        {
            child.OpenComponent<Error>(0);//修改为其它Razor页面
            child.AddAttribute(1, "Message", message);
            child.CloseComponent();
        }));
        builder.CloseComponent();
    });
}
```