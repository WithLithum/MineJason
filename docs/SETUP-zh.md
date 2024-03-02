# 开发环境设置指南

本指南将引导您使用您的 IDE 来配置 MineJason 开发环境。请注意：所有贡献者一般都需要阅读本指南。

## 开发环境

要进行 MineJason 的开发，您需要安装 .NET 8 SDK。

### Windows

如果您使用的是 Windows 10 或以后版本，则您应该可以使用 `winget` 包管理器。要使用包管理器安装 .NET，请打开命令提示符或 PowerShell 并键入：

```powershell
winget install dotnet-sdk-8
```

命令将会自动下载 .NET 8 SDK。这一步无需担心访问速度问题，因为安装包是自动从离您最近的微软 CDN 下载的。

如果您在中国大陆地区，访问在 GitHub 上的 WinGet 包管理器源有困难或者速度较慢，您可以使用以下命令将默认源替换为中科大镜像源（需要管理员权限）：

```powershell
winget source remove winget
winget source add winget https://mirrors.ustc.edu.cn/winget-source
```

### GNU/Linux

建议您使用发行版的包管理器安装 .NET，而不是手动安装。

各包管理器的安装命令如下参考（带 `sudo`，需要管理员权限）：

- Ubuntu：`sudo apt-get install dotnet-sdk-8.0`

- 红帽系（Fedora, RHEL, CentOS Stream）：`sudo dnf install dotnet-sdk-8.0`

以上未列出的包管理器的默认源中没有 .NET SDK；这种情况下请参阅[官方指南](https://learn.microsoft.com/zh-cn/dotnet/core/install/linux)配置 .NET SDK。请勿手动安装或使用 `dotnet-install` 安装脚本。

### macOS

| :warning: 警告                           |
| -------------------------------------- |
| 我们会尽力帮助您配置 macOS 上的开发环境，但是我们不能指导您进行安装。 |

建议您使用官方安装包安装 .NET，而不是手动安装。[点此前往下载页面](https://dotnet.microsoft.com/zh-cn/download/dotnet/8.0)。

## 配置存储库

在这一步，您需要使用 Git 克隆存储库。

### 安装 Git

#### Windows

Git 在 Windows 上有几种方法可以使用，但多数人都会选择 Git for Windows—一套含有 Git 和一些 GNU 工具的工具包。你可以在[此处](https://gitforwindows.org)；下载到 Git for Windows；或者，您也可以在 Windows 10 及以后版本上使用以下命令：

```batch
winget install git
```

Git for Windows 在 Visual Studio Installer 中也可以获得。如果您是中国大陆地区的用户，使用官方源下载较慢，我们建议您使用[清华镜像源](https://mirrors.tuna.tsinghua.edu.cn/github-release/git-for-windows/git/LatestRelease/)下载 Git for Windows。注意：除非有特别原因，您不应使用 PortableGit。

除了 Git for Windows 以外，还有以下获得 Git 的方式：

- MSYS2
- Cygwin
- Windows Subsystem for Linux
- 及其它....

我们建议您使用 Git for Windows；它提供的 Git 版本专门针对 Windows 环境进行适配和优化，而其它（如 MSYS 和 Cygwin）的版本则没有作过多优化，而只是移植了而已（更不要提 WSL 中的 Git 到底还是 GNU/Linux Git）。

#### GNU/Linux

Git 应该在您的发行版的包管理器内以一个名为 `git` 的包提供。如果没有，请看[此指南](https://git-scm.com/download/linux)。

#### macOS

Git 应该在 Homebrew 和 MacPorts 中以一个名为 `git` 的包提供。Xcode 也自带了 Git。

详见[此指南](https://git-scm.com/download/mac)。

### Cloning Repository

| ❕ NOTE                                                                                                                                                                                       |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| The GitHub repository is a read-only mirror of the [primary repository on GitLab.com](https://gitlab.com/WithLithum/MineJason). Do not use it to submit contributions or use it as upstream. |

To clone the repository, type the following command in your shell:

```bash
git clone https://gitlab.com/WithLithum/MineJason.git
```

Other options such as SSH are available. Open the [repository page](https://gitlab.com/WithLithum/MineJason) and click on the *Clone* button.

## Setting up IDE

### Visual Studio

Visual Studio is a full-fledged IDE for Windows software development, by Microsoft. It provides first-class citizen support for .NET.

To download it, visit the [official website](https://visualstudio.com).

To open the repository in Visual Studio you will need to open the solution. If you don't yet have the repository locally cloned, open Visual Studio first, then:

- If an empty environment opens, press `File` > `Clone Repository...`, and enter `https://gitlab.com/WithLithum/MineJason.git` in the Repository Location. Specify a path you want to clone the repository to, and press `Clone`. Once the clone is complete, you have the repository opened automatically. Preceed below.
- If a start window opens, press `Clone a repository` and enter `https://gitlab.com/WithLithum/MineJason.git` in the Repository Location. Specify a path you want to clone the repository to, and press `Clone`. Once the clone is complete, you have the repository opened automatically. Preceed below.

Open the repository folder (With either the File Explorer or Visual Studio) and navigate to `src`, and you will see the `MineJason.sln` file. Double click it.

Visual Studio will prompt when an absolute requirement for developing MineJason is missing. Make sure you install them.

### Visual Studio Code

Visual Studio Code is a code editor that can be quickly spin up as an IDE. It is the IDE of choice for working with .NET code on platforms outside Windows (if you don't have Rider).

To download it, visit the [official website](https://code.visualstudio.com/).

Open the repository in Visual Studio Code. If you don't have the repository yet locally cloned, use [this link to open Visual Studio Code and clone the repository through it](vscode://vscode.git/clone?url=https%3A%2F%2Fgitlab.com%2FWithLithum%2FMineJason.git). Then, click the Extensions tab. You will see "Workspace Recommendations" - install all extensions within that you don't currently have.

| :warning: WARNING                                                                                                                                          |
| ---------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Currently VIsual Studio Code's C# Dev Box extensions requries .NET 7, a Standard Term Support version of .NET to work. This may change in the near future. |

### JetBrains Rider

The JetBrains Rider is a .NET IDE, and requires a subscription to use. You can get a free (renewable) subscription limited for Open Source development for 1 year under [certain requirements](https://www.jetbrains.com/community/opensource/#support).

Open the repository in Rider. If you don't have the repository yet locally cloned, use [this link to open Rider and clone the repository through the IDE](jetbrains://rider/checkout/git?idea.required.plugins.id=Git4Idea&checkout.repo=https%3A%2F%2Fgitlab.com%2FWithLithum%2FMineJason.git). navigate to `src` folder and double click `MineJason.sln`. Everything should be set up as usual. Please note that you will need to install .NET 8 SDK.

## Complete

You now have the repository open and ready for you to work on with your favourite IDE.
