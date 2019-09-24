// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Map/Security_Complex/SDC_hole"
{
	Properties
	{
		[HDR]_MainPanelColor("MainPanelColor", Color) = (1,1,1,1)
		[HDR]_WarningPanelColor("WarningPanelColor", Color) = (1,1,1,1)
		_MainPanelSpeed("MainPanelSpeed", Float) = 0.5
		_WarningPanelSpeed("WarningPanelSpeed", Float) = 2
		_MainPanelTexture("MainPanelTexture", 2D) = "white" {}
		_WarningPanelTexture("WarningPanelTexture", 2D) = "white" {}
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

			uniform sampler2D _MainPanelTexture;
			uniform float _MainPanelSpeed;
			uniform float4 _MainPanelTexture_ST;
			uniform float4 _MainPanelColor;
			uniform sampler2D _WarningPanelTexture;
			uniform float _WarningPanelSpeed;
			uniform float4 _WarningPanelTexture_ST;
			uniform float4 _WarningPanelColor;
			
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
			
			fixed4 frag (v2f i , half ase_vface : VFACE) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				fixed4 finalColor;
				float temp_output_29_0 = (0.1 + (ase_vface - -1.0) * (1.0 - 0.1) / (1.0 - -1.0));
				float2 uv_MainPanelTexture = i.ase_texcoord.xy * _MainPanelTexture_ST.xy + _MainPanelTexture_ST.zw;
				float2 panner13 = ( ( _Time.y * _MainPanelSpeed ) * float2( 0.5,0 ) + uv_MainPanelTexture);
				float2 uv_WarningPanelTexture = i.ase_texcoord.xy * _WarningPanelTexture_ST.xy + _WarningPanelTexture_ST.zw;
				float2 panner20 = ( ( _Time.y * _WarningPanelSpeed ) * float2( 0.5,0 ) + uv_WarningPanelTexture);
				
				
				finalColor = ( temp_output_29_0 * ( ( tex2D( _MainPanelTexture, panner13 ) * _MainPanelColor * _MainPanelColor.a ) + ( tex2D( _WarningPanelTexture, panner20 ) * _WarningPanelColor * _WarningPanelColor.a ) ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
0;529;1641;472;1296.515;197.6142;1.783807;True;True
Node;AmplifyShaderEditor.TexturePropertyNode;8;-2186.295,-350.1385;Float;True;Property;_MainPanelTexture;MainPanelTexture;4;0;Create;True;0;0;False;0;1cbe35fa148d6ef45965dd1f3a26a0c3;e4aae47326aee604cb684875f107434a;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TimeNode;9;-2214,-140;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;17;-2001.778,410.5537;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;18;-2019.745,588.9916;Float;False;Property;_WarningPanelSpeed;WarningPanelSpeed;3;0;Create;True;0;0;False;0;2;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-2188,19;Float;False;Property;_MainPanelSpeed;MainPanelSpeed;2;0;Create;True;0;0;False;0;0.5;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;15;-1943.195,181.3184;Float;True;Property;_WarningPanelTexture;WarningPanelTexture;5;0;Create;True;0;0;False;0;922dbfe5f0dcdcb4e937df7bdadbfac6;922dbfe5f0dcdcb4e937df7bdadbfac6;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-1805.403,-165.2877;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-1984,-34;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-1736.185,503.9917;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;19;-1498.675,249.2674;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;20;-1343.685,544.4916;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.5,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;13;-1507.339,3.694623;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0.5,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;7;-1209.893,-160.1729;Float;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;2;-759.4537,126.3973;Float;False;Property;_MainPanelColor;MainPanelColor;0;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0,3.609598,4,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;21;-1018.185,562.9736;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;23;-651.8741,711.2488;Float;False;Property;_WarningPanelColor;WarningPanelColor;1;1;[HDR];Create;True;0;0;False;0;1,1,1,1;0,3.609598,4,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;-339.9357,564.8274;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FaceVariableNode;25;-174.4247,9.559372;Float;False;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1;-447.5152,-20.02424;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;29;93.07065,-4.963057;Float;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0.1;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-64.94678,341.2265;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;633.5641,267.9594;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;27;273.2351,157.3634;Float;False;Constant;_Float0;Float 0;6;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;465.8862,82.44346;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;887.0468,293.3604;Float;False;True;2;Float;ASEMaterialInspector;0;1;Map/Security_Complex/SDC_hole;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;4;1;False;-1;1;False;-1;4;1;False;-1;1;False;-1;True;0;False;-1;0;False;-1;True;False;True;2;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;14;2;8;0
WireConnection;11;0;9;2
WireConnection;11;1;10;0
WireConnection;16;0;17;2
WireConnection;16;1;18;0
WireConnection;19;2;15;0
WireConnection;20;0;19;0
WireConnection;20;1;16;0
WireConnection;13;0;14;0
WireConnection;13;1;11;0
WireConnection;7;0;8;0
WireConnection;7;1;13;0
WireConnection;21;0;15;0
WireConnection;21;1;20;0
WireConnection;22;0;21;0
WireConnection;22;1;23;0
WireConnection;22;2;23;4
WireConnection;1;0;7;0
WireConnection;1;1;2;0
WireConnection;1;2;2;4
WireConnection;29;0;25;0
WireConnection;24;0;1;0
WireConnection;24;1;22;0
WireConnection;28;0;29;0
WireConnection;28;1;24;0
WireConnection;26;0;29;0
WireConnection;26;1;27;0
WireConnection;0;0;28;0
ASEEND*/
//CHKSM=1A97A932CBD17032F8C8EADD0EBE84684265FC5B