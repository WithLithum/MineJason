# 贡献者指南

简体中文 | [English](docs/CONTRIBUTING-en.md)

感谢您愿意为本项目变得更好而付出时间和精力。在开始之前请先阅读本项目的[行为规范](CODE_OF_CONDUCT.md)——这个文档是本项目的管理规定，用来保证本项目始终是包容且开放的社区。

本指南将指导如何作出从报告问题到提交代码合并请求来改进项目等一系列贡献。您可以点击本文档网页端显示区域的左上角的“目录”按钮来快速浏览到某个标题处。

| :information: 注意                                            |
| ----------------------------------------------------------- |
| 本文档可能与[英文版](docs/CONTRIBUING-en.md)有所出入。如果英文版与中文版冲突，以中文版为准。 |

## 重要事项

如果您是从 GitHub 上看到本项目，则需要注意本条。

GitHub 的 MineJason 存储库只是一个只读的镜像，并且不直接接受贡献。如果您在 GitHub 上新建拉取请求（Pull Request），则十有八九会被忽略，所以请不要在 GitHub 上提交贡献。

请您使用 [GitLab 合并请求](https://gitlab.com/WithLithum/MineJason/-/merge-requests)和 [GitLab 议题](https://gitlab.com/WithLithum/MineJason/-/issues)功能。感谢您的配合。

## 有关于使用上的提问？

议题区不应用来提交一般问题。如有关于如何使用 MineJason 以及其它一般疑问，请先查看 [Wiki](https://gitlab.com/WithLithum/MineJason/-/wikis/home)。如果 Wiki 仍不能解答您的疑问，则请前往下列区域讨论：

- Discord 服务器：[点击加入](https://discord.gg/UFfWb9Rj)
- QQ群：[点击加入](https://qm.qq.com/cgi-bin/qm/qr?k=reIRa9w7-vMBemqim7NdREX7vNKirNFo&jump_from=webapi&authKey=UnyZ5LWlfV8g8VCEffm2CShHd9PVPHP5CaXVbxkF2wwZj6FtXGEU/M7jRbU4e/K2)

## 议题

### 在新建议题之前

请先在议题区中搜索*开放中*的议题，以寻找是否有符合您所要报告的问题的议题。请勿搜索整个问题，而是先搜索您要描述的问题的关键字（比如，如果是目标选择器的类型有问题，则搜索“选择器”）。为了防止有英语的议题已经打开，请尽量再用英文搜索一遍。如果一个议题重复了之前的议题所提交的问题，那么就会被关闭。

但是，如果出现了之前已经修复但是后来版本又出现的问题，那么就算是新的问题。

### 在写议题时

请尽量使用与您想要新建议题讨论的内容相符合的模板，且尽量使用标准中文或者标准英文（切勿使用粤语吴语臺語等方言、注音符號，而且一些网络用语/網路用語也要少用），并且切勿混杂多种语言。

为了方便，提供了中文和英文版本的各种议题模板。但是，请勿以如使用英文模板写中文正文的方式写议题正文。

如果您写议题是为了告诉我们您要以此作出贡献时，请明确写清。但是，如果您写好议题提交后有相当时间内都没有提交合并请求，那么议题就会被再次开放，由维护者或其它贡献者接手。

下方列出了各种类型的议题的说明，请务必遵守。

#### 漏洞报告

请尽量描述清楚：

- 您觉得正常情况下应该发生什么
- 实际上发生了什么
- 重现该问题的步骤

如果是 API 问题的话，那么我们建议您提交一份示例代码来演示该问题。请为各个问题各提交一个议题；切勿一个议题报告多个漏洞。

#### 功能请求

请尽量描述清楚：

- 您希望添加的功能是什么？做什么用？
- 您认为该功能在何种情况下会用到？
- 在没有该功能的情况下，有没有其它方法代替？

功能请求会被审查并且决定是否实现。但请注意，如果该功能的用途不符合本项目的预期用途，则可能会被退回；见[预期用途](https://gitlab.com/WithLithum/MineJason/-/wikis/English/Development/Intended-Use-Case)。请为您要请求的各个功能各提交一个议题；切勿一个议题包含多个功能请求。

如果功能请求包含破坏性变更，则可能会被退回或者考虑在下个大版本再加入。

#### 改进请求

请在提交改进请求时尽量描述清楚：您要请求什么改进？

改进请求一般应避免出现破坏性变更。如果需要作出破坏性变更，那么要么这个改进需要到下个大版本实现，要么会被退回。

### 在提交议题后

请记住各个议题将会按照其优先级和严重程度被决定处理的顺序。严重的漏洞一般会被优先处理。

## 提交代码贡献

一般来说，向项目提交代码贡献并不难。但是，本项目对提交代码贡献有一些小要求，请注意遵循这里的指导。

注意：提交代码贡献即代表您同意遵守 [Developer Certificate of Origin](#developer-certificate-of-origin)。尽管提交详细信息中写上 `Signed-off-by` 并非绝对必要，但是我们仍然建议您写上 `Signed-off-by`。这样，我们就能知道您明确同意遵守 Developer Certificate of Origin。

### 预期用途

本库的预期用途是：为编程处理与 Minecraft：Java 版配合使用的“原始JSON文本”格式文本组件提供支持。本项目可能支持其它用途，但并不会积极对其提供新的功能；见[预期用途一页](https://gitlab.com/WithLithum/MineJason/-/wikis/中文/开发/预期用途)。

### 该做什么和不该做什么

请在编写代码及参与讨论时注意以下事项：

- **遵循**本项目的[样式指南](docs/STYLEGUIDE.md)。
- 新的代码一般情况下**应当**基于现有代码的样式编写，即使现有样式与指南有所出入。
- 在增加新功能时**应当**同步增加单元测试代码。如果您要修复漏洞，可以先增加有关于漏洞的单元测试（即，该漏洞存在则失败，该漏洞修复则成功的单元测试）。
- 注意**防止**讨论区跑题。如果一个新的问题反复被讨论的话，一般最好还是创建一个新的议题讨论这个问题，而不是让别的议题跑题。
- 如创建议题是要表明您要实现该议题，则**应当**明确说明。

并且：

- **切勿**因为代码样式的问题就开合并请求。
- **不要**突然就提交超大合并请求。如果您要进行重大改动，在投入时间和精力之前，请先通过议题讨论，以及评估这种改动是否合适。
- **切勿**提交修改许可证相关文件的合并请求。如果您认为许可证相关事项有问题，请先通过议题讨论。

### 破坏性变更

本项目可以接受带有破坏性变更的合并请求，但：

- 只会在下一个大版本被正式发布（在 `0.x` 时则可以立即发布）
- 破坏性变更必须审核
- 只有创建议题讨论并审核通过后才可以提交合并请求

### 提交合并请求

在您第一次作为新手提交合并请求之前，请先阅读 [GitLab 的合并请求指南](https://docs.gitlab.com/ee/user/project/merge_requests/creating_merge_requests.html)。

如果您的合并请求还没有准备好接受审核并被合并，请在创建时勾选[标记为草稿](https://docs.gitlab.com/ee/user/project/merge_requests/drafts.html)。请您注意：不要突然就提交超大合并请求。如果您要进行重大改动，在投入时间和精力之前，请先通过议题讨论，以及评估这种改动是否合适，以免您耗费大量时间精力但是由于没有讨论，改动不合适，被直接退回而浪费。

## Developer Certificate of Origin

当您：

- 以补丁、合并请求或其它方式提交代码贡献，或
- 在提交信息中写明 `Signed-off-by` 与您的电子邮箱地址，

即表明同意 Developer Certificate of Origin：

```plain
Developer Certificate of Origin
Version 1.1

Copyright (C) 2004, 2006 The Linux Foundation and its contributors.

Everyone is permitted to copy and distribute verbatim copies of this
license document, but changing it is not allowed.


Developer's Certificate of Origin 1.1

By making a contribution to this project, I certify that:

(a) The contribution was created in whole or in part by me and I
    have the right to submit it under the open source license
    indicated in the file; or

(b) The contribution is based upon previous work that, to the best
    of my knowledge, is covered under an appropriate open source
    license and I have the right under that license to submit that
    work with modifications, whether created in whole or in part
    by me, under the same open source license (unless I am
    permitted to submit under a different license), as indicated
    in the file; or

(c) The contribution was provided directly to me by some other
    person who certified (a), (b) or (c) and I have not modified
    it.

(d) I understand and agree that this project and the contribution
    are public and that a record of the contribution (including all
    personal information I submit with it, including my sign-off) is
    maintained indefinitely and may be redistributed consistent with
    this project or the open source license(s) involved.
```
