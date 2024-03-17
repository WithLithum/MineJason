// Copyright (c) WithLithum & contributors 2023-2024. All rights reserved.
// Licensed under the GNU Lesser General Public License, either version 3 or
// (at your opinion) any later version.

namespace MineJason.Items.Buckets;

using JetBrains.Annotations;

/// <summary>
/// Represents a component that sets a limited set of tags for entities in a bucket.
/// </summary>
[PublicAPI]
public class BucketEntityDataItemComponent : ObjectItemComponent<BucketEntityData>
{
    /// <summary>
    /// The registered type of this data component.
    /// </summary>
    public static readonly ResourceLocation Type = 
        new ResourceLocation("minecraft", "bucket_entity_data");
    
    /// <summary>
    /// Initialises a new instance of the <see cref="BucketEntityDataItemComponent"/> class.
    /// </summary>
    /// <param name="value">The data.</param>
    public BucketEntityDataItemComponent(BucketEntityData value) : base(value)
    {
    }
}