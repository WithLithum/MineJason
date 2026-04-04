// SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026
// SPDX-License-Identifier: Apache-2.0

using MineJason.Text.Behaviour.Click;

namespace MineJason.Tests.Client.Models;

using System.Text.Json;
using MineJason.Dialogs;
using MineJason.Events;
using MineJason.Serialization.IO.Json;
using MineJason.Serialization.Schema;

public class DialogTests
{
    [Fact]
    public void DialogActionSchema_StaticAction_EncodeCorrectly()
    {
        // Arrange
        var action = new StaticDialogAction(new RunCommandClickEvent("command"));
        var encoder = new JsonNodeEncoder();

        // Act
        var result = DialogActionSchema.Instance.Encode(action, encoder);

        // Assert
        Assert.Null(result.Error);
        Assert.Equal("{\"type\":\"run_command\",\"command\":\"command\"}", result.Value!.ToJsonString());
    }

    [Fact]
    public void DialogActionSchema_StaticAction_DecodeCorrectly()
    {
        // Arrange
        const string json = "{\"type\":\"run_command\",\"command\":\"command\"}";
        var encoder = new JsonElementDecoder();
        var element = JsonDocument.Parse(json);

        // Act
        var result = DialogActionSchema.Instance.Decode(element.RootElement, encoder);

        // Assert
        Assert.Null(result.Error);
        Assert.Equal(new StaticDialogAction(new RunCommandClickEvent("command")), result.Value!);
    }

    [Theory]
    [InlineData("{\"type\":\"minecraft:text\",\"key\":\"key\",\"label\":{\"type\":\"text\",\"text\":\"Hello World!\"}}")]
    [InlineData("{\"type\":\"minecraft:text\",\"key\":\"key\",\"label\":{\"type\":\"text\",\"text\":\"Hello World!\"},\"width\":100,\"label_visible\":false,\"max_length\":10}")]
    [InlineData("{\"type\":\"minecraft:boolean\",\"key\":\"key\",\"label\":{\"type\":\"text\",\"text\":\"Hello World!\"}}")]
    [InlineData("{\"type\":\"minecraft:boolean\",\"key\":\"key\",\"label\":{\"type\":\"text\",\"text\":\"Hello World!\"},\"initial\":true,\"on_true\":\"yes\",\"on_false\":\"no\"}")]
    [InlineData("{\"type\":\"minecraft:single_option\",\"key\":\"key\",\"label\":{\"type\":\"text\",\"text\":\"Hello World!\"},\"options\":[{\"id\":\"ID\",\"display\":{\"type\":\"text\",\"text\":\"Option\"},\"initial\":true},{\"id\":\"Situ\",\"display\":{\"type\":\"text\",\"text\":\"Rapid\"},\"initial\":false}]}")]
    [InlineData("{\"type\":\"minecraft:number_range\",\"key\":\"key\",\"label\":{\"type\":\"text\",\"text\":\"Hello World!\"},\"start\":1,\"end\":100}")]
    [InlineData("{\"type\":\"minecraft:number_range\",\"key\":\"key\",\"label\":{\"type\":\"text\",\"text\":\"Hello World!\"},\"width\":100,\"start\":1,\"end\":100,\"step\":10,\"initial\":5}")]
    public void DialogInputSchema_EndToEndJson_EncodeAndDecodesCorrectly(string json)
    {
        // Arrange
        var encoder = new JsonNodeEncoder();
        var decoder = new JsonElementDecoder();

        var element = JsonDocument.Parse(json);

        // Act
        var stageA = DialogInputControlSchema.Instance.Decode(element.RootElement,
            decoder);
        var result = DialogInputControlSchema.Instance.Encode(stageA, encoder);

        // Assert
        Assert.Null(result.Error);
        Assert.Equal(json, result.Value!.ToJsonString());
    }

    [Theory]
    [InlineData("{\"type\":\"minecraft:notice\",\"title\":{\"type\":\"text\",\"text\":\"Yeet\"}}")]
    [InlineData("{\"type\":\"minecraft:notice\",\"title\":{\"type\":\"text\",\"text\":\"Yeet\"},\"body\":[{\"type\":\"minecraft:plain_message\",\"contents\":{\"type\":\"text\",\"text\":\"Hello World!\"}}]}")]
    [InlineData("{\"type\":\"minecraft:confirmation\",\"title\":{\"type\":\"text\",\"text\":\"Yeet\"},\"yes\":{\"label\":{\"type\":\"text\",\"text\":\"Label\"}},\"no\":{\"label\":{\"type\":\"text\",\"text\":\"Label\"}}}")]
    [InlineData("{\"type\":\"minecraft:multi_action\",\"title\":{\"type\":\"text\",\"text\":\"Yeet\"},\"actions\":[{\"label\":{\"type\":\"text\",\"text\":\"Label\"}},{\"label\":{\"type\":\"text\",\"text\":\"Label\"}}]}")]
    [InlineData("{\"type\":\"minecraft:multi_action\",\"title\":{\"type\":\"text\",\"text\":\"Yeet\"},\"actions\":[{\"label\":{\"type\":\"text\",\"text\":\"Label\"}},{\"label\":{\"type\":\"text\",\"text\":\"Label\"}}],\"columns\":5}")]
    [InlineData("{\"type\":\"minecraft:server_links\",\"title\":{\"type\":\"text\",\"text\":\"Yeet\"}}")]
    [InlineData("{\"type\":\"minecraft:server_links\",\"title\":{\"type\":\"text\",\"text\":\"Yeet\"},\"columns\":5}")]
    public void Dialog_EndToEndJson_EncodeAndDecodesCorrectly(string json)
    {
        // Arrange
        var encoder = new JsonNodeEncoder();
        var decoder = new JsonElementDecoder();

        var element = JsonDocument.Parse(json);

        // Act
        var stageA = DialogSchema.Instance.Decode(element.RootElement,
            decoder);
        var result = DialogSchema.Instance.Encode(stageA, encoder);

        // Assert
        if (result.Error != null)
        {
            Assert.Fail(result.Error);
        }
        Assert.Equal(json, result.Value!.ToJsonString());
    }

    [Theory]
    [InlineData("\"foo:bar\"")]
    [InlineData("\"#foo:tag\"")]
    [InlineData("[\"foo:bar\",\"bar:foo\"]")]
    [InlineData("{\"type\":\"minecraft:server_links\",\"title\":{\"type\":\"text\",\"text\":\"Yeet\"}}")]
    [InlineData("[\"foo:bar\",{\"type\":\"minecraft:server_links\",\"title\":{\"type\":\"text\",\"text\":\"Yeet\"}}]")]
    public void DialogList_EndToEndJson_EncodeAndDecodesCorrectly(string json)
    {
        // Arrange
        var encoder = new JsonNodeEncoder();
        var decoder = new JsonElementDecoder();

        var element = JsonDocument.Parse(json);

        // Act
        var stageA = DialogSourceSchema.MultiInstance.Decode(element.RootElement,
            decoder);
        var result = DialogSourceSchema.MultiInstance.Encode(stageA, encoder);

        // Assert
        Assert.Null(result.Error);
        Assert.Equal(json, result.Value!.ToJsonString());
    }
}