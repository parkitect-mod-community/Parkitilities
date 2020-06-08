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
                bounds.SetMinMax(min,max);
                result[x] = bounds;
            }

            return result;
        }
    }
}
