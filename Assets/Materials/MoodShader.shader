Shader "Custom/MoodShader"
{
    Properties
    {
        _Color ("Outline Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" }

        Pass 
        {
            Name "Universal2D"
            Tags{ "LightMode" = "Universal2D" }

            Cull Front
            ZWrite On
            ZTest LEqual
            Blend[_SrcBlend][_DstBlend]

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonMaterial.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/BSDF.hlsl"          

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float3 normalOS     : NORMAL;
                float4 tangentOS    : TANGENT;
            };

            struct Varyings {
                float4 positionCS   : SV_POSITION;
                float3 positionWS   : TEXCOORD3;
            };    

            CBUFFER_START(UnityPerMaterial)
                half4 _Color;          
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;

                VertexPositionInputs vertexInput = GetVertexPositionInputs(IN.positionOS.xyz);
                VertexNormalInputs vertexNormalInput = GetVertexNormalInputs(IN.normalOS, IN.tangentOS);
                
                OUT.positionWS = vertexInput.positionWS;
                OUT.positionCS = vertexInput.positionCS;

                return OUT;
            }
            
            half4 frag(Varyings IN) : SV_Target
            {
                return _Color;
            }

            ENDHLSL
        }
    }
}