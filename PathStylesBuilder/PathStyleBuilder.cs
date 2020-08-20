using System;
using System.Linq;
using UnityEngine;

namespace Parkitilities.PathStylesBuilder
{
    public class PathStyleBuilder : BaseBuilder<PathStyle>, IBuildableNonNonSerializable<PathStyle>,
        IBuildableNonNonSerializable<PathStyle, PathStyle>, IRecolorable<PathStyleBuilder>
    {
        public static class EmployeePathIds
        {
            public static readonly String Concrete = "concrete";
        }

        public static class NormalPathIds
        {
            public static readonly String Concrete = "concrete";
            public static readonly String Dirt = "dirt";
            public static readonly String Cobblestone = "cobblestone";
            public static readonly String Gravel = "gravel";
            public static readonly String Sidewalk = "sidewalk";
            public static readonly String Wood = "wood";
            public static readonly String Metal = "metal";
            public static readonly String StoneBlock = "stoneblock";
            public static readonly String AngledBrick = "angledbrick";
        }

        public static class QueueIds
        {
            public static readonly String Queue1 = "queue1";
            public static readonly String Queue2 = "queue2";
            public static readonly String Queue3 = "queue3";
            public static readonly String Concrete = "concrete";
            public static readonly String Dirt = "dirt";
            public static readonly String Cobblestone = "cobblestone";
            public static readonly String Gravel = "gravel";
            public static readonly String Sidewalk = "sidewalk";
            public static readonly String Wood = "wood";
            public static readonly String Metal = "metal";
            public static readonly String StoneBlock = "stoneblock";
            public static readonly String AngledBrick = "angledbrick";
        }


        public static PathStyle GetPathStyle(String name, PathType type)
        {
            switch (type)
            {
                case PathType.Employee:
                    return AssetManager.Instance.employeePathStyles.getPathStyle(name);
                case PathType.Normal:
                    return AssetManager.Instance.pathStyles.getPathStyle(name);
                case PathType.Queue:
                    return AssetManager.Instance.queueStyles.getPathStyle(name);
            }

            return AssetManager.Instance.pathStyles.getPathStyle(name);
        }

        public enum PathType
        {
            Employee,
            Normal,
            Queue
        }

        public PathStyleBuilder Id(String id)
        {
            AddStep("Id", target => target.identifier = id);
            return this;
        }

        public PathStyleBuilder Name(String name)
        {
            AddStep("Id", target => target.name = name);
            return this;
        }

        public PathStyleBuilder Material(Material material)
        {
            AddStep(target => target.material = material);
            return this;
        }


        public PathStyleBuilder HandrailGo(GameObject go)
        {
            AddStep(target => target.handRailGO = go);
            return this;
        }

        public PathStyleBuilder HandrailRampGo(GameObject go)
        {
            AddStep(target => target.handRailRampGO = go);
            return this;
        }

        public PathStyleBuilder HandrailHalfRampGo(GameObject go)
        {
            AddStep(target => target.handRailRampHalfGO = go);
            return this;
        }

        public PathStyleBuilder TileMapper(PathTileMapper mapper)
        {
            AddStep(target => target.spawnTilesOnPlatforms = mapper);
            return this;
        }

        public PathStyleBuilder TileMapper(PathTileMapper mapper, PathTileMapperBuilder builder)
        {
            AddStep(target => target.spawnTilesOnPlatforms = builder.Build(mapper));
            return this;
        }

        public PathStyleBuilder TileMapper(PathTileMapperBuilder builder)
        {
            AddStep(target => target.spawnTilesOnPlatforms = builder.Build());
            return this;
        }

        public PathStyleBuilder CustomColor(Color c1)
        {

            return CustomColor(new[] {c1});
        }

        public PathStyleBuilder CustomColor(Color c1, Color c2)
        {
            return CustomColor(new[] {c1, c2});
        }

        public PathStyleBuilder CustomColor(Color c1, Color c2, Color c3)
        {
            return CustomColor(new[] {c1, c2, c3});
        }

        public PathStyleBuilder CustomColor(Color c1, Color c2, Color c3, Color c4)
        {
            return CustomColor(new[] {c1, c2, c3, c4});
        }

        public PathStyleBuilder CustomColor(Color[] colors)
        {
            AddStep("CUSTOM_COLOR", target => { target.customColors = colors; });
            return this;
        }

        public PathStyleBuilder DisableCustomColors()
        {
            RemoveAllStepsByTag("CUSTOM_COLOR");
            return this;
        }

        public PathStyle Build()
        {
            return Build(new PathStyle());
        }

        public PathStyle Build(PathStyle input)
        {
            PathStyle style = new PathStyle
            {
                identifier = input.identifier,
                name = input.name,
                material = input.material,
                handRailGO = input.handRailGO,
                handRailRampGO = input.handRailRampGO,
                handRailRampHalfGO = input.handRailRampHalfGO,
                platformTileMapper = input.platformTileMapper,
                spawnTilesOnPlatforms = input.spawnTilesOnPlatforms,
                spawnSound = input.spawnSound,
                spawnLastSound = input.spawnLastSound,
                despawnSoundEvent = input.despawnSoundEvent,
                defaultQueueSignGO = input.defaultQueueSignGO,
                customColors = input.customColors
            };
            Apply(style);
            return style;
        }


        private void _register(PathType type, AssetManagerLoader loader, PathStyle path)
        {
            switch (type)
            {
                case PathType.Employee:
                    AssetManager.Instance.employeePathStyles.registerPathStyle(path);
                    loader.AddUnregisterHandler(() =>
                    {
                        AssetManager.Instance.employeePathStyles.unregisterPathStyle(path);
                    });
                    break;
                case PathType.Normal:
                    AssetManager.Instance.pathStyles.registerPathStyle(path);
                    loader.AddUnregisterHandler(() => { AssetManager.Instance.pathStyles.unregisterPathStyle(path); });
                    break;
                case PathType.Queue:
                    AssetManager.Instance.queueStyles.registerPathStyle(path);
                    loader.AddUnregisterHandler(() => { AssetManager.Instance.queueStyles.unregisterPathStyle(path); });
                    break;
            }
        }

        public PathStyle Register(PathType type, AssetManagerLoader loader)
        {
            PathStyle path = Build();
            _register(type, loader, path);
            return path;
        }

        public PathStyle Register(PathType type, AssetManagerLoader loader, PathStyle style)
        {
            PathStyle path = Build(style);
            _register(type, loader, path);
            return path;
        }
    }
}
