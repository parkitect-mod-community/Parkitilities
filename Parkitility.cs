using System;
using System.Runtime.CompilerServices;
using Parkitect.Mods.AssetPacks;
using UnityEngine;

namespace Parkitilities
{
    public static class Parkitility
    {

        private static Material standard;
        private static Material diffuse;
        private static Material colorDiffuse;
        private static Material specular;
        private static Material colorSpecular;
        private static Material colorDiffuseIllum;
        private static Material colorSpecularIllum;
        private static Material colorDiffuseTransparent;
        private static Material colorSpecularTransparent;

        static Parkitility()
        {
            foreach (Material objectMaterial in ScriptableSingleton<AssetManager>.Instance.objectMaterials)
            {
                switch (objectMaterial.name)
                {
                    case "CustomColorsDiffuse":
                        colorDiffuse = objectMaterial;
                        break;
                    case "CustomColorsIllum":
                        colorDiffuseIllum = objectMaterial;
                        break;
                    case "CustomColorsIllumSpecular":
                        colorSpecularIllum = objectMaterial;
                        break;
                    case "CustomColorsSpecular":
                        colorSpecular = objectMaterial;
                        break;
                    case "CustomColorsSpecularTransparent":
                        colorSpecularTransparent = objectMaterial;
                        break;
                    case "CustomColorsTransparent":
                        colorDiffuseTransparent = objectMaterial;
                        break;
                    case "Diffuse":
                        diffuse = objectMaterial;
                        break;
                    case "Specular":
                        specular = objectMaterial;
                        break;
                }
            }
        }

        public static void OnlyActiveInBuildMode(GameObject go)
        {
            go.AddComponent<OnlyActiveInBuildMode>();
        }

        public static DecoBuilder<TDeco> Create<TDeco>(GameObject go) where TDeco : Deco
        {
            return new DecoBuilder<TDeco>(go);
        }

        public static DecoBuilder<TDeco> Create<TDeco>(GameObject go, Asset asset)  where TDeco : Deco
        {
            Color[] colors = new Color[asset.CustomColors.Count];
            for (int x = 0; x < asset.CustomColors.Count; x++)
            {
                colors[x] = new Color(asset.CustomColors[x].Red,asset.CustomColors[x].Green,asset.CustomColors[x].Blue,asset.CustomColors[x].Alpha);
            }

            return Create<TDeco>(go)
                .Id(asset.Guid)
                .DisplayName(asset.Name)
                .Price(asset.Price)
                .CustomColor(colors);
        }

        public static VehicleBuilder<TVehicle> FromVehicle<TVehicle>() where TVehicle : Vehicle
        {
            return new VehicleBuilder<TVehicle>();
        }

        public static CarBuilder<TCar> FromCar<TCar>() where TCar : Car
        {
            return new CarBuilder<TCar>();
        }


        public static CustomColors ApplyRecolorable(Color[] colors, GameObject go)
        {
            if (colors.Length > 0)
            {
                CustomColors customColors = go.AddComponent<CustomColors>();
                customColors.setColors(colors, true);
                return customColors;
            }

            return null;
        }

        public static void ReplaceWithParkitectMaterial(Renderer renderer)
        {
            Material[] materials = renderer.sharedMaterials;
            for (int i = 0; i < materials.Length; i++)
            {
                if (materials[i] != null)
                {
                    if (materials[i].name.StartsWith("Diffuse"))
                        materials[i] = diffuse;
                    else if (materials[i].name.StartsWith("CustomColorsDiffuseTransparent"))
                        materials[i] = colorDiffuseTransparent;
                    else if (materials[i].name.StartsWith("CustomColorsDiffuseIllum"))
                        materials[i] = colorDiffuseIllum;
                    else if (materials[i].name.StartsWith("CustomColorsDiffuse"))
                        materials[i] = colorDiffuse;
                    else if (materials[i].name.StartsWith("Specular"))
                        materials[i] = specular;
                    else if (materials[i].name.StartsWith("CustomColorsSpecularTransparent"))
                        materials[i] = colorSpecularTransparent;
                    else if (materials[i].name.StartsWith("CustomColorsSpecularIllum"))
                        materials[i] = colorSpecularIllum;
                    else if (materials[i].name.StartsWith("CustomColorsSpecular"))
                        materials[i] = colorSpecular;
                }
            }

            renderer.sharedMaterials = materials;
        }
    }
}
