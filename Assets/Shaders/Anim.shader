Shader "Unlit/Anim"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AnimTex ("Texture", 2D) = "white" {}
        _Index("Int",Int) = 0
        _VertexCount("Int",Int) = 59
        _Width("Float",int) = 128
        _Height("Float",int) = 128
    }
    SubShader
    {
        Tags { "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True" }
        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float3 vertex : POSITION;
                float2 uv : TEXCOORD0;
                uint vertexID : SV_VertexID;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _AnimTex;
            float4 _AnimTex_ST;

            int _Index;
            int _VertexCount;
            int _Width;
            int _Height;

            v2f vert (appdata v)
            {
                v2f o;
                int index = _Index*_VertexCount + v.vertexID;
                float x = (index%_Width)/128.0f;
                float y = (index/_Width)/128.0f;
                float4 animUV = float4(x, y, 0, 0);
                float4 col = tex2Dlod(_AnimTex, animUV);
                float3 vertex = v.vertex + float3(col.r-0.5f, col.g-0.5f, col.b);
                o.vertex = UnityObjectToClipPos(vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
