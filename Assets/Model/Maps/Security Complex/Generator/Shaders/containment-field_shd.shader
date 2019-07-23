// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify/ContainmentField"
{
	Properties
	{
		_BaseTexture("BaseTexture", 2D) = "white" {}
		[HDR]_Color("Color", Color) = (0,0.9293938,1,1)
		_RipplesTexture("RipplesTexture", 2D) = "white" {}
		_Speed("Speed", Range( 0 , 1)) = 0
		_Alpha("Alpha", Range( 0 , 1)) = 0.01
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		ZWrite Off
		Blend One One
		
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
		};

		uniform float4 _Color;
		uniform sampler2D _RipplesTexture;
		uniform float _Speed;
		uniform sampler2D _BaseTexture;
		uniform float4 _BaseTexture_ST;
		uniform float _Alpha;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 appendResult32 = (float4(0.0 , ( _Speed * -0.5 ) , 0.0 , 0.0));
			float2 uv_TexCoord29 = i.uv_texcoord * float2( 4,2 );
			float2 panner35 = ( 1.0 * _Time.y * ( appendResult32 * -1.0 ).xy + uv_TexCoord29);
			float3 ase_vertex3Pos = mul( unity_WorldToObject, float4( i.worldPos , 1 ) );
			float smoothstepResult26 = smoothstep( -10.0 , -30.0 , ase_vertex3Pos.y);
			float2 panner28 = ( 1.0 * _Time.y * appendResult32.xy + uv_TexCoord29);
			float smoothstepResult30 = smoothstep( 10.0 , 30.0 , ase_vertex3Pos.y);
			float2 uv_BaseTexture = i.uv_texcoord * _BaseTexture_ST.xy + _BaseTexture_ST.zw;
			o.Emission = ( _Color * ( ( ( tex2D( _RipplesTexture, panner35 ) * smoothstepResult26 ) + ( tex2D( _RipplesTexture, panner28 ) * smoothstepResult30 ) ) * ( tex2D( _BaseTexture, uv_BaseTexture ) * _Alpha ) ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16103
0;601;1497;400;1297.553;560.5911;4.785252;True;True
Node;AmplifyShaderEditor.RangedFloatNode;41;-23.13602,-26.34247;Float;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;False;0;-0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-210.7315,-209.2355;Float;False;Property;_Speed;Speed;3;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;208.9228,-116.4359;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;32;508.005,-215.8686;Float;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;37;463.3372,-24.0408;Float;False;Constant;_Float0;Float 0;4;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;29;496.8433,-348.2613;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;4,2;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;684.6386,-37.70137;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.PosVertexDataNode;17;905.6391,126.4295;Float;True;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;2;903.531,-572.6366;Float;True;Property;_RipplesTexture;RipplesTexture;2;0;Create;True;0;0;False;0;bee2f2179257635479df635a6f512fdc;bee2f2179257635479df635a6f512fdc;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;28;869.8231,-276.985;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-5;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;35;878.426,-65.94453;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-5;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SmoothstepOpNode;30;1351.136,-54.47308;Float;True;3;0;FLOAT;0;False;1;FLOAT;10;False;2;FLOAT;30;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;26;1351.713,221.4375;Float;True;3;0;FLOAT;0;False;1;FLOAT;-10;False;2;FLOAT;-30;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;1269.823,-532.985;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;34;1283.343,-289.9834;Float;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;1738.07,-343.0763;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;43;1497.026,769.6494;Float;False;Property;_Alpha;Alpha;5;0;Create;True;0;0;False;0;0.01;0.01;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;44;1444.034,550.3892;Float;True;Property;_BaseTexture;BaseTexture;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;1732.226,2.176321;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;46;1876.568,547.9241;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;38;2179.929,112.8469;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;2526.41,446.0693;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;11;2480.384,35.93344;Float;False;Property;_Color;Color;1;1;[HDR];Create;True;0;0;False;0;0,0.9293938,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;2921.47,237.2155;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;39;3261.116,175.6878;Float;False;True;2;Float;ASEMaterialInspector;0;0;Unlit;Amplify/ContainmentField;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Off;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;True;TransparentCutout;;Transparent;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;4;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;40;0;31;0
WireConnection;40;1;41;0
WireConnection;32;1;40;0
WireConnection;36;0;32;0
WireConnection;36;1;37;0
WireConnection;28;0;29;0
WireConnection;28;2;32;0
WireConnection;35;0;29;0
WireConnection;35;2;36;0
WireConnection;30;0;17;2
WireConnection;26;0;17;2
WireConnection;1;0;2;0
WireConnection;1;1;28;0
WireConnection;34;0;2;0
WireConnection;34;1;35;0
WireConnection;24;0;1;0
WireConnection;24;1;30;0
WireConnection;33;0;34;0
WireConnection;33;1;26;0
WireConnection;46;0;44;0
WireConnection;46;1;43;0
WireConnection;38;0;33;0
WireConnection;38;1;24;0
WireConnection;55;0;38;0
WireConnection;55;1;46;0
WireConnection;8;0;11;0
WireConnection;8;1;55;0
WireConnection;39;2;8;0
ASEEND*/
//CHKSM=E6E0969B44885DB64B967E17A0C81E2704DE1124