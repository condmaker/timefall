Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma vertex vert
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        float4 _MainTex_ST;

        struct Input
        {
            fixed4 pos : SV_POSITION;
            half4 color : COLOR0;
            half4 colorFog : COLOR1;
            float2 uv_MainText : TEXCOORD0;
            half3 normal : TEXCOORD1;
        };

        uniform half4 unity_FogStart;
        uniform half4 unity_FogEnd;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        Input vert(inout appdata_full v)
        {
            Input o;

            //Vertex snapping
            float4 snapToPixel = UnityObjectToClipPos(v.vertex);
            float4 vertex = snapToPixel;
            vertex.xyz = snapToPixel.xyz / snapToPixel.w;
            vertex.x = floor(120 * vertex.x) / 120;
            vertex.y = floor(100 * vertex.y) / 100;
            vertex.xyz *= snapToPixel.w;
            o.pos = vertex;

            //Vertex lighting 
        //	o.color =  float4(ShadeVertexLights(v.vertex, v.normal), 1.0);
            o.color = float4(ShadeVertexLightsFull(v.vertex, v.normal, 4, true), 1.0);
            o.color *= v.color;

            float distance = length(UnityObjectToViewPos(v.vertex));

            //Affine Texture Mapping
            float4 affinePos = vertex; //vertex;				
            o.uv_MainText = TRANSFORM_TEX(v.texcoord, _MainTex);
            o.uv_MainText *= distance + (vertex.w * (UNITY_LIGHTMODEL_AMBIENT.a * 8)) / distance / 2;
            o.normal = distance + (vertex.w * (UNITY_LIGHTMODEL_AMBIENT.a * 8)) / distance / 2;

            //Fog
            float4 fogColor = unity_FogColor;

            float fogDensity = (unity_FogEnd - distance) / (unity_FogEnd - unity_FogStart);
            o.normal.g = fogDensity;
            o.normal.b = 1;

            o.colorFog = fogColor;
            o.colorFog.a = clamp(fogDensity, 0, 1);

            //Cut out polygons
            if (distance > unity_FogEnd.z + unity_FogColor.a * 255)
            {
                o.pos = 0;
            }

            return o;
        }

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainText) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
