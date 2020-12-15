// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "psx/shadowunlit" {
	Properties{

		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
	}
		SubShader{
			Tags { "RenderType" = "Opaque"}
			LOD 200

			Pass {
			Lighting On

				CGPROGRAM

					#pragma vertex vert
					#pragma fragment frag target 3.0 
					#include "UnityCG.cginc"
					#pragma multi_compile_fwdbase
					#include "AutoLight.cginc"

					struct v2f
					{
						fixed4 pos : SV_POSITION;
						half4 color : COLOR0;
						half4 colorFog : COLOR1;
						float2 uv_MainTex : TEXCOORD0;
						half3 normal : TEXCOORD1;

						LIGHTING_COORDS(4, 5)
					};

					float4 _MainTex_ST;
					uniform half4 unity_FogStart;
					uniform half4 unity_FogEnd;

					v2f vert(appdata_full v)
					{
						v2f o;

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
						o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
						o.uv_MainTex *= distance + (vertex.w * (UNITY_LIGHTMODEL_AMBIENT.a * 8)) / distance / 2;
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

						TRANSFER_VERTEX_TO_FRAGMENT(o);

						return o;
					}

					sampler2D _MainTex;

					float4 frag(v2f IN) : COLOR
					{
						half4 c = tex2D(_MainTex, IN.uv_MainTex / IN.normal.r) * IN.color;
						half4 color = c * (IN.colorFog.a);
						color.rgb += IN.colorFog.rgb * (1 - IN.colorFog.a);
						float attenuation = LIGHT_ATTENUATION(i);
						return color * attenuation;
					}

					UNITY_INSTANCING_BUFFER_START(Props)
						// put more per-instance properties here
						UNITY_INSTANCING_BUFFER_END(Props)


					ENDCG
				}
		}

			Fallback "VertexLit"
}