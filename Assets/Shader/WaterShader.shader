Shader "Unlit/WaterShader_URP"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed("Speed", Range(0, 100)) = 2
        _SwingCycle ("SwingCycle", Range(0.0,30.0)) = 9.0
        _Amplitude ("Amplitude", Range(0,1.0)) = 0.07
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 200

        Pass
        {
            Name "ForwardLit"
            Tags { "LightMode"="UniversalForward" }

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            
            float4 _MainTex_ST;
            
            CBUFFER_START(UnityPerMaterial)
            float _Speed;
            float _SwingCycle;
            float _Amplitude;
            CBUFFER_END

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
            };

            struct Varyings
            {
                float2 uv           : TEXCOORD0;
                float4 positionHCS  : SV_POSITION;
            };

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                
                OUT.uv = IN.uv * _MainTex_ST.xy + _MainTex_ST.zw;
                
                float time = _Time.y * _Speed;
                
                float randomFactor = frac(sin(dot(IN.positionOS.xy, float2(12.9898,78.233))) * 43758.5453); 

                float wave = sin(time + (IN.positionOS.x + randomFactor) * _SwingCycle) *
                             cos(time + (IN.positionOS.y + randomFactor) * _SwingCycle);
                             
                float offsetZ = wave * _Amplitude;
                IN.positionOS.z += offsetZ;

                OUT.positionHCS = TransformObjectToHClip(IN.positionOS);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, IN.uv);
                col.a = 0.7;
                return col;
            }

            ENDHLSL
        }
    }
}
