Shader "Unlit/Anim"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _AnimTex ("Texture", 2D) = "white" {}
        _Index("Int",Int) = 0
        _VertexCount("Int",Int) = 59
        _Width("Float", Float) = 128
        _Height("Float", Float) = 128
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
            #pragma multi_compile_instancing
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"


            sampler2D _MainTex;
            sampler2D _AnimTex;
            

            struct appdata
            {
                float3 vertex : POSITION;
                float2 uv : TEXCOORD0;
                uint vertexID : SV_VertexID;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            UNITY_INSTANCING_BUFFER_START(UnityPerMaterial)
                UNITY_DEFINE_INSTANCED_PROP(int, _Index)
                UNITY_DEFINE_INSTANCED_PROP(int, _VertexCount)
                UNITY_DEFINE_INSTANCED_PROP(float, _Width)
                UNITY_DEFINE_INSTANCED_PROP(float, _Height)
            UNITY_INSTANCING_BUFFER_END(UnityPerMaterial)


            v2f vert (appdata v)
            {
                
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                int index = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _Index)*UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _VertexCount) + v.vertexID;
                float w = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _Width);
                float h = UNITY_ACCESS_INSTANCED_PROP(UnityPerMaterial, _Height);
                float x = (index%w)/w;
                float y = (index/w)/h;
                float4 animUV = float4(x, y, 0, 0);
                float4 col = tex2Dlod(_AnimTex, animUV);
                float3 vertex = v.vertex + float3(col.r-0.5f, col.g-0.5f, col.b);
                o.vertex = UnityObjectToClipPos(vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
