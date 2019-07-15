// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Item/Mine/Ground"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (1,1,1,1)
		_GroundFade("Ground Fade", Float) = 1
	}
	
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend One One , One One
		AlphaToMask On
		Cull Off
		ColorMask RGBA
		ZWrite Off
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
				
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_OUTPUT_STEREO
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			uniform float4 _Color;
			uniform sampler2D _CameraDepthTexture;
			uniform float _GroundFade;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord = screenPos;
				
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float4 screenPos = i.ase_texcoord;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float screenDepth2 = LinearEyeDepth(UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture,UNITY_PROJ_COORD( screenPos ))));
				float distanceDepth2 = abs( ( screenDepth2 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _GroundFade ) );
				float clampResult4 = clamp( ( 1.0 - distanceDepth2 ) , 0.0 , 1.0 );
				
				
				finalColor = ( _Color.a * clampResult4 * _Color );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
2075;87;1593;965;898.4907;370.1019;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;1;-1143.245,151.7013;Float;False;Property;_GroundFade;Ground Fade;1;0;Create;True;0;0;False;0;1;0.14;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;2;-927.6975,164.0321;Float;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;3;-432.6022,-3.666116;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;5;-536.7683,-334.2849;Float;False;Property;_Color;Color;0;1;[HDR];Create;True;0;0;False;0;1,1,1,1;2,0,1.780392,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;4;-226.6022,-2.666116;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;31.50928,-99.10193;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;8;509,-239;Float;False;True;2;Float;ASEMaterialInspector;0;1;Item/Mine/Ground;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;4;1;False;-1;1;False;-1;4;1;False;-1;1;False;-1;True;0;False;-1;0;False;-1;True;True;True;2;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Transparent=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;2;0;1;0
WireConnection;3;0;2;0
WireConnection;4;0;3;0
WireConnection;6;0;5;4
WireConnection;6;1;4;0
WireConnection;6;2;5;0
WireConnection;8;0;6;0
ASEEND*/
//CHKSM=A085051F8D4C1C204EB45CD93A8BA314E799D37E