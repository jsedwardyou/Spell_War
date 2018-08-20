// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "LoadComplete/FBXDarkPrinter" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Bright ("Dark", Range (0,1)) = 1.0
		_Color("Color", Color) = (1,1,1,1)
	}
	SubShader {	
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZWrite On
		Lighting Off Cull Off Fog { Mode Off } Blend SrcAlpha OneMinusSrcAlpha
		LOD 110
		
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed _Bright;
			fixed4 _Color;
			
			struct appdata_t {
				fixed4 vertex : POSITION;
				fixed2 texcoord : TEXCOORD0;
			};

			struct v2f {
				fixed4 vertex : POSITION;
				fixed2 texcoord : TEXCOORD0;
			};
			
			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				return o;
			}

			fixed4 frag (v2f i) : COLOR
			{
//				fixed4 col = tex2D(_MainTex, i.texcoord) * (_Bright + _Color) * (1-_Bright);
//				clip( col.a < 0.02 ? -1 : col.a );
//				return col;
			
				fixed4 tex = tex2D(_MainTex, i.texcoord);
				fixed texa = tex.a;
				clip(texa < 0.01 ? -1 : texa);
				tex = tex * _Bright + _Color * (1-_Bright);
				tex.a = texa * _Color.a;
				return tex;
			}
			ENDCG 
		}
		
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZWrite Off Cull Off Fog { Mode Off }  Blend SrcAlpha OneMinusSrcAlpha
		LOD 100

		BindChannels 
		{
			Bind "Vertex", vertex
			Bind "TexCoord", texcoord
			Bind "Color", color
		}

		Pass 
		{
			Lighting Off
			SetTexture [_MainTex] { combine texture * primary } 
		}
	}
}