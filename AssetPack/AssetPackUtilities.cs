using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Parkitilities.AssetPack;
using Parkitilities.ShopBuilder;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Parkitilities
{
    public static class AssetPackUtilities
    {

        public static DecoBuilder<TDeco> FromDeco<TDeco>(GameObject go, Asset asset) where TDeco : Deco
        {
            DecoBuilder<TDeco> builder = Parkitility.CreateDeco<TDeco>(go)
                .Id(asset.Guid)
                .DisplayName(asset.Name)
                .Price(asset.Price, false)
                .Category(asset.Category, asset.SubCategory)
                .SeeThrough(asset.SeeThrough)
                .BlockRain(asset.BlocksRain)
                .SnapGridToCenter(asset.SnapCenter)
                .GridSubdivisions(asset.GridSubdivision);

            if (asset.IsResizable) builder.Resizable(asset.MinSize, asset.MaxSize);
            if (asset.HasCustomColors) builder.CustomColor(ConvertColors(asset.CustomColors));
            foreach (var bound in ConvertBoundingBox(asset.BoundingBoxes.ToArray()))
                builder.AddBoundingBox(bound);

            return builder;
        }

        /// <summary>
        /// load in product shop, will require loading in products seperatly
        /// </summary>
        /// <param name="go"></param>
        /// <param name="asset"></param>
        /// <typeparam name="TShop"></typeparam>
        /// <returns></returns>
        public static ProductShopBuilder<TShop> FromProductShop<TShop>(GameObject go, Asset asset)
            where TShop : ProductShop
        {
             var builder = new ProductShopBuilder<TShop>(go)
                .DisplayName(asset.Name)
                .Id(asset.Guid)
                .WalkableFlag(Block.WalkableFlagType.FORWARD);

             return builder;
        }

        public static CarBuilder<TCar> FromCar<TCar>(GameObject go, Asset asset) where TCar : Car
        {
            return new CarBuilder<TCar>(go)
                .Id(asset.Guid)
                .BackOffset(asset.Car.OffsetBack)
                .FrontOffset(asset.Car.OffsetFront)
                .CustomColor(ConvertColors(asset.CustomColors));
        }


        public static TResult LoadAsset<TResult>(AssetBundle bundle, string guid) where TResult : Object
        {
            TResult result = null;
            if (typeof(TResult) == typeof(Texture2D))
            {
                result =
                    bundle.LoadAsset<TResult>(string.Format("Assets/Resources/AssetPack/{0}.png", guid));
            }
            else
            {
                result =
                    bundle.LoadAsset<TResult>(string.Format("Assets/Resources/AssetPack/{0}.prefab", guid));
            }

            if (result == null)
                Debug.Log("Failed to load asset for: " + guid);
            return result;
        }

        public static Color[] ConvertColors(List<CustomColor> colors)
        {
            return ConvertColors(colors, colors.Count);
        }

        public static Color[] ConvertColors(List<CustomColor> colors, int count)
        {
            Color[] results = new Color[count];
            if (count == 0)
                return results;
            for (int x = 0; x < count; x++)
            {
                results[x] = new Color(colors[x].Red, colors[x].Green,
                    colors[x].Blue, colors[x].Alpha);
            }

            return results;
        }

        public static Bounds[] ConvertBoundingBox(AssetPack.BoundingBox[] boxes)
        {
            Bounds[] result = new Bounds[boxes.Length];
            for (int x = 0; x < boxes.Length; x++)
            {
                Bounds bounds = new Bounds();
                Vector3 min = new Vector3(boxes[x].BoundsMin[0], boxes[x].BoundsMin[1], boxes[x].BoundsMin[2]);
                Vector3 max = new Vector3(boxes[x].BoundsMax[0], boxes[x].BoundsMax[1], boxes[x].BoundsMax[2]);
                bounds.SetMinMax(min, max);
                result[x] = bounds;
            }

            return result;
        }

        public static TrackedRide TrackedRide(String attractionName)
        {
            foreach (Attraction attractionObject in ScriptableSingleton<AssetManager>.Instance.getAttractionObjects())
            {
                if (attractionObject is TrackedRide && attractionObject.getUnlocalizedName().Equals(attractionName))
                {
                    return (TrackedRide) attractionObject;
                }
            }

            return null;
        }

        public static AssetPack.AssetPack LoadAsset(String contents) {
            return JsonConvert.DeserializeObject<AssetPack.AssetPack>(contents);
        }

        public static List<Waypoint> ConvertWaypoints(List<Parkitilities.AssetPack.Waypoint> waypoints)
        {
            List<Waypoint> points = new List<Waypoint>();
            foreach (var waypoint in waypoints)
            {

                points.Add(new Waypoint
                {
                    isOuter = waypoint.IsOuter,
                    isRabbitHoleGoal = waypoint.IsRabbitHoleGoal,
                    localPosition = new Vector3(waypoint.Position[0], waypoint.Position[1], waypoint.Position[2]),
                    connectedTo = waypoint.ConnectedTo
                });
            }

            return points;
        }

        public static Mesh RemapSkinnedMesh(GameObject target, GameObject from)
        {
            target.transform.rotation = new Quaternion();
            from.transform.rotation = new Quaternion();

            target.transform.position = new Vector3();
            from.transform.position = new Vector3();

            var targetSkinnedMesh = target.GetComponentInChildren<SkinnedMeshRenderer>();
            var fromSkinnedMesh = from.GetComponentInChildren<SkinnedMeshRenderer>();

            var fMapping = new Dictionary<int, String>();
            for (var x = 0; x < fromSkinnedMesh.bones.Length; x++) fMapping.Add(x, fromSkinnedMesh.bones[x].name);

            var bp = new List<Matrix4x4>();
            var nMapping = new Dictionary<String, int>();
            for (var x = 0; x < targetSkinnedMesh.bones.Length; x++)
            {
                nMapping.Add(targetSkinnedMesh.bones[x].name, x);

                var t = target.transform.FindRecursive(targetSkinnedMesh.bones[x].name);
                if (t != null)
                    bp.Add(t.worldToLocalMatrix * target.transform.localToWorldMatrix);
                else
                    bp.Add(targetSkinnedMesh.sharedMesh.bindposes[x]);
            }
            var boneWeights = new List<BoneWeight>();
            Mesh fromMesh = fromSkinnedMesh.sharedMesh;
            for (var x = 0; x < fromSkinnedMesh.sharedMesh.boneWeights.Length; x++)
            {
                var weight = new BoneWeight();

                weight.boneIndex0 = RemapBone(fromSkinnedMesh.sharedMesh.boneWeights[x].boneIndex0, fMapping, nMapping);
                weight.boneIndex1 = RemapBone(fromSkinnedMesh.sharedMesh.boneWeights[x].boneIndex1, fMapping, nMapping);
                weight.boneIndex2 = RemapBone(fromSkinnedMesh.sharedMesh.boneWeights[x].boneIndex2, fMapping, nMapping);
                weight.boneIndex3 = RemapBone(fromSkinnedMesh.sharedMesh.boneWeights[x].boneIndex3, fMapping, nMapping);

                weight.weight0 = fromMesh.boneWeights[x].weight0;
                weight.weight1 = fromMesh.boneWeights[x].weight1;
                weight.weight2 = fromMesh.boneWeights[x].weight2;
                weight.weight3 = fromMesh.boneWeights[x].weight3;
                boneWeights.Add(weight);
            }

            Mesh mesh = Object.Instantiate(targetSkinnedMesh.sharedMesh);
            mesh.Clear();
            mesh.vertices = fromSkinnedMesh.sharedMesh.vertices;
            mesh.uv = fromMesh.uv;
            mesh.triangles = fromMesh.triangles;
            mesh.RecalculateBounds();
            mesh.normals = fromMesh.normals;
            mesh.tangents = fromMesh.tangents;

            mesh.boneWeights = boneWeights.ToArray();
            mesh.bindposes = bp.ToArray();
            targetSkinnedMesh.sharedMesh = mesh;

            targetSkinnedMesh.sharedMaterial = fromSkinnedMesh.sharedMaterial;

            return mesh;
        }

        private static int RemapBone(int index, Dictionary<int, String> from, Dictionary<String, int> target)
        {
            var name = from[index];
            if (target.ContainsKey(name))
                return target[name];
            Debug.Log("can't find bone mapping:" + name);
            return 0;
        }
    }
}
