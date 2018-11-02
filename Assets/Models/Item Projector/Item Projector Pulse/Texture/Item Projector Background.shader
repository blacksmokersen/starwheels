// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33923,y:32629,varname:node_4795,prsc:2|emission-2218-OUT;n:type:ShaderForge.SFN_TexCoord,id:249,x:30820,y:32556,varname:node_249,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:8673,x:31094,y:32422,varname:node_8673,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-249-UVOUT;n:type:ShaderForge.SFN_ArcTan2,id:4965,x:31662,y:32487,varname:node_4965,prsc:2,attp:2|A-3909-R,B-3909-G;n:type:ShaderForge.SFN_ComponentMask,id:3909,x:31448,y:32464,varname:node_3909,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-8673-OUT;n:type:ShaderForge.SFN_Append,id:3879,x:31902,y:32512,varname:node_3879,prsc:2|A-6887-OUT,B-4965-OUT;n:type:ShaderForge.SFN_OneMinus,id:6887,x:31645,y:32639,varname:node_6887,prsc:2|IN-5575-OUT;n:type:ShaderForge.SFN_Distance,id:5575,x:31461,y:32624,varname:node_5575,prsc:2|A-249-UVOUT,B-6434-OUT;n:type:ShaderForge.SFN_Vector2,id:6434,x:31272,y:32663,varname:node_6434,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_Multiply,id:2218,x:33713,y:32701,varname:node_2218,prsc:2|A-4892-OUT,B-1059-RGB,C-3499-OUT,D-4877-RGB;n:type:ShaderForge.SFN_Color,id:1059,x:33433,y:32695,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_1059,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:3499,x:33446,y:32843,varname:node_3499,prsc:2,v1:2;n:type:ShaderForge.SFN_VertexColor,id:4877,x:33446,y:32909,varname:node_4877,prsc:2;n:type:ShaderForge.SFN_Time,id:4358,x:31917,y:32802,varname:node_4358,prsc:2;n:type:ShaderForge.SFN_Multiply,id:794,x:32100,y:32780,varname:node_794,prsc:2|A-1134-OUT,B-4358-T;n:type:ShaderForge.SFN_ValueProperty,id:1134,x:31917,y:32724,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_1134,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2d,id:5866,x:32752,y:32499,varname:node_5866,prsc:2,ntxv:0,isnm:False|UVIN-6374-UVOUT,TEX-4820-TEX;n:type:ShaderForge.SFN_Panner,id:6374,x:32378,y:32508,varname:node_6374,prsc:2,spu:0.3,spv:0|UVIN-3879-OUT,DIST-794-OUT;n:type:ShaderForge.SFN_Panner,id:3598,x:32397,y:32677,varname:node_3598,prsc:2,spu:0.5,spv:0|UVIN-3879-OUT,DIST-794-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:4820,x:32431,y:32875,ptovrint:False,ptlb:BackGround,ptin:_BackGround,varname:node_4820,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:4587,x:33018,y:32588,varname:node_4587,prsc:2|A-5866-R,B-2150-R;n:type:ShaderForge.SFN_Tex2d,id:2150,x:32768,y:32675,varname:node_2150,prsc:2,ntxv:0,isnm:False|UVIN-3598-UVOUT,TEX-4820-TEX;n:type:ShaderForge.SFN_Length,id:4918,x:31456,y:32231,varname:node_4918,prsc:2|IN-8673-OUT;n:type:ShaderForge.SFN_OneMinus,id:2609,x:31827,y:32229,varname:node_2609,prsc:2|IN-4918-OUT;n:type:ShaderForge.SFN_Vector1,id:2044,x:31827,y:32354,varname:node_2044,prsc:2,v1:1.3;n:type:ShaderForge.SFN_Multiply,id:3652,x:32274,y:32239,varname:node_3652,prsc:2|A-2609-OUT,B-2044-OUT;n:type:ShaderForge.SFN_Power,id:5331,x:32643,y:32243,varname:node_5331,prsc:2|VAL-3652-OUT,EXP-1667-OUT;n:type:ShaderForge.SFN_Vector1,id:1667,x:32406,y:32392,varname:node_1667,prsc:2,v1:3;n:type:ShaderForge.SFN_Multiply,id:4892,x:33478,y:32425,varname:node_4892,prsc:2|A-4061-OUT,B-4587-OUT;n:type:ShaderForge.SFN_Multiply,id:2060,x:31882,y:32023,varname:node_2060,prsc:2|A-4720-OUT,B-4918-OUT;n:type:ShaderForge.SFN_Vector1,id:4720,x:31540,y:31992,varname:node_4720,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:6019,x:32958,y:32174,varname:node_6019,prsc:2|A-2666-OUT,B-5331-OUT,C-4673-OUT;n:type:ShaderForge.SFN_Power,id:2666,x:32223,y:32034,varname:node_2666,prsc:2|VAL-2060-OUT,EXP-9914-OUT;n:type:ShaderForge.SFN_Vector1,id:9914,x:32027,y:32151,varname:node_9914,prsc:2,v1:2.5;n:type:ShaderForge.SFN_Vector1,id:4673,x:32775,y:32392,varname:node_4673,prsc:2,v1:3.2;n:type:ShaderForge.SFN_Power,id:4061,x:33260,y:32308,varname:node_4061,prsc:2|VAL-6019-OUT,EXP-348-OUT;n:type:ShaderForge.SFN_Vector1,id:348,x:33042,y:32386,varname:node_348,prsc:2,v1:3.5;proporder:1059-1134-4820;pass:END;sub:END;*/

Shader "TIAB/Item Projector/Background" {
    Properties {
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _Speed ("Speed", Float ) = 1
        _BackGround ("BackGround", 2D) = "white" {}
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
            uniform sampler2D _BackGround; uniform float4 _BackGround_ST;
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
                float node_4918 = length(node_8673);
                float4 node_4358 = _Time;
                float node_794 = (_Speed*node_4358.g);
                float2 node_3909 = node_8673.rg;
                float2 node_3879 = float2((1.0 - distance(i.uv0,float2(0.5,0.5))),((atan2(node_3909.r,node_3909.g)/6.28318530718)+0.5));
                float2 node_6374 = (node_3879+node_794*float2(0.3,0));
                float4 node_5866 = tex2D(_BackGround,TRANSFORM_TEX(node_6374, _BackGround));
                float2 node_3598 = (node_3879+node_794*float2(0.5,0));
                float4 node_2150 = tex2D(_BackGround,TRANSFORM_TEX(node_3598, _BackGround));
                float3 emissive = ((pow((pow((2.0*node_4918),2.5)*pow(((1.0 - node_4918)*1.3),3.0)*3.2),3.5)*(node_5866.r+node_2150.r))*_Color.rgb*2.0*i.vertexColor.rgb);
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
