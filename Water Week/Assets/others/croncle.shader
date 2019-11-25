Shader "Unlit/Croncle"
{
    Properties
    {
        _Col ("Color", Color) = (1,0,0,1)
    }
    SubShader
    {
        Blend SrcAlpha OneMinusSrcAlpha

        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

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
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv - 0.5;
                return o;
            }

            fixed4 _Col;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = _Col;
                float dist = sqrt(dot(i.uv,i.uv));
                clip(saturate(0.5 - dist) - .01);
                return col;
            }
            ENDCG
        }
    }
}
