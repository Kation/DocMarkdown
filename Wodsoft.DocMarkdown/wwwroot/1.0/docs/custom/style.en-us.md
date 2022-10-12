# Style Sheet
`DocMarkdown` use [`material`](http://materializecss.com/) as base style sheet where file locate at `wwwroot/css/material.min.css`.  
And another style sheet `wwwroot/css/markdown.css` adjust to `Markdown` contents.

## Add Style Sheet
Prepend to `</head>` at `wwwroot/index.html` file.
```html
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <title>Document Markdown</title>
    <base href="/" />
    <!-- Material CSS -->
    <link href="css/material.min.css" rel="stylesheet">
    <!-- Add Material font (Roboto) and Material icon as needed -->
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,300i,400,400i,500,500i,700,700i|Roboto+Mono:300,400,700|Roboto+Slab:300,400,700" rel="stylesheet">
    <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <link href="_content/Blazorise/blazorise.css" rel="stylesheet" />
    <link href="_content/Blazorise.Material/blazorise.material.css" rel="stylesheet" />
    <link href="_content/Blazorise.Icons.Material/blazorise.icons.material.css" rel="stylesheet" />
    <link rel="stylesheet" href="//cdnjs.cloudflare.com/ajax/libs/highlight.js/11.6.0/styles/default.min.css">
    <link href="css/markdown.css" rel="stylesheet" />    
    <!-- Add Custom Style Sheet -->    
</head>
```

