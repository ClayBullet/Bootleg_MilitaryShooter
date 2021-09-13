Shader "Unlit/CameraShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _SecondColor("Second Color", Color) = (1, 1, 1, 1)
        _SecondTexture("Second Texture", 2D) = "white" {}
        _Lerping("Lerping", range(-1, 1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _SecondTexture;
            float4 _MainTex_ST;
            float4 _SecondColor;
            half _Lerping;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 col2 = tex2D(_SecondTexture, i.uv) * (_SecondColor * _SecondColor);
                return lerp(col, col2, _Lerping);
            }
            ENDCG
        }
    }
}
