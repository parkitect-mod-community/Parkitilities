using Parkitilities.PathStylesBuilder;
using UnityEngine;

namespace Parkitilities
{
    public class ShaderUtility
    {
        private static Texture2D _emptyTexture = null;

        public static Texture2D EmptyTexture
        {
            get
            {
                if (_emptyTexture != null)
                {
                    return _emptyTexture;
                }
                _emptyTexture = new Texture2D(1,1,TextureFormat.ARGB32,false);
                _emptyTexture.SetPixel(0,0,new Color(0,0,0,0));
                _emptyTexture.Apply();
                return _emptyTexture;
            }
        }


        public static CustomColorsMaskedNormalBuilder PathMaterial()
        {
            return new CustomColorsMaskedNormalBuilder(Object.Instantiate(PathStyleBuilder.GetPathStyle(PathStyleBuilder.NormalPathIds.Gravel, PathStyleBuilder.PathType.Normal).material));
        }

        public static CustomColorMaskedCutoutBuilder PathMaterialTiled()
        {
            return new CustomColorMaskedCutoutBuilder(Object.Instantiate(PathStyleBuilder.GetPathStyle(PathStyleBuilder.NormalPathIds.StoneBlock, PathStyleBuilder.PathType.Normal).material));
        }

        public static CustomColorsMaskedNormalBuilder CustomColorsMaskedNormal()
        {
            return new CustomColorsMaskedNormalBuilder(new Material(Shader.Find(CustomColorsMaskedNormalBuilder.ShaderName)));
        }

        public static CustomColorMaskedCutoutBuilder CustomColorMaskedCutout()
        {
            return new CustomColorMaskedCutoutBuilder(new Material(Shader.Find(CustomColorMaskedCutoutBuilder.ShaderName)));
        }
    }
}
