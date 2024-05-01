using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueType.AssetSelector;
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
