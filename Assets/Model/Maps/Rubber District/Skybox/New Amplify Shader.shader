// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Map/RubberDistrict/Skybox"
{
	Properties
	{
		[HDR]_DarkSkyColor("DarkSkyColor", Color) = (0.06299388,0.03613386,0.2641509,0)
		[HDR]_HorizonColor("HorizonColor", Color) = (0.8867924,0.5563368,0.6655641,0)
		[HDR]_GroundColor("GroundColor", Color) = (0.4528302,0.3396226,0.3396226,1)
		_Float0("Float 0", Float) = 1
		_Float1("Float 1", Float) = 1
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Front
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
			

			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform sampler2D _TextureSample0;
			uniform float4 _TextureSample0_ST;
			uniform float4 _DarkSkyColor;
			uniform float4 _HorizonColor;
			uniform float _Float0;
			uniform float _Float1;
			uniform float4 _GroundColor;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				o.ase_texcoord1.xyz = ase_worldPos;
				
				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				o.ase_texcoord1.w = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float2 uv_TextureSample0 = i.ase_texcoord.xy * _TextureSample0_ST.xy + _TextureSample0_ST.zw;
				float3 desaturateInitialColor30 = ( tex2D( _TextureSample0, uv_TextureSample0 ) * 2.0 ).rgb;
				float desaturateDot30 = dot( desaturateInitialColor30, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar30 = lerp( desaturateInitialColor30, desaturateDot30.xxx, 0.0 );
				float2 uv19 = i.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float clampResult23 = clamp( ( 1.0 - pow( ( uv19.y * _Float0 ) , _Float1 ) ) , 0.0 , 1.0 );
				float4 lerpResult4 = lerp( _DarkSkyColor , _HorizonColor , clampResult23);
				float3 ase_worldPos = i.ase_texcoord1.xyz;
				float clampResult12 = clamp( step( 0.0 , ase_worldPos.y ) , 0.0 , 1.0 );
				float4 lerpResult26 = lerp( float4( desaturateVar30 , 0.0 ) , ( _DarkSkyColor + ( lerpResult4 * clampResult12 ) ) , 0.2);
				
				
				finalColor = ( lerpResult26 + ( _GroundColor * ( 1.0 - clampResult12 ) ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
0;678;1530;323;546.2576;584.774;1.40169;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;19;-1420.442,-160.966;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;7;-1348.071,8.139324;Float;False;Property;_Float0;Float 0;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;21;-1316.216,239.6318;Float;False;Property;_Float1;Float 1;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-1027.095,28.48107;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;20;-779.6931,75.2093;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;5;-2391.946,-51.37363;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.OneMinusNode;11;-527.8908,40.75915;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;3;-743.1921,-214.7698;Float;False;Property;_HorizonColor;HorizonColor;1;1;[HDR];Create;True;0;0;False;0;0.8867924,0.5563368,0.6655641,0;0.8867924,0.5563368,0.6655641,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;23;-331.668,-4.872022;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-736.3391,-438.1374;Float;False;Property;_DarkSkyColor;DarkSkyColor;0;1;[HDR];Create;True;0;0;False;0;0.06299388,0.03613386,0.2641509,0;0.06299388,0.03613386,0.2641509,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StepOpNode;10;-948.3162,467.4505;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;24;19.12604,-582.0151;Float;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;False;0;cd3ab1df7f78784489a139cc0a36136d;cd3ab1df7f78784489a139cc0a36136d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;29;229.0216,-393.006;Float;False;Constant;_Float3;Float 3;7;0;Create;True;0;0;False;0;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;12;-673.9037,474.0052;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;4;-139.1758,-251.9797;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;432.0874,-540.6629;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;158.3544,-90.57074;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;31;400.6532,-307.2617;Float;False;Constant;_Float4;Float 4;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;15;-337.5187,750.1198;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;18;489.2161,-230.4138;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;27;502.4836,35.9951;Float;False;Constant;_Float2;Float 2;6;0;Create;True;0;0;False;0;0.2;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;30;727.8093,-489.9527;Float;True;2;0;FLOAT3;0,0,0;False;1;FLOAT;0.5;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ColorNode;14;-41.12016,517.4543;Float;False;Property;_GroundColor;GroundColor;2;1;[HDR];Create;True;0;0;False;0;0.4528302,0.3396226,0.3396226,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;282.6549,697.001;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;26;1018.669,-246.0452;Float;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0.5;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;1373.54,173.4091;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;8;-2065.278,-159.5959;Float;True;5;0;FLOAT;0;False;1;FLOAT;-5000;False;2;FLOAT;5000;False;3;FLOAT;-1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1720.249,190.5378;Float;False;True;2;Float;ASEMaterialInspector;0;1;Map/RubberDistrict/Skybox;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;1;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;6;0;19;2
WireConnection;6;1;7;0
WireConnection;20;0;6;0
WireConnection;20;1;21;0
WireConnection;11;0;20;0
WireConnection;23;0;11;0
WireConnection;10;1;5;2
WireConnection;12;0;10;0
WireConnection;4;0;2;0
WireConnection;4;1;3;0
WireConnection;4;2;23;0
WireConnection;28;0;24;0
WireConnection;28;1;29;0
WireConnection;13;0;4;0
WireConnection;13;1;12;0
WireConnection;15;0;12;0
WireConnection;18;0;2;0
WireConnection;18;1;13;0
WireConnection;30;0;28;0
WireConnection;30;1;31;0
WireConnection;16;0;14;0
WireConnection;16;1;15;0
WireConnection;26;0;30;0
WireConnection;26;1;18;0
WireConnection;26;2;27;0
WireConnection;17;0;26;0
WireConnection;17;1;16;0
WireConnection;8;0;5;2
WireConnection;0;0;17;0
ASEEND*/
//CHKSM=120CBC58B493E1EA240C4413B3BCE8514E9272CB