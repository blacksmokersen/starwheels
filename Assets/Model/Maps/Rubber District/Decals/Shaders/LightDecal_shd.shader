// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify/LightDecal"
{
	Properties
	{
		_Texture("Texture", 2D) = "white" {}
		_Falloff_Texture("Falloff_Texture", 2D) = "white" {}
		[HDR]_Color("Color", Color) = (1,1,1,1)
		_Luminosity("Luminosity", Float) = 0
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend DstColor One
		Cull Back
		ColorMask RGB
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
				
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform float4 _Color;
			uniform sampler2D _Texture;
			float4x4 unity_Projector;
			uniform sampler2D _Falloff_Texture;
			float4x4 unity_ProjectorClip;
			uniform float _Luminosity;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 vertexToFrag4 = mul( unity_Projector, v.vertex );
				o.ase_texcoord = vertexToFrag4;
				float4 vertexToFrag18 = mul( unity_ProjectorClip, v.vertex );
				o.ase_texcoord1 = vertexToFrag18;
				
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float4 vertexToFrag4 = i.ase_texcoord;
				float4 tex2DNode9 = tex2D( _Texture, ( (vertexToFrag4).xy / (vertexToFrag4).w ) );
				float4 appendResult14 = (float4(( float4( (_Color).rgb , 0.0 ) * tex2DNode9 ).rgb , ( 1.0 - tex2DNode9.a )));
				float4 vertexToFrag18 = i.ase_texcoord1;
				
				
				finalColor = ( ( appendResult14 * tex2D( _Falloff_Texture, ( (vertexToFrag18).xy / (vertexToFrag18).w ) ).a ) * _Luminosity );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
196;490;1401;500;126.3242;252.5889;1.834188;True;True
Node;AmplifyShaderEditor.UnityProjectorMatrixNode;24;-1437.051,-42.88054;Float;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.PosVertexDataNode;2;-1438.982,72.29573;Float;False;1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-1045.896,21.03516;Float;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.UnityProjectorClipMatrixNode;23;-1095.698,313.2841;Float;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.PosVertexDataNode;20;-1098.227,425.6609;Float;False;1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexToFragmentNode;4;-875.0742,22.26987;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-705.1418,374.4003;Float;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ComponentMaskNode;5;-620.0742,29.26987;Float;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ComponentMaskNode;8;-615.7036,125.6726;Float;False;False;False;False;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;7;-350.456,37.37737;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexToFragmentNode;18;-534.3197,375.635;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ColorNode;10;-247.1353,-287.5587;Float;False;Property;_Color;Color;2;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0.7490196,0.2666667,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;11;6.383172,-245.0697;Float;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ComponentMaskNode;22;-279.3196,382.635;Float;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;9;181.5036,5.450307;Float;True;Property;_Texture;Texture;0;0;Create;True;0;0;False;0;None;aa35a46007238e645b6311d39ea9d07b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;21;-274.949,479.0378;Float;False;False;False;False;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;505.4608,-136.7627;Float;False;2;2;0;FLOAT3;0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;19;-9.701433,390.7425;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;13;500.3186,103.6867;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;14;701.5319,-103.2948;Float;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;15;188.4471,358.8426;Float;True;Property;_Falloff_Texture;Falloff_Texture;1;0;Create;True;0;0;False;0;None;58e351c3d21634c4bb7f7efadc10c9b9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;987.6507,178.8948;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;28;1102.582,24.37341;Float;False;Property;_Luminosity;Luminosity;3;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;1308.01,77.56487;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1495.185,204.2899;Float;False;True;2;Float;ASEMaterialInspector;0;1;Amplify/LightDecal;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;1;2;False;-1;1;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;False;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;3;0;24;0
WireConnection;3;1;2;0
WireConnection;4;0;3;0
WireConnection;17;0;23;0
WireConnection;17;1;20;0
WireConnection;5;0;4;0
WireConnection;8;0;4;0
WireConnection;7;0;5;0
WireConnection;7;1;8;0
WireConnection;18;0;17;0
WireConnection;11;0;10;0
WireConnection;22;0;18;0
WireConnection;9;1;7;0
WireConnection;21;0;18;0
WireConnection;12;0;11;0
WireConnection;12;1;9;0
WireConnection;19;0;22;0
WireConnection;19;1;21;0
WireConnection;13;0;9;4
WireConnection;14;0;12;0
WireConnection;14;3;13;0
WireConnection;15;1;19;0
WireConnection;25;0;14;0
WireConnection;25;1;15;4
WireConnection;27;0;25;0
WireConnection;27;1;28;0
WireConnection;0;0;27;0
ASEEND*/
//CHKSM=28296BC7390AA16447C99088D11283E4D74A6D5D