# MineJason

[![Discord](https://img.shields.io/discord/1178887806286823424?style=flat-square&logo=discord&logoColor=white&label=%20&color=blue)](https://discord.gg/UFfWb9Rj)

MineJason is a .NET library that provides serialization compatible models for chat component (Raw JSON text format) in Minecraft: Java Edition.

## Usage

### Serialize and deserialize

To serialize or deserialize chat components, make sure to use `ChatComponent.SerializerOptions`, so that your serialized output conforms to Minecraft: Java Edition format.

```csharp
// Use the serializer options!

JsonConverter.Serialize(component, ChatComponent.SerializerOptions);
JsonConverter.Deserialize(json, ChatComponent.SerializerOptions);
```

### Creating components

`Create` methods in `ChatComponent` class allows you to create various types of components.

## License

[LGPL-3.0-or-later](COPYING.LESSER.txt)
