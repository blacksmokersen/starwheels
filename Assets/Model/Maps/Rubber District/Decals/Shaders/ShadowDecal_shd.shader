// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify/ShadowDecal"
{
	Properties
	{
		_Shadow_Texture("Shadow_Texture", 2D) = "white" {}
		_ShadowColor("ShadowColor", Color) = (0,0,0,0)
		_Falloff_Texture("Falloff_Texture", 2D) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend DstColor Zero
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
				
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform float4 _ShadowColor;
			uniform sampler2D _Shadow_Texture;
			float4x4 unity_Projector;
			uniform sampler2D _Falloff_Texture;
			float4x4 unity_ProjectorClip;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 vertexToFrag4 = mul( unity_Projector, v.vertex );
				o.ase_texcoord = vertexToFrag4;
				float4 vertexToFrag11 = mul( unity_ProjectorClip, v.vertex );
				o.ase_texcoord1 = vertexToFrag11;
				
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float4 vertexToFrag4 = i.ase_texcoord;
				float temp_output_16_0 = ( 1.0 - tex2D( _Shadow_Texture, ( (vertexToFrag4).xy / (vertexToFrag4).w ) ).a );
				float4 appendResult18 = (float4(( _ShadowColor * temp_output_16_0 ).rgb , temp_output_16_0));
				float4 vertexToFrag11 = i.ase_texcoord1;
				float4 lerpResult19 = lerp( float4(1,1,1,0) , appendResult18 , tex2D( _Falloff_Texture, ( (vertexToFrag11).xy / (vertexToFrag11).w ) ).a);
				
				
				finalColor = lerpResult19;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
0;613;1582;388;129.1861;385.558;1.3;True;True
Node;AmplifyShaderEditor.UnityProjectorMatrixNode;1;-895.609,-297.3927;Float;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.PosVertexDataNode;2;-897.54,-182.2164;Float;False;1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;-504.4547,-233.477;Float;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.VertexToFragmentNode;4;-333.6326,-232.2423;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.UnityProjectorClipMatrixNode;5;-920.907,65.6899;Float;False;0;1;FLOAT4x4;0
Node;AmplifyShaderEditor.PosVertexDataNode;6;-923.436,178.0667;Float;False;1;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;7;-74.26201,-128.8395;Float;False;False;False;False;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;8;-78.63261,-225.2423;Float;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;9;-530.3508,126.8061;Float;False;2;2;0;FLOAT4x4;0,0,0,0,0,1,0,0,0,0,1,0,0,0,0,1;False;1;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;10;190.9856,-217.1348;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.VertexToFragmentNode;11;-359.5287,128.0408;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;12;356.2946,-242.1439;Float;True;Property;_Shadow_Texture;Shadow_Texture;0;0;Create;True;0;0;False;0;None;405582584194fdb4985a97eb3f42b013;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ComponentMaskNode;13;-100.158,231.4436;Float;False;False;False;False;True;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;14;-104.5286,135.0408;Float;False;True;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;21;383.1956,-434.3212;Float;False;Property;_ShadowColor;ShadowColor;1;0;Create;True;0;0;False;0;0,0,0,0;1,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;16;687.2948,-192.0279;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;859.1902,-384.6309;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;15;165.0896,143.1483;Float;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;17;363.2381,111.2484;Float;True;Property;_Falloff_Texture;Falloff_Texture;2;0;Create;True;0;0;False;0;None;58e351c3d21634c4bb7f7efadc10c9b9;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;20;952.373,-57.23383;Float;False;Constant;_Vector0;Vector 0;2;0;Create;True;0;0;False;0;1,1,1,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;18;1087.478,-235.4163;Float;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.LerpOp;19;1283.413,-38.37466;Float;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1444.901,-33.22387;Float;False;True;2;Float;ASEMaterialInspector;0;1;Amplify/ShadowDecal;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;6;2;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;3;0;1;0
WireConnection;3;1;2;0
WireConnection;4;0;3;0
WireConnection;7;0;4;0
WireConnection;8;0;4;0
WireConnection;9;0;5;0
WireConnection;9;1;6;0
WireConnection;10;0;8;0
WireConnection;10;1;7;0
WireConnection;11;0;9;0
WireConnection;12;1;10;0
WireConnection;13;0;11;0
WireConnection;14;0;11;0
WireConnection;16;0;12;4
WireConnection;22;0;21;0
WireConnection;22;1;16;0
WireConnection;15;0;14;0
WireConnection;15;1;13;0
WireConnection;17;1;15;0
WireConnection;18;0;22;0
WireConnection;18;3;16;0
WireConnection;19;0;20;0
WireConnection;19;1;18;0
WireConnection;19;2;17;4
WireConnection;0;0;19;0
ASEEND*/
//CHKSM=F4CC73D867CA4313B58FA79EE5CAE2EC5B8B16BD