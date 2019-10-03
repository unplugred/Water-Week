Shader "Custom/bullet"
{
	Properties
	{
		_c ("color", 2D) = "white" {}
		_n ("normal", 2D) = "bump" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200

		CGPROGRAM
		#pragma surface surf StandardSpecular
		#pragma target 3.0

		sampler2D _c;
		sampler2D _n;
		struct Input
		{
			float2 uv_c;
			float2 uv_n;
			float3 viewDir;
		};
		void surf (Input IN, inout SurfaceOutputStandardSpecular o)
		{
			fixed cc = tex2D(_c, IN.uv_c).a;
			o.Albedo = (1 - cc) * fixed3(.4295, .3319, .2038) + cc * fixed3(.666, .5113, .3157);
			o.Normal = normalize(UnpackNormal(tex2D(_n, IN.uv_n)) * float3(1,1,1.4286));
			o.Specular = fixed3(.006, .0399, .0938);
			o.Smoothness = .52;
			o.Emission = fixed3(.0385, .041, .2427)*pow(1 - saturate(dot(normalize(IN.viewDir),o.Normal)), 2.02);
		}
		ENDCG
	}
	FallBack "Diffuse"
}
