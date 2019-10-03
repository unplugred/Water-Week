Shader "Custom/gun"
{
	Properties
	{
		_t ("transparency", Float) = 1
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 200
		Blend DstColor Zero
		Cull Off
		ZWrite Off

		CGPROGRAM
		#pragma surface surf StandardSpecular
		#pragma target 3.0

		struct Input
		{
			float3 viewDir;
		};

		float _t;
		void surf (Input IN, inout SurfaceOutputStandardSpecular o)
		{
			o.Albedo = .5754717;
			o.Specular = fixed3(.006, .0399, .0938);
			o.Smoothness = .5;
			o.Emission = 1 - _t*(1 - fixed3(.0385, .041, .2427)*pow(1 - saturate(dot(normalize(IN.viewDir),o.Normal)), 2.02));
		}
		ENDCG
	}
	FallBack "Diffuse"
}
