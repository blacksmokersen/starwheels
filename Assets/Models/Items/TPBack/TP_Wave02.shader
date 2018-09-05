// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32974,y:32750,varname:node_3138,prsc:2|emission-8305-OUT,alpha-8938-OUT,voffset-388-OUT;n:type:ShaderForge.SFN_Tex2d,id:7683,x:32117,y:32807,varname:node_8582,prsc:2,tex:ab9a196429174694aa19bacb5c1840dc,ntxv:0,isnm:False|TEX-3176-TEX;n:type:ShaderForge.SFN_TexCoord,id:3109,x:31166,y:33191,varname:node_3109,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:3703,x:31994,y:33246,varname:node_3703,prsc:2|A-2525-OUT,B-9176-OUT,C-9529-OUT;n:type:ShaderForge.SFN_Vector1,id:2525,x:31775,y:33130,varname:node_2525,prsc:2,v1:8;n:type:ShaderForge.SFN_Tau,id:9529,x:31875,y:33314,varname:node_9529,prsc:2;n:type:ShaderForge.SFN_Sin,id:8351,x:32175,y:33236,varname:node_8351,prsc:2|IN-3703-OUT;n:type:ShaderForge.SFN_RemapRange,id:4686,x:32370,y:33218,varname:node_4686,prsc:2,frmn:-1,frmx:1,tomn:0,tomx:1|IN-8351-OUT;n:type:ShaderForge.SFN_Clamp01,id:2946,x:32544,y:33218,varname:node_2946,prsc:2|IN-4686-OUT;n:type:ShaderForge.SFN_ComponentMask,id:5981,x:31418,y:33223,varname:node_5981,prsc:2,cc1:1,cc2:-1,cc3:-1,cc4:-1|IN-3109-UVOUT;n:type:ShaderForge.SFN_Add,id:9176,x:31773,y:33421,varname:node_9176,prsc:2|A-5461-OUT,B-4728-OUT;n:type:ShaderForge.SFN_Slider,id:8142,x:31314,y:33631,ptovrint:False,ptlb:node_7827,ptin:_node_7827,varname:node_7827,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Time,id:7434,x:31488,y:33745,varname:node_7434,prsc:2;n:type:ShaderForge.SFN_OneMinus,id:5461,x:31590,y:33051,varname:node_5461,prsc:2|IN-5981-OUT;n:type:ShaderForge.SFN_Vector1,id:4117,x:31611,y:33927,varname:node_4117,prsc:2,v1:5;n:type:ShaderForge.SFN_Multiply,id:4728,x:31736,y:33681,varname:node_4728,prsc:2|A-7434-TSL,B-4117-OUT;n:type:ShaderForge.SFN_Multiply,id:1943,x:32762,y:33412,varname:node_1943,prsc:2|A-2946-OUT,B-4561-OUT,C-6698-OUT;n:type:ShaderForge.SFN_NormalVector,id:4561,x:32326,y:33472,prsc:2,pt:False;n:type:ShaderForge.SFN_Vector1,id:6698,x:32528,y:33689,varname:node_6698,prsc:2,v1:0.02;n:type:ShaderForge.SFN_Clamp01,id:388,x:32950,y:33412,varname:node_388,prsc:2|IN-1943-OUT;n:type:ShaderForge.SFN_Blend,id:9246,x:32486,y:32999,varname:node_9246,prsc:2,blmd:1,clmp:True|SRC-3902-A,DST-2946-OUT;n:type:ShaderForge.SFN_Tex2d,id:3902,x:32192,y:32972,varname:node_9538,prsc:2,tex:ab9a196429174694aa19bacb5c1840dc,ntxv:0,isnm:False|TEX-3176-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:3176,x:31935,y:32890,ptovrint:False,ptlb:node_7632,ptin:_node_7632,varname:node_7632,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ab9a196429174694aa19bacb5c1840dc,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Color,id:5165,x:32284,y:32502,ptovrint:False,ptlb:node_1350,ptin:_node_1350,varname:node_1350,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:1,c3:0.9361701,c4:1;n:type:ShaderForge.SFN_Vector1,id:1704,x:32384,y:32881,varname:node_1704,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:8305,x:32621,y:32714,varname:node_8305,prsc:2|A-5165-RGB,B-7683-RGB,C-7683-RGB,D-1704-OUT;n:type:ShaderForge.SFN_Clamp01,id:8938,x:32728,y:33016,varname:node_8938,prsc:2|IN-9246-OUT;n:type:ShaderForge.SFN_Vector1,id:5312,x:32715,y:33218,varname:node_5312,prsc:2,v1:1;proporder:3176-5165;pass:END;sub:END;*/

Shader "Shader Forge/TP_Wave02" {
    Properties {
        _node_7632 ("node_7632", 2D) = "white" {}
        _node_1350 ("node_1350", Color) = (0,1,0.9361701,1)
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
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _node_7632; uniform float4 _node_7632_ST;
            uniform float4 _node_1350;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_7434 = _Time;
                float node_2946 = saturate((sin((8.0*((1.0 - o.uv0.g)+(node_7434.r*5.0))*6.28318530718))*0.5+0.5));
                v.vertex.xyz += saturate((node_2946*v.normal*0.02));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_8582 = tex2D(_node_7632,TRANSFORM_TEX(i.uv0, _node_7632));
                float3 emissive = (_node_1350.rgb*node_8582.rgb*node_8582.rgb*1.5);
                float3 finalColor = emissive;
                float4 node_9538 = tex2D(_node_7632,TRANSFORM_TEX(i.uv0, _node_7632));
                float4 node_7434 = _Time;
                float node_2946 = saturate((sin((8.0*((1.0 - i.uv0.g)+(node_7434.r*5.0))*6.28318530718))*0.5+0.5));
                return fixed4(finalColor,saturate(saturate((node_9538.a*node_2946))));
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float4 node_7434 = _Time;
                float node_2946 = saturate((sin((8.0*((1.0 - o.uv0.g)+(node_7434.r*5.0))*6.28318530718))*0.5+0.5));
                v.vertex.xyz += saturate((node_2946*v.normal*0.02));
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
