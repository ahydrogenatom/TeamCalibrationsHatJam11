// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Darkness" {
	Properties{
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	_EffectAmount("Effect Amount", Range(0, 1)) = 1.0
		_ColourRadius("ColourRadius", Float) = 1.0
		_ColourMaxRadius("ColourMaxRadius", Float) = 1.0
		_OverallTransparancy("OverallTransparancy", Float) = 1.0
		_MaxTransparancy("MaxTransparancy", Float) = 1.0
		_ColourR("ColourR", Float) = 0.3
		_ColourG("ColourG", Float) = 0.59
		_ColourB("ColourB", Float) = 0.11
		_Light_1_Pos("Light_1_Pos", Vector) = (0,0,0,1)
		_Light_2_Pos("Light_2_Pos", Vector) = (0,0,0,1)
		_Light_3_Pos("Light_3_Pos", Vector) = (0,0,0,1)
		_Transparent("Transparent", Vector) = (0,0,0,1)

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
	float _MaxTransparancy;
	float _OverallTransparancy;
	float4 _Light_1_Pos;
	float4 _Light_2_Pos;
	float4 _Light_3_Pos;
	float4 _Transparent;
	float _ColourR;
	float _ColourG;
	float _ColourB;

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
	float areaTransparancy(float4 pos, float2 nearVertex);


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


		float alpha = (1.0 - (powerForPos(_Light_1_Pos, IN.location) + powerForPos(_Light_2_Pos, IN.location) + powerForPos(_Light_3_Pos, IN.location) + areaTransparancy(_Transparent, IN.location)));

		o.Albedo = lerp(baseColour.rgb, dot(baseColour.rgb, float3(_ColourR, _ColourG, _ColourB)), _EffectAmount);
		o.Alpha = alpha;
	}

	float powerForPos(float4 pos, float2 nearVertex)
	{
		float atten = clamp(_ColourRadius - length(pos.xy - nearVertex.xy), 0.0, _ColourRadius);
		return (1.0 / _ColourMaxRadius)*atten / _ColourRadius;
	}

	float areaTransparancy(float4 pos, float2 nearVertex)
	{
		float atten = clamp(_MaxTransparancy - length(pos.xy - nearVertex.xy), 0.0, _MaxTransparancy);
		return (1.0 / _OverallTransparancy)*atten / _MaxTransparancy;
	}

	ENDCG
	}
		Fallback "Transparent/VertexLit"



}
