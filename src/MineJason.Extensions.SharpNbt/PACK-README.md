# MineJason.Extensions.SharpNbt

[![Discord](https://img.shields.io/discord/1178887806286823424?style=flat-square&logo=discord&logoColor=white&label=%20&color=blue)](https://discord.gg/UFfWb9Rj)

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

Please report issues in the [issue tracker](https://codeberg.org/WithLithum/MineJason/issues).

## Thanks

- Thanks JetBrains for providing a licence of their tools for open source development. The ReSharper VS extension is used in the development of this library.
- Thanks you for your interest on this library.

