// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Map/Rubber/Spaceships"
{
	Properties
	{
		_Line1("Line1", 2D) = "white" {}
		_Color1("Color1", Color) = (0,0,0,0)
		_Line1_emissive("Line1_emissive", 2D) = "white" {}
		[HDR]_EmissivePower1("EmissivePower1", Color) = (0,0,0,0)
		_Tiling1("Tiling1", Vector) = (0,0,0,0)
		_Offset1("Offset1", Vector) = (0,0,0,0)
		_Speed1("Speed1", Float) = 1
		_Line2("Line2", 2D) = "white" {}
		_Color2("Color2", Color) = (0,0,0,0)
		_Line2_emissive("Line2_emissive", 2D) = "white" {}
		[HDR]_EmissivePower2("EmissivePower2", Color) = (0,0,0,0)
		_Tiling2("Tiling2", Vector) = (0,0,0,0)
		_Speed2("Speed2", Float) = 1
		_Offset2("Offset2", Vector) = (0,0,0,0)
		_Cutoff( "Mask Clip Value", Float ) = 0.76
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _Color1;
		uniform sampler2D _Line1;
		uniform float _Speed1;
		uniform float2 _Tiling1;
		uniform float2 _Offset1;
		uniform float4 _Color2;
		uniform sampler2D _Line2;
		uniform float _Speed2;
		uniform float2 _Tiling2;
		uniform float2 _Offset2;
		uniform float4 _EmissivePower1;
		uniform sampler2D _Line1_emissive;
		uniform sampler2D _Line2_emissive;
		uniform float4 _EmissivePower2;
		uniform float _Cutoff = 0.76;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float4 appendResult7 = (float4(_Speed1 , 0.0 , 0.0 , 0.0));
			float2 uv_TexCoord4 = i.uv_texcoord * _Tiling1 + _Offset1;
			float2 panner3 = ( 1.0 * _Time.y * appendResult7.xy + uv_TexCoord4);
			float4 tex2DNode2 = tex2D( _Line1, panner3 );
			float4 appendResult15 = (float4(_Speed2 , 0.0 , 0.0 , 0.0));
			float2 uv_TexCoord14 = i.uv_texcoord * _Tiling2 + _Offset2;
			float2 panner16 = ( 1.0 * _Time.y * appendResult15.xy + uv_TexCoord14);
			float4 tex2DNode10 = tex2D( _Line2, panner16 );
			o.Albedo = ( ( _Color1 * tex2DNode2 ) + ( _Color2 * tex2DNode10 ) ).rgb;
			o.Emission = ( ( _EmissivePower1 * tex2D( _Line1_emissive, panner3 ) ) + ( tex2D( _Line2_emissive, panner16 ) * _EmissivePower2 ) ).rgb;
			o.Alpha = 1;
			clip( ( tex2DNode2.a + tex2DNode10.a ) - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16103
0;619;1567;382;2597.485;668.2358;3.368397;True;True
Node;AmplifyShaderEditor.Vector2Node;13;-1337.326,266.4987;Float;False;Property;_Tiling2;Tiling2;11;0;Create;True;0;0;False;0;0,0;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;23;-1449.863,429.3737;Float;False;Property;_Offset2;Offset2;13;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;5;-1343.079,-203.6722;Float;False;Property;_Tiling1;Tiling1;4;0;Create;True;0;0;False;0;0,0;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;24;-1333.309,-69.32318;Float;False;Property;_Offset1;Offset1;5;0;Create;True;0;0;False;0;0,0;0,0.36;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;6;-1198.249,71.38632;Float;False;Property;_Speed1;Speed1;6;0;Create;True;0;0;False;0;1;-0.01;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;12;-1228.451,506.5725;Float;False;Property;_Speed2;Speed2;12;0;Create;True;0;0;False;0;1;0.02;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;15;-963.1377,421.4454;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.DynamicAppendNode;7;-932.9357,-13.7408;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;4;-1076.421,-151.3629;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-1106.623,283.8233;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;16;-804.592,309.3614;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;3;-774.39,-125.8248;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;2;-535.5543,-125.2458;Float;True;Property;_Line1;Line1;0;0;Create;True;0;0;False;0;None;4219f510f47d88547b2bc35fde4f2110;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;30;-353.622,717.2942;Float;False;Property;_EmissivePower2;EmissivePower2;10;1;[HDR];Create;True;0;0;False;0;0,0,0,0;766.9961,766.9961,766.9961,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;31;-389.4089,-738.5864;Float;False;Property;_EmissivePower1;EmissivePower1;3;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;10;-475.1379,292.8737;Float;True;Property;_Line2;Line2;7;0;Create;True;0;0;False;0;None;de53d5ca51ed98846b7bba365671e64b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;11;-327.4051,76.98272;Float;False;Property;_Color2;Color2;8;0;Create;True;0;0;False;0;0,0,0,0;0.7529413,0.4039215,0.6392157,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;8;-216.2964,-294.2609;Float;False;Property;_Color1;Color1;1;0;Create;True;0;0;False;0;0,0,0,0;0.7547169,0.4022782,0.6381121,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;27;-534.4478,-516.6116;Float;True;Property;_Line1_emissive;Line1_emissive;2;0;Create;True;0;0;False;0;None;7a61957c1854cd6438dd01311667fe6a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;25;-461.1073,504.0333;Float;True;Property;_Line2_emissive;Line2_emissive;9;0;Create;True;0;0;False;0;None;5de1b91edb8af0a4f8109ab3b5644303;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;101.6446,-98.81691;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;-42.84004,180.4396;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;64.38281,543.2552;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-43.92297,-558.4465;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;20;434.4939,33.84995;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;26;795.8247,58.82297;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;386.3596,199.5626;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;1;1062.341,-80.11974;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Map/Rubber/Spaceships;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.76;True;True;0;True;TransparentCutout;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;14;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;15;0;12;0
WireConnection;7;0;6;0
WireConnection;4;0;5;0
WireConnection;4;1;24;0
WireConnection;14;0;13;0
WireConnection;14;1;23;0
WireConnection;16;0;14;0
WireConnection;16;2;15;0
WireConnection;3;0;4;0
WireConnection;3;2;7;0
WireConnection;2;1;3;0
WireConnection;10;1;16;0
WireConnection;27;1;3;0
WireConnection;25;1;16;0
WireConnection;9;0;8;0
WireConnection;9;1;2;0
WireConnection;18;0;11;0
WireConnection;18;1;10;0
WireConnection;28;0;25;0
WireConnection;28;1;30;0
WireConnection;32;0;31;0
WireConnection;32;1;27;0
WireConnection;20;0;9;0
WireConnection;20;1;18;0
WireConnection;26;0;32;0
WireConnection;26;1;28;0
WireConnection;19;0;2;4
WireConnection;19;1;10;4
WireConnection;1;0;20;0
WireConnection;1;2;26;0
WireConnection;1;10;19;0
ASEEND*/
//CHKSM=043441A79D03A53FECB61BFBD0F6A38818DA8B1A