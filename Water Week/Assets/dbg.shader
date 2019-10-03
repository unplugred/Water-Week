Shader "Unlit/NewUnlitShader"
{
	Properties
	{
		_c1 ("Color1", Color) = (1.0, 1.0, 1.0, 1.0)
		_c2 ("Color2", Color) = (1.0, 1.0, 1.0, 1.0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		Cull Off
		ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			fixed4 _c1;
			fixed4 _c2;
			fixed4 frag (v2f i) : SV_Target
			{
				fixed thing = ceil(((sin(_Time[1]) * sin(i.uv.r * 50 + _Time[1]) * .02 + i.uv.g) * 13.9 + 1)%1 - .79);
				return (1 - thing)*fixed4(0.0005, 0.0004, 0.0018, 1) + thing*fixed4(.0033, .006, .0152, 1);
			}
			ENDCG
		}
	}
}
