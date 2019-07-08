// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Map/Rubbre District//border Wall/Force Field"
{
	Properties
	{
		_FieldTexture("Field Texture", 2D) = "white" {}
		[HDR]_FieldColor("Field Color", Color) = (0.9811321,0.9579922,0.9579922,1)
		[HDR]_WaveColor("Wave Color", Color) = (1,1,1,1)
		[HDR]_EnergyColor("Energy Color", Color) = (1,1,1,1)
		_WaveTexture("Wave Texture ", 2D) = "white" {}
		_EnergySpeed("Energy Speed", Float) = 0.5
		_WaveSpeed("Wave Speed", Float) = 0.5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend One One , One One
		Cull Off
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		
		

		Pass
		{
			Name "Unlit"
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				float4 ase_texcoord : TEXCOORD0;
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform float4 _FieldColor;
			uniform sampler2D _FieldTexture;
			uniform float4 _FieldTexture_ST;
			uniform float4 _WaveColor;
			uniform sampler2D _WaveTexture;
			uniform float _WaveSpeed;
			uniform float4 _WaveTexture_ST;
			uniform float4 _EnergyColor;
			uniform float _EnergySpeed;
			float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }
			float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }
			float snoise( float2 v )
			{
				const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
				float2 i = floor( v + dot( v, C.yy ) );
				float2 x0 = v - i + dot( i, C.xx );
				float2 i1;
				i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
				float4 x12 = x0.xyxy + C.xxzz;
				x12.xy -= i1;
				i = mod2D289( i );
				float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
				float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
				m = m * m;
				m = m * m;
				float3 x = 2.0 * frac( p * C.www ) - 1.0;
				float3 h = abs( x ) - 0.5;
				float3 ox = floor( x + 0.5 );
				float3 a0 = x - ox;
				m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
				float3 g;
				g.x = a0.x * x0.x + h.x * x0.y;
				g.yz = a0.yz * x12.xz + h.yz * x12.yw;
				return 130.0 * dot( m, g );
			}
			
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				o.ase_texcoord.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float2 uv_FieldTexture = i.ase_texcoord.xy * _FieldTexture_ST.xy + _FieldTexture_ST.zw;
				float4 tex2DNode1 = tex2D( _FieldTexture, uv_FieldTexture );
				float2 uv_WaveTexture = i.ase_texcoord.xy * _WaveTexture_ST.xy + _WaveTexture_ST.zw;
				float2 panner27 = ( ( _WaveSpeed * _Time.y ) * float2( 1,0 ) + uv_WaveTexture);
				float2 temp_output_17_0 = ( _Time.y * ( _EnergySpeed * float2( 1,0 ) ) );
				float2 uv15 = i.ase_texcoord.xy * float2( 10,10 ) + temp_output_17_0;
				float simplePerlin2D14 = snoise( uv15 );
				float2 uv20 = i.ase_texcoord.xy * float2( 8,8 ) + ( temp_output_17_0 * float2( 0.25,0.5 ) );
				float simplePerlin2D19 = snoise( uv20 );
				float smoothstepResult9 = smoothstep( 0.25 , 1.0 , ( simplePerlin2D14 * simplePerlin2D19 ));
				
				
				finalColor = ( ( _FieldColor.a * tex2DNode1 ) * ( ( ( _FieldColor * tex2DNode1 ) + ( ( _WaveColor * _WaveColor.a ) * tex2D( _WaveTexture, panner27 ) ) ) + ( ( _EnergyColor * _EnergyColor.a ) * smoothstepResult9 ) ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
1920;7;1906;1045;864.4207;581.0475;1;True;False
Node;AmplifyShaderEditor.Vector2Node;24;-1830.113,977.2807;Float;False;Constant;_Vector0;Vector 0;6;0;Create;True;0;0;False;0;1,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;18;-1824.113,820.2807;Float;False;Property;_EnergySpeed;Energy Speed;5;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;16;-1749.113,433.2807;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-1596.113,900.2807;Float;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1458.113,807.2807;Float;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-1664.113,219.2807;Float;False;Property;_WaveSpeed;Wave Speed;6;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1219.113,911.2807;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0.25,0.5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;20;-1011.113,870.2807;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;8,8;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-1131.113,592.2807;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;10,10;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;31;-1338.421,152.9525;Float;False;0;12;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1268.113,374.2807;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;27;-1013.113,325.2807;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NoiseGeneratorNode;14;-780.7847,570.182;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;11;-834,35;Float;False;Property;_WaveColor;Wave Color;2;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NoiseGeneratorNode;19;-767.1128,822.2807;Float;True;Simplex2D;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;12;-741,275;Float;True;Property;_WaveTexture;Wave Texture ;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-433.1128,711.2807;Float;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;1;-506,-176;Float;True;Property;_FieldTexture;Field Texture;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;8;-119,225;Float;False;Property;_EnergyColor;Energy Color;3;1;[HDR];Create;True;0;0;False;0;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;2;-513,-413;Float;False;Property;_FieldColor;Field Color;1;1;[HDR];Create;True;0;0;False;0;0.9811321,0.9579922,0.9579922,1;0.9811321,0.9579922,0.9579922,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;29;-527.4207,117.9525;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-367,260;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SmoothstepOpNode;9;-190,474;Float;True;3;0;FLOAT;0;False;1;FLOAT;0.25;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-48.2,-185.6;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;130.5793,291.9525;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;4;142,23;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;351,368;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;6;419,-17;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;142.5793,-388.0475;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;701.7,-165.2;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;980.6,-181.1;Float;False;True;2;Float;ASEMaterialInspector;0;1;Map/Rubbre District//border Wall/Force Field;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;4;1;False;-1;1;False;-1;4;1;False;-1;1;False;-1;True;0;False;-1;0;False;-1;True;False;True;2;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;False;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;23;0;18;0
WireConnection;23;1;24;0
WireConnection;17;0;16;2
WireConnection;17;1;23;0
WireConnection;21;0;17;0
WireConnection;20;1;21;0
WireConnection;15;1;17;0
WireConnection;25;0;26;0
WireConnection;25;1;16;2
WireConnection;27;0;31;0
WireConnection;27;1;25;0
WireConnection;14;0;15;0
WireConnection;19;0;20;0
WireConnection;12;1;27;0
WireConnection;22;0;14;0
WireConnection;22;1;19;0
WireConnection;29;0;11;0
WireConnection;29;1;11;4
WireConnection;10;0;29;0
WireConnection;10;1;12;0
WireConnection;9;0;22;0
WireConnection;3;0;2;0
WireConnection;3;1;1;0
WireConnection;30;0;8;0
WireConnection;30;1;8;4
WireConnection;4;0;3;0
WireConnection;4;1;10;0
WireConnection;7;0;30;0
WireConnection;7;1;9;0
WireConnection;6;0;4;0
WireConnection;6;1;7;0
WireConnection;32;0;2;4
WireConnection;32;1;1;0
WireConnection;5;0;32;0
WireConnection;5;1;6;0
WireConnection;0;0;5;0
ASEEND*/
//CHKSM=48F3775362E4CD3629170A25518B0E52D222CED7