# MineJason

[![Nuget](https://img.shields.io/nuget/v/MineJason?style=flat-square&logo=nuget&label=%20)](https://www.nuget.org/packages/MineJason)
![GitLab Last Commit](https://img.shields.io/gitlab/last-commit/WithLithum%2FMineJason?gitlab_url=https%3A%2F%2Fjihulab.com&style=flat-square)
[![Discord](https://img.shields.io/discord/1178887806286823424?style=flat-square&logo=discord&logoColor=white&label=%20&color=blue)](https://discord.gg/UFfWb9Rj)
[![QQ](https://img.shields.io/badge/qq%20group-join-blue?style=flat-square
)](https://qm.qq.com/cgi-bin/qm/qr?k=reIRa9w7-vMBemqim7NdREX7vNKirNFo&jump_from=webapi&authKey=UnyZ5LWlfV8g8VCEffm2CShHd9PVPHP5CaXVbxkF2wwZj6FtXGEU/M7jRbU4e/K2)

MineJason is a .NET library that provides serialization compatible models for chat component (Raw JSON text format) in Minecraft: Java Edition.

## Usage

### Serialize and deserialize

You need to make sure that you use `ChatComponent.SerializerOptions` when serializing or deserializing chat components, so that your serialized output conforms to Minecraft: Java Edition format.

To make it easier, we have provided `JsonComponentSerializer` class which provides a few static methods similar to `JsonSerializer`, but pre-configured to use the specified serializer options:

```csharp
// Use our custom serializers!
using MineJason.Serialization;

JsonComponentSerializer.Serialize(component);
JsonComponentSerializer.Deserialize(json);
```

If you prefer to do it yourself then you must use or respect all options defined in `ChatComponent.SerializerOptions`!

### Creating components

`Create` methods in `ChatComponent` class allows you to create various types of components.

## Issues

Please report issues in the [issue tracker](https://jihulab.com/WithLithum/MineJason/issues).

## Thanks

- Thanks JetBrains for providing a licence of their tools for open source development. The ReSharper VS extension is used in the development of this library.
- Thanks you for your interest on this library.

