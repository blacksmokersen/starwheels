// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Map/Rubber District/Tp Back/Cylinder"
{
	Properties
	{
		[HDR]_Color("Color", Color) = (1,1,1,1)
		_Speed("Speed", Float) = 0.5
		_Alpha("Alpha", 2D) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Transparent" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend One One
		Cull Back
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

			uniform sampler2D _Alpha;
			uniform float _Speed;
			uniform float4 _Alpha_ST;
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
				float temp_output_11_0 = ( _Time.y * _Speed );
				float2 uv_Alpha = i.ase_texcoord.xy * _Alpha_ST.xy + _Alpha_ST.zw;
				float2 panner12 = ( temp_output_11_0 * float2( 0,1 ) + uv_Alpha);
				float2 uv15 = i.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float clampResult21 = clamp( pow( uv15.y , 10.0 ) , 0.0 , 1.0 );
				float2 panner13 = ( temp_output_11_0 * float2( 0,-1 ) + uv_Alpha);
				float clampResult22 = clamp( pow( ( 1.0 - uv15.y ) , 10.0 ) , 0.0 , 1.0 );
				
				
				finalColor = ( ( ( tex2D( _Alpha, panner12 ) * clampResult21 ) + ( tex2D( _Alpha, panner13 ) * clampResult22 ) ) * _Color * _Color.a );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
0;568;1536;433;3393.825;493.0578;3.02075;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;15;-2376.277,299.6711;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TimeNode;9;-2214,-140;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-2188,19;Float;False;Property;_Speed;Speed;1;0;Create;True;0;0;False;0;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-1988,-375;Float;False;0;8;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;25;-1897.277,258.6711;Float;False;Constant;_Float0;Float 0;3;0;Create;True;0;0;False;0;10;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;16;-2076.277,429.6711;Float;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-1984,-34;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;13;-1585,39;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,-1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;12;-1576,-226;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,1;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TexturePropertyNode;8;-1654,-422;Float;True;Property;_Alpha;Alpha;2;0;Create;True;0;0;False;0;6c22261054f277c4cbe4acd52ce69348;6c22261054f277c4cbe4acd52ce69348;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.PowerNode;24;-1760.277,419.6711;Float;False;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;23;-1690.154,244.2218;Float;False;2;0;FLOAT;0;False;1;FLOAT;3;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;6;-1245,-235;Float;True;Property;_TextureSample0;Texture Sample 0;1;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;22;-1462.277,417.6711;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;7;-1266,39;Float;True;Property;_TextureSample1;Texture Sample 1;2;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;21;-1450.277,212.6711;Float;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-877,54;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;4;-854,-89;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;3;-608.9408,-26.90912;Float;True;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;2;-660.8232,222.1763;Float;False;Property;_Color;Color;0;1;[HDR];Create;True;0;0;False;0;1,1,1,1;16,2.321569,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1;-172.8071,-59.69312;Float;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;27;156.1409,-32.70452;Float;False;True;2;Float;ASEMaterialInspector;0;1;Map/Rubber District/Tp Back/Cylinder;0770190933193b94aaa3065e307002fa;0;0;Unlit;2;True;4;1;False;-1;1;False;-1;0;5;False;-1;10;False;-1;True;0;False;-1;0;False;-1;True;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Transparent=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;0;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;16;0;15;2
WireConnection;11;0;9;2
WireConnection;11;1;10;0
WireConnection;13;0;14;0
WireConnection;13;1;11;0
WireConnection;12;0;14;0
WireConnection;12;1;11;0
WireConnection;24;0;16;0
WireConnection;24;1;25;0
WireConnection;23;0;15;2
WireConnection;23;1;25;0
WireConnection;6;0;8;0
WireConnection;6;1;12;0
WireConnection;22;0;24;0
WireConnection;7;0;8;0
WireConnection;7;1;13;0
WireConnection;21;0;23;0
WireConnection;5;0;7;0
WireConnection;5;1;22;0
WireConnection;4;0;6;0
WireConnection;4;1;21;0
WireConnection;3;0;4;0
WireConnection;3;1;5;0
WireConnection;1;0;3;0
WireConnection;1;1;2;0
WireConnection;1;2;2;4
WireConnection;27;0;1;0
ASEEND*/
//CHKSM=5E623471E8446BEE1336973BB29138887849BD69