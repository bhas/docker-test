﻿namespace Application.Utilities;
public interface IAssetSelector
{
    HashSet<string> GetAssetIds();
}

public class AssetSelector : IAssetSelector
{
    public HashSet<string> GetAssetIds()
    {
        return new HashSet<string> { "ASSET1", "ASSET2" };
    }
}
