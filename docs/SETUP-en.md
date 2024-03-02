# Set-Up Guide

This guide will walk you through configuring your favourite IDE and setting up a environment for the development of MineJason. All contributors are expected to read this guide.

## Table of Contents

- [Setting up Repository](#setting-up-repository)
  - [Installing Git](#installing-git)
  - [Cloning Repository](#cloning-repository)
- [Setting up IDE](#setting-up-ide)
  - [Visual Studio](#visual-studio)
  - [Visual Studio Code](#visual-studio-code)
  - [JetBrains Rider](#jetbrains-rider)
- [Complete](#complete)

## Setting up development environment

You will need .NET 8 SDK to work on MineJason. To do so, click [this link](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) and select the latest version.

## Setting up Repository

To start development on this repository, your first step is to clone this repository to your own machine.

Obviously, you will need Git for this step. Git is available for all major platforms.

### Installing Git

#### Windows

Git is available in Windows in several ways, but most people uses Git for Windows, a compiled package of Git and relevant GNU tools bundled in a single package. It is available [here](https://gitforwindows.org); or, if you use Windows 10 or later, open Command Prompt or PowerShell and enter the following command:

```batch
winget install git
```

Git for Windows is also available in the Visual Studio Installer.

Other than the package described above other people also uses other forms of Git on Windows:

- Git from MSYS2
- Git from Cygwin
- Git in Windows Subsystem for Linux
- and so on...

We recommend Git for Windows.

#### GNU/Linux

In your package manager the Git should be available as a package named `git`. If it is not, see [the official guide](https://git-scm.com/download/linux).

#### macOS

Git should be available as package `git` in Homebrew and MacPorts. If you have Xcode, it comes with Git as well.

See the [official guide](https://git-scm.com/download/mac).

### Cloning Repository

| ❕ NOTE                                                                                                                                                                                       |
| -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| The GitHub repository is a read-only mirror of the [primary repository on GitLab.com](https://gitlab.com/WithLithum/MineJason). Do not use it to submit contributions or use it as upstream. |

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
