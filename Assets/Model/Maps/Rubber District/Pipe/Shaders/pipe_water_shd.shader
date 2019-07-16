// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Map/Rubber District/Pipe/Water"
{
	Properties
	{
		_ColorBase("Color Base", Color) = (0,0,0,0)
		_ColorMove("Color Move", Color) = (0,0,0,0)
		_Water("Water", 2D) = "white" {}
		_MoveVisibility("Move Visibility", Float) = 0.5
		_MoveSharpness("Move Sharpness", Float) = 0.5
		_Speed("Speed", Float) = 0.5
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Back
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

			uniform float4 _ColorMove;
			uniform float4 _ColorBase;
			uniform sampler2D _Water;
			uniform float _Speed;
			uniform float4 _Water_ST;
			uniform float _MoveSharpness;
			uniform float _MoveVisibility;
			
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
				float temp_output_25_0 = ( _Time.y * _Speed );
				float2 uv_Water = i.ase_texcoord.xy * _Water_ST.xy + _Water_ST.zw;
				float2 panner30 = ( temp_output_25_0 * float2( 0.2,1 ) + uv_Water);
				float2 panner31 = ( temp_output_25_0 * float2( -0.22,0.22 ) + ( uv_Water * float2( 2,2 ) ));
				float2 panner32 = ( temp_output_25_0 * float2( 0,0.3 ) + ( uv_Water * float2( 3,1 ) ));
				float clampResult3 = clamp( ( 1.0 - pow( ( ( ( tex2D( _Water, panner30 ).r * 0.5 ) + ( tex2D( _Water, panner31 ).r * 0.4 ) + ( tex2D( _Water, panner32 ).r * 0.5 ) ) * 1.5 ) , _MoveSharpness ) ) , ( 1.0 - _MoveVisibility ) , 1.0 );
				float4 lerpResult2 = lerp( _ColorMove , _ColorBase , clampResult3);
				
				
				finalColor = lerpResult2;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
0;552;1472;449;1368.197;187.4442;1.387461;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;35;-3238.949,-376.1692;Float;False;0;16;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;24;-3332.949,-128.1692;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;23;-3279.949,13.83081;Float;False;Property;_Speed;Speed;5;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;39;-2928.949,200.8308;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;3,1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-3108.949,-55.16919;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;-2939.949,-190.1692;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;2,2;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;32;-2690.949,175.8308;Float;False;3;0;FLOAT2;0,0.3;False;2;FLOAT2;0,0.3;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;31;-2636.949,-63.1692;Float;False;3;0;FLOAT2;-0.22,0.22;False;2;FLOAT2;-0.22,0.22;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;30;-2639.949,-222.1692;Float;False;3;0;FLOAT2;0.2,1;False;2;FLOAT2;0.2,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;16;-2618,-459;Float;True;Property;_Water;Water;2;0;Create;True;0;0;False;0;None;6a6921b4060c40942a5459388016c99c;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.SamplerNode;15;-2352,206;Float;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;19;-2301.949,-0.1691895;Float;True;Property;_TextureSample1;Texture Sample 1;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;20;-2311.949,-222.1692;Float;True;Property;_TextureSample2;Texture Sample 2;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-1961.949,-162.1692;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-1957.949,264.8308;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-1961.949,28.83081;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0.4;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-1792,98;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-1602,139;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;1.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;11;-1575,314;Float;False;Property;_MoveSharpness;Move Sharpness;4;0;Create;True;0;0;False;0;0.5;3.47;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;10;-1318,171;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;8;-1270,335;Float;False;Property;_MoveVisibility;Move Visibility;3;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;9;-1071,198;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;6;-1046,338;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;4;-805,-9;Float;False;Property;_ColorBase;Color Base;0;0;Create;True;0;0;False;0;0,0,0,0;0,0.5249328,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;3;-790,217;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;5;-828,-210;Float;False;Property;_ColorMove;Color Move;1;0;Create;True;0;0;False;0;0,0,0,0;0.2311321,0.8190465,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;2;-490,28;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;40;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;1;Map/Rubber District/Pipe/Water;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;39;0;35;0
WireConnection;25;0;24;2
WireConnection;25;1;23;0
WireConnection;38;0;35;0
WireConnection;32;0;39;0
WireConnection;32;1;25;0
WireConnection;31;0;38;0
WireConnection;31;1;25;0
WireConnection;30;0;35;0
WireConnection;30;1;25;0
WireConnection;15;0;16;0
WireConnection;15;1;32;0
WireConnection;19;0;16;0
WireConnection;19;1;31;0
WireConnection;20;0;16;0
WireConnection;20;1;30;0
WireConnection;22;0;20;1
WireConnection;17;0;15;1
WireConnection;21;0;19;1
WireConnection;14;0;22;0
WireConnection;14;1;21;0
WireConnection;14;2;17;0
WireConnection;12;0;14;0
WireConnection;10;0;12;0
WireConnection;10;1;11;0
WireConnection;9;0;10;0
WireConnection;6;0;8;0
WireConnection;3;0;9;0
WireConnection;3;1;6;0
WireConnection;2;0;5;0
WireConnection;2;1;4;0
WireConnection;2;2;3;0
WireConnection;40;0;2;0
ASEEND*/
//CHKSM=A43360D8A215AEC41FC3102049EFCC8EE0E6B86B