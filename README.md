[![.NET Core](https://github.com/petrsvihlik/statiq-starter-kontent-lumen/workflows/.NET%20Core/badge.svg)](https://github.com/petrsvihlik/statiq-starter-kontent-lumen/actions)
[![Live Demo](https://img.shields.io/badge/Live-DEMO-brightgreen.svg?logo=github&logoColor=white)](https://petrsvihlik.github.io/statiq-starter-kontent-lumen/)

# Kentico Kontent 💖 Statiq - Lumen Starter
Lumen is a minimal, lightweight, and mobile-first starter for creating blogs using Statiq and Kentico Kontent.

---
![Lumen - screenshot](https://i.imgur.com/vLBpkl6.png)


## Features

+ Content from [Kontent](http://kontent.ai/) headless CMS.
+ Beautiful typography inspired by [matejlatin/Gutenberg](https://github.com/matejlatin/Gutenberg).
+ [Mobile-First](https://medium.com/@mrmrs_/mobile-first-css-48bc4cc3f60f) approach in development.
+ Stylesheet built using SASS and [BEM](http://getbem.com/naming/)-Style naming.
+ Syntax highlighting in code blocks.
+ Archive organized by tags and categories.
+ Automatic Sitemap generation.
+ RSS/Atom support out of the box
+ Google Analytics support.

## Getting Started

### Requirements
- [.NET Core 3.1](https://dotnet.microsoft.com/download)

### Clone the codebase

1. Click the ["Use this template"](https://github.com/petrsvihlik/statiq-starter-kontent-lumen/generate) button to [create your own repository from this template](https://help.github.com/en/github/creating-cloning-and-archiving-repositories/creating-a-repository-from-a-template).

### Create a content source

1. Go to [app.kontent.ai](https://app.kontent.ai) and [create an empty project](https://docs.kontent.ai/tutorials/set-up-kontent/projects/manage-projects#a-creating-projects)
1. Go to the "Project Settings", select API keys and copy the following keys for further reference
    + Project ID
    + Management API key
1. Install [Kontent Backup Manager](https://github.com/Kentico/kontent-backup-manager-js) and import data to the newly created project from [`kontent-backup.zip`](./kontent-backup.zip) file:

    ```sh
    npm i -g @kentico/kontent-backup-manager

    kbm --action=restore --projectId=<Project ID> --apiKey=<Management API key> --zipFilename=kontent-backup
    ```

    > Alternatively, you can use the [Template Manager UI](https://kentico.github.io/kontent-template-manager/import) for importing the content.

1. Go to your Kontent project and [publish all the imported items](https://docs.kontent.ai/tutorials/write-and-collaborate/publish-your-work/publish-content-items).

### Map the codebase to the data source

- adjust the `DeliveryOptions:ProjectId` key in `appSettings.json`

🎊🎉 **You are now ready to use the site as your own!**

### Running locally
- `dotnet run -- preview`
  - You can also emulate running the project in a virtual directory by appending `--virtual-dir statiq-starter-kontent-lumen`. See all preview [options](https://statiq.dev/web/running/preview-server).
- Go to `http://localhost:5080/`



### Production deployment to GitHub pages
- Enable GitHub actions in your repo
- Copy the [`.github/workflows/dotnet-core.yml`](https://github.com/petrsvihlik/statiq-starter-kontent-lumen/blob/master/.github/workflows/dotnet-core.yml) to your project
- Go to the repository secrets and set `LinkRoot` to the relative path of your project (e.g. `/statiq-starter-kontent-lumen`) - this is to ensure that all links work properly when deployed to a subfolder

## Original work
This template is licensed under the [MIT](LICENSE) license and the credits for the [original work](https://github.com/alxshelepenok/gatsby-starter-lumen) on the template go to [Alexander Shelepenok](https://github.com/alxshelepenok).
