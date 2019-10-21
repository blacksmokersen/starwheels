// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Menu/HexaField"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (0,0,0,0)
		_Pattern("Pattern", 2D) = "white" {}
		_GradientScale("GradientScale", Float) = 10
		_GradientGlow("GradientGlow", Float) = 1
		_BorderGlow("BorderGlow", Range( 0 , 0.1)) = 0.03602256
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float _BorderGlow;
		uniform float _GradientScale;
		uniform float4 _Color;
		uniform float _GradientGlow;
		uniform sampler2D _Pattern;
		uniform float4 _Pattern_ST;
		uniform float _Cutoff = 0.5;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float temp_output_20_0 = ( 1.0 - i.uv_texcoord.x );
			float temp_output_22_0 = ( pow( i.uv_texcoord.x , _GradientScale ) + pow( temp_output_20_0 , _GradientScale ) );
			o.Emission = ( ( ( step( i.uv_texcoord.x , _BorderGlow ) + step( temp_output_20_0 , _BorderGlow ) ) + ( ( ( temp_output_22_0 * _Color ) * _GradientGlow ) + _Color ) ) / 1.0 ).rgb;
			o.Alpha = 1;
			float2 uv_Pattern = i.uv_texcoord * _Pattern_ST.xy + _Pattern_ST.zw;
			clip( ( temp_output_22_0 + ( 1.0 - tex2D( _Pattern, uv_Pattern ) ) ).r - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16103
0;547;1545;454;2722.605;746.5119;2.94357;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-1379.819,-681.8718;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;20;-1048.671,-347.523;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-1039.785,-524.0921;Float;False;Property;_GradientScale;GradientScale;2;0;Create;True;0;0;False;0;10;3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;21;-761.5766,-384.8857;Float;False;2;0;FLOAT;0;False;1;FLOAT;6;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;19;-743.9338,-502.0459;Float;False;2;0;FLOAT;0;False;1;FLOAT;6;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;22;-431.0085,-396.2127;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;-471.1447,-85.23077;Float;False;Property;_Color;Color;0;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;30;-86.81136,-198.072;Float;False;Property;_GradientGlow;GradientGlow;3;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-1239.862,-795.3024;Float;False;Property;_BorderGlow;BorderGlow;4;0;Create;True;0;0;False;0;0.03602256;3;0;0.1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;31;-51.4881,-343.8245;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;38;-774.9014,-824.7248;Float;False;2;0;FLOAT;0;False;1;FLOAT;0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;129.2923,-261.4284;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StepOpNode;39;-780.0674,-716.1755;Float;False;2;0;FLOAT;0;False;1;FLOAT;0.01;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;23;291.5254,-180.2811;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;37;-440.4991,-805.2025;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-978.9676,164.1436;Float;True;Property;_Pattern;Pattern;1;0;Create;True;0;0;False;0;a830d0b8f4aa44741a23bcdb1dd90f5e;a830d0b8f4aa44741a23bcdb1dd90f5e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;5;-627.2844,147.7158;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;40;446.8733,-281.494;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;27;376.3455,-2.355431;Float;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;28;161.4329,159.0743;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;25;579.2747,-100.4765;Float;False;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;782.2343,-158.8953;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Menu/HexaField;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;5;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;20;0;16;1
WireConnection;21;0;20;0
WireConnection;21;1;18;0
WireConnection;19;0;16;1
WireConnection;19;1;18;0
WireConnection;22;0;19;0
WireConnection;22;1;21;0
WireConnection;31;0;22;0
WireConnection;31;1;1;0
WireConnection;38;0;16;1
WireConnection;38;1;33;0
WireConnection;29;0;31;0
WireConnection;29;1;30;0
WireConnection;39;0;20;0
WireConnection;39;1;33;0
WireConnection;23;0;29;0
WireConnection;23;1;1;0
WireConnection;37;0;38;0
WireConnection;37;1;39;0
WireConnection;5;0;2;0
WireConnection;40;0;37;0
WireConnection;40;1;23;0
WireConnection;28;0;22;0
WireConnection;28;1;5;0
WireConnection;25;0;40;0
WireConnection;25;1;27;0
WireConnection;0;2;25;0
WireConnection;0;10;28;0
ASEEND*/
//CHKSM=835F44B80B43380FAC7B247FFEFAD95D4773411D