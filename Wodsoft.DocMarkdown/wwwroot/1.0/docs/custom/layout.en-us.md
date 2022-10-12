# Layout
`DocMarkdown` use `Razor` and [`Blazorise`](https://blazorise.com) to render `Markdown` pages.  
You should learn [`ASP.NET Core Razor Component`](https://docs.microsoft.com/zh-cn/aspnet/core/blazor/components) before change it.

## Master Page
`Shared/MainLayout.razor` used to declared layout structure of page.  
It declared navigation menu, sider and content presenter.

Use other master page should change code at `Services/DocEngine.cs`.
```csharp
private void RenderError(RenderHandle renderHandle, string message)
{
    renderHandle.Render(builder =>
    {
        builder.OpenComponent<LayoutView>(0);//Change to other master page class
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
And
```csharp
private void RenderMarkdown(RenderHandle renderHandle, MarkdownDocument document)
{
    var content = ConvertDocument(document);
    renderHandle.Render(builder =>
    {
        builder.OpenComponent<LayoutView>(0);//Change to other master page class
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

## Sider
`Shared/NavMenu.razor` used to display verions, languages and document navigation.  
Navigate trigger operation is inside.

## Content Presenter Page
`Pages/Index.razor` used to present `Markdown` document contents.  
Also, it is include page catalog navigation.

Use other page should change code at `Services/DocEngine.cs`.
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
            child.OpenComponent<Index>(0);//Change to other Razor page class
            child.AddAttribute(1, "Content", content);
            child.CloseComponent();
        }));
        builder.CloseComponent();
    });
}
```

## Error Page
`Pages/Error.razor` used to present error message.

Use other page should change code at `Services/DocEngine.cs`.
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