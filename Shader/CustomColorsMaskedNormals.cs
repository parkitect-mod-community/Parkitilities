using System;
using UnityEngine;

namespace Parkitilities
{

    // _MainTex ("Base (RGB)", 2D)
    // _MaskTex ("Color Mask (RGB)", 2D)
    // [Normal] _NormalMap ("Normal Map", 2D)
    // _Glossiness ("Smoothness", Range(0, 1))
    // _Metallic ("Metallic", Range(0, 1))
    // _RimColor ("Rim Color", Vector)
    // _RimPower ("Rim Power", Range(0.5, 8))
    // _CustomColor1 ("Custom Color (1)", Vector)
    // _CustomColor2 ("Custom Color (2)", Vector)
    // _CustomColor3 ("Custom Color (3)", Vector)
    // _CustomColor4 ("Custom Color (4)", Vector)
    // [Header(Reflections)] [Toggle(PARKITECT_REFLECTIONS)] _Reflection ("Reflection", Float)
    // _ReflectionCubemap ("Reflection Cubemap", Cube)
    // _Reflectivity ("Reflectivity", Range(0, 1))
    // _ReflectionBlur ("Reflection Blur", Range(0, 7))
    // _ReflectionColor ("Reflection Color", Vector)
    // [Toggle(PARKITECT_SMUDGES)] _Smudges ("Smudges", Float)
    // _SmudgesMap ("Smudges Map ", 2D)
    // _SmudgesTextureScale ("Smudges Texture Scale", Float)
    // _SmudgesTriplanarBlendSharpness ("Smudges Blend Sharpness", Float)
    // _SmudgesPower ("Smudges Power", Range(0, 8))
    public class CustomColorsMaskedNormals
    {
        private Material _material;
        public static readonly String ShaderName = "Rollercoaster/CustomColorsMaskedNormals";

        public CustomColorsMaskedNormals(Material material)
        {
            _material = material;
        }

        public CustomColorsMaskedNormals MainTex(Texture2D value)
        {

            _material.SetTexture("_MainTex", value);
            return this;
        }

        public CustomColorsMaskedNormals MaskTex(Texture2D value)
        {
            _material.SetTexture("_MaskTex", value);
            return this;
        }

        public Material build()
        {
            return new Material(_material);
        }
    }
}
