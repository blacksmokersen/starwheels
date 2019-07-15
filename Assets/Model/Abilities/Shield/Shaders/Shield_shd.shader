// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Abilities/Shield"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (1,1,1,1)
		_Texture("Texture", 2D) = "white" {}
		_Fresnel("Fresnel", Float) = 1
		_Interieur("Interieur", Float) = 1
		_Speed("Speed", Float) = 0.5
		_GroundFade("Ground Fade", Float) = 1
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend One One , One One
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		

		Pass
		{
			Name "Unlit"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
				float3 ase_normal : NORMAL;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform float4 _Color;
			uniform sampler2D _Texture;
			uniform float _Speed;
			uniform float4 _Texture_ST;
			uniform float _Interieur;
			uniform float _Fresnel;
			uniform sampler2D _CameraDepthTexture;
			uniform float _GroundFade;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.ase_texcoord1.xyz = ase_worldPos;
				float3 ase_worldNormal = UnityObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord2.xyz = ase_worldNormal;
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord3 = screenPos;
				
				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				o.ase_texcoord1.w = 0;
				o.ase_texcoord2.w = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float temp_output_30_0 = ( _Time.y * _Speed );
				float2 uv_Texture = i.ase_texcoord.xy * _Texture_ST.xy + _Texture_ST.zw;
				float2 panner20 = ( temp_output_30_0 * float2( 0,1 ) + ( uv_Texture * float2( 2,2 ) ));
				float2 panner23 = ( temp_output_30_0 * float2( -1,-1 ) + ( uv_Texture * float2( 5,4 ) ));
				float3 ase_worldPos = i.ase_texcoord1.xyz;
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(ase_worldPos);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = i.ase_texcoord2.xyz;
				float fresnelNdotV12 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode12 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV12, _Interieur ) );
				float clampResult5 = clamp( ( 1.0 - round( ( ( tex2D( _Texture, panner20 ).r + tex2D( _Texture, panner23 ).r ) * fresnelNode12 ) ) ) , 0.0 , 1.0 );
				float fresnelNdotV9 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode9 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV9, _Fresnel ) );
				float clampResult6 = clamp( pow( ( fresnelNode9 * 1.5 ) , 5.0 ) , 0.0 , 1.0 );
				float4 screenPos = i.ase_texcoord3;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth39 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD( screenPos ))));
				float distanceDepth39 = abs( ( screenDepth39 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _GroundFade ) );
				float clampResult33 = clamp( ( 1.0 - distanceDepth39 ) , 0.0 , 1.0 );
				
				
				finalColor = ( ( _Color * _Color.a * ( clampResult5 + clampResult6 ) ) + ( _Color * _Color.a * clampResult33 ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
1920;1;1906;1051;798.9944;339.7469;1;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;24;-2897.828,-197.3982;Float;False;0;19;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;29;-2933.828,177.6018;Float;False;Property;_Speed;Speed;5;0;Create;True;0;0;False;0;0.5;0.03;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;28;-2977.828,36.60181;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-2572.828,-69.39819;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;5,4;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-2577.828,-172.3982;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;2,2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-2775.828,110.6018;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;19;-2426.828,-377.3982;Float;True;Property;_Texture;Texture;2;0;Create;True;0;0;False;0;None;a70feaecb0d54b6489d3c2e20d554370;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;20;-2374.828,-132.3982;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;23;-2371.828,-3.398163;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-1,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-1640.054,271.8446;Float;False;Property;_Interieur;Interieur;4;0;Create;True;0;0;False;0;1;-3.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;17;-2065.054,-3.155396;Float;True;Property;_TextureSample1;Texture Sample 1;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;16;-2058.054,-217.1554;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;12;-1430.054,196.8446;Float;False;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-1619.067,496.5535;Float;False;Property;_Fresnel;Fresnel;3;0;Create;True;0;0;False;0;1;2.67;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;46;-1345.197,-151.1284;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-1151.536,112.1923;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FresnelNode;9;-1441.067,420.5535;Float;False;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-1174.013,375.7089;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;1.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;32;-1665.112,925.5148;Float;False;Property;_GroundFade;Ground Fade;6;0;Create;True;0;0;False;0;1;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RoundOpNode;47;-983.629,96.28953;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;7;-1003.013,370.7089;Float;False;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;39;-1403.565,756.8456;Float;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;48;-828.2963,105.7476;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;34;-908.4695,589.1474;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;6;-814.0127,342.7089;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;5;-674.1715,127.9149;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;4;-482.9602,174.1809;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;33;-702.4695,590.1474;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-548.617,-199.0365;Float;False;Property;_Color;Color;1;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0,0.454902,2.996078,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-1.751579,-7.893631;Float;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;41;-1.699203,133.5317;Float;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;40;286.9286,126.5164;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;51;622.8695,9.48073;Float;False;True;2;Float;ASEMaterialInspector;0;1;Abilities/Shield;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;4;1;False;-1;1;False;-1;4;1;False;-1;1;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;27;0;24;0
WireConnection;25;0;24;0
WireConnection;30;0;28;2
WireConnection;30;1;29;0
WireConnection;20;0;25;0
WireConnection;20;1;30;0
WireConnection;23;0;27;0
WireConnection;23;1;30;0
WireConnection;17;0;19;0
WireConnection;17;1;23;0
WireConnection;16;0;19;0
WireConnection;16;1;20;0
WireConnection;12;3;13;0
WireConnection;46;0;16;1
WireConnection;46;1;17;1
WireConnection;11;0;46;0
WireConnection;11;1;12;0
WireConnection;9;3;10;0
WireConnection;8;0;9;0
WireConnection;47;0;11;0
WireConnection;7;0;8;0
WireConnection;39;0;32;0
WireConnection;48;0;47;0
WireConnection;34;0;39;0
WireConnection;6;0;7;0
WireConnection;5;0;48;0
WireConnection;4;0;5;0
WireConnection;4;1;6;0
WireConnection;33;0;34;0
WireConnection;3;0;2;0
WireConnection;3;1;2;4
WireConnection;3;2;4;0
WireConnection;41;0;2;0
WireConnection;41;1;2;4
WireConnection;41;2;33;0
WireConnection;40;0;3;0
WireConnection;40;1;41;0
WireConnection;51;0;40;0
ASEEND*/
//CHKSM=FBD74D154E8DBBD74F7182DF60E89F50FA61B06D