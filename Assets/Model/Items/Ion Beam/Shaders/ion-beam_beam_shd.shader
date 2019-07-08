// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Item/Ion Beam/Beam"
{
	Properties
	{
		_Fresnel("Fresnel", Float) = 1
		[HDR]_Color("Color", Color) = (1,1,1,1)
		_Interieur("Interieur", Float) = 0.5
		_Speed("Speed", Float) = 0.5
		_Base("Base", 2D) = "white" {}
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
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform float _Fresnel;
			uniform sampler2D _Base;
			uniform float _Speed;
			uniform float4 _Base_ST;
			uniform float _Interieur;
			uniform float4 _Color;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.ase_texcoord.xyz = ase_worldPos;
				float3 ase_worldNormal = UnityObjectToWorldNormal(v.ase_normal);
				o.ase_texcoord1.xyz = ase_worldNormal;
				
				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.w = 0;
				o.ase_texcoord1.w = 0;
				o.ase_texcoord2.zw = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float3 ase_worldPos = i.ase_texcoord.xyz;
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(ase_worldPos);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = i.ase_texcoord1.xyz;
				float fresnelNdotV8 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode8 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV8, _Fresnel ) );
				float clampResult6 = clamp( pow( fresnelNode8 , 5.0 ) , 0.0 , 1.0 );
				float temp_output_33_0 = ( _Speed * _Time.y );
				float2 uv_Base = i.ase_texcoord2.xy * _Base_ST.xy + _Base_ST.zw;
				float2 panner34 = ( temp_output_33_0 * float2( 0,0 ) + uv_Base);
				float2 temp_output_44_0 = ( uv_Base * float2( 2,2 ) );
				float2 panner35 = ( temp_output_33_0 * float2( 0,0 ) + temp_output_44_0);
				float2 panner36 = ( temp_output_33_0 * float2( -0.5,-0.85 ) + temp_output_44_0);
				float2 panner37 = ( temp_output_33_0 * float2( 0.675,-1.575 ) + temp_output_44_0);
				float2 panner38 = ( temp_output_33_0 * float2( -0.75,-1.275 ) + ( uv_Base * float2( 7,7 ) ));
				float2 panner39 = ( temp_output_33_0 * float2( 0.45,-1.05 ) + ( uv_Base * float2( 6,6 ) ));
				float fresnelNdotV25 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode25 = ( 0.0 + 1.0 * pow( 1.0 - fresnelNdotV25, 0.0 ) );
				float2 panner40 = ( temp_output_33_0 * float2( -0.1,0 ) + ( uv_Base * float2( 1.1,1.1 ) ));
				float2 panner41 = ( temp_output_33_0 * float2( 0.05,0 ) + uv_Base);
				float clampResult3 = clamp( ( clampResult6 + ( ( tex2D( _Base, panner34 ).r + tex2D( _Base, panner35 ).r ) * 0.25 ) + ( ( ( 2.0 * tex2D( _Base, panner36 ).b * tex2D( _Base, panner37 ).b ) + ( 2.0 * tex2D( _Base, panner38 ).b * tex2D( _Base, panner39 ).b ) ) * pow( ( fresnelNode25 * _Interieur ) , 2.0 ) ) + ( pow( ( 2.0 * tex2D( _Base, panner40 ).a * tex2D( _Base, panner41 ).a ) , 3.0 ) * 0.5 ) ) , 0.0 , 1.0 );
				
				
				finalColor = ( clampResult3 * _Color * _Color.a );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
1954;10;1844;1027;1328.541;358.3983;1;True;False
Node;AmplifyShaderEditor.TimeNode;31;-3580.232,767.6417;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;32;-3544.132,615.6417;Float;False;Property;_Speed;Speed;3;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;42;-3005.891,115.5343;Float;False;0;43;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;-2490.325,834.342;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;6,6;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-3351.56,714.6028;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-2543.325,648.342;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;7,7;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-2444.27,1158.141;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;1.1,1.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-2641.273,272.9853;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;2,2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;43;-2278.037,-180.2216;Float;True;Property;_Base;Base;4;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;39;-2152.184,1057.339;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.45,-1.05;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;37;-2158.477,692.3634;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.675,-1.575;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;41;-2131.208,1527.192;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.05,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;40;-2152.184,1334.217;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;36;-2173.159,501.4853;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.5,-0.85;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;38;-2168.965,908.412;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.75,-1.275;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;19;-1814.963,1091.351;Float;True;Property;_TextureSample4;Texture Sample 4;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;9;-1754.365,-189.9348;Float;False;Property;_Fresnel;Fresnel;0;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;-1816.263,488.584;Float;True;Property;_TextureSample2;Texture Sample 2;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;23;-1809.763,1334.451;Float;True;Property;_TextureSample7;Texture Sample 7;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;34;-2206.72,67.29045;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;22;-1812.363,1533.351;Float;True;Property;_TextureSample6;Texture Sample 6;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;28;-1215.235,1315.819;Float;False;Property;_Interieur;Interieur;2;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;35;-2183.647,314.8025;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FresnelNode;25;-1378.221,1143.382;Float;False;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;15;-1818.863,687.4839;Float;True;Property;_TextureSample3;Texture Sample 3;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;20;-1812.363,892.4507;Float;True;Property;_TextureSample5;Texture Sample 5;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;11;-1803.263,288.3841;Float;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1366.463,1437.151;Float;False;3;3;0;FLOAT;2;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;10;-1814.963,33.58405;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FresnelNode;8;-1513.365,-250.9348;Float;False;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-1372.963,591.2839;Float;False;3;3;0;FLOAT;2;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1375.6,978.1548;Float;False;3;3;0;FLOAT;2;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-1064.964,1159.959;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;29;-1119.235,1472.819;Float;False;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-1486.063,77.78408;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-1131.163,605.584;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;26;-917.8126,1168.691;Float;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;7;-1196.9,-169;Float;False;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;-1272.864,60.88402;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.25;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-938.7629,606.884;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;6;-1013,-183;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-895.2355,1512.819;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;5;-728,-90;Float;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;3;-565,-92;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;4;-588,135;Float;False;Property;_Color;Color;1;1;[HDR];Create;True;0;0;False;0;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1;-227,-7;Float;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;1;Item/Ion Beam/Beam;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;4;1;False;-1;1;False;-1;4;1;False;-1;1;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;46;0;42;0
WireConnection;33;0;32;0
WireConnection;33;1;31;2
WireConnection;45;0;42;0
WireConnection;47;0;42;0
WireConnection;44;0;42;0
WireConnection;39;0;46;0
WireConnection;39;1;33;0
WireConnection;37;0;44;0
WireConnection;37;1;33;0
WireConnection;41;0;42;0
WireConnection;41;1;33;0
WireConnection;40;0;47;0
WireConnection;40;1;33;0
WireConnection;36;0;44;0
WireConnection;36;1;33;0
WireConnection;38;0;45;0
WireConnection;38;1;33;0
WireConnection;19;0;43;0
WireConnection;19;1;39;0
WireConnection;14;0;43;0
WireConnection;14;1;36;0
WireConnection;23;0;43;0
WireConnection;23;1;40;0
WireConnection;34;0;42;0
WireConnection;34;1;33;0
WireConnection;22;0;43;0
WireConnection;22;1;41;0
WireConnection;35;0;44;0
WireConnection;35;1;33;0
WireConnection;15;0;43;0
WireConnection;15;1;37;0
WireConnection;20;0;43;0
WireConnection;20;1;38;0
WireConnection;11;0;43;0
WireConnection;11;1;35;0
WireConnection;21;1;23;4
WireConnection;21;2;22;4
WireConnection;10;0;43;0
WireConnection;10;1;34;0
WireConnection;8;3;9;0
WireConnection;16;1;14;3
WireConnection;16;2;15;3
WireConnection;18;1;20;3
WireConnection;18;2;19;3
WireConnection;27;0;25;0
WireConnection;27;1;28;0
WireConnection;29;0;21;0
WireConnection;12;0;10;1
WireConnection;12;1;11;1
WireConnection;17;0;16;0
WireConnection;17;1;18;0
WireConnection;26;0;27;0
WireConnection;7;0;8;0
WireConnection;13;0;12;0
WireConnection;24;0;17;0
WireConnection;24;1;26;0
WireConnection;6;0;7;0
WireConnection;30;0;29;0
WireConnection;5;0;6;0
WireConnection;5;1;13;0
WireConnection;5;2;24;0
WireConnection;5;3;30;0
WireConnection;3;0;5;0
WireConnection;1;0;3;0
WireConnection;1;1;4;0
WireConnection;1;2;4;4
WireConnection;0;0;1;0
ASEEND*/
//CHKSM=FD68E62287CF935BC5F5D9A8ACD22A321CF9795D