# Quick Useage
In this paper, it will help you quickly use `DocMarkdown`.

## Config file
There must have `config.json` configuration file in website folder.
Configuration file declared relationships between `DocMarkdown` and `Markdown` documents.

`config.json` is a `JSON` format configuration file.  
Below is a full configuration file example.

```json
{
  "Title": "DocMarkdown",
  "Icon": "logo.png",
  "BaseUrl": "https://raw.githubusercontent.com/who/project",
  "Path": "docs",
  "Languages": [
    {
      "Name": "English",
      "Value": "en-us",
      "CatalogText": "In this article"
    }
  ],
  "Versions": [
    {
      "Name": "DocMarkdown 1.0",
      "Value": "1.0",
      "Path": "main"
    }
  ]
}
```

### Title
`$.Title` property declared document global title of upper left corner(default theme).  
Property value is required.

### Icon
`$.Icon` property declared path of icon on the left side of the document global title.  
Property is optional.  

### Base Url
`$.BaseUrl` property declared location of `Markdown` documents.  
Property is required. Value can be empty string value.  
It will use local resource while value is empty or a relative path.

### Path
`$.Path` property declared a relative path attach before each `Markdown` document.  
Property is optional.

### Multiple languages
`$.Languages` property declared languages of documents.  
Property is optional.  
Property value must be a array.  
The first value of array will be default language.

#### Language Name
`$.Languages[0].Name` property declared display name of language.  
Property value is required.

#### Language Value
`$.Languages[0].Value` property declared file name of language.  
Property value is required.
Property value will attach before file extension of `Markdown` file. For example `.en-us`.

#### Catalog Text
`$.Languages[0].CatalogText` property declared text display on navigation for document anchors.  
Property value is required.

### Multiple Versions
`$.Versions` property declared versions of documents.  
Property is optional.  
Property value must be a array.  
The first value of array will be default version.

#### Version Name
`$.Versions[0].Name` property declared display name of version.  
Property value is required.

#### Version Value
`$.Languages[0].Value` property declared value of verion.  
Property value is required.  
Property value used as version value of querystring of url.

#### Version Path
`$.Languages[0].Path` proerty declared path of verion of relative path to `Markdown` document file.  
Property value is required.  
Property value will attach after `Base Url` and `Path` as a folder while access to a `Markdown` file.

## Navigation Configuration
The root of documents must be a `nav.json` file.
If [`Multiple Languages`](#Multiple-Languages) configured.
Eech verion must have it\`s own navigation configuration.  
Base on [`Document Path Rule`](#Document-Path-Rule), it should be a file at `https://raw.githubusercontent.com/who/project/main/docs/nav.en-us.json`.

`nav.json` is a `JSON` format configuration file.
Below is full example configuration file.
```json
{
  "Introduction": {
    "Path": "index"
  },
  "Quick Useage": {
    "Path": "quick"
  },
  "Advanced": {
    "Children": {
      "Content A": {
        "Path": "advanced/content1"
      },
      "Content B": {
        "Path": "advanced/content2"
      }
    }
  }
}
```

Contents will parse to tree structure to display on website.

### Node Name
`$.{name}` property name declared the name of tree node of navigation.  
Property value must be a `object` and can not be `null`.  
There could be multiple properties.

### Node Path
`$.{name}.Path` property declared the document path of navigation.  
Property is optional.  
When it is `null` or empty, it cound not navigate to page while click, but folding.

### Node Children
`$.{name}.Children` property declared the children of tree node of navigation.  
Property is optional.  

Multilevel tree navigation are allowed.

```json
{
  "Top Level 1": {
    "Path": "c1"
  },
  "Top Level 2": {
    "Path": "c2"
  },
  "Top Level 3": {
    "Children": {
      "Secondary Level 1": {
        "Path": "c3/c1"
      },
      "Secondary Level 2": {
        "Children": {
          "Thirdly Level 1": {
            "Path": "c3/c2/c1"
          },
          "Thirdly Level 2": {
            "Path": "c3/c2/c2"              
          }
        }
      }
    }
  }
}
```

## Document Path Rule
`DocMarkdown` will access a `Markdown` document file base on configuration.  
For example path `/grpc/`.  
When path end with `/` or is empty, append path with `index`.  
Then we get the path `/grpc/index`.

If [`Multiple Languages`](#Multiple-Languages) configured.  
Append path with `.{lang}` which is [`Language Value`](#Language-Value) of current language.  
And append path with `.md` file extension.
Then we get the path `/grpc/index.en-us.md`.

if [`Path`](#Path) configured.
Append with `/{path}` which is [`Path`](#Path).  
Then we get the path `/docs/grpc/index.en-us.md`.

If [`Multiple Verions`](#Multiple-Versions) configured.  
Prepend path with `/{path}` which is [`Version Path`](#Version-Path) of current version.  
Then we get the path `/main/docs/grpc/index.en-us.md`.

Finally, prepend path with `{baseUrl}` which is [`Base Url`](#Base-Url).  
We get the path `https://raw.githubusercontent.com/who/project/main/docs/grpc/index.en-us.md`.

`DocMarkdown` will request to this url. Parse response content and render to HTML content.