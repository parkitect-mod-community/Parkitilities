using System;
using UnityEngine;

namespace Parkitilities
{

    // _MainTex ("Base (RGB)", 2D) = "white" {}
    // _MaskTex ("Color Mask (RGB)", 2D) = "white" {}
    // [Normal] _NormalTex ("Normal Map", 2D) = "bump" {}
    // _Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
    // [Header(Rim Lighting)] [Toggle(RIM_LIGHTING)] _RimLighting ("Rim Lighting", Float) = 1
    // _RimColor ("Rim Color", Vector) = (0.26,0.19,0.16,0)
    // _RimPower ("Rim Power", Range(0.5, 8)) = 3
    // [HideInInspector] _CustomColor1 ("Custom Color (1)", Vector) = (1,0,0,1)
    // [HideInInspector] _CustomColor2 ("Custom Color (2)", Vector) = (0,1,0,1)
    // [HideInInspector] _CustomColor3 ("Custom Color (3)", Vector) = (0,0,1,1)
    // [HideInInspector] _CustomColor4 ("Custom Color (4)", Vector) = (1,0,1,1)
    public class CustomColorMaskedCutoutBuilder
    {
        private Material _material;
        public static readonly String ShaderName = "Rollercoaster/CustomColorsMaskedCutout (Instanced)";

        public CustomColorMaskedCutoutBuilder(Material material)
        {
            _material = material;
        }


        public CustomColorMaskedCutoutBuilder MainTex(Texture2D value)
        {
            _material.SetTexture("_MainTex", value);
            return this;
        }

        public CustomColorMaskedCutoutBuilder MaskTex(Texture2D value)
        {
            _material.SetTexture("_MaskTex", value);
            return this;
        }

        public CustomColorMaskedCutoutBuilder NormalTex(Texture2D value)
        {
            _material.SetTexture("_NormalMap", value);
            return this;
        }

        public Material build()
        {
            return new Material(_material);
        }

    }
}
