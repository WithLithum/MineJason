# MineJason.Extensions.SharpNbt

[![GitLab Last Commit](https://img.shields.io/gitlab/last-commit/WithLithum%2FMineJason?style=flat-square)](https://gitlab.com/WithLithum/MineJason/-/commits/trunk?ref_type=heads)
[![AppVeyor Build](https://img.shields.io/appveyor/build/WithLithum/minejason?style=flat-square&logo=appveyor&logoColor=white&label=%20)](https://ci.appveyor.com/project/WithLithum/minejason)
[![Discord](https://img.shields.io/discord/1178887806286823424?style=flat-square&logo=discord&logoColor=white&label=%20&color=blue)](https://discord.gg/UFfWb9Rj)
[![QQ](https://img.shields.io/badge/qq%20group-join-blue?style=flat-square
)](https://qm.qq.com/cgi-bin/qm/qr?k=reIRa9w7-vMBemqim7NdREX7vNKirNFo&jump_from=webapi&authKey=UnyZ5LWlfV8g8VCEffm2CShHd9PVPHP5CaXVbxkF2wwZj6FtXGEU/M7jRbU4e/K2)

This package provides SharpNbt-related extension methods as an accessory to MineJason.

## Usage

### Creating MineJason NBT data

```csharp
var listProvider = listTag.ToMineJason();
var compoundProvider = compoundTag.ToMineJason();
```

### Creating SharpNbt NBT data

```csharp
var listTag = listProvider.ToListTag();
var compoundTag = compoundProvider.ToCompoundTag();
```

## Issues

Please report issues in the [issue tracker](https://gitlab.com/WithLithum/MineJason/issues) of this library.

## Thanks

- Thanks JetBrains for providing a licence of their tools for open source development. The ReSharper VS extension is used in the development of this library.
- Thanks you for your interest on this library.
