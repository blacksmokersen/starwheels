// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33930,y:32775,varname:node_3138,prsc:2|emission-1924-RGB,alpha-2414-OUT;n:type:ShaderForge.SFN_Tex2d,id:1924,x:33366,y:32829,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_1924,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-1878-OUT;n:type:ShaderForge.SFN_Multiply,id:3608,x:33399,y:33130,varname:node_3608,prsc:2|A-6047-A,B-7567-OUT,C-7360-OUT;n:type:ShaderForge.SFN_Slider,id:7567,x:32787,y:33381,ptovrint:False,ptlb:Alpha,ptin:_Alpha,varname:node_7567,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_TexCoord,id:6401,x:32859,y:32553,varname:node_6401,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:3018,x:32095,y:32531,varname:node_3018,prsc:2,tex:35e41873efdb0624b84e03cd87a0b810,ntxv:0,isnm:False|UVIN-9916-UVOUT,TEX-6253-TEX;n:type:ShaderForge.SFN_Slider,id:7225,x:32579,y:32926,ptovrint:False,ptlb:node_7225,ptin:_node_7225,varname:node_7225,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:0.2;n:type:ShaderForge.SFN_ComponentMask,id:9450,x:32578,y:32566,varname:node_9450,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-188-OUT;n:type:ShaderForge.SFN_Panner,id:9916,x:31693,y:32414,varname:node_9916,prsc:2,spu:1,spv:1|UVIN-7853-OUT,DIST-7962-OUT;n:type:ShaderForge.SFN_Time,id:6293,x:31307,y:32391,varname:node_6293,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7962,x:31529,y:32459,varname:node_7962,prsc:2|A-6293-TSL,B-1604-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1604,x:31350,y:32551,ptovrint:False,ptlb:node_1604,ptin:_node_1604,varname:node_1604,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_TexCoord,id:531,x:31180,y:32207,varname:node_531,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:2011,x:32912,y:32829,varname:node_2011,prsc:2|A-9450-OUT,B-7225-OUT;n:type:ShaderForge.SFN_Add,id:1878,x:33124,y:32688,varname:node_1878,prsc:2|A-6401-UVOUT,B-2011-OUT;n:type:ShaderForge.SFN_Multiply,id:4137,x:33168,y:32816,varname:node_4137,prsc:2|A-6401-UVOUT,B-2011-OUT;n:type:ShaderForge.SFN_Multiply,id:188,x:32379,y:32472,varname:node_188,prsc:2|A-7313-RGB,B-3018-RGB;n:type:ShaderForge.SFN_Tex2dAsset,id:6253,x:31815,y:32621,ptovrint:False,ptlb:node_6253,ptin:_node_6253,varname:node_6253,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:35e41873efdb0624b84e03cd87a0b810,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7313,x:32109,y:32326,varname:node_7313,prsc:2,tex:35e41873efdb0624b84e03cd87a0b810,ntxv:0,isnm:False|UVIN-9939-UVOUT,TEX-6253-TEX;n:type:ShaderForge.SFN_Panner,id:9939,x:31840,y:32219,varname:node_9939,prsc:2,spu:-1.1,spv:-0.9|UVIN-2721-OUT,DIST-7962-OUT;n:type:ShaderForge.SFN_Multiply,id:2721,x:31654,y:32106,varname:node_2721,prsc:2|A-7853-OUT,B-1716-OUT;n:type:ShaderForge.SFN_Vector1,id:1716,x:31492,y:32246,varname:node_1716,prsc:2,v1:1.1;n:type:ShaderForge.SFN_Tex2d,id:6047,x:32631,y:33145,varname:node_6047,prsc:2,tex:35e41873efdb0624b84e03cd87a0b810,ntxv:0,isnm:False|TEX-6253-TEX;n:type:ShaderForge.SFN_Multiply,id:7853,x:31417,y:32064,varname:node_7853,prsc:2|A-1608-OUT,B-531-UVOUT;n:type:ShaderForge.SFN_Vector1,id:1608,x:31197,y:32065,varname:node_1608,prsc:2,v1:3;n:type:ShaderForge.SFN_Tex2d,id:7407,x:32819,y:33028,varname:node_7407,prsc:2,tex:35e41873efdb0624b84e03cd87a0b810,ntxv:0,isnm:False|TEX-6253-TEX;n:type:ShaderForge.SFN_Add,id:7360,x:33252,y:33026,varname:node_7360,prsc:2|A-8409-OUT,B-7407-B;n:type:ShaderForge.SFN_Vector1,id:3744,x:32900,y:32958,varname:node_3744,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Clamp01,id:2414,x:33667,y:33099,varname:node_2414,prsc:2|IN-3608-OUT;n:type:ShaderForge.SFN_Add,id:8409,x:33100,y:32940,varname:node_8409,prsc:2|A-3744-OUT,B-7567-OUT;proporder:1924-7567-7225-1604-6253;pass:END;sub:END;*/

Shader "BSE/Item/Tp Back/Portal" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        _Alpha ("Alpha", Range(0, 1)) = 0
        _node_7225 ("node_7225", Range(0, 0.2)) = 0
        _node_1604 ("node_1604", Float ) = 0.1
        _node_6253 ("node_6253", 2D) = "white" {}
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
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Alpha;
            uniform float _node_7225;
            uniform float _node_1604;
            uniform sampler2D _node_6253; uniform float4 _node_6253_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_6293 = _Time;
                float node_7962 = (node_6293.r*_node_1604);
                float2 node_7853 = (3.0*i.uv0);
                float2 node_9939 = ((node_7853*1.1)+node_7962*float2(-1.1,-0.9));
                float4 node_7313 = tex2D(_node_6253,TRANSFORM_TEX(node_9939, _node_6253));
                float2 node_9916 = (node_7853+node_7962*float2(1,1));
                float4 node_3018 = tex2D(_node_6253,TRANSFORM_TEX(node_9916, _node_6253));
                float2 node_2011 = ((node_7313.rgb*node_3018.rgb).rg*_node_7225);
                float2 node_1878 = (i.uv0+node_2011);
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_1878, _Texture));
                float3 emissive = _Texture_var.rgb;
                float3 finalColor = emissive;
                float4 node_6047 = tex2D(_node_6253,TRANSFORM_TEX(i.uv0, _node_6253));
                float4 node_7407 = tex2D(_node_6253,TRANSFORM_TEX(i.uv0, _node_6253));
                return fixed4(finalColor,saturate((node_6047.a*_Alpha*((0.5+_Alpha)+node_7407.b))));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
