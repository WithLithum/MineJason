#!/bin/sh
# SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
# SPDX-License-Identifier: Apache-2.0

execute_pack() {
    dotnet pack "$PWD/src/$1/$1.csproj" --configuration Release --no-build --include-symbols --output "$PWD/bin"
    pack_success=$?

    if [ $pack_success -ne '0' ]; then
        exit 1
    fi
}

execute_pack "MineJason"
execute_pack "MineJason.Serialization"
execute_pack "MineJason.Serialization.fNbt"
execute_pack "MineJason.SNbt"