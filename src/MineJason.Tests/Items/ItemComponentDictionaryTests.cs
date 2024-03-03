// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Tests.Items;

using System.Diagnostics.CodeAnalysis;
using MineJason.Items;

[SuppressMessage("ReSharper", "CollectionNeverUpdated.Local",
    Justification = "This is a test. The collections are thrown away afterwards.")]
[SuppressMessage("ReSharper", "CollectionNeverQueried.Local",
    Justification = "This is a test. The collections are thrown away afterwards.")]
public class ItemComponentDictionaryTests
{
    [Test]
    public void Indexer_Get_DontFailWhenNotExists()
    {
        var dictionary = new ItemComponentDictionary();
        
        Assert.DoesNotThrow(() => _ = dictionary[DamageItemComponent.Type]);
    }
    
    [Test]
    public void Indexer_Set_SuccessWhenTypeMismatch()
    {
        var dictionary = new ItemComponentDictionary();
        
        Assert.DoesNotThrow(() => dictionary[DamageItemComponent.Type] =
            new ItemComponentCollection(DamageItemComponent.Type));
    }
    
    [Test]
    public void Indexer_Set_FailsWhenCollectionTypeMismatch()
    {
        var dictionary = new ItemComponentDictionary();
        
        Assert.Throws<ArgumentException>(() => dictionary[DamageItemComponent.Type] =
            new ItemComponentCollection(EnchantmentGlintOverrideItemComponent.Type));
    }
    
    [Test]
    public void Add_SuccessWhenTypeMatch()
    {
        var dictionary = new ItemComponentDictionary();
        
        Assert.DoesNotThrow(() => dictionary.Add(DamageItemComponent.Type,
            new ItemComponentCollection(DamageItemComponent.Type)));
    }
    
    [Test]
    public void Add_FailsWhenCollectionTypeMismatch()
    {
        var dictionary = new ItemComponentDictionary();
        
        Assert.Throws<ArgumentException>(() => dictionary.Add(DamageItemComponent.Type,
            new ItemComponentCollection(EnchantmentGlintOverrideItemComponent.Type)));
    }
}