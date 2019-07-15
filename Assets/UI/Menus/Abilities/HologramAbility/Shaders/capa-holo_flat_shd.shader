// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Menu/Abilities/Hologram"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (1,1,1,1)
		_BaseColor("Base Color", 2D) = "white" {}
		_Holograme("Holograme", 2D) = "white" {}
		_Speed("Speed", Float) = 0.5
		_Opacitybase("Opacity base", Float) = 1
		_PaternePower("Paterne Power", Float) = 1
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

			uniform float4 _Color;
			uniform sampler2D _BaseColor;
			uniform float4 _BaseColor_ST;
			uniform sampler2D _Holograme;
			uniform float _Speed;
			uniform float4 _Holograme_ST;
			uniform float _PaternePower;
			uniform float _Opacitybase;
			
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
				float2 uv_BaseColor = i.ase_texcoord.xy * _BaseColor_ST.xy + _BaseColor_ST.zw;
				float4 tex2DNode13 = tex2D( _BaseColor, uv_BaseColor );
				float temp_output_25_0 = ( _Time.y * _Speed );
				float2 uv_Holograme = i.ase_texcoord.xy * _Holograme_ST.xy + _Holograme_ST.zw;
				float2 panner27 = ( temp_output_25_0 * float2( 0,1 ) + uv_Holograme);
				float2 panner31 = ( temp_output_25_0 * float2( 0,-0.8 ) + ( uv_Holograme * float2( 1,0.9 ) ));
				float clampResult16 = clamp( ( pow( ( tex2D( _Holograme, panner27 ).r * tex2D( _Holograme, panner31 ).r ) , _PaternePower ) + _Opacitybase ) , 0.0 , 1.0 );
				
				
				finalColor = ( ( _Color * _Color.a * tex2DNode13.r * tex2DNode13.a * (( ( sin( ( _Time.y * 15.0 ) ) * cos( ( _Time.y * 0.0 ) ) ) >= 0.0 && ( sin( ( _Time.y * 15.0 ) ) * cos( ( _Time.y * 0.0 ) ) ) <= 0.5 ) ? 1.1 :  0.8 ) ) * clampResult16 );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
1948;256;1450;737;1172.533;153.2542;1;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;33;-1703.533,398.7458;Float;False;0;23;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;26;-1831.533,735.7458;Float;False;Property;_Speed;Speed;3;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;24;-1872.533,592.7458;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-1439.533,521.7458;Float;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;1,0.9;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-1618.533,661.7458;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;8;-1261,22;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TexturePropertyNode;23;-1377.533,247.7458;Float;True;Property;_Holograme;Holograme;2;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PannerNode;27;-1278.533,457.7458;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;31;-1286.533,702.7458;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-0.8;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;21;-1032.533,375.7458;Float;True;Property;_TextureSample0;Texture Sample 0;3;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;22;-1030.533,575.7458;Float;True;Property;_TextureSample1;Texture Sample 1;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-1037,84;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-1039,-9;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;15;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-712.5332,407.7458;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-711.5332,500.7458;Float;False;Property;_PaternePower;Paterne Power;5;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;11;-806,108;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;12;-826,0;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;19;-591.988,573.8403;Float;False;Property;_Opacitybase;Opacity base;4;0;Create;True;0;0;False;0;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-663,7;Float;True;2;2;0;FLOAT;5;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;20;-578.5331,404.7458;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;14;-455.0887,-481.5403;Float;False;Property;_Color;Color;0;1;[HDR];Create;True;0;0;False;0;1,1,1,1;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCCompareWithRange;2;-463,5;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.5;False;3;FLOAT;1.1;False;4;FLOAT;0.8;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-405.3821,439.5097;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;13;-556.2183,-293.5711;Float;True;Property;_BaseColor;Base Color;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1;-107.3819,-172.5952;Float;True;5;5;0;COLOR;1,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;16;-284.7366,454.8028;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;276.011,-0.5921631;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;512.5844,-31.88218;Float;False;True;2;Float;ASEMaterialInspector;0;1;Menu/Abilities/Hologram;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;4;1;False;-1;1;False;-1;4;1;False;-1;1;False;-1;True;0;False;-1;0;False;-1;True;False;True;2;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;37;0;33;0
WireConnection;25;0;24;2
WireConnection;25;1;26;0
WireConnection;27;0;33;0
WireConnection;27;1;25;0
WireConnection;31;0;37;0
WireConnection;31;1;25;0
WireConnection;21;0;23;0
WireConnection;21;1;27;0
WireConnection;22;0;23;0
WireConnection;22;1;31;0
WireConnection;10;0;8;2
WireConnection;9;0;8;2
WireConnection;34;0;21;1
WireConnection;34;1;22;1
WireConnection;11;0;10;0
WireConnection;12;0;9;0
WireConnection;5;0;12;0
WireConnection;5;1;11;0
WireConnection;20;0;34;0
WireConnection;20;1;35;0
WireConnection;2;0;5;0
WireConnection;17;0;20;0
WireConnection;17;1;19;0
WireConnection;1;0;14;0
WireConnection;1;1;14;4
WireConnection;1;2;13;1
WireConnection;1;3;13;4
WireConnection;1;4;2;0
WireConnection;16;0;17;0
WireConnection;15;0;1;0
WireConnection;15;1;16;0
WireConnection;0;0;15;0
ASEEND*/
//CHKSM=B3209C54848201356B096717460F05C0F3CD939B