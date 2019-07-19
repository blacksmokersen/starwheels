// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Map/Rubbre District/Bridge/Pannel"
{
	Properties
	{
		[HDR]_ColorBase("Color Base", Color) = (1,1,1,1)
		[HDR]_ColorBorder("Color Border", Color) = (1,1,1,1)
		[HDR]_ColorEffect("Color Effect", Color) = (1,1,1,1)
		_Pannel("Pannel", 2D) = "white" {}
		_Speed("Speed", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		Blend One One , One One
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _ColorBase;
		uniform float4 _ColorBorder;
		uniform sampler2D _Pannel;
		uniform float4 _Pannel_ST;
		uniform float4 _ColorEffect;
		uniform float _Speed;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_Pannel = i.uv_texcoord * _Pannel_ST.xy + _Pannel_ST.zw;
			float temp_output_36_0 = ( _Time.y * _Speed );
			float2 temp_output_39_0 = ( uv_Pannel * float2( 2,2 ) );
			float2 panner31 = ( temp_output_36_0 * float2( 0,-2 ) + ( temp_output_39_0 + float2( 0.55,0 ) ));
			float4 tex2DNode22 = tex2D( _Pannel, panner31 );
			float2 panner32 = ( temp_output_36_0 * float2( 0,1.8 ) + ( temp_output_39_0 + float2( 0.45,0 ) ));
			float4 tex2DNode23 = tex2D( _Pannel, panner32 );
			float2 panner33 = ( temp_output_36_0 * float2( 0,1.2 ) + ( uv_Pannel + float2( -0.05,0 ) ));
			float4 tex2DNode28 = tex2D( _Pannel, panner33 );
			float2 panner34 = ( temp_output_36_0 * float2( 0,-1 ) + ( uv_Pannel + float2( 0.054,0 ) ));
			float4 tex2DNode27 = tex2D( _Pannel, panner34 );
			float temp_output_14_0 = ( _ColorEffect.a * ( ( ( tex2DNode22.r * tex2DNode23.r ) + ( tex2DNode28.r * tex2DNode27.r ) ) + ( ( ( tex2DNode22.r + tex2DNode23.r ) * 0.05 ) + ( ( tex2DNode28.r + tex2DNode27.r ) * 0.01 ) ) ) );
			float temp_output_13_0 = ( ( _ColorBorder.a * tex2D( _Pannel, uv_Pannel ).a ) + temp_output_14_0 );
			float4 lerpResult6 = lerp( _ColorBase , _ColorBorder , temp_output_13_0);
			float4 lerpResult4 = lerp( lerpResult6 , _ColorEffect , temp_output_14_0);
			float clampResult11 = clamp( ( _ColorBase.a + temp_output_13_0 ) , 0.0 , 1.0 );
			o.Emission = ( lerpResult4 * clampResult11 ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16103
2003;204;1717;929;1449.867;1593.441;2.917256;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;38;-3195.533,170.5245;Float;False;0;20;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;35;-3187.095,661.0233;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;37;-3136.095,903.0233;Float;False;Property;_Speed;Speed;5;0;Create;True;0;0;False;0;0;0.25;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-2858.533,142.5245;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;2,2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;42;-2539.482,591.9176;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;-0.05,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;41;-2533.482,357.9176;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0.45,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;43;-2535.482,700.9176;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0.054,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-2867.095,677.0233;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;40;-2557.482,111.9176;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0.55,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;33;-2343.095,617.0233;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,1.2;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;32;-2367.095,388.0233;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,1.8;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;34;-2343.095,787.0233;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;31;-2342.095,188.0233;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-2;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;20;-2546.874,-318.8665;Float;True;Property;_Pannel;Pannel;4;0;Create;True;0;0;False;0;None;e562bfdd5c20d974d89877fab0dc5894;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;28;-1967.014,650.6096;Float;True;Property;_TextureSample4;Texture Sample 4;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;23;-2023.014,410.6096;Float;True;Property;_TextureSample2;Texture Sample 2;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;27;-1982.014,857.6096;Float;True;Property;_TextureSample3;Texture Sample 3;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;22;-2009.014,191.6096;Float;True;Property;_TextureSample1;Texture Sample 1;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-1593.014,299.6096;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;-1603.095,848.0233;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-1396,662;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-1633.095,681.0233;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-1665.014,470.6096;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1420,305;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.05;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-1206.014,481.6096;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-1467.014,449.6096;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;19;-1684.4,15.7;Float;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;7;-1204,-257;Float;False;Property;_ColorBorder;Color Border;2;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0,0.8187398,2,0.5372549;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;8;-1219,-74;Float;False;Property;_ColorEffect;Color Effect;3;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0,2.76481,4,0.9215686;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;16;-1073,368;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-881,222;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-911,350;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-719,221;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;5;-1208,-456;Float;False;Property;_ColorBase;Color Base;0;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0,0.4180913,1,0.3294118;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;12;-427,248;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;6;-462,-104;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;4;-219,-57;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;11;-195,256;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;85,29;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;44;333,33;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Map/Rubbre District/Bridge/Pannel;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;False;Transparent;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;4;1;False;-1;1;False;-1;4;1;False;-1;1;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;39;0;38;0
WireConnection;42;0;38;0
WireConnection;41;0;39;0
WireConnection;43;0;38;0
WireConnection;36;0;35;2
WireConnection;36;1;37;0
WireConnection;40;0;39;0
WireConnection;33;0;42;0
WireConnection;33;1;36;0
WireConnection;32;0;41;0
WireConnection;32;1;36;0
WireConnection;34;0;43;0
WireConnection;34;1;36;0
WireConnection;31;0;40;0
WireConnection;31;1;36;0
WireConnection;28;0;20;0
WireConnection;28;1;33;0
WireConnection;23;0;20;0
WireConnection;23;1;32;0
WireConnection;27;0;20;0
WireConnection;27;1;34;0
WireConnection;22;0;20;0
WireConnection;22;1;31;0
WireConnection;21;0;22;1
WireConnection;21;1;23;1
WireConnection;29;0;28;1
WireConnection;29;1;27;1
WireConnection;18;0;29;0
WireConnection;30;0;28;1
WireConnection;30;1;27;1
WireConnection;26;0;22;1
WireConnection;26;1;23;1
WireConnection;17;0;21;0
WireConnection;24;0;17;0
WireConnection;24;1;18;0
WireConnection;25;0;26;0
WireConnection;25;1;30;0
WireConnection;19;0;20;0
WireConnection;16;0;25;0
WireConnection;16;1;24;0
WireConnection;15;0;7;4
WireConnection;15;1;19;4
WireConnection;14;0;8;4
WireConnection;14;1;16;0
WireConnection;13;0;15;0
WireConnection;13;1;14;0
WireConnection;12;0;5;4
WireConnection;12;1;13;0
WireConnection;6;0;5;0
WireConnection;6;1;7;0
WireConnection;6;2;13;0
WireConnection;4;0;6;0
WireConnection;4;1;8;0
WireConnection;4;2;14;0
WireConnection;11;0;12;0
WireConnection;9;0;4;0
WireConnection;9;1;11;0
WireConnection;44;2;9;0
ASEEND*/
//CHKSM=1CC28B51BD65491907D4254F22024A68AEC971C8