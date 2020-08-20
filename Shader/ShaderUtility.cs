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


        public static CustomColorsMaskedNormals PathMaterial()
        {
            return new CustomColorsMaskedNormals(Object.Instantiate(PathStyleBuilder.GetPathStyle(PathStyleBuilder.NormalPathIds.Gravel, PathStyleBuilder.PathType.Normal).material));
        }

        public static CustomColorsMaskedNormals PathMaterialTiled()
        {
            return new CustomColorsMaskedNormals(Object.Instantiate(PathStyleBuilder.GetPathStyle(PathStyleBuilder.NormalPathIds.StoneBlock, PathStyleBuilder.PathType.Normal).material));
        }

        public static CustomColorsMaskedNormals CustomColorsMaskedNormal()
        {
            return new CustomColorsMaskedNormals(new Material(Shader.Find(CustomColorsMaskedNormals.ShaderName)));
        }
    }
}
