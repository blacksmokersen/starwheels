// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify/TP_Portal"
{
	Properties
	{
		_EdgeLength ( "Edge length", Range( 2, 50 ) ) = 15
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Float0("Float 0", Float) = 0.1
		_Float1("Float 1", Float) = 0.6
		_Texture0("Texture 0", 2D) = "bump" {}
		_DistorsionSpeed("DistorsionSpeed", Float) = 0.01
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 4.6
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform sampler2D _Texture0;
		uniform float _DistorsionSpeed;
		uniform sampler2D _TextureSample0;
		uniform float4 _TextureSample0_ST;
		uniform float _Float0;
		uniform float _Float1;
		uniform float _EdgeLength;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float3 ase_vertex3Pos = v.vertex.xyz;
			float2 temp_cast_0 = (_DistorsionSpeed).xx;
			float2 uv_TexCoord38 = v.texcoord.xy * float2( 0.5,0.5 );
			float2 panner33 = ( 1.0 * _Time.y * temp_cast_0 + uv_TexCoord38);
			float2 temp_cast_1 = (( _DistorsionSpeed * -1.0 )).xx;
			float2 panner41 = ( 1.0 * _Time.y * temp_cast_1 + uv_TexCoord38);
			float clampResult48 = clamp( ( tex2Dlod( _Texture0, float4( panner33, 0, 0.0) ).g + tex2Dlod( _Texture0, float4( panner41, 0, 0.0) ).a ) , 0.0 , 1.0 );
			v.vertex.xyz += ( ase_vertex3Pos * clampResult48 );
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample0 = i.uv_texcoord * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float clampResult19 = clamp( ( ( 1.0 - distance( 0 , ase_vertex3Pos.x ) ) * ( 1.0 - distance( 0 , ase_vertex3Pos.y ) ) ) , 0.0 , 1.0 );
			float smoothstepResult10 = smoothstep( _Float0 , _Float1 , clampResult19);
			o.Emission = ( tex2D( _TextureSample0, uv_TextureSample0 ) * smoothstepResult10 ).rgb;
			float2 temp_cast_1 = (_DistorsionSpeed).xx;
			float2 uv_TexCoord38 = i.uv_texcoord * float2( 0.5,0.5 );
			float2 panner33 = ( 1.0 * _Time.y * temp_cast_1 + uv_TexCoord38);
			float2 temp_cast_2 = (( _DistorsionSpeed * -1.0 )).xx;
			float2 panner41 = ( 1.0 * _Time.y * temp_cast_2 + uv_TexCoord38);
			float clampResult48 = clamp( ( tex2D( _Texture0, panner33 ).g + tex2D( _Texture0, panner41 ).a ) , 0.0 , 1.0 );
			o.Alpha = ( (0.0 + (clampResult48 - 0.0) * (0.6 - 0.0) / (1.0 - 0.0)) * smoothstepResult10 );
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Standard alpha:fade keepalpha fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 4.6
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
				vertexDataFunc( v );
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
				surfIN.worldPos = worldPos;
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
0;574;1534;427;343.7703;114.8174;1.776317;True;True
Node;AmplifyShaderEditor.RangedFloatNode;36;-2117.231,-302.4861;Float;False;Property;_DistorsionSpeed;DistorsionSpeed;9;0;Create;True;0;0;False;0;0.01;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;8;-1056.446,55.64549;Float;False;Constant;_Vector0;Vector 0;1;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;16;-1033.39,375.6637;Float;False;Constant;_Vector1;Vector 1;1;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.PosVertexDataNode;15;-1076.386,552.609;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;9;-1097.442,213.5908;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;38;-1863.386,-501.6804;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.5,0.5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-1808.68,-209.4059;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;4;-785.6425,142.2682;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DistanceOpNode;17;-764.5864,481.2864;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;39;-1524.783,-591.2783;Float;True;Property;_Texture0;Texture 0;8;0;Create;True;0;0;False;0;1bebd6d7ff975584bb5a33a6b4ab76e0;1bebd6d7ff975584bb5a33a6b4ab76e0;True;bump;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;41;-1505.713,-192.5357;Float;False;3;0;FLOAT2;5,5;False;2;FLOAT2;0,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;33;-1510.457,-357.9763;Float;False;3;0;FLOAT2;5,5;False;2;FLOAT2;0,0.1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;29;-1138.692,-359.8201;Float;True;Property;_TextureSample1;Texture Sample 1;3;0;Create;True;0;0;False;0;1bebd6d7ff975584bb5a33a6b4ab76e0;1bebd6d7ff975584bb5a33a6b4ab76e0;True;0;True;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;40;-1137.812,-167.8357;Float;True;Property;_TextureSample2;Texture Sample 2;3;0;Create;True;0;0;False;0;1bebd6d7ff975584bb5a33a6b4ab76e0;1bebd6d7ff975584bb5a33a6b4ab76e0;True;0;True;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;24;-479.9719,468.9088;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;23;-490.4413,205.6829;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;44;-757.1658,-238.2893;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-260.5717,335.8564;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;19;-2.532172,294.3509;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;48;-490.5917,-246.3802;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;20;-20.92804,64.04008;Float;False;Property;_Float0;Float 0;6;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-14.78347,179.0656;Float;False;Property;_Float1;Float 1;7;0;Create;True;0;0;False;0;0.6;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;837.7182,-325.22;Float;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;False;0;350b51827f871174ba5e7822c569aee9;350b51827f871174ba5e7822c569aee9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;64;815.4428,427.4986;Float;True;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;10;272.0918,193.607;Float;True;3;0;FLOAT;0;False;1;FLOAT;0.4;False;2;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;74;922.3661,15.71674;Float;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;0.6;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;73;-189.2114,-360.3129;Float;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.9;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;52;161.4014,-382.9479;Float;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;1157.875,346.4562;Float;True;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;1217.322,-227.9142;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;71;1262.105,88.40544;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;75;475.2618,547.2677;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;68;1662.102,-70.48996;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;Amplify/TP_Portal;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;0;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;45;0;36;0
WireConnection;4;0;8;1
WireConnection;4;1;9;1
WireConnection;17;0;16;2
WireConnection;17;1;15;2
WireConnection;41;0;38;0
WireConnection;41;2;45;0
WireConnection;33;0;38;0
WireConnection;33;2;36;0
WireConnection;29;0;39;0
WireConnection;29;1;33;0
WireConnection;40;0;39;0
WireConnection;40;1;41;0
WireConnection;24;0;17;0
WireConnection;23;0;4;0
WireConnection;44;0;29;2
WireConnection;44;1;40;4
WireConnection;18;0;23;0
WireConnection;18;1;24;0
WireConnection;19;0;18;0
WireConnection;48;0;44;0
WireConnection;10;0;19;0
WireConnection;10;1;20;0
WireConnection;10;2;21;0
WireConnection;74;0;48;0
WireConnection;73;0;48;0
WireConnection;52;1;73;0
WireConnection;69;0;64;0
WireConnection;69;1;48;0
WireConnection;12;0;2;0
WireConnection;12;1;10;0
WireConnection;71;0;74;0
WireConnection;71;1;10;0
WireConnection;75;0;48;0
WireConnection;75;1;10;0
WireConnection;68;2;12;0
WireConnection;68;9;71;0
WireConnection;68;11;69;0
ASEEND*/
//CHKSM=AF1F80917F8C69C15703F06F85D2FE281DEF889C