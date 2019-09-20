// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Map/Security_Complex/Glass+Opaque"
{
	Properties
	{
		_albedo("albedo", 2D) = "white" {}
		_Color("Color", Color) = (0,0,0,0)
		_Transparency("Transparency", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color;
		uniform float _Transparency;
		uniform sampler2D _albedo;
		uniform float4 _albedo_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_albedo = i.uv_texcoord * _albedo_ST.xy + _albedo_ST.zw;
			float4 tex2DNode1 = tex2D( _albedo, uv_albedo );
			o.Albedo = ( _Color * ( ( 0.0 * ( _Transparency * tex2DNode1.a ) ) + tex2DNode1 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16103
0;602;1547;399;1943.791;468.0114;2.267449;True;True
Node;AmplifyShaderEditor.SamplerNode;1;-1027.107,-148.4183;Float;True;Property;_albedo;albedo;0;0;Create;True;0;0;False;0;None;0c4872d2331330f4282bb41abbad128d;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;26;-724.0911,-282.58;Float;False;Property;_Transparency;Transparency;7;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-341.9737,-183.0341;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-147.1551,-393.3491;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;89.11353,-169.0654;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;12;97.74416,-364.7695;Float;False;Property;_Color;Color;5;0;Create;True;0;0;False;0;0,0,0,0;0.6509804,0.6509804,0.6509804,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-967.2171,314.2193;Float;True;Property;_metallic;metallic;2;0;Create;True;0;0;False;0;None;3c83b52a33efc5f4baea687eae4a2603;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;9;-476.6821,436.5831;Float;False;Property;_EmissionColor;EmissionColor;4;1;[HDR];Create;True;0;0;False;0;0,0,0,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-54.53462,304.6415;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-197.6991,611.8547;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;2;-999.0491,91.464;Float;True;Property;_normal;normal;1;0;Create;True;0;0;False;0;None;0363318fbf3627e4c8ff235768308c4d;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;13;498.3741,-150.203;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;4;-943.8068,578.7582;Float;True;Property;_emission;emission;3;0;Create;True;0;0;False;0;None;ab767608b20f6e94b8a3480c580e559c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;16;-384.6962,184.255;Float;False;Property;_Smoothness;Smoothness;6;0;Create;True;0;0;False;0;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;824.1693,-145.6855;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Map/Security_Complex/Glass+Opaque;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;True;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;0;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;10;1,1,1,1;VertexOffset;True;False;Cylindrical;False;Relative;0;;6;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;25;0;26;0
WireConnection;25;1;1;4
WireConnection;20;1;25;0
WireConnection;24;0;20;0
WireConnection;24;1;1;0
WireConnection;17;0;16;0
WireConnection;17;1;3;4
WireConnection;10;0;9;0
WireConnection;10;1;4;0
WireConnection;13;0;12;0
WireConnection;13;1;24;0
WireConnection;0;0;13;0
WireConnection;0;9;1;4
ASEEND*/
//CHKSM=B6C9FB7D30AAE28CEA785084E0A1B0A1959B46CD