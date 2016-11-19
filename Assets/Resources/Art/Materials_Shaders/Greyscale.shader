// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Grayscale" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_EffectAmount("Effect Amount", Range(0, 1)) = 1.0
		_ColourRadius("ColourRadius", Float) = 1.0
		_ColourMaxRadius("ColourMaxRadius", Float) = 1.0
		_PlayerPos("_PlayerPos", Vector) = (0,0,0,1)
	}
		SubShader{
			Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
			LOD 200
			Blend SrcAlpha OneMinusSrcAlpha
			cull off

CGPROGRAM
#pragma surface surf Lambert vertex:vert alpha:blend 
#include "Tessellation.cginc"
		sampler2D _MainTex;
		uniform float _EffectAmount;
		float _ColourRadius;
		float _ColourMaxRadius;
		float4 _PlayerPos;
		sampler2D _DeathPos;



		struct Input 
		{
			float2 uv_MainTex;
			float2 location;
		};

		struct appdata {
			float4 vertex : POSITION;
			float4 tangent : TANGENT;
			float3 normal : NORMAL;
			float2 texcoord : TEXCOORD0;
		};
		

		float powerForPos(float4 pos, float2 nearVertex);

		float3 calcNormal(float2 texcoord)
		{
			const float3 off = float3(-0.01f, 0, 0.01f); // texture resolution to sample exact texels
			const float2 size = float2(0.01, 0.0); // size of a single texel in relation to world units

			float s01 = tex2D(_DeathPos, float2(texcoord.xy - off.xy));
			float s21 = tex2D(_DeathPos, float2(texcoord.xy - off.xy));
			float s10 = tex2D(_DeathPos, float2(texcoord.xy - off.xy));
			float s12 = tex2D(_DeathPos, float2(texcoord.xy - off.xy));

			float3 va = normalize(float3(size.xy, s21 - s01));
			float3 vb = normalize(float3(size.yx, s12 - s10));

			//return float3(s01, s12, 0);
			return normalize(cross(va, vb));
		}


		void vert(inout appdata_full vertexData, out Input outData)
		{
			float4 pos = mul(UNITY_MATRIX_MVP, vertexData.vertex);
			float4 posWorld = mul(unity_ObjectToWorld, vertexData.vertex);
			outData.uv_MainTex = vertexData.texcoord;
			outData.location = posWorld.xy;
		}

		

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 baseColour = tex2D(_MainTex, IN.uv_MainTex);

			
			float alpha = (1.0 - powerForPos(_PlayerPos, IN.location));

			o.Albedo = lerp(baseColour.rgb, dot(baseColour.rgb, float3(0.3, 0.59, 0.11)), _EffectAmount);
			o.Alpha = alpha;
		}

		//return 0 if pos - nearVertex >_ColourRadius
		float powerForPos(float4 pos, float2 nearVertex)
		{
			float atten = clamp(_ColourRadius - length(pos.xy - nearVertex.xy), 0.0, _ColourRadius);
			return (1.0 / _ColourMaxRadius)*atten / _ColourRadius;
		}

		ENDCG
		}
			Fallback "Transparent/VertexLit"



}
