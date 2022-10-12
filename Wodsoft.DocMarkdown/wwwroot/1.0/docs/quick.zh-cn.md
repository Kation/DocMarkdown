# 快速使用
本文档将帮助使用者快速使用`DocMarkdown`。

## 配置文件
站点目录必须包含`config.json`配置文件，
配置文件声明了`DocMarkdown`该从哪里读取`Markdown`文档并建立目录关系。

`config.json`是一个`JSON`格式的配置文件，以下配置是一个完整的配置文件示例。

```json
{
  "Title": "DocMarkdown",
  "Icon": "logo.png",
  "BaseUrl": "https://raw.githubusercontent.com/who/project",
  "Path": "docs",
  "Languages": [
    {
      "Name": "简体中文",
      "Value": "zh-cn",
      "CatalogText": "本文内容"
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

### 标题
`$.Title`属性值决定了显示于左上角（默认主题）的文档标题名称。  
该属性必须填写。

### 图标
`$.Icon`属性决定了显示于文档标题左侧的图标路径。  
该属性可不存在或为空。

### 基础地址
`$.BaseUrl`属性决定了整个`Markdown`文档的路径。  
该属性必须填写，可以为空字符串。  
当属性为空字符串或相对路径时，将使用本域名内资源。

### 路径地址
`$.Path`属性将附加于每个`Markdown`文档路径之前。  
该属性可以不存在。

### 多语言
`$.Languages`属性用于定义文档的多语言支持。  
该属性可以不存在。
属性内容必须为数组。  
第一个元素将作为默认语言。

#### 语言名称
`$.Languages[0].Name`属性用于显示语言名称。
该属性必须填写。

#### 语言值
`$.Languages[0].Value`属性决定了该语言的文件名称。
该属性必须填写。
属性内容将附加在`Markdown`文档路径扩展名之前。例如`.zh-cn`。

#### 目录文本
`$.Languages[0].CatalogText`属性决定了选择该语言时，文档页右侧的导航目录标题。   
该属性必须填写。

### 多版本
`$.Versions`属性用于定义文档的多版本支持。  
该属性可以不存在。  
属性内容必须为数组。  
第一个元素将作为默认版本。

#### 版本名称
`$.Versions[0].Name`属性用于显示版本名称。
该属性必须填写。

#### 版本值
`$.Languages[0].Value`属性决定了该版本在Url上的值。
该属性必须填写。

#### 版本路径
`$.Languages[0].Path`属性决定了该版本在Url上的值。
该属性必须填写。

## 导航配置
文档根路径必须存在`nav.json`，如果存在多语言，每个语言都需要一份导航配置。  
以[文档路径规则](#文档路径规则)里的示例为例，则必须存在`https://raw.githubusercontent.com/who/project/main/docs/nav.zh-cn.json`导航配置文件。

`nav.json`是一个`JSON`格式的配置文件，以下配置是一个完整的配置文件示例。
```json
{
  "简介": {
    "Path": "index"
  },
  "快速使用": {
    "Path": "quick"
  },
  "高级": {
    "Children": {
      "内容A": {
        "Path": "advanced/content1"
      },
      "内容B": {
        "Path": "advanced/content2"
      }
    }
  }
}
```

导航文件的内容将被解析生成树形结构展示于页面。

### 节点名称
`$.{name}`属性名称将作为导航目录的树形节点名。  
属性值为对象，不能为空。  
可以存在多个节点。

### 节点路径
`$.{name}.Path`属性作为该节点对应的文档路径，路径为相对路径。  
属性可以不存在。不存在或为空时，只作为可折叠节点，点击不会导航至其它页面。

### 节点子项
`$.{name}.Children`属性作为该节点的子项容器，里面包含了该节点下的所有子节点内容。
属性可以不存在。

可以组合多层树形导航目录。

```json
{
  "一级目录1": {
    "Path": "c1"
  },
  "一级目录2": {
    "Path": "c2"
  },
  "一级目录3": {
    "Children": {
      "二级目录1": {
        "Path": "c3/c1"
      },
      "二级目录2": {
        "Children": {
          "三级目录1": {
            "Path": "c3/c2/c1"
          },
          "三级目录2": {
            "Path": "c3/c2/c2"              
          }
        }
      }
    }
  }
}
```

## 文档路径规则
基于配置，DocMarkdown会将网站的路径映射至目标文档。  
例如`/grpc/`。  
当以`/`结尾或为空值时，自动添加`index`。  
然后得到路径`/grpc/index`。

如果存在[`多语言`](#多语言)，则于路径末尾添加`.{lang}`，`{lang}`为当前[`语言值`](#语言值)。  
最后于末尾添加`.md`扩展名。  
得到路径`/grpc/index.zh-cn.md`。  

如果存在路径地址，则于路径前添加`/{path}`[`路径地址`](#路径地址)。  
得到路径`/docs/grpc/index.zh-cn.md`。

如果存在[`多版本`](#多版本)，则于路径前添加`/{version}`，`{version}`为[`版本路径`](#版本路径)。  
得到路径`/main/docs/grpc/index.zh-cn.md`

最后于路径前添加`{baseUrl}`[`基础地址`](#基础地址)。  
得到路径`https://raw.githubusercontent.com/who/project/main/docs/grpc/index.zh-cn.md`。

DocMarkdown将请求该地址以获取Markdown文档内容并解析生成Html内容展现出来。