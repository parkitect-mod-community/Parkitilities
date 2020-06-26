using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Parkitilities
{
    public static class AssetPackUtilities
    {

        public static Color[] ConvertColors(List<AssetPack.CustomColor> colors)
        {
            Color[] results = new Color[colors.Count];
            for (int x = 0; x < results.Length; x++)
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
            var targetSkinnedMesh = target.GetComponentInChildren<SkinnedMeshRenderer>();
            var fromSkinnedMesh = from.GetComponentInChildren<SkinnedMeshRenderer>();

            var fMapping = new Dictionary<int, String>();
            for (var x = 0; x < fromSkinnedMesh.bones.Length; x++) fMapping.Add(x, fromSkinnedMesh.bones[x].name);

            var bp = new List<Matrix4x4>();
            var nMapping = new Dictionary<String, int>();
            for (var x = 0; x < targetSkinnedMesh.bones.Length; x++)
            {
                nMapping.Add(targetSkinnedMesh.bones[x].name, x);

                var t = from.transform.FindRecursive(targetSkinnedMesh.bones[x].name);
                if (t != null)
                    bp.Add(t.worldToLocalMatrix * from.transform.localToWorldMatrix);
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

            Mesh mesh = targetSkinnedMesh.sharedMesh;
            mesh.Clear();
            mesh.vertices = fromSkinnedMesh.sharedMesh.vertices;
            mesh.uv = fromMesh.uv;
            mesh.triangles = fromMesh.triangles;
            mesh.RecalculateBounds();
            mesh.normals = fromMesh.normals;
            mesh.tangents = fromMesh.tangents;

            mesh.boneWeights = boneWeights.ToArray();
            mesh.bindposes = bp.ToArray();

            return null;
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
