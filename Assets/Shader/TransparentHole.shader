Shader "Unlit/TransparentHole"
{
    Properties
    {
        _MainTex ("Panel Texture", 2D) = "white" {}
        _PanelColor ("Panel Color", Color) = (1, 1, 1, 1) // Panel rengi
        _PanelAlpha ("Panel Transparency", Float) = 1.0 // Panel genel şeffaflık
        _HoleCenter ("Hole Center (X, Y)", Vector) = (0.5, 0.5, 0, 0) // Delik merkezi
        _HoleSize ("Hole Size (Width, Height)", Vector) = (0.2, 0.2, 0, 0) // Delik boyutları (genişlik, yükseklik)
        _HoleType ("Hole Type (0=Ellipse, 1=Rectangle)", Float) = 0 // Delik tipi
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _PanelColor;
            float _PanelAlpha;
            float4 _HoleCenter;
            float4 _HoleSize;
            float _HoleType;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Panel dokusu ve rengi
                fixed4 col = tex2D(_MainTex, i.uv) * _PanelColor;

                // Panelin genel şeffaflık değeri
                col.a *= _PanelAlpha;

                // Delik boyutlarını ve merkezini al
                float2 holeCenter = float2(_HoleCenter.x, _HoleCenter.y);
                float2 holeSize = float2(_HoleSize.x, _HoleSize.y);

                // Delik tipi kontrolü
                if (_HoleType == 0) // Ellipse (oval)
                {
                    float2 normalizedDist = abs(i.uv - holeCenter) / holeSize;
                    float dist = length(normalizedDist);

                    if (dist < 1.0) // İçindeyse
                        col.a = 0; // Şeffaf
                }
                else if (_HoleType == 1) // Rectangle
                {
                    float2 dist = abs(i.uv - holeCenter);

                    if (dist.x < holeSize.x && dist.y < holeSize.y)
                        col.a = 0; // Şeffaf
                }

                return col;
            }
            ENDCG
        }
    }
}