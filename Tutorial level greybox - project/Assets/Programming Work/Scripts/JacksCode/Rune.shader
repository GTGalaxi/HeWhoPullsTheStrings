Shader "Rune/AlwaysVisible" {
	Properties
	{
		_Color("Main Color", Color) = (0,0,0,0)
		_MainTex("Base (RGB) Gloss (A)", 2D) = "white" {}
	}

		SubShader
	{
		Tags{ "Queue" = "Transparent" }
		LOD 100

		Pass
	{
		Cull Off
		ZWrite Off

		ZTest Always
		CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

		struct appdata
		{
			float4 vertex : POSITION;
		};

		struct v2f
		{
			float4 vertex : SV_POSITION;
		};

		float4 _Color;

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			return _Color;
		}

		ENDCG
	}


		Pass
		{

			CGPROGRAM
		#pragma vertex vert
		#pragma fragment frag
		#include "UnityCG.cginc"

			struct appdata
		{
			float2 uv : TEXCOORD0;
			float4 vertex : POSITION;
		};

		struct v2f
		{

			float2 uv : TEXCOORD0;
			float4 vertex : SV_POSITION;
		};

		sampler2D _MainTex;
		float4 _MainTex_ST;

		v2f vert(appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = TRANSFORM_TEX(v.uv, _MainTex);
			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			fixed4 col = tex2D(_MainTex, i.uv);
			return col;
		}

			ENDCG
		}

	}
}
