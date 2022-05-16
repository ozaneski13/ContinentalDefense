/*
        ░█████╗░░█████╗░███████╗░█████╗░███╗░░██╗  ░██████╗██╗░░██╗░█████╗░██████╗░███████╗██████╗░
        ██╔══██╗██╔══██╗██╔════╝██╔══██╗████╗░██║  ██╔════╝██║░░██║██╔══██╗██╔══██╗██╔════╝██╔══██╗
        ██║░░██║██║░░╚═╝█████╗░░███████║██╔██╗██║  ╚█████╗░███████║███████║██║░░██║█████╗░░██████╔╝
        ██║░░██║██║░░██╗██╔══╝░░██╔══██║██║╚████║  ░╚═══██╗██╔══██║██╔══██║██║░░██║██╔══╝░░██╔══██╗
        ╚█████╔╝╚█████╔╝███████╗██║░░██║██║░╚███║  ██████╔╝██║░░██║██║░░██║██████╔╝███████╗██║░░██║
        ░╚════╝░░╚════╝░╚══════╝╚═╝░░╚═╝╚═╝░░╚══╝  ╚═════╝░╚═╝░░╚═╝╚═╝░░╚═╝╚═════╝░╚══════╝╚═╝░░╚═╝

                █▀▀▄ █──█ 　 ▀▀█▀▀ █──█ █▀▀ 　 ░█▀▀▄ █▀▀ ▀█─█▀ █▀▀ █── █▀▀█ █▀▀█ █▀▀ █▀▀█ 
                █▀▀▄ █▄▄█ 　 ─░█── █▀▀█ █▀▀ 　 ░█─░█ █▀▀ ─█▄█─ █▀▀ █── █──█ █──█ █▀▀ █▄▄▀ 
                ▀▀▀─ ▄▄▄█ 　 ─░█── ▀──▀ ▀▀▀ 　 ░█▄▄▀ ▀▀▀ ──▀── ▀▀▀ ▀▀▀ ▀▀▀▀ █▀▀▀ ▀▀▀ ▀─▀▀
____________________________________________________________________________________________________________________________________________

        ▄▀█ █▀ █▀ █▀▀ ▀█▀ ▀   █░█ █░░ ▀█▀ █ █▀▄▀█ ▄▀█ ▀█▀ █▀▀   ▄█ █▀█ ▄█▄   █▀ █░█ ▄▀█ █▀▄ █▀▀ █▀█ █▀
        █▀█ ▄█ ▄█ ██▄ ░█░ ▄   █▄█ █▄▄ ░█░ █ █░▀░█ █▀█ ░█░ ██▄   ░█ █▄█ ░▀░   ▄█ █▀█ █▀█ █▄▀ ██▄ █▀▄ ▄█
____________________________________________________________________________________________________________________________________________
License:
    The license is ATTRIBUTION 3.0

    More license info here:
        https://creativecommons.org/licenses/by/3.0/
____________________________________________________________________________________________________________________________________________
This shader has NOT been tested on any other PC configuration except the following:
    CPU: Intel Core i5-6400
    GPU: NVidia GTX 750Ti
    RAM: 16GB
    Windows: 10 x64
    DirectX: 11
____________________________________________________________________________________________________________________________________________
*/

Shader "Ultimate 10+ Shaders/Ocean"
{
    Properties
    {
        [HDR] _Color ("Color", Color) = (0.0,0.25,0.35,0.0)

        _Normal1 ("Normal Map (1)", 2D) = "white" {}
        _NormalStrength1 ("Normal Strength (1)", Range(0, 2)) = 0.17
        _FlowDirection1("Flow Direction (1)", float) = (0.05, 0, 0, 1)

        _Normal2 ("Normal Map (2)", 2D) = "white" {}
        _NormalStrength2 ("Normal Strength (2)", Range(0, 2)) = 0.8
        _FlowDirection2("Flow Direction (2)", float) = (0, 0.05, 0, 1)

        _Glossiness ("Smoothness", Range(0,1)) = 0.6
        _Metallic ("Metallic", Range(0,1)) = 0.2

        _HeightMap ("Height Map (Black and White)", 2D) = "bump" {}
        _Speed ("Speed", float) = 0.25
        _FlowDirection ("Flow Direction", Vector) = (1, 0, 0, 0)
        _Amplitude ("Amplitude", float) = 1.0
        
        [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", Float) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
        LOD 150
        Cull [_Cull]
        Lighting Off
        ZWrite On

        CGINCLUDE
        #define _GLOSSYENV 1
        #define UNITY_SETUP_BRDF_INPUT SpecularSetup
        #define UNITY_SETUP_BRDF_INPUT MetallicSetup
        ENDCG

        CGPROGRAM
        // Physically based Standard lighting model, and disabled shadows (the ocean doesn't have a shadow :D)
        #pragma surface surf Standard alpha
        #pragma vertex vert

        #ifndef SHADER_API_D3D11
            #pragma target 3.0
        #else
            #pragma target 4.0
        #endif

        fixed4 _Color;

        sampler2D _Normal1;
        half _NormalStrength1;
        half2 _FlowDirection1;

        sampler2D _Normal2;
        half _NormalStrength2;
        half2 _FlowDirection2;

        half _Glossiness;
        half _Metallic;

        sampler2D _HeightMap;
        half4 _FlowDirection;
        half _Speed;
        half _Amplitude;

        struct Input
        {
            float2 uv_Normal1;
            float2 uv_Normal2;
        };

        fixed4 pixel;
        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            IN.uv_Normal1 += _Time.y * _FlowDirection1;
            IN.uv_Normal2 += _Time.y * _FlowDirection2;

            pixel = (tex2D (_Normal1, IN.uv_Normal1) * _NormalStrength1 + tex2D(_Normal2, IN.uv_Normal2) * _NormalStrength2);
            
            o.Albedo = _Color.rgb;
            o.Alpha = _Color.a;
            
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;

            o.Normal = UnpackNormal(pixel);
        }

        struct appdata {
            float4 vertex : POSITION;
            float4 tangent : TANGENT;
            float3 normal : NORMAL;
            float4 texcoord : TEXCOORD0;
            float4 texcoord1 : TEXCOORD1;
            fixed4 color : COLOR;
            UNITY_VERTEX_INPUT_INSTANCE_ID
        };

        fixed4 texPixel;
        void vert (inout appdata vert){
            texPixel = tex2Dlod(_HeightMap, vert.texcoord1 + _FlowDirection * fmod(_Time.y, 1200) * _Speed);
            vert.vertex.y = texPixel.r * _Amplitude;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
