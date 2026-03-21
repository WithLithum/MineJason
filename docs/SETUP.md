# Setting up development environment

MineJason is a fairly straightforward project that does not need a special setup, other than (obviously) installing .NET.

## Choosing a version of .NET

MineJason _requires_ and targets .NET 10, and this is specified in the `global.json` file.

The way of how you would install .NET on your computer depends on what operating system you use.

> [!NOTE]
> Microsoft builds of .NET [collects telemetry data by default](https://learn.microsoft.com/dotnet/core/tools/telemetry), which some of those data are [published](https://dotnet.microsoft.com/en-us/platform/telemetry) on a 90-day basis.
>
> To disable telemetry, set `DOTNET_CLI_TELEMETRY_OPTOUT` to 1.

### Windows

If you have Visual Studio with the appropriate workload, it might already have been installed on your computer. Check this by running:

```pwsh
dotnet --version
```

If you do not have .NET 10 installed, [download the official build](https://dotnet.microsoft.com/en-us/download) and install it like any other Windows application.

You can also use package managers:

- **WinGet (Windows Package Manager)**: `winget install "Microsoft.DotNet.SDK.10"` ([More information](https://learn.microsoft.com/en-us/dotnet/core/install/windows#install-with-windows-package-manager-winget))
- **Scoop**: `scoop install main/dotnet-sdk`

Beware that Scoop installs .NET [differently](https://github.com/ScoopInstaller/Scoop/wiki/So-What#but-i-already-use-x-why-should-i-use-scoop) than how apps would be normally installed on Windows. 

### Linux

First, check if your distribution packages .NET.

The following distributions are currently known to package .NET:

- [Alpine Linux](https://learn.microsoft.com/en-us/dotnet/core/install/linux-alpine?tabs=dotnet10)
- Arch Linux and derivatives (e.g. Manjaro)
- CentOS Stream
- [Fedora](https://learn.microsoft.com/en-us/dotnet/core/install/linux-fedora?tabs=dotnet10)
- NixOS
- [RHEL](https://learn.microsoft.com/en-us/dotnet/core/install/linux-rhel) and derivatives (e.g. Alma, Rocky, etc.)
- [Ubuntu](https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu-install)

In addition, Microsoft also [offers .NET packages in the Microsoft Package Repository](https://learn.microsoft.com/en-us/dotnet/core/install/linux) for the following distributions:

- Azure Linux
- [Debian](https://learn.microsoft.com/en-us/dotnet/core/install/linux-debian?tabs=dotnet10)
- openSUSE Leap
- SUSE Enterprise Linux

Using your distribution package manager is the best way to install .NET on your Linux system. Consult the documentation of your Linux distribution on how to use the package manager.

If your Linux distribution isn't listed above, or the available version(s) isn't .NET 10, you will need to manually install .NET. 

## IDE

You will most likely need an IDE to develop MineJason efficiently.

There are multiple kinds of IDEs available for .NET. The universal solution is Visual Studio Code (_not including_ derivatives) with C# Dev Kit, which allows you to use some Visual Studio features.

On Windows, Visual Studio or Rider are recommended and are both free for non-commercial use. However, Visual Studio comes with more free features.

On other operating systems, beside Visual Studio Code, you can also use Code derivatives with either the C# extension with Samsung debugger, or ReSharper.

## Using the Cloud

You can also attempt to use GitHub Codespaces or similar solutions, which is a cloud VM that you can use with relevant developer tools.

<!-- Put this here because this breaks KIO -->
<!-- SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026 -->
<!-- SPDX-License-Identifier: Apache-2.0 -->