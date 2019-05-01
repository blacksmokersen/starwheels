// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33410,y:32654,varname:node_4795,prsc:2|emission-2393-OUT,alpha-8089-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32235,y:32599,varname:_MainTex,prsc:2,ntxv:0,isnm:False|TEX-4318-TEX;n:type:ShaderForge.SFN_Multiply,id:2393,x:32538,y:32794,varname:node_2393,prsc:2|A-2053-RGB,B-797-RGB,C-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32235,y:32772,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32235,y:32930,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32235,y:33081,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:798,x:32781,y:32929,varname:node_798,prsc:2|A-6074-A,B-2053-A,C-797-A,D-797-B,E-2193-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:4318,x:31983,y:33553,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_4318,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2629,x:32220,y:33310,varname:node_2629,prsc:2,ntxv:0,isnm:False|UVIN-9749-UVOUT,TEX-4318-TEX;n:type:ShaderForge.SFN_TexCoord,id:2140,x:31482,y:33111,varname:node_2140,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:4780,x:32220,y:33151,varname:node_4780,prsc:2,ntxv:0,isnm:False|UVIN-7854-UVOUT,TEX-4318-TEX;n:type:ShaderForge.SFN_Multiply,id:2045,x:31721,y:33051,varname:node_2045,prsc:2|A-766-OUT,B-2140-UVOUT;n:type:ShaderForge.SFN_Vector2,id:766,x:31482,y:33009,varname:node_766,prsc:2,v1:1,v2:1.5;n:type:ShaderForge.SFN_Panner,id:7854,x:31970,y:33051,varname:node_7854,prsc:2,spu:0,spv:1.1|UVIN-2045-OUT,DIST-4018-OUT;n:type:ShaderForge.SFN_Panner,id:9749,x:31946,y:33307,varname:node_9749,prsc:2,spu:0,spv:-0.9|UVIN-2140-UVOUT,DIST-4018-OUT;n:type:ShaderForge.SFN_Multiply,id:4018,x:31555,y:33333,varname:node_4018,prsc:2|A-5384-T,B-5621-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5621,x:31364,y:33515,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_5621,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Time,id:5384,x:31305,y:33302,varname:node_5384,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2193,x:32504,y:33159,varname:node_2193,prsc:2|A-4780-R,B-2629-R;n:type:ShaderForge.SFN_ValueProperty,id:1305,x:32504,y:33343,ptovrint:False,ptlb:Power,ptin:_Power,varname:node_1305,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:8089,x:32972,y:32941,varname:node_8089,prsc:2|A-798-OUT,B-1305-OUT;proporder:797-4318-5621-1305;pass:END;sub:END;*/

Shader "BSE/Item/Guile/Trail" {
    Properties {
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _Texture ("Texture", 2D) = "white" {}
        _Speed ("Speed", Float ) = 0.5
        _Power ("Power", Float ) = 1.5
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TintColor;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Speed;
            uniform float _Power;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float3 emissive = (i.vertexColor.rgb*_TintColor.rgb*2.0);
                float3 finalColor = emissive;
                float4 _MainTex = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float4 node_5384 = _Time;
                float node_4018 = (node_5384.g*_Speed);
                float2 node_7854 = ((float2(1,1.5)*i.uv0)+node_4018*float2(0,1.1));
                float4 node_4780 = tex2D(_Texture,TRANSFORM_TEX(node_7854, _Texture));
                float2 node_9749 = (i.uv0+node_4018*float2(0,-0.9));
                float4 node_2629 = tex2D(_Texture,TRANSFORM_TEX(node_9749, _Texture));
                fixed4 finalRGBA = fixed4(finalColor,((_MainTex.a*i.vertexColor.a*_TintColor.a*_TintColor.b*(node_4780.r*node_2629.r))*_Power));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
