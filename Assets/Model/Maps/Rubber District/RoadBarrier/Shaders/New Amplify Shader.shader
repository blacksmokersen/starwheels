// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Map/Rubber District/Road/Barrier"
{
	Properties
	{
		_Texture("Texture", 2D) = "white" {}
		_Gradiant("Gradiant", Float) = 1
		[HDR]_InnerColor("InnerColor", Color) = (1,1,1,1)
		[HDR]_OuterColor("OuterColor", Color) = (0.5943396,0.5943396,0.5943396,1)
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

			uniform float4 _InnerColor;
			uniform float4 _OuterColor;
			uniform float _Gradiant;
			uniform sampler2D _Texture;
			uniform float4 _Texture_ST;
			
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
				float2 uv11 = i.ase_texcoord.xy * float2( 1,1 ) + float2( -0.5,-0.5 );
				float clampResult16 = clamp( pow( ( length( uv11 ) * _Gradiant ) , _Gradiant ) , 0.0 , 1.0 );
				float4 lerpResult3 = lerp( _InnerColor , _OuterColor , (0.0 + (clampResult16 - 0.1) * (1.0 - 0.0) / (1.0 - 0.1)));
				float2 uv_Texture = i.ase_texcoord.xy * _Texture_ST.xy + _Texture_ST.zw;
				
				
				finalColor = ( lerpResult3 * tex2D( _Texture, uv_Texture ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
1920;7;1906;1045;2291.709;506.9237;1;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;11;-2365,-251;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;-0.5,-0.5;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LengthOpNode;13;-2034,-261;Float;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-1910,31;Float;False;Property;_Gradiant;Gradiant;1;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;15;-1745,-240;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;1.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;14;-1525,-129;Float;True;2;0;FLOAT;0;False;1;FLOAT;0.87;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;16;-1109,-85;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;6;-854,-47;Float;True;5;0;FLOAT;0;False;1;FLOAT;0.1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;4;-767,-421;Float;False;Property;_InnerColor;InnerColor;2;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-799,-237;Float;False;Property;_OuterColor;OuterColor;3;1;[HDR];Create;True;0;0;False;0;0.5943396,0.5943396,0.5943396,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-812,170;Float;True;Property;_Texture;Texture;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;3;-511,-96;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1;-257,28;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;0,0;Float;False;True;2;Float;ASEMaterialInspector;0;1;Map/Rubber District/Road/Barrier;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;4;1;False;-1;1;False;-1;4;1;False;-1;1;False;-1;True;0;False;-1;0;False;-1;True;False;True;2;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;13;0;11;0
WireConnection;15;0;13;0
WireConnection;15;1;17;0
WireConnection;14;0;15;0
WireConnection;14;1;17;0
WireConnection;16;0;14;0
WireConnection;6;0;16;0
WireConnection;3;0;4;0
WireConnection;3;1;5;0
WireConnection;3;2;6;0
WireConnection;1;0;3;0
WireConnection;1;1;2;0
WireConnection;0;0;1;0
ASEEND*/
//CHKSM=F74E81F4C6D39A01CF628C78428A6BD45104C74E