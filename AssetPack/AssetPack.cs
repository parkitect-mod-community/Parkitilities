using System.Collections.Generic;

namespace Parkitilities.AssetPack
{
    public class AssetPack
    {
        public List<Asset> Assets = new List<Asset>();
        public List<string> Assemblies = new List<string>();
        public string Name;
        public string Description;
        public int OrderPriority;
    }
}
