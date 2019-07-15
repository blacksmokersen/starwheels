// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Kart/VFX/Flat Shild"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (1,1,1,1)
		_Speed("Speed", Float) = 0.5
		_Texture("Texture", 2D) = "white" {}
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
		Offset 0 , 0
		
		

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

			uniform sampler2D _Texture;
			uniform float4 _Texture_ST;
			uniform float _Speed;
			uniform float4 _Color;
			
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
				float2 uv_Texture = i.ase_texcoord.xy * _Texture_ST.xy + _Texture_ST.zw;
				float temp_output_20_0 = ( _Time.y * _Speed );
				float2 panner22 = ( temp_output_20_0 * float2( 0.5,0 ) + uv_Texture);
				float2 temp_output_27_0 = ( uv_Texture * float2( 1.1,1.1 ) );
				float2 panner23 = ( temp_output_20_0 * float2( -0.5,0 ) + temp_output_27_0);
				float clampResult9 = clamp( pow( ( 2.0 * tex2D( _Texture, panner22 ).b * tex2D( _Texture, panner23 ).b ) , 2.0 ) , 0.0 , 1.0 );
				float temp_output_7_0 = ( 0.1 + tex2D( _Texture, uv_Texture ).r + clampResult9 );
				float2 panner24 = ( temp_output_20_0 * float2( 1.3,1 ) + uv_Texture);
				float2 panner25 = ( temp_output_20_0 * float2( -0.9,-1.2 ) + temp_output_27_0);
				float clampResult3 = clamp( ( temp_output_7_0 + ( temp_output_7_0 * ( tex2D( _Texture, panner24 ).g * tex2D( _Texture, panner25 ).g ) ) ) , 0.0 , 1.0 );
				
				
				finalColor = ( clampResult3 * _Color * _Color.a );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
1980;188;1450;737;1955.343;349.7823;1.496911;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;26;-2660.921,-44.07416;Float;False;0;17;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;19;-2647.921,249.9258;Float;False;Property;_Speed;Speed;1;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;18;-2706.921,100.9258;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-2399.921,53.92584;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;1.1,1.1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-2480.921,188.9258;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;23;-2154.921,194.9258;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.5,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;22;-2170.921,50.92584;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.5,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;17;-2310.077,-162.2505;Float;True;Property;_Texture;Texture;2;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;13;-1937,198;Float;True;Property;_TextureSample2;Texture Sample 2;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;12;-1940,-2;Float;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-1439,28;Float;False;3;3;0;FLOAT;2;False;1;FLOAT;2;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;24;-2139.921,409.9258;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1.3,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;25;-2135.921,590.9258;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.9,-1.2;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PowerNode;10;-1305,27;Float;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;-1930,394;Float;True;Property;_TextureSample3;Texture Sample 3;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;15;-1928,585;Float;True;Property;_TextureSample4;Texture Sample 4;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;9;-1165,25;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;8;-1311,-162;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-1411,549;Float;False;2;2;0;FLOAT;2;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-908,-157;Float;False;3;3;0;FLOAT;0.1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;-750,-19;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;5;-586,-114;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;-487,5;Float;False;Property;_Color;Color;0;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;3;-433,-110;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-188,-68;Float;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;1;Kart/VFX/Flat Shild;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;4;1;False;-1;1;False;-1;4;1;False;-1;1;False;-1;True;0;False;-1;0;False;-1;True;False;True;2;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;27;0;26;0
WireConnection;20;0;18;2
WireConnection;20;1;19;0
WireConnection;23;0;27;0
WireConnection;23;1;20;0
WireConnection;22;0;26;0
WireConnection;22;1;20;0
WireConnection;13;0;17;0
WireConnection;13;1;23;0
WireConnection;12;0;17;0
WireConnection;12;1;22;0
WireConnection;11;1;12;3
WireConnection;11;2;13;3
WireConnection;24;0;26;0
WireConnection;24;1;20;0
WireConnection;25;0;27;0
WireConnection;25;1;20;0
WireConnection;10;0;11;0
WireConnection;14;0;17;0
WireConnection;14;1;24;0
WireConnection;15;0;17;0
WireConnection;15;1;25;0
WireConnection;9;0;10;0
WireConnection;8;0;17;0
WireConnection;16;0;14;2
WireConnection;16;1;15;2
WireConnection;7;1;8;1
WireConnection;7;2;9;0
WireConnection;6;0;7;0
WireConnection;6;1;16;0
WireConnection;5;0;7;0
WireConnection;5;1;6;0
WireConnection;3;0;5;0
WireConnection;4;0;3;0
WireConnection;4;1;1;0
WireConnection;4;2;1;4
WireConnection;0;0;4;0
ASEEND*/
//CHKSM=4FF778F84114D02A86FA49F1FE5A7F31AD90D39C