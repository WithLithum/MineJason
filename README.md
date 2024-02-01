# MineJason

简体中文 | [English](README-en.md)

[![Nuget](https://img.shields.io/nuget/v/MineJason?style=flat-square&logo=nuget&label=%20)](https://www.nuget.org/packages/MineJason)
![GitLab Last Commit](https://img.shields.io/gitlab/last-commit/WithLithum%2FMineJason?tyle=flat-square)
![AppVeyor Build](https://img.shields.io/appveyor/build/WithLithum/minejason?style=flat-square&logo=appveyor&logoColor=white&label=%20)
[![Discord](https://img.shields.io/discord/1178887806286823424?style=flat-square&logo=discord&logoColor=white&label=%20&color=blue)](https://discord.gg/UFfWb9Rj)
[![QQ](https://img.shields.io/badge/qq%20group-join-blue?style=flat-square
)](https://qm.qq.com/cgi-bin/qm/qr?k=reIRa9w7-vMBemqim7NdREX7vNKirNFo&jump_from=webapi&authKey=UnyZ5LWlfV8g8VCEffm2CShHd9PVPHP5CaXVbxkF2wwZj6FtXGEU/M7jRbU4e/K2)

MineJason 提供支持使用 System.Text.Json 序列化和反序列化的原始 JSON 文本格式模型。

## 用法

### 安装

请使用我们提供的 NuGet 包（点击下方版本号图标直达包页面）

| 包名                          | 版本号（链接）                                                                                                                                                                 |
|-------------------------------|-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| MineJason                     | [![Nuget](https://img.shields.io/nuget/v/MineJason?style=flat-square&logo=nuget&label=%20)](https://www.nuget.org/packages/MineJason)                                         |
| MineJason.Extensions.SharpNbt | [![Nuget](https://img.shields.io/nuget/v/MineJason.Extensions.SharpNbt?style=flat-square&logo=nuget&label=%20)](https://www.nuget.org/packages/MineJason.Extensions.SharpNbt) |

### 序列化和反序列化

从 0.3.1-alpha 开始，序列化时可以直接使用默认设置进行序列化，不需要再使用特殊的设置。

`ChatComponent.SerializerOptions` 已被弃用。

目前尚不支持 `Newtonsoft.Json`，请勿使用该库进行序列化；请使用 `System.Text.Json`。

### 创建文本组件

`ChatComponent` 类中 `Create` 开头的文本组件支持帮助创建不同的组件。

## 报告问题

请通过[本库的议题区](https://gitlab.com/WithLithum/MineJason/issues)报告问题。

## 贡献代码

您可以通过合并请求的方式向本库提交代码。但如果改动较大，请先创建相关议题讨论。

请同时按需添加或者更新对应的单元测试，并且查看[贡献者指南](CONTRIBUTING.md)中本项目的代码细则。

## 鸣谢

- 感谢 JetBrains 提供其工具的许可证用于开发开源项目。在本库开发中使用了 ReSharper 及 Rider IDE。
- 感谢您对本库的兴趣和支持。

## 许可

本项目以 [GNU LGPL](COPYING.LESSER.txt) 第三版（或者您也可以自行选择适用新版）许可。

请详见[本库使用的其它开源项目的许可证声明](ACKNOWLEDGEMENTS.txt)。

本项目的 SPDX License ID 是 `LGPL-3.0-or-later`。
