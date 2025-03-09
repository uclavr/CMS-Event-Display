Shader "Custom/LegoJetShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,0,0.8)  // Yellow with transparency
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
            };

            fixed4 _Color;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float zAbs = abs(i.worldPos.z);
                float alpha = zAbs > (3.14159 / 2) ? 0.0 : _Color.a;
                return fixed4(_Color.rgb, alpha);
            }
            ENDCG
        }
    }
}
