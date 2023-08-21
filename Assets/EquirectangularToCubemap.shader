Shader "Custom/EquirectangularToCubemap" {
    Properties {
        _MainTex ("Equirectangular Texture", 2D) = "white" {}
    }
    SubShader {
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4x4 unity_CameraToWorld;
            float4x4 unity_CameraProjection;

            v2f vert (appdata v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_CameraToWorld, v.vertex).xyz;
                return o;
            }

            float4 frag (v2f i) : SV_Target {
                float3 worldPos = normalize(i.worldPos);
                float3 cubemapPos = float3(0, 0, 0);
                float3 absWorldPos = abs(worldPos);
                if (absWorldPos.x >= absWorldPos.y && absWorldPos.x >= absWorldPos.z) {
                    cubemapPos = float3(-sign(worldPos.x), worldPos.z, -worldPos.y);
                } else if (absWorldPos.y >= absWorldPos.x && absWorldPos.y >= absWorldPos.z) {
                    cubemapPos = float3(worldPos.x, -sign(worldPos.y), worldPos.z);
                } else {
                    cubemapPos = float3(worldPos.x, worldPos.z, sign(worldPos.y));
                }
                float2 uv = 0.5 * (cubemapPos.xy / abs(cubemapPos.z) + 1.0);
                return tex2D(_MainTex, uv);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}