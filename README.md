# MineJason

[![Discord](https://img.shields.io/discord/1178887806286823424?style=flat-square&logo=discord&logoColor=white&label=%20&color=blue)](https://discord.gg/UFfWb9Rj)
[![Nuget](https://img.shields.io/nuget/v/MineJason?style=flat-square&logo=nuget&label=%20)](https://www.nuget.org/packages/MineJason)

MineJason is a .NET library that provides serialization compatible models for chat component (Raw JSON text format) in Minecraft: Java Edition.

## Usage

### Installation

Use our NuGet package (click the status badge/icon to get to the package page!)

| Name                          | Status & Link |
| ----------------------------- | ------------- |
| MineJason                     | [![Nuget](https://img.shields.io/nuget/v/MineJason?style=flat-square&logo=nuget&label=%20)](https://www.nuget.org/packages/MineJason) |
| MineJason.Extensions.SharpNbt | [![Nuget](https://img.shields.io/nuget/v/MineJason.Extensions.SharpNbt?style=flat-square&logo=nuget&label=%20)](https://www.nuget.org/packages/MineJason.Extensions.SharpNbt) |

### Serialize and deserialize

To serialize or deserialize chat components, make sure to use `ChatComponent.SerializerOptions`, so that your serialized output conforms to Minecraft: Java Edition format.

```csharp
// Use the serializer options!

JsonConverter.Serialize(component, ChatComponent.SerializerOptions);
JsonConverter.Deserialize(json, ChatComponent.SerializerOptions);
```

### Creating components

`Create` methods in `ChatComponent` class allows you to create various types of components.

## Issues

Please report issues in the [issue tracker](https://codeberg.org/WithLithum/MineJason/issues).

## Contributing

Pull requests are welcome. For major changes, please open an issue to discuss what you would like to change first.

Please make sure to update tests as appropriately, and don't forget to check the conventions of this project written in [the Contributing guide](CONTRIBUTING.md).

## Thanks

- Thanks JetBrains for providing a licence of their tools for open source development. The ReSharper VS extension is used in the development of this library.
- Thanks you for your interest on this library.

## License

[LGPL-3.0-or-later](COPYING.LESSER.txt)
