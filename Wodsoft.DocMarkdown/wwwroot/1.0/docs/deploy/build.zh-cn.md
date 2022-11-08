# 生成

## 编译生成
通过`git`下载源码并编译发布。
```powershell
git clone https://github.com/Kation/DocMarkdown.git
cd DocMarkdown
cd Wodsoft.DocMarkdown
dotnet publish -c Release
```
发布内容位于`/DocMarkdown/Wodsoft.DocMarkdown/bin/Release/net6.0/publish/wwwroot`下，
并将示例文档文件夹`1.0`删除。  
修改`config.json`配置文件以适用于您的`Markdown`文档配置。

## 下载发布版本
访问[Github仓库](https://github.com/Kation/DocMarkdown/releases)以下载发布的版本。