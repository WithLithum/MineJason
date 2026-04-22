<div style="text-align: center;">
<img alt="Logo" src="assets/logo-128x.png" />
<h1>MineJason</h1>
</div>

[![FOSSA Status](https://app.fossa.com/api/projects/custom%2B61921%2Fgithub.com%2FWithLithum%2FMineJason.svg?type=shield&issueType=license)](https://app.fossa.com/projects/custom%2B61921%2Fgithub.com%2FWithLithum%2FMineJason?ref=badge_shield&issueType=license)
![GitHub branch check runs](https://img.shields.io/github/check-runs/WithLithum/MineJason/main?style=flat-square&logo=github)
![Codecov](https://img.shields.io/codecov/c/github/WithLithum/MineJason?style=flat-square)

MineJason is a suite of libraries that deals with various formats used in Minecraft: Java Editions.

The suite is primarily consisted of the following "modules":

- [Client](src/text/): Client formats such as text components and dialogs
- [Serialization](src/MineJason.Serialization): Format agnostic
- [NBT](src/MineJason.NBT/): Supports NBT format (originally made for SNBT)

This repository is the consolidated form of the three previously separated projects managed under the same hood.

## Contributing

Please see the [contributing guide](docs/CONTRIBUTING.md) on how to report issues and send code contributions.

## Licence

All code in this repository is licensed under the [Apache-2.0](LICENSE.txt) licence unless otherwise specified.

Note that the Client modules is currently licensed under [LGPL-3.0-or-later](src/MineJason/COPYING.LESSER.txt).

<!-- Put this here because this breaks KIO -->
<!-- SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026 -->
<!-- SPDX-License-Identifier: Apache-2.0 -->