# MineJason

[![GitLab Last Commit](https://img.shields.io/gitlab/last-commit/WithLithum%2FMineJason?style=flat-square)](https://gitlab.com/WithLithum/MineJason/-/commits/trunk?ref_type=heads)
[![AppVeyor Build](https://img.shields.io/appveyor/build/WithLithum/minejason?style=flat-square&logo=appveyor&logoColor=white&label=%20)](https://ci.appveyor.com/project/WithLithum/minejason)
[![Discord](https://img.shields.io/discord/1178887806286823424?style=flat-square&logo=discord&logoColor=white&label=%20&color=blue)](https://discord.gg/UFfWb9Rj)
[![QQ](https://img.shields.io/badge/qq%20group-join-blue?style=flat-square
)](https://qm.qq.com/cgi-bin/qm/qr?k=reIRa9w7-vMBemqim7NdREX7vNKirNFo&jump_from=webapi&authKey=UnyZ5LWlfV8g8VCEffm2CShHd9PVPHP5CaXVbxkF2wwZj6FtXGEU/M7jRbU4e/K2)

MineJason is a .NET library that provides serialization compatible models for chat component (Raw JSON text format) in Minecraft: Java Edition.

## Usage

### Serialize and deserialize

From version 0.3.1-alpha onwards, you serialize and deserialize the `ChatComponent` types normally like how you would serialize other types.

The `ChatComponent.SerializerOptions` is deprecated and removed. Please do not use it. The `JsonComponentSerializer` class is also deprecated.

Please do not use `Newtonsoft.Json`, as it is not currently supported. Instead, use `System.Text.Json`.

### Creating components

`Create` methods in `ChatComponent` class allows you to create various types of components.

## Issues

Please report issues in the [issue tracker](https://gitlab.com/WithLithum/MineJason/issues).

## Thanks

- Thanks JetBrains for providing a licence of their tools for open source development. The ReSharper VS extension is used in the development of this library.
- Thanks you for your interest on this library.
