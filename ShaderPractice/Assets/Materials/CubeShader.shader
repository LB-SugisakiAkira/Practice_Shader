Shader "Unlit/CubeShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}

        _Color1("CubeColor1", Color) = (1,1,0,1)
        _Color2("CubeColor2", Color) = (1,0,0,1)

        _RotationSpeed("RotationSpeed", Range(0, 10)) = 10
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
            // make fog work
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
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _Color1;
            float4 _Color2;

            float _RotationSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 回転
                half angleCos = cos(_Time * _RotationSpeed);
                half angleSin = sin(_Time * _RotationSpeed);
                half2x2 rotateMatrix = half2x2(angleCos, -angleSin, angleSin, angleCos);
                half2 uv = i.uv - 0.5;

                i.uv = mul(uv, rotateMatrix) + 0.5;

                fixed4 col = tex2D(_MainTex, i.uv);
                return col;

                // 色の変更
                // fixed4 col = lerp(_Color1, _Color2, i.uv.x * 0.5 + i.uv.y * 0.5);
                // UNITY_APPLY_FOG(i.fogCoord, col);
                // return col;
            }
            ENDCG
        }
    }
}
