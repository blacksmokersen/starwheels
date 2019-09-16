// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Map/Security_Complex/Laser_Wall"
{
	Properties
	{
		_ClosedOpen("Closed-Open", Range( 0 , 1)) = 0.5
		_Noise_Texture("Noise_Texture", 2D) = "white" {}
		[HDR]_Wall_Color("Wall_Color", Color) = (1,0,0,1)
		[HDR]_Border_Color("Border_Color", Color) = (0,0,0,0)
		[HDR]_Particles_Color("Particles_Color", Color) = (1,0.07060622,0,0)
		_Tiling("Tiling", Float) = 0
		_Vertical_Border_Thickness("Vertical_Border_Thickness", Range( 0 , 0.5)) = 0.025
		_Fine_Border_Thickness("Fine_Border_Thickness", Range( 0 , 0.5)) = 0.1
		_Wall_Panning_Speed("Wall_Panning_Speed", Float) = 0.5
		_Particles_Texture("Particles_Texture", 2D) = "white" {}
		_Particles_Speed("Particles_Speed", Float) = 0
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha , SrcAlpha OneMinusSrcAlpha
		
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Wall_Color;
		uniform sampler2D _Noise_Texture;
		uniform float _Tiling;
		uniform float _Wall_Panning_Speed;
		uniform float _ClosedOpen;
		uniform float _Vertical_Border_Thickness;
		uniform float _Fine_Border_Thickness;
		uniform float4 _Particles_Color;
		uniform sampler2D _Particles_Texture;
		uniform float _Particles_Speed;
		uniform float4 _Border_Color;
		uniform float _Cutoff = 0.5;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 temp_cast_0 = (_Tiling).xx;
			float4 appendResult287 = (float4(( _Time.y * _Wall_Panning_Speed ) , 0.0 , 0.0 , 0.0));
			float2 uv_TexCoord267 = i.uv_texcoord * temp_cast_0 + appendResult287.xy;
			float smoothstepResult311 = smoothstep( 0.45 , 0.55 , i.uv_texcoord.x);
			float2 temp_cast_2 = (_Tiling).xx;
			float4 appendResult295 = (float4(( _Time.y * ( _Wall_Panning_Speed * -1.0 ) ) , 0.0 , 0.0 , 0.0));
			float2 uv_TexCoord296 = i.uv_texcoord * temp_cast_2 + appendResult295.xy;
			float4 temp_output_310_0 = ( ( tex2D( _Noise_Texture, uv_TexCoord267 ) * smoothstepResult311 ) + ( tex2D( _Noise_Texture, uv_TexCoord296 ) * ( 1.0 - smoothstepResult311 ) ) );
			float temp_output_235_0 = ( 1.0 - _ClosedOpen );
			float smoothstepResult32 = smoothstep( ( temp_output_235_0 - _Vertical_Border_Thickness ) , temp_output_235_0 , i.uv_texcoord.x);
			float smoothstepResult7 = smoothstep( _ClosedOpen , ( _ClosedOpen + _Vertical_Border_Thickness ) , i.uv_texcoord.x);
			float clampResult128 = clamp( ( ( 1.0 - smoothstepResult32 ) + smoothstepResult7 ) , 0.0 , 1.0 );
			float2 _Vector1 = float2(0.42,0.38);
			float smoothstepResult44 = smoothstep( _Vector1.x , _Vector1.y , i.uv_texcoord.y);
			float2 _Vector0 = float2(0.015,0.05);
			float smoothstepResult42 = smoothstep( _Vector0.x , _Vector0.y , i.uv_texcoord.y);
			float temp_output_278_0 = (-1.0 + (( clampResult128 * ( smoothstepResult44 * smoothstepResult42 ) ) - 0.0) * (1.5 - -1.0) / (1.0 - 0.0));
			float4 temp_cast_4 = (( temp_output_278_0 - _Fine_Border_Thickness )).xxxx;
			float4 temp_output_262_0 = step( temp_output_310_0 , temp_cast_4 );
			float4 appendResult72 = (float4(( _Time.y * _Particles_Speed ) , 0.0 , 0.0 , 0.0));
			float2 uv_TexCoord65 = i.uv_texcoord * float2( 1,6 ) + appendResult72.xy;
			float smoothstepResult166 = smoothstep( 0.45 , 0.55 , i.uv_texcoord.x);
			float4 appendResult77 = (float4(( ( _Time.y + 1.0 ) * ( _Particles_Speed * -1.0 ) ) , 0.5 , 0.0 , 0.0));
			float2 uv_TexCoord73 = i.uv_texcoord * float2( 1,6 ) + appendResult77.xy;
			float4 temp_cast_7 = (temp_output_278_0).xxxx;
			float4 temp_output_279_0 = step( temp_output_310_0 , temp_cast_7 );
			float4 temp_cast_8 = (( temp_output_278_0 - _Fine_Border_Thickness )).xxxx;
			float smoothstepResult318 = smoothstep( 0.95 , 1.0 , i.uv_texcoord.x);
			float smoothstepResult320 = smoothstep( 0.05 , 0.0 , i.uv_texcoord.x);
			o.Emission = ( ( ( ( _Wall_Color * temp_output_262_0 ) + ( _Particles_Color * ( ( tex2D( _Particles_Texture, uv_TexCoord65 ).r * smoothstepResult166 ) + ( tex2D( _Particles_Texture, uv_TexCoord73 ).r * ( 1.0 - smoothstepResult166 ) ) ) ) ) + ( ( temp_output_279_0 - temp_output_262_0 ) * _Border_Color ) ) + ( _Border_Color * ( smoothstepResult318 + smoothstepResult320 ) ) ).rgb;
			o.Alpha = _Wall_Color.a;
			clip( temp_output_279_0.r - _Cutoff );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				SurfaceOutputStandard o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandard, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16103
177;501;1234;372;-6924.055;-90.02594;1.3;True;True
Node;AmplifyShaderEditor.CommentaryNode;85;482.5743,-1232.821;Float;False;2923.216;1399.569;Comment;17;63;49;128;44;42;146;7;48;238;47;32;34;246;13;235;30;27;Wall;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;27;590.2339,-1111.891;Float;False;Property;_ClosedOpen;Closed-Open;0;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;30;835.2781,-856.4108;Float;False;Property;_Vertical_Border_Thickness;Vertical_Border_Thickness;6;0;Create;True;0;0;False;0;0.025;0.5;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;235;1090.61,-1069.193;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;299;1249.73,314.1561;Float;False;4208.665;1008.234;Comment;24;304;296;298;294;295;302;284;262;279;301;283;267;300;291;303;287;288;289;286;308;309;310;314;311;Noise Dissolve;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;82;3407.509,1463.611;Float;False;3185.119;1007.449;Comment;19;91;65;73;77;72;68;78;80;66;69;162;164;166;165;167;169;168;170;247;Particles;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;289;1266.05,824.005;Float;False;Property;_Wall_Panning_Speed;Wall_Panning_Speed;8;0;Create;True;0;0;False;0;0.5;0.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;246;1323.229,-990.744;Float;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;13;899.4469,-644.9097;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;32;1674.204,-945.8544;Float;True;3;0;FLOAT;0;False;1;FLOAT;0.6;False;2;FLOAT;0.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;286;1537.153,772.3817;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;66;3661.225,1513.611;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;34;1203.095,-817.4995;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;298;1513.379,912.2981;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;69;3457.509,1641.107;Float;False;Property;_Particles_Speed;Particles_Speed;10;0;Create;True;0;0;False;0;0;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;294;1789.781,1021.944;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;288;1790.654,830.0361;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;238;2008.402,-957.8411;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;7;1675.527,-701.6159;Float;True;3;0;FLOAT;0;False;1;FLOAT;19;False;2;FLOAT;20;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;170;3799.136,1786.985;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;80;3848.983,1600.359;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;48;1823.357,-325.7358;Float;False;Constant;_Vector1;Vector 1;4;0;Create;True;0;0;False;0;0.42,0.38;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;47;1804.568,-158.1797;Float;False;Constant;_Vector0;Vector 0;4;0;Create;True;0;0;False;0;0.015,0.05;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;4076.117,1535.124;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;42;2166.913,-128.8927;Float;True;3;0;FLOAT;0;False;1;FLOAT;0.01;False;2;FLOAT;0.03;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;303;1798.917,594.2613;Float;False;Property;_Tiling;Tiling;5;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;44;2167.066,-364.6328;Float;True;3;0;FLOAT;0;False;1;FLOAT;19;False;2;FLOAT;20;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;287;2051.392,799.8413;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;295;1993.862,1015.802;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;304;2509.275,1071.72;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;78;4039.187,1697.29;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;146;2327.065,-860.6327;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;300;2228.537,767.378;Float;True;Property;_Noise_Texture;Noise_Texture;1;0;Create;True;0;0;False;0;None;0def6eeb3d52c404a8a26ca9aff20c2f;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;267;2213.648,606.6413;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;72;4274.352,1567.754;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;77;4188.401,1814.988;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;165;4677.431,2124.594;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;128;2622.56,-856.8488;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;296;2242.076,1002.121;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;311;2911.176,1037.757;Float;True;3;0;FLOAT;0;False;1;FLOAT;0.45;False;2;FLOAT;0.55;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;2743.065,-364.6328;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;314;3253.162,724.2313;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;247;4577.145,1507.973;Float;True;Property;_Particles_Texture;Particles_Texture;9;0;Create;True;0;0;False;0;0def6eeb3d52c404a8a26ca9aff20c2f;0def6eeb3d52c404a8a26ca9aff20c2f;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;3067.768,-596.0886;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;166;5042.889,2159.744;Float;True;3;0;FLOAT;0;False;1;FLOAT;0.45;False;2;FLOAT;0.55;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;302;2616.276,847.7131;Float;True;Property;_TextureSample2;Texture Sample 2;11;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;73;4505.986,1882.68;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,6;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;301;2621.835,574.251;Float;True;Property;_TextureSample3;Texture Sample 3;11;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;65;4503.399,1705.326;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,6;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;291;3368.493,549.511;Float;False;Property;_Fine_Border_Thickness;Fine_Border_Thickness;7;0;Create;True;0;0;False;0;0.1;0.5;0;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;167;5379.561,2157.648;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;162;4893.586,1887.1;Float;True;Property;_TextureSample1;Texture Sample 1;7;0;Create;True;0;0;False;0;None;d2a8b7c745ef49e4398108d339a604e3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;308;3557.831,671.4863;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;309;3568.626,897.0454;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;91;4898.118,1670.392;Float;True;Property;_TextureSample0;Texture Sample 0;6;0;Create;True;0;0;False;0;None;d2a8b7c745ef49e4398108d339a604e3;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;278;3515.468,130.883;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;-1;False;4;FLOAT;1.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;312;5580.723,-522.5589;Float;False;1026.563;1354.922;Comment;7;316;274;272;273;23;87;86;Colors;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;323;5637.608,979.8475;Float;False;821.4707;343.0389;Comment;4;317;320;318;321;SideBorders;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;168;5319.992,1616.871;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;310;3847.895,783.6497;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;169;5387.316,1792.899;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;283;3716.995,475.3396;Float;False;2;0;FLOAT;0;False;1;FLOAT;0.1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;23;5644.859,-463.135;Float;False;Property;_Wall_Color;Wall_Color;2;1;[HDR];Create;True;0;0;False;0;1,0,0,1;5.992157,0,0,0.4509804;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;164;5739.717,1697.124;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;317;5687.608,1120.003;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;262;4130.195,683.2143;Float;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;86;5768.108,370.3036;Float;False;Property;_Particles_Color;Particles_Color;4;1;[HDR];Create;True;0;0;False;0;1,0.07060622,0,0;5.992157,1.098039,0.8784314,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;279;4105.371,423.4319;Float;True;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;318;5993.787,1029.848;Float;False;3;0;FLOAT;0;False;1;FLOAT;0.95;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;273;5741.843,-25.80963;Float;False;Property;_Border_Color;Border_Color;3;1;[HDR];Create;True;0;0;False;0;0,0,0,0;15.8134,3.642877,2.649365,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;272;6103.184,-450.837;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;284;4408.37,584.7143;Float;True;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;320;5993.384,1166.886;Float;False;3;0;FLOAT;0;False;1;FLOAT;0.05;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;87;6131.574,539.0669;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;321;6224.079,1068.155;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;274;6117.623,187.9067;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;316;6426.761,-84.73444;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;322;6753.634,610.2196;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;269;7031.745,167.7493;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;324;7465.333,274.3004;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;228;7784.566,189.7232;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Map/Security_Complex/Laser_Wall;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;2;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;11;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;235;0;27;0
WireConnection;246;0;235;0
WireConnection;246;1;30;0
WireConnection;32;0;13;1
WireConnection;32;1;246;0
WireConnection;32;2;235;0
WireConnection;34;0;27;0
WireConnection;34;1;30;0
WireConnection;298;0;289;0
WireConnection;294;0;286;0
WireConnection;294;1;298;0
WireConnection;288;0;286;0
WireConnection;288;1;289;0
WireConnection;238;0;32;0
WireConnection;7;0;13;1
WireConnection;7;1;27;0
WireConnection;7;2;34;0
WireConnection;170;0;69;0
WireConnection;80;0;66;0
WireConnection;68;0;66;0
WireConnection;68;1;69;0
WireConnection;42;0;13;2
WireConnection;42;1;47;1
WireConnection;42;2;47;2
WireConnection;44;0;13;2
WireConnection;44;1;48;1
WireConnection;44;2;48;2
WireConnection;287;0;288;0
WireConnection;295;0;294;0
WireConnection;78;0;80;0
WireConnection;78;1;170;0
WireConnection;146;0;238;0
WireConnection;146;1;7;0
WireConnection;267;0;303;0
WireConnection;267;1;287;0
WireConnection;72;0;68;0
WireConnection;77;0;78;0
WireConnection;128;0;146;0
WireConnection;296;0;303;0
WireConnection;296;1;295;0
WireConnection;311;0;304;1
WireConnection;49;0;44;0
WireConnection;49;1;42;0
WireConnection;314;0;311;0
WireConnection;63;0;128;0
WireConnection;63;1;49;0
WireConnection;166;0;165;1
WireConnection;302;0;300;0
WireConnection;302;1;296;0
WireConnection;73;1;77;0
WireConnection;301;0;300;0
WireConnection;301;1;267;0
WireConnection;65;1;72;0
WireConnection;167;0;166;0
WireConnection;162;0;247;0
WireConnection;162;1;73;0
WireConnection;308;0;301;0
WireConnection;308;1;311;0
WireConnection;309;0;302;0
WireConnection;309;1;314;0
WireConnection;91;0;247;0
WireConnection;91;1;65;0
WireConnection;278;0;63;0
WireConnection;168;0;91;1
WireConnection;168;1;166;0
WireConnection;310;0;308;0
WireConnection;310;1;309;0
WireConnection;169;0;162;1
WireConnection;169;1;167;0
WireConnection;283;0;278;0
WireConnection;283;1;291;0
WireConnection;164;0;168;0
WireConnection;164;1;169;0
WireConnection;262;0;310;0
WireConnection;262;1;283;0
WireConnection;279;0;310;0
WireConnection;279;1;278;0
WireConnection;318;0;317;1
WireConnection;272;0;23;0
WireConnection;272;1;262;0
WireConnection;284;0;279;0
WireConnection;284;1;262;0
WireConnection;320;0;317;1
WireConnection;87;0;86;0
WireConnection;87;1;164;0
WireConnection;321;0;318;0
WireConnection;321;1;320;0
WireConnection;274;0;284;0
WireConnection;274;1;273;0
WireConnection;316;0;272;0
WireConnection;316;1;87;0
WireConnection;322;0;273;0
WireConnection;322;1;321;0
WireConnection;269;0;316;0
WireConnection;269;1;274;0
WireConnection;324;0;269;0
WireConnection;324;1;322;0
WireConnection;228;2;324;0
WireConnection;228;9;23;4
WireConnection;228;10;279;0
ASEEND*/
//CHKSM=8102E9BBC0A91DBF7F1B709F06E94E84283CBCD4