// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify/BlurScreen"
{
	Properties
	{
		_BlurPower("BlurPower", Range( 0 , 0.005)) = 0.001
		_Darkening("Darkening", Range( 0 , 1)) = 0
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		GrabPass{ }


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
				
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform sampler2D _GrabTexture;
			uniform float _BlurPower;
			uniform float _Darkening;
			inline float4 ASE_ComputeGrabScreenPos( float4 pos )
			{
				#if UNITY_UV_STARTS_AT_TOP
				float scale = -1.0;
				#else
				float scale = 1.0;
				#endif
				float4 o = pos;
				o.y = pos.w * 0.5f;
				o.y = ( pos.y - o.y ) * _ProjectionParams.x * scale + o.y;
				return o;
			}
			
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord = screenPos;
				
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float4 screenPos = i.ase_texcoord;
				float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( screenPos );
				float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
				float4 appendResult25 = (float4(( ase_grabScreenPosNorm.r + _BlurPower ) , ase_grabScreenPosNorm.g , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
				float4 screenColor2 = tex2D( _GrabTexture, appendResult25.xy );
				float4 appendResult32 = (float4(ase_grabScreenPosNorm.r , ( ase_grabScreenPosNorm.g + _BlurPower ) , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
				float4 screenColor33 = tex2D( _GrabTexture, appendResult32.xy );
				float4 appendResult59 = (float4(( ase_grabScreenPosNorm.r + _BlurPower ) , ( ase_grabScreenPosNorm.g + _BlurPower ) , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
				float4 screenColor60 = tex2D( _GrabTexture, appendResult59.xy );
				float temp_output_43_0 = ( _BlurPower * -1.0 );
				float4 appendResult84 = (float4(( ase_grabScreenPosNorm.r + _BlurPower ) , ( ase_grabScreenPosNorm.g + temp_output_43_0 ) , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
				float4 screenColor85 = tex2D( _GrabTexture, appendResult84.xy );
				float4 appendResult89 = (float4(( ase_grabScreenPosNorm.r + temp_output_43_0 ) , ( ase_grabScreenPosNorm.g + _BlurPower ) , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
				float4 screenColor90 = tex2D( _GrabTexture, appendResult89.xy );
				float4 appendResult41 = (float4(( ase_grabScreenPosNorm.r + temp_output_43_0 ) , ase_grabScreenPosNorm.g , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
				float4 screenColor42 = tex2D( _GrabTexture, appendResult41.xy );
				float4 appendResult37 = (float4(ase_grabScreenPosNorm.r , ( ase_grabScreenPosNorm.g + temp_output_43_0 ) , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
				float4 screenColor38 = tex2D( _GrabTexture, appendResult37.xy );
				float4 appendResult55 = (float4(( ase_grabScreenPosNorm.r + temp_output_43_0 ) , ( ase_grabScreenPosNorm.g + temp_output_43_0 ) , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
				float4 screenColor56 = tex2D( _GrabTexture, appendResult55.xy );
				float3 desaturateInitialColor91 = (float4( 0,0,0,0 ) + (( screenColor2 + screenColor33 + screenColor60 + screenColor85 + screenColor90 + screenColor42 + screenColor38 + screenColor56 ) - float4( 0,0,0,0 )) * (float4( 0.1698113,0.1698113,0.1698113,0 ) - float4( 0,0,0,0 )) / (float4( 1,1,1,0 ) - float4( 0,0,0,0 ))).rgb;
				float desaturateDot91 = dot( desaturateInitialColor91, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar91 = lerp( desaturateInitialColor91, desaturateDot91.xxx, _Darkening );
				
				
				finalColor = float4( ( desaturateVar91 * ( 1.0 - _Darkening ) ) , 0.0 );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
0;691;1545;310;1600.948;-269.2339;1.251475;True;False
Node;AmplifyShaderEditor.RangedFloatNode;24;-2985.227,362.1604;Float;False;Property;_BlurPower;BlurPower;0;0;Create;True;0;0;False;0;0.001;0.00324;0;0.005;0;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;86;-2244.733,383.7374;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-2631.714,686.0003;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;58;-2248.053,-36.30204;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;53;-2243.408,1063.018;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;82;-2255.915,179.8271;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;39;-2232.617,592.5517;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;22;-2255.959,-462.2588;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;30;-2280.444,-235.7683;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;35;-2233.897,824.5475;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;87;-1928.418,361.4303;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;57;-1962.404,1026.525;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;83;-1934.842,254.2969;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;88;-1923.66,458.2072;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-1947.929,-439.9555;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-1963.373,-183.3727;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;54;-1958.723,1118.013;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;81;-1939.6,157.52;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;40;-1924.586,614.8551;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-1931.012,876.943;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;61;-1931.738,-58.60915;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;62;-1932.051,31.40596;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;59;-1728.241,16.35499;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;84;-1736.103,232.4841;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;25;-1780.359,-425.5474;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;37;-1742.456,863.019;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;89;-1724.921,436.3944;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;32;-1774.818,-197.2967;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;41;-1757.017,629.2632;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;55;-1751.967,1101.489;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ScreenColorNode;85;-1482.752,218.4102;Float;False;Global;_GrabScreen6;Grab Screen 6;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;42;-1485.041,643.4481;Float;False;Global;_GrabScreen3;Grab Screen 3;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;38;-1489.105,848.9443;Float;False;Global;_GrabScreen2;Grab Screen 2;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;33;-1488.712,-201.8467;Float;False;Global;_GrabScreen1;Grab Screen 1;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;60;-1474.89,2.28098;Float;False;Global;_GrabScreen5;Grab Screen 5;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;56;-1498.616,1087.415;Float;False;Global;_GrabScreen4;Grab Screen 4;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;90;-1471.57,422.3205;Float;False;Global;_GrabScreen7;Grab Screen 7;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;2;-1489.435,-413.2573;Float;False;Global;_GrabScreen0;Grab Screen 0;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;34;-1037.496,337.9368;Float;False;8;8;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;65;-741.3116,310.3693;Float;False;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0.1698113,0.1698113,0.1698113,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;93;-841.3307,494.2881;Float;False;Property;_Darkening;Darkening;1;0;Create;True;0;0;False;0;0;0.65;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;97;-475.9674,443.307;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;91;-390.1071,298.6528;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0.5;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;99;-74.26508,288.6246;Float;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;66;320.9151,308.1653;Float;False;True;2;Float;ASEMaterialInspector;0;1;Amplify/BlurScreen;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;43;0;24;0
WireConnection;87;0;86;1
WireConnection;87;1;43;0
WireConnection;57;0;53;1
WireConnection;57;1;43;0
WireConnection;83;0;82;2
WireConnection;83;1;43;0
WireConnection;88;0;86;2
WireConnection;88;1;24;0
WireConnection;23;0;22;1
WireConnection;23;1;24;0
WireConnection;31;0;30;2
WireConnection;31;1;24;0
WireConnection;54;0;53;2
WireConnection;54;1;43;0
WireConnection;81;0;82;1
WireConnection;81;1;24;0
WireConnection;40;0;39;1
WireConnection;40;1;43;0
WireConnection;36;0;35;2
WireConnection;36;1;43;0
WireConnection;61;0;58;1
WireConnection;61;1;24;0
WireConnection;62;0;58;2
WireConnection;62;1;24;0
WireConnection;59;0;61;0
WireConnection;59;1;62;0
WireConnection;59;2;58;3
WireConnection;59;3;58;4
WireConnection;84;0;81;0
WireConnection;84;1;83;0
WireConnection;84;2;82;3
WireConnection;84;3;82;4
WireConnection;25;0;23;0
WireConnection;25;1;22;2
WireConnection;25;2;22;3
WireConnection;25;3;22;4
WireConnection;37;0;35;1
WireConnection;37;1;36;0
WireConnection;37;2;35;3
WireConnection;37;3;35;4
WireConnection;89;0;87;0
WireConnection;89;1;88;0
WireConnection;89;2;86;3
WireConnection;89;3;86;4
WireConnection;32;0;30;1
WireConnection;32;1;31;0
WireConnection;32;2;30;3
WireConnection;32;3;30;4
WireConnection;41;0;40;0
WireConnection;41;1;39;2
WireConnection;41;2;39;3
WireConnection;41;3;39;4
WireConnection;55;0;57;0
WireConnection;55;1;54;0
WireConnection;55;2;53;3
WireConnection;55;3;53;4
WireConnection;85;0;84;0
WireConnection;42;0;41;0
WireConnection;38;0;37;0
WireConnection;33;0;32;0
WireConnection;60;0;59;0
WireConnection;56;0;55;0
WireConnection;90;0;89;0
WireConnection;2;0;25;0
WireConnection;34;0;2;0
WireConnection;34;1;33;0
WireConnection;34;2;60;0
WireConnection;34;3;85;0
WireConnection;34;4;90;0
WireConnection;34;5;42;0
WireConnection;34;6;38;0
WireConnection;34;7;56;0
WireConnection;65;0;34;0
WireConnection;97;0;93;0
WireConnection;91;0;65;0
WireConnection;91;1;93;0
WireConnection;99;0;91;0
WireConnection;99;1;97;0
WireConnection;66;0;99;0
ASEEND*/
//CHKSM=D309C196D390C8630B7C30D8AE3D9C8E1B3ED9C1