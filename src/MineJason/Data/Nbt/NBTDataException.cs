// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

namespace MineJason.Data.Nbt;

/// <summary>
/// The exception that is thrown when any NBT data is found to be invalid or malformed.
/// </summary>
public class NBTDataException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NBTDataException"/> class.
    /// </summary>
    public NBTDataException()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="NBTDataException"/> with the specified message.
    /// </summary>
    /// <param name="message">The message.</param>
    public NBTDataException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="NBTDataException"/> with a specified message and a
    /// reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The message,</param>
    /// <param name="innerException">The inner exception.</param>
    public NBTDataException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    internal static NBTDataException MissingRequiredTag(string key)
    {
        return new NBTDataException($"Required tag '{key}' is missing.");
    }

    /// <summary>
    /// Returns a new instance of <see cref="NBTDataException"/> with a message describing an error
    /// caused by finding a tag not of an expected type.
    /// </summary>
    /// <param name="type">The expected type.</param>
    /// <param name="found">The actual type.</param>
    /// <returns>A new instance of <see cref="NBTDataException"/>.</returns>
    public static NBTDataException CreateUnexpectedType(string type, string found)
    {
        return new NBTDataException($"Expected tag of '{type}' type but found '{found}'");
    }

    internal static NBTDataException CreateUnsupportedTagTypeForTargetType(byte tagType,
        string targetType)
    {
        return new NBTDataException($"Don't know how to turn tag of type 0x{tagType:x2} to type '{targetType}'");
    }

    internal static NBTDataException CreateUnsupportedObjectType(string objectType)
    {
        return new NBTDataException($"Don't know how to turn type '{objectType}' to NBT.");
    }

    internal static NBTDataException CreateNonMinecraft()
    {
        return new NBTDataException("Non-vanilla types are not supported.");
    }
}
