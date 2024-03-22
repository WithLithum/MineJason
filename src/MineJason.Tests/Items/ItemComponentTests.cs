// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Tests.Items;

using MineJason.Items.Foods;

public class ItemComponentTests
{
    [Test]
    public void FoodItemComponent_Serialize()
    {
        var component = new FoodItemComponent(new FoodInfo()
        {
            SaturationModifier = 7.5f,
            Nutrition = 10
        });
        
        Assert.That(component.GetString(), Is.EqualTo("{nutrition:10,saturation_modifier:7.5f}"));
    }
}