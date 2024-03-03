// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Tests.Items;

using System.Diagnostics.CodeAnalysis;
using MineJason.Data;
using MineJason.Items;

[SuppressMessage("ReSharper", 
    "CollectionNeverQueried.Local",
    Justification = "This is a test. The collection is thrown away afterwards.")]
public class ItemComponentCollectionTests
{
    [Test]
    public void Indexer_Set_InvalidType()
    {
        var collection = new ItemComponentCollection(CustomDataItemComponent.Type)
        {
            new CustomDataItemComponent(NbtProvider.Empty)
        };

        Assert.Throws<ArgumentException>(() =>
        {
            collection[0] = new DamageItemComponent(250);
        });
    }
    
    [Test]
    public void Add_InvalidType()
    {
        var collection = new ItemComponentCollection(CustomDataItemComponent.Type);

        Assert.Throws<ArgumentException>(() =>
        {
            collection.Add(new DamageItemComponent(250));
        });
    }

    [Test]
    public void Add_ValidType()
    {
        var collection = new ItemComponentCollection(DamageItemComponent.Type);
        
        Assert.DoesNotThrow(() =>
        {
            collection.Add(new DamageItemComponent(101));
        });
    }
}