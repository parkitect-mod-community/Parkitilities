using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Parkitilities.PathStylesBuilder;

namespace Parkitilities.AssetPack
{
    public class Asset
    {
        public enum PathMaterial
        {
            Tiled,
            Sheet,
            Custom
        }

        public enum WalkableFlag
        {
            NOT_WALKABLE,
            WALKABLE,
            DIRECTIONAL,
            FORWARD
        }


        public int FootprintX = 1;
        public int FootprintZ = 1;
        public int DefaultTrainLength = 1;
        public int MinTrainLength = 1;
        public int MaxTrainLength = 1;
        public string Guid;
        public string Name;
        public Parkitect.Mods.AssetPacks.AssetType Type { get; set; }
        public bool LoadAsset { get; set; } = true;

        [JsonConverter(typeof(StringEnumConverter))]
        public AssetType TargetType { get; set; }

        public float Price;
        public string Category;
        public string SubCategory;
        public bool BuildOnGrid;
        public bool SnapCenter;
        public float GridSubdivision;
        public float HeightDelta;
        public bool HasCustomColors;
        public List<CustomColor> CustomColors = new List<CustomColor>();
        public int ColorCount;
        public bool IsResizable;
        public bool SeeThrough;
        public bool BlocksRain;
        public float MinSize;
        public float MaxSize;
        public bool HasBackRest;
        public bool HasMidPost;
        public int WallSettings;
        public float Height;
        public bool LightsTurnOnAtNight;
        public bool LightsUseCustomColors;
        public int LightsCustomColorSlot;
        public string Description;
        public float RainProtection;
        public float Excitement;
        public float Intensity;
        public float Nausea;
        public string TrackedRideName;
        public CoasterCar LeadCar;
        public CoasterCar Car;
        public CoasterCar RearCar;
        public string FlatRideCategory;
        public List<BoundingBox> BoundingBoxes;
        public List<Waypoint> Waypoints;
        public AspectRatio AspectRatio;
        public List<ShopProduct> Products = new List<ShopProduct>();
        public WalkableFlag Walkable = WalkableFlag.DIRECTIONAL;

        public PathMaterial PathMaterialType = PathMaterial.Tiled;
        public PathStyleBuilder.PathType PathType = PathStyleBuilder.PathType.Normal;


        public static Block.WalkableFlagType ConvertWalkable(WalkableFlag walkable)
        {
            switch (walkable)
            {
                case WalkableFlag.NOT_WALKABLE:
                    return Block.WalkableFlagType.NOT_WALKABLE;
                case WalkableFlag.WALKABLE:
                    return Block.WalkableFlagType.WALKABLE;
                case WalkableFlag.DIRECTIONAL:
                    return Block.WalkableFlagType.DIRECTIONAL;
                case WalkableFlag.FORWARD:
                    return Block.WalkableFlagType.FORWARD;
            }

            return Block.WalkableFlagType.DIRECTIONAL;
        }

    }
}
