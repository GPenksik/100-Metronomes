Shader "Custom/GlassShader"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _BumpMap ("Normalmap", 2D) = "bump" {}
        _RimColor ("Rim Color", Color) = (1,1,1,0.5)
        _RimPower ("Rim Power", Range (0.5, 8.0)) = 3.0
        _Refraction ("Refraction", Range(0, 1)) = 0.02
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert alpha

        sampler2D _MainTex;
        sampler2D _BumpMap;
        fixed4 _Color;
        fixed4 _RimColor;
        float _RimPower;
        float _Refraction;

        struct Input
        {
            float2 uv_MainTex;
            float3 viewDir;
            float3 worldNormal;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a;

            // Normal mapping
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));

            // Rim lighting
            half rim = 1.0 - saturate(dot(IN.viewDir, o.Normal));
            o.Emission = _RimColor.rgb * pow(rim, _RimPower);

            // Refraction
            float3 refraction = refract(IN.viewDir, o.Normal, _Refraction);
            o.Albedo += texCUBE(_Cube, refraction).rgb;
        }
        ENDCG
    }
    FallBack "Transparent/Diffuse"
}
