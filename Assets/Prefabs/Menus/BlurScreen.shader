// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify/BlurScreen"
{
	Properties
	{
		_BlurPower("BlurPower", Range( 0 , 0.005)) = 0.001
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		GrabPass{ }
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float4 screenPos;
		};

		uniform sampler2D _GrabTexture;
		uniform float _BlurPower;


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


		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_grabScreenPos = ASE_ComputeGrabScreenPos( ase_screenPos );
			float4 ase_grabScreenPosNorm = ase_grabScreenPos / ase_grabScreenPos.w;
			float4 appendResult25 = (float4(( ase_grabScreenPosNorm.r + _BlurPower ) , ase_grabScreenPosNorm.g , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
			float4 screenColor2 = tex2D( _GrabTexture, appendResult25.xy );
			float4 appendResult32 = (float4(ase_grabScreenPosNorm.r , ( ase_grabScreenPosNorm.g + _BlurPower ) , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
			float4 screenColor33 = tex2D( _GrabTexture, appendResult32.xy );
			float temp_output_43_0 = ( _BlurPower * -1.0 );
			float4 appendResult41 = (float4(( ase_grabScreenPosNorm.r + temp_output_43_0 ) , ase_grabScreenPosNorm.g , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
			float4 screenColor42 = tex2D( _GrabTexture, appendResult41.xy );
			float4 appendResult37 = (float4(ase_grabScreenPosNorm.r , ( ase_grabScreenPosNorm.g + temp_output_43_0 ) , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
			float4 screenColor38 = tex2D( _GrabTexture, appendResult37.xy );
			float4 appendResult59 = (float4(( ase_grabScreenPosNorm.r + _BlurPower ) , ( ase_grabScreenPosNorm.g + _BlurPower ) , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
			float4 screenColor60 = tex2D( _GrabTexture, appendResult59.xy );
			float4 appendResult55 = (float4(( ase_grabScreenPosNorm.r + temp_output_43_0 ) , ( ase_grabScreenPosNorm.g + temp_output_43_0 ) , ase_grabScreenPosNorm.b , ase_grabScreenPosNorm.a));
			float4 screenColor56 = tex2D( _GrabTexture, appendResult55.xy );
			o.Albedo = (float4( 0,0,0,0 ) + (( screenColor2 + screenColor33 + screenColor42 + screenColor38 + screenColor60 + screenColor56 ) - float4( 0,0,0,0 )) * (float4( 0.3,0.3,0.3,0 ) - float4( 0,0,0,0 )) / (float4( 1,1,1,0 ) - float4( 0,0,0,0 ))).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16103
0;641;1437;360;1773.441;-97.9494;1.7418;True;True
Node;AmplifyShaderEditor.RangedFloatNode;24;-2985.227,362.1604;Float;False;Property;_BlurPower;BlurPower;0;0;Create;True;0;0;False;0;0.001;0;0;0.005;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;43;-2571.282,840.707;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GrabScreenPosition;22;-2221.714,-168.5432;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;35;-2233.897,824.5475;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;53;-2243.408,1063.018;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;39;-2232.617,592.5517;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;58;-2185.436,271.5994;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GrabScreenPosition;30;-2232.013,57.94727;Float;False;0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;40;-1924.586,614.8551;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;61;-1897.493,235.1064;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;62;-1900.752,326.5944;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-1931.012,876.943;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;54;-1958.723,1118.013;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;-1913.684,-146.2399;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-1929.128,110.3428;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;57;-1962.404,1026.525;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;32;-1740.573,96.41882;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;59;-1693.996,310.0705;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;41;-1757.017,629.2632;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;25;-1746.114,-131.8318;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;37;-1742.456,863.019;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;55;-1751.967,1101.489;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ScreenColorNode;38;-1489.105,848.9443;Float;False;Global;_GrabScreen2;Grab Screen 2;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;42;-1485.041,643.4481;Float;False;Global;_GrabScreen3;Grab Screen 3;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;2;-1474.138,-117.6469;Float;False;Global;_GrabScreen0;Grab Screen 0;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;56;-1498.616,1087.415;Float;False;Global;_GrabScreen4;Grab Screen 4;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;33;-1522.505,85.28448;Float;False;Global;_GrabScreen1;Grab Screen 1;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ScreenColorNode;60;-1440.645,295.9965;Float;False;Global;_GrabScreen5;Grab Screen 5;0;0;Create;True;0;0;False;0;Object;-1;False;False;1;0;FLOAT2;0,0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;34;-1086.665,379.2428;Float;False;6;6;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;65;-741.3116,310.3693;Float;False;5;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,1,1,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0.3,0.3,0.3,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;28;53.23637,228.6214;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Amplify/BlurScreen;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;43;0;24;0
WireConnection;40;0;39;1
WireConnection;40;1;43;0
WireConnection;61;0;58;1
WireConnection;61;1;24;0
WireConnection;62;0;58;2
WireConnection;62;1;24;0
WireConnection;36;0;35;2
WireConnection;36;1;43;0
WireConnection;54;0;53;2
WireConnection;54;1;43;0
WireConnection;23;0;22;1
WireConnection;23;1;24;0
WireConnection;31;0;30;2
WireConnection;31;1;24;0
WireConnection;57;0;53;1
WireConnection;57;1;43;0
WireConnection;32;0;30;1
WireConnection;32;1;31;0
WireConnection;32;2;30;3
WireConnection;32;3;30;4
WireConnection;59;0;61;0
WireConnection;59;1;62;0
WireConnection;59;2;58;3
WireConnection;59;3;58;4
WireConnection;41;0;40;0
WireConnection;41;1;39;2
WireConnection;41;2;39;3
WireConnection;41;3;39;4
WireConnection;25;0;23;0
WireConnection;25;1;22;2
WireConnection;25;2;22;3
WireConnection;25;3;22;4
WireConnection;37;0;35;1
WireConnection;37;1;36;0
WireConnection;37;2;35;3
WireConnection;37;3;35;4
WireConnection;55;0;57;0
WireConnection;55;1;54;0
WireConnection;55;2;53;3
WireConnection;55;3;53;4
WireConnection;38;0;37;0
WireConnection;42;0;41;0
WireConnection;2;0;25;0
WireConnection;56;0;55;0
WireConnection;33;0;32;0
WireConnection;60;0;59;0
WireConnection;34;0;2;0
WireConnection;34;1;33;0
WireConnection;34;2;42;0
WireConnection;34;3;38;0
WireConnection;34;4;60;0
WireConnection;34;5;56;0
WireConnection;65;0;34;0
WireConnection;28;0;65;0
ASEEND*/
//CHKSM=7E0C9390E922538BE23E1C990F50525CC67BCB64