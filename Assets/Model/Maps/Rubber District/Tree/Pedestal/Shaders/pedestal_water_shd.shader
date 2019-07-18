// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify/TreeWater"
{
	Properties
	{
		_WavesColor("WavesColor", Color) = (1,1,1,1)
		_BaseColor("BaseColor", Color) = (0,0.5208681,1,1)
		_CurrentSpeed3("CurrentSpeed3", Float) = 1
		_BigRipplesSpeed("BigRipplesSpeed", Float) = 1
		_LittlesRipplesSpeed("LittlesRipplesSpeed", Float) = 1
		_Texture0("Texture 0", 2D) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float4 _BaseColor;
		uniform sampler2D _Texture0;
		uniform sampler2D _Sampler6019;
		uniform float _BigRipplesSpeed;
		uniform float4 _WavesColor;
		uniform float _CurrentSpeed3;
		uniform sampler2D _Sampler6077;
		uniform float _LittlesRipplesSpeed;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 temp_output_1_0_g1 = float2( 1,1 );
			float2 appendResult10_g1 = (float2(( (temp_output_1_0_g1).x * i.uv_texcoord.x ) , ( i.uv_texcoord.y * (temp_output_1_0_g1).y )));
			float2 temp_output_11_0_g1 = float2( 1,1 );
			float2 panner18_g1 = ( ( (temp_output_11_0_g1).x * _Time.y ) * float2( 1,0 ) + i.uv_texcoord);
			float2 panner19_g1 = ( ( _Time.y * (temp_output_11_0_g1).y ) * float2( 0,1 ) + i.uv_texcoord);
			float2 appendResult24_g1 = (float2((panner18_g1).x , (panner19_g1).y));
			float4 appendResult46 = (float4(0.0 , ( _BigRipplesSpeed * -0.1 ) , 0.0 , 0.0));
			float2 temp_output_47_0_g1 = appendResult46.xy;
			float2 uv_TexCoord78_g1 = i.uv_texcoord * float2( 2,2 );
			float2 temp_output_31_0_g1 = ( uv_TexCoord78_g1 - float2( 1,1 ) );
			float2 appendResult39_g1 = (float2(frac( ( atan2( (temp_output_31_0_g1).x , (temp_output_31_0_g1).y ) / 6.28318548202515 ) ) , length( temp_output_31_0_g1 )));
			float2 panner54_g1 = ( ( (temp_output_47_0_g1).x * _Time.y ) * float2( 1,0 ) + appendResult39_g1);
			float2 panner55_g1 = ( ( _Time.y * (temp_output_47_0_g1).y ) * float2( 0,1 ) + appendResult39_g1);
			float2 appendResult58_g1 = (float2((panner54_g1).x , (panner55_g1).y));
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float temp_output_16_0 = distance( 0 , ase_vertex3Pos.z );
			float smoothstepResult18 = smoothstep( 6.0 , 9.0 , temp_output_16_0);
			float4 appendResult42 = (float4(0.5 , 0.0 , 0.0 , 0.0));
			float2 uv_TexCoord30 = i.uv_texcoord * float2( 5,5 );
			float2 panner28 = ( 1.0 * _Time.y * appendResult42.xy + uv_TexCoord30);
			float4 appendResult44 = (float4(0.3 , 0.0 , 0.0 , 0.0));
			float2 panner31 = ( 1.0 * _Time.y * appendResult44.xy + uv_TexCoord30);
			float4 appendResult43 = (float4(_CurrentSpeed3 , 0.0 , 0.0 , 0.0));
			float2 panner33 = ( 1.0 * _Time.y * appendResult43.xy + uv_TexCoord30);
			float smoothstepResult67 = smoothstep( 5.0 , 8.0 , temp_output_16_0);
			float smoothstepResult68 = smoothstep( 9.8 , 10.0 , temp_output_16_0);
			float2 temp_output_1_0_g2 = float2( 1,1 );
			float2 appendResult10_g2 = (float2(( (temp_output_1_0_g2).x * i.uv_texcoord.x ) , ( i.uv_texcoord.y * (temp_output_1_0_g2).y )));
			float2 temp_output_11_0_g2 = float2( 1,1 );
			float2 panner18_g2 = ( ( (temp_output_11_0_g2).x * _Time.y ) * float2( 1,0 ) + i.uv_texcoord);
			float2 panner19_g2 = ( ( _Time.y * (temp_output_11_0_g2).y ) * float2( 0,1 ) + i.uv_texcoord);
			float2 appendResult24_g2 = (float2((panner18_g2).x , (panner19_g2).y));
			float4 appendResult76 = (float4(0.0 , ( _LittlesRipplesSpeed * -0.1 ) , 0.0 , 0.0));
			float2 temp_output_47_0_g2 = appendResult76.xy;
			float2 uv_TexCoord78_g2 = i.uv_texcoord * float2( 2,2 );
			float2 temp_output_31_0_g2 = ( uv_TexCoord78_g2 - float2( 1,1 ) );
			float2 appendResult39_g2 = (float2(frac( ( atan2( (temp_output_31_0_g2).x , (temp_output_31_0_g2).y ) / 6.28318548202515 ) ) , length( temp_output_31_0_g2 )));
			float2 panner54_g2 = ( ( (temp_output_47_0_g2).x * _Time.y ) * float2( 1,0 ) + appendResult39_g2);
			float2 panner55_g2 = ( ( _Time.y * (temp_output_47_0_g2).y ) * float2( 0,1 ) + appendResult39_g2);
			float2 appendResult58_g2 = (float2((panner54_g2).x , (panner55_g2).y));
			o.Albedo = ( ( ( _BaseColor + ( tex2D( _Texture0, ( ( (tex2D( _Sampler6019, ( appendResult10_g1 + appendResult24_g1 ) )).rg * 1.0 ) + ( float2( 4,6 ) * appendResult58_g1 ) ) ).a * _WavesColor ) ) * ( 1.0 - smoothstepResult18 ) ) + ( ( _BaseColor + ( ( tex2D( _Texture0, panner28 ).b + tex2D( _Texture0, panner31 ).g + tex2D( _Texture0, panner33 ).r ) * _WavesColor ) ) * ( smoothstepResult67 * ( 1.0 - smoothstepResult68 ) ) ) + ( ( _BaseColor + ( _WavesColor * tex2D( _Texture0, ( ( (tex2D( _Sampler6077, ( appendResult10_g2 + appendResult24_g2 ) )).rg * 1.0 ) + ( float2( 4,25 ) * appendResult58_g2 ) ) ).a ) ) * smoothstepResult68 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16103
0;552;1494;449;1703.771;181.82;2.491402;True;True
Node;AmplifyShaderEditor.RangedFloatNode;45;-2555.338,-196.3841;Float;False;Property;_BigRipplesSpeed;BigRipplesSpeed;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;72;-2516.711,227.919;Float;False;Constant;_Float3;Float 3;7;0;Create;True;0;0;False;0;-0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-2382.469,-864.0487;Float;False;Property;_CurrentSpeed3;CurrentSpeed3;2;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-2416.317,-1078.986;Float;False;Constant;_CurrentSpeed1;CurrentSpeed1;5;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;-2577.755,90.72419;Float;False;Property;_LittlesRipplesSpeed;LittlesRipplesSpeed;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-1965.804,-1156.568;Float;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;49;-2524.789,-88.07921;Float;False;Constant;_Float2;Float 2;7;0;Create;True;0;0;False;0;-0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-2399.393,-972.3636;Float;False;Constant;_CurrentSpeed2;CurrentSpeed2;5;0;Create;True;0;0;False;0;0.3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;44;-1666.41,-982.7979;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;42;-1681.703,-1112.775;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;47;-2327.069,-256.5666;Float;False;Constant;_Float1;Float 1;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;30;-1742.783,-1291.265;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;5,5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-2340.116,-124.1808;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;74;-2332.038,191.8174;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;43;-1664.41,-840.7979;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.CommentaryNode;79;-1051.64,331.8695;Float;False;1473.192;1109.775;LocalDistanceDetection;8;68;24;67;18;16;17;15;86;;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;75;-2318.991,59.43161;Float;False;Constant;_Float5;Float 5;7;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;76;-2145.425,120.5266;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PannerNode;28;-1400.911,-1215.05;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.5;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;46;-2153.503,-195.4716;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PannerNode;33;-1407.399,-804.1617;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.5;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;31;-1403.617,-1025.401;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.5;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;59;-2087.043,-557.1114;Float;True;Property;_Texture0;Texture 0;5;0;Create;True;0;0;False;0;36506a394b21ced40bc01c32d77dc1c1;None;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PosVertexDataNode;15;-980.9005,629.6602;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;17;-862.5007,420.0603;Float;False;Constant;_Vector0;Vector 0;2;0;Create;True;0;0;False;0;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SamplerNode;27;-1025.131,-1304.563;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;36506a394b21ced40bc01c32d77dc1c1;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;32;-1025.131,-1112.563;Float;True;Property;_TextureSample2;Texture Sample 2;3;0;Create;True;0;0;False;0;36506a394b21ced40bc01c32d77dc1c1;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;34;-1030.657,-885.7816;Float;True;Property;_TextureSample3;Texture Sample 3;4;0;Create;True;0;0;False;0;36506a394b21ced40bc01c32d77dc1c1;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DistanceOpNode;16;-547.7311,453.213;Float;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;19;-1939.75,-281.4416;Float;True;RadialUVDistortion;-1;;1;051d65e7699b41a4c800363fd0e822b2;0;7;60;SAMPLER2D;_Sampler6019;False;1;FLOAT2;1,1;False;11;FLOAT2;1,1;False;65;FLOAT;1;False;68;FLOAT2;4,6;False;47;FLOAT2;0,-0.1;False;29;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;77;-1931.672,34.55661;Float;True;RadialUVDistortion;-1;;2;051d65e7699b41a4c800363fd0e822b2;0;7;60;SAMPLER2D;_Sampler6077;False;1;FLOAT2;1,1;False;11;FLOAT2;1,1;False;65;FLOAT;1;False;68;FLOAT2;4,25;False;47;FLOAT2;0,-0.1;False;29;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-1361.459,-297.1532;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;36506a394b21ced40bc01c32d77dc1c1;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;68;-240.2066,1009.62;Float;True;3;0;FLOAT;0;False;1;FLOAT;9.8;False;2;FLOAT;10;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;56;-475.5161,-727.8978;Float;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;78;-1353.381,18.84501;Float;True;Property;_TextureSample4;Texture Sample 4;0;0;Create;True;0;0;False;0;36506a394b21ced40bc01c32d77dc1c1;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-1261.874,-516.1699;Float;False;Property;_WavesColor;WavesColor;0;0;Create;True;0;0;False;0;1,1,1,1;1,1,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-870.0325,-244.1953;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;67;-230.3938,659.9051;Float;True;3;0;FLOAT;0;False;1;FLOAT;5;False;2;FLOAT;8;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;86;204.6425,956.7505;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;82;-865.6751,36.33444;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-111.8071,-598.5288;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;12;-638.4328,-454.389;Float;False;Property;_BaseColor;BaseColor;1;0;Create;True;0;0;False;0;0,0.5208681,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;18;-242.0294,428.437;Float;True;3;0;FLOAT;0;False;1;FLOAT;6;False;2;FLOAT;9;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-166.4744,-194.6659;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;83;-124.8546,51.46537;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;24;121.959,408.4292;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;58;486.3347,-429.2225;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;85;406.6411,564.6679;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;718.8365,262.0334;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;490.4478,-143.6001;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;35;870.8304,-45.35448;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;36;1294.034,-194.0545;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;2024.614,-178.8233;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Amplify/TreeWater;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;44;0;38;0
WireConnection;44;1;41;0
WireConnection;42;0;37;0
WireConnection;42;1;41;0
WireConnection;48;0;45;0
WireConnection;48;1;49;0
WireConnection;74;0;73;0
WireConnection;74;1;72;0
WireConnection;43;0;39;0
WireConnection;43;1;41;0
WireConnection;76;0;75;0
WireConnection;76;1;74;0
WireConnection;28;0;30;0
WireConnection;28;2;42;0
WireConnection;46;0;47;0
WireConnection;46;1;48;0
WireConnection;33;0;30;0
WireConnection;33;2;43;0
WireConnection;31;0;30;0
WireConnection;31;2;44;0
WireConnection;27;0;59;0
WireConnection;27;1;28;0
WireConnection;32;0;59;0
WireConnection;32;1;31;0
WireConnection;34;0;59;0
WireConnection;34;1;33;0
WireConnection;16;0;17;3
WireConnection;16;1;15;3
WireConnection;19;47;46;0
WireConnection;77;47;76;0
WireConnection;1;0;59;0
WireConnection;1;1;19;0
WireConnection;68;0;16;0
WireConnection;56;0;27;3
WireConnection;56;1;32;2
WireConnection;56;2;34;1
WireConnection;78;0;59;0
WireConnection;78;1;77;0
WireConnection;4;0;1;4
WireConnection;4;1;7;0
WireConnection;67;0;16;0
WireConnection;86;0;68;0
WireConnection;82;0;7;0
WireConnection;82;1;78;4
WireConnection;55;0;56;0
WireConnection;55;1;7;0
WireConnection;18;0;16;0
WireConnection;13;0;12;0
WireConnection;13;1;4;0
WireConnection;83;0;12;0
WireConnection;83;1;82;0
WireConnection;24;0;18;0
WireConnection;58;0;12;0
WireConnection;58;1;55;0
WireConnection;85;0;67;0
WireConnection;85;1;86;0
WireConnection;69;0;83;0
WireConnection;69;1;68;0
WireConnection;23;0;13;0
WireConnection;23;1;24;0
WireConnection;35;0;58;0
WireConnection;35;1;85;0
WireConnection;36;0;23;0
WireConnection;36;1;35;0
WireConnection;36;2;69;0
WireConnection;0;0;36;0
ASEEND*/
//CHKSM=4FA1AF43B9C292D71841EA9760A042FD9C0BC21A