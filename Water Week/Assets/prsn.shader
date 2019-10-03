Shader "Custom/prsn"
{
	Properties
	{
		_c ("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf StandardSpecular
		#pragma target 3.0

		struct Input
		{
			float3 viewDir;
		};

		fixed4 _c;
		void surf (Input IN, inout SurfaceOutputStandardSpecular o)
		{
			o.Albedo = _c.rgb;
			o.Specular = fixed3(.006, .0399, .0938);
			o.Smoothness = .38;
			o.Emission = fixed3(.0385, .041, .2427)*pow(1 - saturate(dot(normalize(IN.viewDir),o.Normal)), 2.02);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
