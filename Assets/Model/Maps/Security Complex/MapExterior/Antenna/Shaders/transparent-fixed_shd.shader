// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify/TransparentFixed"
{
	Properties
	{
		_albedo("albedo", 2D) = "white" {}
		_normal("normal", 2D) = "bump" {}
		_metallic("metallic", 2D) = "white" {}
		_emission("emission", 2D) = "white" {}
		[HDR]_EmissionColor("EmissionColor", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows exclude_path:deferred 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _normal;
		uniform float4 _normal_ST;
		uniform sampler2D _albedo;
		uniform float4 _albedo_ST;
		uniform float4 _EmissionColor;
		uniform sampler2D _emission;
		uniform float4 _emission_ST;
		uniform sampler2D _metallic;
		uniform float4 _metallic_ST;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_normal = i.uv_texcoord * _normal_ST.xy + _normal_ST.zw;
			o.Normal = UnpackNormal( tex2D( _normal, uv_normal ) );
			float2 uv_albedo = i.uv_texcoord * _albedo_ST.xy + _albedo_ST.zw;
			float4 tex2DNode1 = tex2D( _albedo, uv_albedo );
			o.Albedo = tex2DNode1.rgb;
			float2 uv_emission = i.uv_texcoord * _emission_ST.xy + _emission_ST.zw;
			o.Emission = ( _EmissionColor * tex2D( _emission, uv_emission ) ).rgb;
			float2 uv_metallic = i.uv_texcoord * _metallic_ST.xy + _metallic_ST.zw;
			float4 tex2DNode3 = tex2D( _metallic, uv_metallic );
			o.Metallic = tex2DNode3.r;
			o.Smoothness = ( 1.0 - tex2DNode3 ).r;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16103
274;399;1401;500;991.135;233.3166;1.670555;True;True
Node;AmplifyShaderEditor.SamplerNode;4;-250.4359,482.0862;Float;True;Property;_emission;emission;4;0;Create;True;0;0;False;0;None;9b576ae98c892d044b412abc04f0035e;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-276.197,255.6688;Float;True;Property;_metallic;metallic;3;0;Create;True;0;0;False;0;None;6dea3328702f4584ab1ec29b44e08b87;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;9;66.68079,349.9117;Float;False;Property;_EmissionColor;EmissionColor;5;1;[HDR];Create;True;0;0;False;0;0,0,0,0;1.423658,2.449718,2.132874,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-266.4358,-189.8499;Float;True;Property;_albedo;albedo;0;0;Create;True;0;0;False;0;None;e40fe3b6a79a5a245b70fd49e4cb2dcf;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-285.677,41.46128;Float;True;Property;_normal;normal;1;0;Create;True;0;0;False;0;None;bb62a4f2b39b5a242b06d44368c5bef8;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;345.6636,465.1801;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;5;136.9452,112.9949;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;600.1671,-58.89236;Float;False;True;2;Float;ASEMaterialInspector;0;0;Standard;Amplify/TransparentFixed;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;True;TransparentCutout;;AlphaTest;ForwardOnly;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;5;False;-1;10;False;-1;2;5;False;-1;10;False;-1;0;False;-1;0;False;-1;0;False;10;1,1,1,1;VertexOffset;True;False;Cylindrical;False;Relative;0;;2;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;10;0;9;0
WireConnection;10;1;4;0
WireConnection;5;0;3;0
WireConnection;0;0;1;0
WireConnection;0;1;2;0
WireConnection;0;2;10;0
WireConnection;0;3;3;0
WireConnection;0;4;5;0
WireConnection;0;9;1;4
ASEEND*/
//CHKSM=9316787D2183330FC393CB570162C7A9020492FD