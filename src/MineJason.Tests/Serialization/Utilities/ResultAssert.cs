// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Serialization.Utilities.Results;

namespace MineJason.Tests.Serialization.Utilities;

public static class ResultAssert
{
    public static T? Success<T>(Result<T> result)
    {
        if (result.Error != null)
        {
            Assert.Fail($"Errored data result: '{result.Error}'");
        }

        return result.Value;
    }

    public static void Success(Result result)
    {
        if (result.Error != null)
        {
            Assert.Fail($"Errored data result: '{result.Error}'");
        }
    }

    public static T NotEmpty<T>(Result<T> result)
    {
        if (result.Error != null)
        {
            Assert.Fail($"Errored data result: '{result.Error}'");
        }

        return result.Value!;
    }

    public static void Failure(Result result)
    {
        if (result.Error == null)
        {
            Assert.Fail("Expected failure operation result but found successful one");
        }
    }

    public static void Failure<T>(Result<T> result)
    {
        if (result.Error == null)
        {
            Assert.Fail("Expected failure data result but found successful one");
        }
    }
}