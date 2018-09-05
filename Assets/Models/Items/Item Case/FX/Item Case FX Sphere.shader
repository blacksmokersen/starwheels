// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33071,y:32592,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32736,y:32830,varname:node_2393,prsc:2|A-1605-OUT,B-2053-RGB,C-797-RGB,D-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:31754,y:32716,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:31582,y:32842,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32089,y:32926,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Fresnel,id:2310,x:31719,y:32127,varname:node_2310,prsc:2|NRM-6663-OUT,EXP-388-OUT;n:type:ShaderForge.SFN_NormalVector,id:6663,x:31454,y:31998,prsc:2,pt:False;n:type:ShaderForge.SFN_ValueProperty,id:388,x:31454,y:32197,ptovrint:False,ptlb:Fresnel power,ptin:_Fresnelpower,varname:_Fresnelpower,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_FragmentPosition,id:8224,x:29456,y:32967,varname:node_8224,prsc:2;n:type:ShaderForge.SFN_Append,id:536,x:29680,y:32860,varname:node_536,prsc:2|A-8224-Y,B-8224-Z;n:type:ShaderForge.SFN_Append,id:9127,x:29680,y:32989,varname:node_9127,prsc:2|A-8224-Z,B-8224-X;n:type:ShaderForge.SFN_Append,id:5312,x:29680,y:33127,varname:node_5312,prsc:2|A-8224-X,B-8224-Y;n:type:ShaderForge.SFN_NormalVector,id:613,x:29502,y:32503,prsc:2,pt:False;n:type:ShaderForge.SFN_Abs,id:2269,x:29682,y:32503,varname:node_2269,prsc:2|IN-613-OUT;n:type:ShaderForge.SFN_Multiply,id:1149,x:29876,y:32503,varname:node_1149,prsc:2|A-2269-OUT,B-2269-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2875,x:30280,y:32637,varname:node_2875,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-1149-OUT;n:type:ShaderForge.SFN_ChannelBlend,id:6181,x:30688,y:32661,varname:node_6181,prsc:2,chbt:0|M-2875-OUT,R-3401-RGB,G-514-RGB,B-3160-RGB;n:type:ShaderForge.SFN_Tex2dAsset,id:5413,x:30045,y:33296,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:_Texture,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3401,x:30307,y:32875,varname:node_3401,prsc:2,ntxv:0,isnm:False|UVIN-6427-UVOUT,TEX-5413-TEX;n:type:ShaderForge.SFN_Tex2d,id:514,x:30326,y:33040,varname:node_514,prsc:2,ntxv:0,isnm:False|UVIN-9526-UVOUT,TEX-5413-TEX;n:type:ShaderForge.SFN_Tex2d,id:3160,x:30326,y:33196,varname:node_3160,prsc:2,ntxv:0,isnm:False|UVIN-9617-UVOUT,TEX-5413-TEX;n:type:ShaderForge.SFN_Add,id:19,x:32072,y:32479,varname:node_19,prsc:2|A-763-OUT,B-6903-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8614,x:29631,y:33357,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:_Speed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Rotator,id:6427,x:30007,y:32839,varname:node_6427,prsc:2|UVIN-536-OUT,SPD-8614-OUT;n:type:ShaderForge.SFN_Rotator,id:9526,x:30024,y:33001,varname:node_9526,prsc:2|UVIN-9127-OUT,SPD-8614-OUT;n:type:ShaderForge.SFN_Rotator,id:9617,x:30024,y:33136,varname:node_9617,prsc:2|UVIN-5312-OUT,SPD-8614-OUT;n:type:ShaderForge.SFN_Multiply,id:6903,x:31648,y:32518,varname:node_6903,prsc:2|A-6272-OUT,B-3068-OUT,C-6181-OUT;n:type:ShaderForge.SFN_Fresnel,id:6272,x:31315,y:32340,varname:node_6272,prsc:2|NRM-5982-OUT,EXP-2389-OUT;n:type:ShaderForge.SFN_NormalVector,id:5982,x:31112,y:32252,prsc:2,pt:False;n:type:ShaderForge.SFN_Vector1,id:2389,x:31086,y:32429,varname:node_2389,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:5851,x:31928,y:32196,varname:node_5851,prsc:2|A-2310-OUT,B-1782-OUT;n:type:ShaderForge.SFN_Vector1,id:1782,x:31677,y:32295,varname:node_1782,prsc:2,v1:2;n:type:ShaderForge.SFN_Power,id:763,x:32112,y:32220,varname:node_763,prsc:2|VAL-5851-OUT,EXP-1460-OUT;n:type:ShaderForge.SFN_Vector1,id:1460,x:31838,y:32339,varname:node_1460,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:1605,x:32485,y:32670,varname:node_1605,prsc:2|A-763-OUT,B-19-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3068,x:31291,y:32515,ptovrint:False,ptlb:Center Power,ptin:_CenterPower,varname:_CenterPower,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;proporder:797-388-5413-8614-3068;pass:END;sub:END;*/

Shader "TIAB/Item Case/FX Sphere" {
    Properties {
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _Fresnelpower ("Fresnel power", Float ) = 1
        _Texture ("Texture", 2D) = "white" {}
        _Speed ("Speed", Float ) = 0.5
        _CenterPower ("Center Power", Float ) = 0.5
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
            Blend One One
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
            uniform float _Fresnelpower;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Speed;
            uniform float _CenterPower;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float node_763 = pow((pow(1.0-max(0,dot(i.normalDir, viewDirection)),_Fresnelpower)*2.0),1.0);
                float3 node_2269 = abs(i.normalDir);
                float3 node_2875 = (node_2269*node_2269).rgb;
                float4 node_8294 = _Time;
                float node_6427_ang = node_8294.g;
                float node_6427_spd = _Speed;
                float node_6427_cos = cos(node_6427_spd*node_6427_ang);
                float node_6427_sin = sin(node_6427_spd*node_6427_ang);
                float2 node_6427_piv = float2(0.5,0.5);
                float2 node_6427 = (mul(float2(i.posWorld.g,i.posWorld.b)-node_6427_piv,float2x2( node_6427_cos, -node_6427_sin, node_6427_sin, node_6427_cos))+node_6427_piv);
                float4 node_3401 = tex2D(_Texture,TRANSFORM_TEX(node_6427, _Texture));
                float node_9526_ang = node_8294.g;
                float node_9526_spd = _Speed;
                float node_9526_cos = cos(node_9526_spd*node_9526_ang);
                float node_9526_sin = sin(node_9526_spd*node_9526_ang);
                float2 node_9526_piv = float2(0.5,0.5);
                float2 node_9526 = (mul(float2(i.posWorld.b,i.posWorld.r)-node_9526_piv,float2x2( node_9526_cos, -node_9526_sin, node_9526_sin, node_9526_cos))+node_9526_piv);
                float4 node_514 = tex2D(_Texture,TRANSFORM_TEX(node_9526, _Texture));
                float node_9617_ang = node_8294.g;
                float node_9617_spd = _Speed;
                float node_9617_cos = cos(node_9617_spd*node_9617_ang);
                float node_9617_sin = sin(node_9617_spd*node_9617_ang);
                float2 node_9617_piv = float2(0.5,0.5);
                float2 node_9617 = (mul(float2(i.posWorld.r,i.posWorld.g)-node_9617_piv,float2x2( node_9617_cos, -node_9617_sin, node_9617_sin, node_9617_cos))+node_9617_piv);
                float4 node_3160 = tex2D(_Texture,TRANSFORM_TEX(node_9617, _Texture));
                float3 emissive = ((node_763+(node_763+(pow(1.0-max(0,dot(i.normalDir, viewDirection)),1.5)*_CenterPower*(node_2875.r*node_3401.rgb + node_2875.g*node_514.rgb + node_2875.b*node_3160.rgb))))*i.vertexColor.rgb*_TintColor.rgb*2.0);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
