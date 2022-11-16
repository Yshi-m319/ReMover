/*
//First Shader
Shader "Custom/FirstShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = fixed4(0.1f,0.1f,0.1f,1);
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
*/
/*
//ICE
Shader "Custom/sample" {
	SubShader {
		Tags { "Queue"="Transparent" }//後から描画するためのタグ
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard alpha:fade	//透過する為に必要。これを適用した状態だと透明度を0～1で設定しないと透過されちゃう
		#pragma target 3.0

		struct Input {
			float3 worldNormal;
      			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			o.Albedo = fixed4(1, 1, 1, 1);
			float alpha = 1 - (abs(dot(IN.viewDir, IN.worldNormal)));
     			o.Alpha =  alpha*1.5f;
		}
		ENDCG
	}
	FallBack "Diffuse"
}*/

//RimLighting
Shader "Custom/sample" {
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard 
		#pragma target 3.0

		struct Input {
			float2 uv_MainTex;
			float3 worldNormal;
      		float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutputStandard o) {
			fixed4 baseColor = fixed4(0.05, 0.1, 0, 1);
			fixed4 rimColor  = fixed4(0.5,0.7,0.5,1);

			o.Albedo = baseColor;
			float rim = 1 - saturate(dot(IN.viewDir, IN.worldNormal));
     			o.Emission = rimColor * pow(rim, 6);
		}
		ENDCG
	}
	FallBack "Diffuse"
}