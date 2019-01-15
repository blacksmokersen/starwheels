Shader "BSE/Item/AionBeam/BurnedGround"
{
	Properties
	{
		_Color("Color", Color) = (0,0,0,0)
		[HDR]_BurnedColor("Burned Color", Color) = (0,0,0,0)
		_Opacity("Opacity", Range( 0 , 1)) = 0
		_BurnedTexture("Burned Texture", 2D) = "white" {}
		_FalloffTexture("Falloff Texture", 2D) = "white" {}
		_BurnedDispertion("Burned Dispertion", Range( 0 , 10)) = 0
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100
		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Back
		ColorMask RGB
		ZWrite Off
		ZTest LEqual
		Offset -1 , -1
		
		

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
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
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
			};

			uniform float4 _Color;
			uniform float4 _BurnedColor;
			uniform float _BurnedDispertion;
			uniform sampler2D _BurnedTexture;
			float4x4 unity_Projector;
			uniform sampler2D _FalloffTexture;
			float4x4 unity_ProjectorClip;
			uniform float _Opacity;
			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				float4 vertexToFrag11 = mul( unity_Projector, v.vertex );
				o.ase_texcoord = vertexToFrag11;
				float4 vertexToFrag15 = mul( unity_ProjectorClip, v.vertex );
				o.ase_texcoord1 = vertexToFrag15;
				
				
				v.vertex.xyz +=  float3(0,0,0) ;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				fixed4 finalColor;
				float4 vertexToFrag11 = i.ase_texcoord;
				float4 tex2DNode18 = tex2D( _BurnedTexture, ( (vertexToFrag11).xy / (vertexToFrag11).w ) );
				float smoothstepResult71 = smoothstep( (-1.0 + (_BurnedDispertion - 0.0) * (1.0 - -1.0) / (10.0 - 0.0)) , 1.0 , tex2DNode18.r);
				float4 lerpResult94 = lerp( _Color , ( _BurnedColor * ( smoothstepResult71 + pow( tex2DNode18.a , (0.0 + (_BurnedDispertion - 0.0) * (200.0 - 0.0) / (10.0 - 0.0)) ) ) * (1.0 + (_BurnedDispertion - 0.0) * (0.0 - 1.0) / (10.0 - 0.0)) ) , ( tex2DNode18.a * ( 1.0 - _Color.a ) ));
				float4 appendResult58 = (float4(lerpResult94.rgb , tex2DNode18.a));
				float4 vertexToFrag15 = i.ase_texcoord1;
				
				
				finalColor = ( appendResult58 * tex2D( _FalloffTexture, ( (vertexToFrag15).xy / (vertexToFrag15).w ) ).a * _Opacity );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}