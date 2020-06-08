using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Parkitect.Mods.AssetPacks;
using UnityEngine;

namespace Parkitilities
{
    public static class Parkitility
    {

        public enum TrackRide
        {

        }

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

        #region Deco

        public static DecoBuilder<TDeco> CreateDeco<TDeco>(GameObject go, Asset asset) where TDeco : Deco
        {
            DecoBuilder<TDeco> builder = CreateDeco<TDeco>(go)
                .Id(asset.Guid)
                .DisplayName(asset.Name)
                .Price(asset.Price)
                .Category(asset.Category, asset.SubCategory)
                .SeeThrough(asset.SeeThrough)
                .BlockRain(asset.BlocksRain)
                .SnapGridToCenter(asset.SnapCenter)
                .GridSubdivisions(asset.GridSubdivision);

            if (asset.IsResizable) builder.Resizable(asset.MinSize, asset.MaxSize);
            if (asset.HasCustomColors) builder.CustomColor(AssetPackUtilities.ConvertColors(asset.CustomColors));
            foreach (var bound in AssetPackUtilities.ConvertBoundingBox(asset.BoundingBoxes.ToArray()))
                builder.AddBoundingBox(bound);

            return builder;
        }

        public static DecoBuilder<TDeco> CreateDeco<TDeco>(GameObject go) where TDeco : Deco
        {
            return new DecoBuilder<TDeco>(go);
        }

        public static DecoBuilder<Deco> CreateDeco(GameObject go)
        {
            return CreateDeco<Deco>(go);
        }

        #endregion

        #region Vehicle

        public static CarBuilder<Car> CreateCar(GameObject go)
        {
            return CreateCar<Car>(go);
        }

        public static CarBuilder<TCar> CreateCar<TCar>(GameObject go, CoasterCar car) where TCar : Car
        {
            return CreateCar<TCar>(go)
                .Id(car.Guid);
        }

        public static VehicleBuilder<TVehicle> CreateVehicle<TVehicle>(GameObject go) where TVehicle : Vehicle
        {
            return new VehicleBuilder<TVehicle>(go);
        }

        public static CarBuilder<TCar> CreateCar<TCar>(GameObject go) where TCar : Car
        {
            return new CarBuilder<TCar>(go);
        }

        #endregion


        #region Train
        public static FrontBackTrainBuilder<TTrain> CreateTrain<TTrain>(GameObject go)
            where TTrain : CoasterCarInstantiatorFrontMiddleBack
        {
            return new FrontBackTrainBuilder<TTrain>();
        }

        #endregion

        #region TrackRide

        public static TrackRideBuilder<TTrackRide> FromTrackedRide<TTrackRide>(string name) where TTrackRide : TrackedRide
        {

            TrackedRide original = null;
            foreach (Attraction current in ScriptableSingleton<AssetManager>.Instance.getAttractionObjects())
            {
                if (current.getUnlocalizedName() == name)
                {
                    original = (TrackedRide)current;
                    break;
                }
            }
            if(original == null)
                throw new Exception("Can't Find Track Ride: " + name);
            return new TrackRideBuilder<TTrackRide>(original.gameObject);
        }

        /// <summary>
        /// create tracked ride with empty object.
        /// </summary>
        /// <param name="name"></param>
        /// <typeparam name="TTrackRide"></typeparam>
        /// <returns></returns>
        public static TrackRideBuilder<TTrackRide> CreateTrackedRide<TTrackRide>(string name)
            where TTrackRide : TrackedRide
        {
            return new TrackRideBuilder<TTrackRide>(new GameObject());
        }

        #endregion


        #region Utilities

        public static String CurrentModDirectory()
        {
            return System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
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

        #endregion


    }
}
