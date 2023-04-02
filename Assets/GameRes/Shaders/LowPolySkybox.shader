// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Skybox/Farland Skies/Low Poly" {		

	Properties{
		// Sky
		_TopColor("Color Top", Color) = (.5, .5, .5, 1.0)
		_MiddleColor("Color Middle", Color) = (.5, .5, .5, 1.0)
		_BottomColor("Color Bottom", Color) = (.5, .5, .5, 1.0)
		_TopExponent("Exponent Top", Range(0.01, 5)) = 1.0
		_BottomExponent("Exponent Bottom", Range(0.01, 5)) = 1.0

		// Stars
		_StarsTint("Stars Tint", Color) = (.5, .5, .5, 1.0)
		_StarsExtinction("Stars Extinction", Range(0, 10)) = 2.0
		_StarsTwinklingSpeed("Stars Twinkling Speed", Range(0, 25)) = 4.0
		[NoScaleOffset]
		_StarsTex("Stars Cubemap", Cube) = "grey" {}

		// Sun
		_SunSize("Sun Size", Range(0.1, 3)) = 1.0
		_SunTint("Sun Tint", Color) = (.5, .5, .5, 1.0)
		_SunHalo("Sun Halo", Range(0, 2)) = 1.0
		[NoScaleOffset]
		_SunTex("Sun Texture", 2D) = "grey" {}

		// Moon
		_MoonSize("Moon Size", Range(0.1, 3)) = 1.0
		_MoonTint("Moon Tint", Color) = (.5, .5, .5, 1.0)
		_MoonHalo("Moon Halo", Range(0, 2)) = 1.0
		[NoScaleOffset]
		_MoonTex("Moon Texture", 2D) = "grey" {}

		// Clouds
		_CloudsTint("Clouds Tint", Color) = (.5, .5, .5, 1.0)
		_CloudsRotation("Clouds Rotation", Range(0, 360)) = 0
		_CloudsHeight("Clouds Height", Range(-0.75, 0.75)) = 0
		[NoScaleOffset]
		_CloudsTex("Clouds Cubemap", Cube) = "grey" {}

		// General
		[Gamma] _Exposure("Exposure", Range(0, 8)) = 1.0
	}

	CustomEditor "LowPolyShaderGUI"

	SubShader{
		Tags{ "Queue" = "Background" "RenderType" = "Background" "PreviewType" = "Skybox" }
		Cull Off ZWrite Off

		Pass{
			CGPROGRAM
			#pragma target 3.0
			#pragma vertex vert
			#pragma fragment frag

			#pragma shader_feature STARS_OFF
			#pragma shader_feature SUN_OFF
			#pragma shader_feature MOON_OFF
			#pragma shader_feature CLOUDS_OFF

			#include "UnityCG.cginc"

			// Exposed
			half3 _TopColor;
			half3 _MiddleColor;
			half3 _BottomColor;

			half _TopExponent;
			half _BottomExponent;			
			
			#if !STARS_OFF
				half4 _StarsTint;
				half _StarsTwinklingSpeed;
				half _StarsExtinction;
				samplerCUBE _StarsTex;
			#endif

			#if !SUN_OFF
				fixed _SunSize;
				fixed _SunHalo;
				half4 _SunTint;
				sampler2D _SunTex;
				float4x4 sunMatrix;
			#endif

			#if !MOON_OFF
				fixed _MoonSize;
				fixed _MoonHalo;
				half4 _MoonTint;
				sampler2D _MoonTex;
				float4x4 moonMatrix;
			#endif

			#if !CLOUDS_OFF
				half3 _CloudsTint;
				float _CloudsRotation;
				fixed _CloudsHeight;
				samplerCUBE _CloudsTex;
			#endif

			half _Exposure;

			// -----------------------------------------
			// Structs
			// -----------------------------------------

			struct appdata
            {
                float4 vertex   : POSITION;

                // Single pass instanced rendering
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

			struct v2f {
				float4 position : SV_POSITION;
				float3 vertex : TEXCOORD0;

				#if !SUN_OFF
					float3 sunPosition : TEXCOORD1;
				#endif
				#if !MOON_OFF
					float3 moonPosition : TEXCOORD2;
				#endif
				#if !CLOUDS_OFF
				float3 cloudsPosition : TEXCOORD3;
				#endif
				#if !STARS_OFF
					float3 twinklingPosition : TEXCOORD4;
				#endif

                // Single pass instanced rendering
				UNITY_VERTEX_OUTPUT_STEREO
			};

			// -----------------------------------------
			// Functions
			// -----------------------------------------

			#if !STARS_OFF || !CLOUDS_OFF
				float4 RotateAroundYInDegrees(float4 vertex, float degrees)
				{
					float alpha = degrees * UNITY_PI / 180.0;
					float sina, cosa;
					sincos(alpha, sina, cosa);
					float2x2 m = float2x2(cosa, -sina, sina, cosa);
					return float4(mul(m, vertex.xz), vertex.yw).xzyw;
				}
			#endif

			#if !SUN_OFF || !MOON_OFF
				static const half4 kHaloBase = half4(1.0, 1.0, 1.0, 0);

				half4 CelestialColor(float3 position, sampler2D tex, fixed size, half4 tint, fixed haloCoef) {
					fixed depthCheck = step(position.z, 0); // equivalent of (position.z < 0)			
					half4 sTex = tex2D(tex, position.xy / (0.5 * size) + float2(0.5, 0.5));

					half4 halo = 1.0 - smoothstep(0, 0.35 * size, length(position.xy));
					sTex = sTex.r + (kHaloBase + 1.75 * halo * halo) * haloCoef * sTex.b;

					tint.rgb = unity_ColorSpaceDouble.rgb * tint.rgb;

					return depthCheck * sTex * tint;
				}
			#endif

			v2f vert(appdata v)
			{
				v2f OUT;

                // Single pass instanced rendering
				UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_OUTPUT(v2f, OUT);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

				OUT.position = UnityObjectToClipPos(v.vertex);
				OUT.vertex = v.vertex;

				#if !SUN_OFF
					OUT.sunPosition = mul(sunMatrix, v.vertex);
				#endif
				#if !MOON_OFF
					OUT.moonPosition = mul(moonMatrix, v.vertex);
				#endif
				#if !CLOUDS_OFF
					OUT.cloudsPosition = RotateAroundYInDegrees(v.vertex, _CloudsRotation);
					OUT.cloudsPosition.y -= _CloudsHeight;
				#endif
				#if !STARS_OFF
					OUT.twinklingPosition = RotateAroundYInDegrees(v.vertex, _Time.y * _StarsTwinklingSpeed);
				#endif
				return OUT;
			}

			half4 frag(v2f IN) : SV_Target
			{
				float t1 = max(+IN.vertex.y, 0);
				float t2 = max(-IN.vertex.y, 0);
				
				// Gradient
				half3 color = lerp(lerp(_MiddleColor, _TopColor, pow(t1, _TopExponent)), _BottomColor, pow(t2, _BottomExponent));

				// Stars
				#if !STARS_OFF
					half starsVal = texCUBE(_StarsTex, IN.vertex).r;
					half twinklingVal = texCUBE(_StarsTex, IN.twinklingPosition).b;
					half extinction = saturate(abs(IN.vertex.y * _StarsExtinction));
					half starsCoef = starsVal * twinklingVal * _StarsTint.a * extinction;
					color = color * (1 - starsCoef) + (_StarsTint.rgb * unity_ColorSpaceDouble.rgb) * starsCoef;
				#endif

				// Sun
				#if !SUN_OFF
					half4 sunColor = CelestialColor(IN.sunPosition, _SunTex, _SunSize, _SunTint, _SunHalo);
					color = lerp(color, sunColor.rgb, sunColor.a);
				#endif

				// Moon
				#if !MOON_OFF
					half4 moonColor = CelestialColor(IN.moonPosition, _MoonTex, _MoonSize, _MoonTint, _MoonHalo);
					color = lerp(color, moonColor.rgb, moonColor.a);
				#endif

				// Clouds
				#if !CLOUDS_OFF
					half3 cloudsTex = texCUBE(_CloudsTex, IN.cloudsPosition);
					color = (cloudsTex.r * (unity_ColorSpaceDouble.rgb * _CloudsTint.rgb) + cloudsTex.b * color) * _Exposure;
				#endif

				return half4(color, 1);
			}
			ENDCG
		}
	}
}