﻿using System.Text.Json;

namespace Domain.ValueTypes;

public class AssetSelectorPattern
{
    public HashSet<string> FolderIds { get; set; } = [];
    public HashSet<string> AssetIds { get; set; } = [];

    public static AssetSelectorPattern? FromJson(string? json)
    {
        return json != null ? JsonSerializer.Deserialize<AssetSelectorPattern>(json) : null;
    }
}