using System;
using System.Collections.Generic;
using Parkitect.Mods.AssetPacks;
using UnityEngine;

namespace Parkitilities
{
    public static class AssetPackUtilities
    {

        public static Color[] ConvertColors(List<CustomColor> colors)
        {
            Color[] results = new Color[colors.Count];
            for (int x = 0; x < results.Length; x++)
            {
                results[x] = new Color(colors[x].Red, colors[x].Green,
                    colors[x].Blue, colors[x].Alpha);
            }

            return results;
        }

        public static Bounds[] ConvertBoundingBox(Parkitect.Mods.AssetPacks.BoundingBox[] boxes)
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

        public static List<Waypoint> ConvertWaypoints(List<Parkitect.Mods.AssetPacks.Waypoint> waypoints)
        {
            List<Waypoint> points = new List<Waypoint>();
            foreach (Parkitect.Mods.AssetPacks.Waypoint waypoint in waypoints)
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
    }
}
