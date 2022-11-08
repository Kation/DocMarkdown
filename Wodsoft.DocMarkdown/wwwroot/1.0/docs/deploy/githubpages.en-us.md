# Work with Github Pages

## Push
Put configured contents to the root of repository and push to `Github Repository`.

## Build and deploy
Select `Pages` tab at settings of repository.  
Look at `Build and deployment` option.
Select `Github Actions` from `Source` dropdown menu.  
Then click `Configure` button of `Static HTML` card to modify build and deploy script.  
Notice which branch need to build and `Github Actions` will execute script when branch have new commit.
```yaml
on:
  # Runs on pushes targeting the default branch
  push:
    branches: ["master"]
```

## Configue domain
Use your own domain by set `Custom domain`.
Then you can use `DocMarkdown` when access this domain.