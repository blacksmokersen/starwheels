// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:34004,y:32624,varname:node_4795,prsc:2|emission-2218-OUT;n:type:ShaderForge.SFN_Tex2d,id:617,x:31977,y:31736,varname:node_1026,prsc:2,ntxv:0,isnm:False|UVIN-8269-UVOUT,TEX-3749-TEX;n:type:ShaderForge.SFN_TexCoord,id:249,x:29358,y:31722,varname:node_249,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:8673,x:29632,y:31588,varname:node_8673,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-249-UVOUT;n:type:ShaderForge.SFN_ArcTan2,id:4965,x:30101,y:31682,varname:node_4965,prsc:2,attp:2|A-3909-R,B-3909-G;n:type:ShaderForge.SFN_ComponentMask,id:3909,x:29887,y:31659,varname:node_3909,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-8673-OUT;n:type:ShaderForge.SFN_Length,id:42,x:32053,y:31459,varname:node_42,prsc:2|IN-8673-OUT;n:type:ShaderForge.SFN_Append,id:3879,x:30338,y:31707,varname:node_3879,prsc:2|A-6887-OUT,B-4965-OUT;n:type:ShaderForge.SFN_OneMinus,id:6887,x:30084,y:31834,varname:node_6887,prsc:2|IN-5575-OUT;n:type:ShaderForge.SFN_Distance,id:5575,x:29900,y:31819,varname:node_5575,prsc:2|A-249-UVOUT,B-6434-OUT;n:type:ShaderForge.SFN_Vector2,id:6434,x:29711,y:31858,varname:node_6434,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_Panner,id:8269,x:31434,y:31696,varname:node_8269,prsc:2,spu:0.5,spv:0.1|UVIN-3879-OUT,DIST-794-OUT;n:type:ShaderForge.SFN_OneMinus,id:5384,x:32212,y:31459,varname:node_5384,prsc:2|IN-42-OUT;n:type:ShaderForge.SFN_Multiply,id:7867,x:32401,y:31583,varname:node_7867,prsc:2|A-5384-OUT,B-1787-OUT;n:type:ShaderForge.SFN_Vector1,id:1787,x:32212,y:31595,varname:node_1787,prsc:2,v1:3;n:type:ShaderForge.SFN_Clamp01,id:2004,x:32597,y:31595,varname:node_2004,prsc:2|IN-7867-OUT;n:type:ShaderForge.SFN_Multiply,id:2218,x:33799,y:32728,varname:node_2218,prsc:2|A-3684-OUT,B-1059-RGB,C-3499-OUT,D-4877-RGB,E-8069-OUT;n:type:ShaderForge.SFN_Color,id:1059,x:33446,y:32691,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1059,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:3499,x:33446,y:32843,varname:node_3499,prsc:2,v1:2;n:type:ShaderForge.SFN_VertexColor,id:4877,x:33446,y:32909,varname:node_4877,prsc:2;n:type:ShaderForge.SFN_Time,id:4358,x:30576,y:31785,varname:node_4358,prsc:2;n:type:ShaderForge.SFN_Multiply,id:794,x:30864,y:31812,varname:node_794,prsc:2|A-4358-T,B-1134-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1134,x:30688,y:31988,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_1134,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:4410,x:31977,y:31871,varname:node_4410,prsc:2,ntxv:0,isnm:False|UVIN-2877-UVOUT,TEX-3749-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:3749,x:31697,y:32394,ptovrint:False,ptlb:Plasma,ptin:_Plasma,varname:node_3749,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Rotator,id:2877,x:31434,y:31882,varname:node_2877,prsc:2|UVIN-8324-UVOUT,SPD-1134-OUT;n:type:ShaderForge.SFN_TexCoord,id:8324,x:31197,y:31862,varname:node_8324,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:3684,x:33490,y:32505,varname:node_3684,prsc:2|A-6680-OUT,B-4848-OUT;n:type:ShaderForge.SFN_Multiply,id:8402,x:32361,y:32107,varname:node_8402,prsc:2|A-4410-G,B-4478-G,C-1107-OUT;n:type:ShaderForge.SFN_Rotator,id:946,x:31434,y:32037,varname:node_946,prsc:2|UVIN-8324-UVOUT,SPD-1797-OUT;n:type:ShaderForge.SFN_Multiply,id:1797,x:31197,y:32114,varname:node_1797,prsc:2|A-1134-OUT,B-215-OUT;n:type:ShaderForge.SFN_Vector1,id:215,x:30991,y:32147,varname:node_215,prsc:2,v1:-0.5;n:type:ShaderForge.SFN_Tex2d,id:4478,x:31977,y:32036,varname:node_4478,prsc:2,ntxv:0,isnm:False|UVIN-946-UVOUT,TEX-3749-TEX;n:type:ShaderForge.SFN_Vector1,id:3285,x:32333,y:32001,varname:node_3285,prsc:2,v1:10;n:type:ShaderForge.SFN_Multiply,id:1891,x:32544,y:31976,varname:node_1891,prsc:2|A-3285-OUT,B-8402-OUT;n:type:ShaderForge.SFN_Clamp01,id:6877,x:32747,y:31989,varname:node_6877,prsc:2|IN-1891-OUT;n:type:ShaderForge.SFN_Vector1,id:6053,x:33091,y:32089,varname:node_6053,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Multiply,id:6680,x:33178,y:32203,varname:node_6680,prsc:2|A-6053-OUT,B-7355-OUT,C-6877-OUT;n:type:ShaderForge.SFN_Multiply,id:7355,x:32780,y:31624,varname:node_7355,prsc:2|A-2004-OUT,B-617-R;n:type:ShaderForge.SFN_Vector1,id:1107,x:32007,y:32210,varname:node_1107,prsc:2,v1:1;n:type:ShaderForge.SFN_Clamp01,id:4848,x:32804,y:32259,varname:node_4848,prsc:2|IN-8402-OUT;n:type:ShaderForge.SFN_Multiply,id:8069,x:33688,y:32882,varname:node_8069,prsc:2|A-1059-A,B-4877-A;proporder:1059-1134-3749;pass:END;sub:END;*/

Shader "TIAB/Item Projector/Center V2" {
    Properties {
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _Speed ("Speed", Float ) = 1
        _Plasma ("Plasma", 2D) = "white" {}
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
            uniform float4 _Color;
            uniform float _Speed;
            uniform sampler2D _Plasma; uniform float4 _Plasma_ST;
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
                float2 node_8673 = (i.uv0*2.0+-1.0);
                float4 node_4358 = _Time;
                float2 node_3909 = node_8673.rg;
                float2 node_8269 = (float2((1.0 - distance(i.uv0,float2(0.5,0.5))),((atan2(node_3909.r,node_3909.g)/6.28318530718)+0.5))+(node_4358.g*_Speed)*float2(0.5,0.1));
                float4 node_1026 = tex2D(_Plasma,TRANSFORM_TEX(node_8269, _Plasma));
                float4 node_3023 = _Time;
                float node_2877_ang = node_3023.g;
                float node_2877_spd = _Speed;
                float node_2877_cos = cos(node_2877_spd*node_2877_ang);
                float node_2877_sin = sin(node_2877_spd*node_2877_ang);
                float2 node_2877_piv = float2(0.5,0.5);
                float2 node_2877 = (mul(i.uv0-node_2877_piv,float2x2( node_2877_cos, -node_2877_sin, node_2877_sin, node_2877_cos))+node_2877_piv);
                float4 node_4410 = tex2D(_Plasma,TRANSFORM_TEX(node_2877, _Plasma));
                float node_946_ang = node_3023.g;
                float node_946_spd = (_Speed*(-0.5));
                float node_946_cos = cos(node_946_spd*node_946_ang);
                float node_946_sin = sin(node_946_spd*node_946_ang);
                float2 node_946_piv = float2(0.5,0.5);
                float2 node_946 = (mul(i.uv0-node_946_piv,float2x2( node_946_cos, -node_946_sin, node_946_sin, node_946_cos))+node_946_piv);
                float4 node_4478 = tex2D(_Plasma,TRANSFORM_TEX(node_946, _Plasma));
                float node_8402 = (node_4410.g*node_4478.g*1.0);
                float3 emissive = (((0.3*(saturate(((1.0 - length(node_8673))*3.0))*node_1026.r)*saturate((10.0*node_8402)))+saturate(node_8402))*_Color.rgb*2.0*i.vertexColor.rgb*(_Color.a*i.vertexColor.a));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5,0.5,0.5,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
