// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Menu/Hexafield_V2"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (0,0,0,0)
		_Pattern("Pattern", 2D) = "white" {}
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
		_Tiling("Tiling", Vector) = (0,0,0,0)
		_BorderGlow("BorderGlow", Float) = 0
		_FresnelScale("FresnelScale", Float) = 0
		_FresnelPower("FresnelPower", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit alpha:fade keepalpha noshadow noambient nofog 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float4 _Color;
		uniform float _BorderGlow;
		uniform float _FresnelScale;
		uniform float _FresnelPower;
		uniform sampler2D _Pattern;
		uniform float2 _Tiling;
		uniform sampler2D _TextureSample0;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float smoothstepResult75 = smoothstep( 0.1 , 0.0 , i.uv_texcoord.y);
			float smoothstepResult76 = smoothstep( 0.9 , 1.0 , i.uv_texcoord.y);
			o.Emission = ( ( _Color + ( _Color * ( ( smoothstepResult75 + smoothstepResult76 ) * _BorderGlow ) ) ) * float4( 1,1,1,0 ) ).rgb;
			float3 ase_worldPos = i.worldPos;
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV66 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode66 = ( 0.0 + _FresnelScale * pow( 1.0 - fresnelNdotV66, _FresnelPower ) );
			float clampResult68 = clamp( fresnelNode66 , 0.0 , 1.0 );
			float smoothstepResult49 = smoothstep( 0.5 , 0.45 , i.uv_texcoord.y);
			float2 uv_TexCoord47 = i.uv_texcoord * _Tiling;
			float2 panner46 = ( 1.0 * _Time.y * float2( 0,-0.02 ) + uv_TexCoord47);
			float2 panner53 = ( 1.0 * _Time.y * float2( 0,0.02 ) + uv_TexCoord47);
			float smoothstepResult51 = smoothstep( 0.45 , 0.5 , i.uv_texcoord.y);
			o.Alpha = ( clampResult68 * ( ( smoothstepResult49 * tex2D( _Pattern, panner46 ) ) + ( tex2D( _TextureSample0, panner53 ) * smoothstepResult51 ) ) ).r;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16103
0;626;1437;375;776.5137;183.6777;1.693817;True;True
Node;AmplifyShaderEditor.Vector2Node;64;-1927.573,84.54381;Float;False;Property;_Tiling;Tiling;3;0;Create;True;0;0;False;0;0,0;2,0.4;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;65;-1845.984,381.6284;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;47;-1649.467,82.28489;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;75;-944.9886,-306.5093;Float;True;3;0;FLOAT;0;False;1;FLOAT;0.1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;76;-941.7443,799.7148;Float;True;3;0;FLOAT;0;False;1;FLOAT;0.9;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;46;-1228.203,157.5934;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-0.02;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;53;-1275.261,379.5233;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0.02;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;77;-151.5081,407.1856;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;80;71.86933,530.1689;Float;False;Property;_BorderGlow;BorderGlow;4;0;Create;True;0;0;False;0;0;2.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;85;113.9408,-64.88106;Float;False;Property;_FresnelScale;FresnelScale;5;0;Create;True;0;0;False;0;0;0.39;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;54;-993.5334,384.4571;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;a830d0b8f4aa44741a23bcdb1dd90f5e;fdb3dffa70ead384f97ddcb99400c185;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-991.8674,155.5437;Float;True;Property;_Pattern;Pattern;1;0;Create;True;0;0;False;0;a830d0b8f4aa44741a23bcdb1dd90f5e;fdb3dffa70ead384f97ddcb99400c185;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SmoothstepOpNode;49;-966.3141,-75.95396;Float;True;3;0;FLOAT;0;False;1;FLOAT;0.5;False;2;FLOAT;0.45;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;51;-949.4133,594.1492;Float;True;3;0;FLOAT;0;False;1;FLOAT;0.45;False;2;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;304.5226,395.9456;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;580.6492,-374.5322;Float;False;Property;_Color;Color;0;1;[HDR];Create;True;0;0;False;0;0,0,0,0;1.209128,0.1822922,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;86;127.9847,29.7433;Float;False;Property;_FresnelPower;FresnelPower;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;58;-620.8218,425.6042;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;56;-615.8503,145.3979;Float;True;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;918.9319,-184.3323;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.FresnelNode;66;380.0731,-66.34373;Float;True;Standard;WorldNormal;ViewDir;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;68;807.0626,-20.10941;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;57;43.49735,144.1321;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;78;1268.935,-190.7809;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;1094.543,112.0525;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;84;1416.551,-80.94716;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;1,1,1,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;1599.81,-92.1541;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Menu/Hexafield_V2;False;False;False;False;True;False;False;False;False;True;False;False;False;False;True;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;47;0;64;0
WireConnection;75;0;65;2
WireConnection;76;0;65;2
WireConnection;46;0;47;0
WireConnection;53;0;47;0
WireConnection;77;0;75;0
WireConnection;77;1;76;0
WireConnection;54;1;53;0
WireConnection;2;1;46;0
WireConnection;49;0;65;2
WireConnection;51;0;65;2
WireConnection;79;0;77;0
WireConnection;79;1;80;0
WireConnection;58;0;54;0
WireConnection;58;1;51;0
WireConnection;56;0;49;0
WireConnection;56;1;2;0
WireConnection;70;0;1;0
WireConnection;70;1;79;0
WireConnection;66;2;85;0
WireConnection;66;3;86;0
WireConnection;68;0;66;0
WireConnection;57;0;56;0
WireConnection;57;1;58;0
WireConnection;78;0;1;0
WireConnection;78;1;70;0
WireConnection;69;0;68;0
WireConnection;69;1;57;0
WireConnection;84;0;78;0
WireConnection;0;2;84;0
WireConnection;0;9;69;0
ASEEND*/
//CHKSM=FEF851CD39A4434614A51587A44A7EE657CF18E4