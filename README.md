# MineJason

[![Discord](https://img.shields.io/discord/1178887806286823424?style=flat-square&logo=discord&logoColor=white&label=%20&color=blue)](https://discord.gg/UFfWb9Rj)
[![Nuget](https://img.shields.io/nuget/v/MineJason?style=flat-square&logo=nuget&label=%20)](https://www.nuget.org/packages/MineJason)

MineJason is a .NET library that provides serialization compatible models for chat component (Raw JSON text format) in Minecraft: Java Edition.

## Usage

### Installation

Use our NuGet package (click the status badge/icon to get to the package page!)

| Name                          | Status & Link                                                                                                                                                                 |
|-------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| MineJason                     | [![Nuget](https://img.shields.io/nuget/v/MineJason?style=flat-square&logo=nuget&label=%20)](https://www.nuget.org/packages/MineJason)                                         |
| MineJason.Extensions.SharpNbt | [![Nuget](https://img.shields.io/nuget/v/MineJason.Extensions.SharpNbt?style=flat-square&logo=nuget&label=%20)](https://www.nuget.org/packages/MineJason.Extensions.SharpNbt) |

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

Please report issues in the [issue tracker](https://codeberg.org/WithLithum/MineJason/issues).

## Contributing

Pull requests are welcome. For major changes, please open an issue to discuss what you would like to change first.

Please make sure to update tests as appropriately, and don't forget to check the conventions of this project written in [the Contributing guide](CONTRIBUTING.md).

## Thanks

- Thanks JetBrains for providing a licence of their tools for open source development. The ReSharper VS extension is used in the development of this library.
- Thanks you for your interest on this library.

## License

The libraries are available under the [GNU Lesser General Public License](COPYING.LESSER.txt), either version 3 of the license, or (at your opinion) any later version.

Please also see the license of [other libraries that this project links to](ACKNOWLEDGEMENTS.txt).

The SPDX License ID of this project is `LGPL-3.0-or-later`.
