Shader "Custom/RingShader_Optimized"
{
    Properties
    {
        _RingColor ("Ring Color", Color) = (1,1,1,1)
        _FillColor ("Fill Color", Color) = (0,0,0,0)
        _Radius    ("Radius", Float)     = 0.5
        _Thickness ("Thickness", Float)  = 0.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv     : TEXCOORD0;
                float2 aspect : TEXCOORD1;
            };

            float4 _RingColor;
            float4 _FillColor;
            float  _Radius;
            float  _Thickness;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);

                float3 scaleX = mul(unity_ObjectToWorld, float4(1, 0, 0, 0)).xyz;
                float3 scaleY = mul(unity_ObjectToWorld, float4(0, 1, 0, 0)).xyz;
                
                o.aspect.x = length(scaleX);
                o.aspect.y = length(scaleY);
                o.uv       = v.uv;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 center = float2(0.5, 0.5);

                float2 adjustedUV = (i.uv - center) * i.aspect;

                float dist = length(adjustedUV);

                float halfThickness = 0.5 * _Thickness;
                float innerRadius   = _Radius - halfThickness;
                float outerRadius   = _Radius;

                float fillMask = 1.0 - step(innerRadius, dist);

                float ringMask = step(innerRadius, dist) * step(dist, outerRadius);

                return _FillColor * fillMask + _RingColor * ringMask;
            }
            ENDCG
        }
    }

    FallBack "Transparent/Diffuse"
}
