# 使用Github Pages

## 推送文件
将编译后的代码与配置放置于根目录，并推送至`Github`仓储库。

## 生成与部署
在仓储库设置中，选择`Pages`选项卡。
在`Build and deployment`选项中，`Source`下拉框选择`Github Actions`。
然后点击`Static HTML`卡片的`Configure`按钮配置生成与部署脚本。  
需要注意的是应修改需要部署的分支，`Github Actions`会在分支提交时执行脚本。
```yaml
on:
  # Runs on pushes targeting the default branch
  push:
    branches: ["master"]
```

## 配置域名
在`Custom domain`选项处配置自己的域名，配置好后即可通过该域名访问。