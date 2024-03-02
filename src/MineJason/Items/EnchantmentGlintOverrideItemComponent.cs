// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or 
// (at your opinion) any later version.

namespace MineJason.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents an item component that overrides the default item enchantment glind display.
/// </summary>
/// <remarks>
/// <para>
/// If this component is set to <see langword="true"/>, the enchantment glint will display even if there are no enchantments;
/// otherwise, if set to <see langword="false"/>, the enchantment glint will not render even if there exists enchantments.
/// </para>
/// <para>
/// To retain the default behaviour, omit this component.
/// </para>
/// </remarks>
public sealed class EnchantmentGlintOverrideItemComponent : BooleanItemComponent
{
    /// <summary>
    /// The type ID of this component.
    /// </summary>
    public static readonly ResourceLocation Id = new("minecraft", "enchantment_glint_override");

    /// <summary>
    /// Initialises a new instance of the <see cref="EnchantmentGlintOverrideItemComponent"/> class.
    /// </summary>
    /// <param name="value">The value.</param>
    public EnchantmentGlintOverrideItemComponent(bool value) : base(value)
    {
    }
}
