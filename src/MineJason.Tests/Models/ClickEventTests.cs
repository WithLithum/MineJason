// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Tests.Models;

using System.Text.Json;
using MineJason.Events;

public class ClickEventTests
{
    [Test]
    public void ChangePageClickEvent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize((ClickEvent)(new ChangePageClickEvent(12))),
            Is.EqualTo("{\"action\":\"change_page\",\"value\":12}"));
    }
    
    [Test]
    public void ChangePageClickEvent_Deserialize()
    {
        Assert.That(JsonSerializer.Deserialize<ClickEvent>("{\"action\":\"change_page\",\"value\":12}"),
            Is.EqualTo(new ChangePageClickEvent(12)));
    }
    
    [Test]
    public void CopyToClipboardClickEvent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize((ClickEvent)(new CopyToClipboardClickEvent("clipboard copy"))),
            Is.EqualTo("{\"action\":\"copy_to_clipboard\",\"value\":\"clipboard copy\"}"));
    }
    
    [Test]
    public void CopyToClipboardClickEvent_Deserialize()
    {
        Assert.That(JsonSerializer.Deserialize<ClickEvent>("{\"action\":\"copy_to_clipboard\",\"value\":\"clipboard copy\"}"),
            Is.EqualTo(new CopyToClipboardClickEvent("clipboard copy")));
    }
    
    [Test]
    public void OpenUrlClickEvent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize((ClickEvent)(new OpenUrlClickEvent(new Uri("https://minecraft.net")))),
            Is.EqualTo("{\"action\":\"open_url\",\"value\":\"https://minecraft.net\"}"));
    }
    
    [Test]
    public void OpenUrlClickEvent_Deserialize()
    {
        Assert.That(JsonSerializer.Deserialize<ClickEvent>("{\"action\":\"open_url\",\"value\":\"https://minecraft.net\"}"),
            Is.EqualTo(new OpenUrlClickEvent(new Uri("https://minecraft.net"))));
    }

    [Test]
    public void RunCommandClickEvent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize((ClickEvent)(new RunCommandClickEvent("effect clear"))),
            Is.EqualTo("{\"action\":\"run_command\",\"value\":\"effect clear\"}"));
    }
    
    [Test]
    public void RunCommandClickEvent_Deserialize()
    {
        Assert.That(JsonSerializer.Deserialize<ClickEvent>("{\"action\":\"run_command\",\"value\":\"effect clear\"}"),
            Is.EqualTo(new RunCommandClickEvent("effect clear")));
    }
    
    [Test]
    public void SuggestCommandClickEvent_Serialize()
    {
        Assert.That(JsonSerializer.Serialize((ClickEvent)(new SuggestCommandClickEvent("effect clear"))),
            Is.EqualTo("{\"action\":\"suggest_command\",\"value\":\"effect clear\"}"));
    }
    
    [Test]
    public void SuggestCommandClickEvent_Deserialize()
    {
        Assert.That(JsonSerializer.Deserialize<ClickEvent>("{\"action\":\"suggest_command\",\"value\":\"effect clear\"}"),
            Is.EqualTo(new SuggestCommandClickEvent("effect clear")));
    }
}