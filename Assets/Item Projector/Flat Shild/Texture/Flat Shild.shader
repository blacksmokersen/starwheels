// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33673,y:32638,varname:node_4795,prsc:2|emission-2393-OUT,alpha-7937-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:31603,y:32490,varname:_MainTex,prsc:2,ntxv:0,isnm:False|UVIN-3925-UVOUT,TEX-4542-TEX;n:type:ShaderForge.SFN_Multiply,id:2393,x:33296,y:32741,varname:node_2393,prsc:2|A-3412-OUT,B-2053-RGB,C-797-RGB,D-9248-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32859,y:32820,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32859,y:32985,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:32848,y:33157,varname:node_9248,prsc:2,v1:2;n:type:ShaderForge.SFN_Tex2dAsset,id:4542,x:31243,y:32775,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_4542,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Panner,id:3925,x:31062,y:32508,varname:node_3925,prsc:2,spu:1.3,spv:1|UVIN-9513-OUT,DIST-8758-OUT;n:type:ShaderForge.SFN_Panner,id:1983,x:31048,y:32672,varname:node_1983,prsc:2,spu:-0.9,spv:-3.2|UVIN-4950-UVOUT,DIST-8758-OUT;n:type:ShaderForge.SFN_TexCoord,id:4950,x:30475,y:32405,varname:node_4950,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:1889,x:30620,y:32710,varname:node_1889,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8758,x:30795,y:32697,varname:node_8758,prsc:2|A-1889-T,B-3239-OUT,C-2074-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3239,x:30660,y:32925,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_3239,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:9173,x:31884,y:32562,varname:node_9173,prsc:2|A-6074-G,B-3817-G,C-2179-OUT;n:type:ShaderForge.SFN_Tex2d,id:3817,x:31507,y:32671,varname:node_3817,prsc:2,ntxv:0,isnm:False|UVIN-1983-UVOUT,TEX-4542-TEX;n:type:ShaderForge.SFN_Vector1,id:2074,x:30884,y:32944,varname:node_2074,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Vector1,id:2179,x:31684,y:32741,varname:node_2179,prsc:2,v1:2;n:type:ShaderForge.SFN_Add,id:1490,x:32397,y:32572,varname:node_1490,prsc:2|A-6921-R,B-6721-OUT;n:type:ShaderForge.SFN_Tex2d,id:6921,x:31620,y:32306,varname:node_6921,prsc:2,ntxv:0,isnm:False|TEX-4542-TEX;n:type:ShaderForge.SFN_Tex2d,id:9175,x:31654,y:32885,varname:node_9175,prsc:2,ntxv:0,isnm:False|UVIN-9106-UVOUT,TEX-4542-TEX;n:type:ShaderForge.SFN_Clamp01,id:6721,x:32284,y:32876,varname:node_6721,prsc:2|IN-9285-OUT;n:type:ShaderForge.SFN_Clamp01,id:7842,x:32165,y:32701,varname:node_7842,prsc:2|IN-9173-OUT;n:type:ShaderForge.SFN_Multiply,id:7937,x:33333,y:32958,varname:node_7937,prsc:2|A-3412-OUT,B-2053-A,C-797-A;n:type:ShaderForge.SFN_Clamp01,id:3412,x:33073,y:32569,varname:node_3412,prsc:2|IN-8897-OUT;n:type:ShaderForge.SFN_TexCoord,id:5880,x:30909,y:33035,varname:node_5880,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:7932,x:30894,y:33284,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_7932,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:9513,x:30828,y:32321,varname:node_9513,prsc:2|A-5049-OUT,B-4950-UVOUT;n:type:ShaderForge.SFN_Vector2,id:5049,x:30539,y:32264,varname:node_5049,prsc:2,v1:1.1,v2:1.1;n:type:ShaderForge.SFN_Panner,id:9106,x:31419,y:33026,varname:node_9106,prsc:2,spu:1,spv:0|UVIN-5640-OUT,DIST-4744-OUT;n:type:ShaderForge.SFN_Multiply,id:4744,x:31066,y:33217,varname:node_4744,prsc:2|A-7932-OUT,B-7736-T;n:type:ShaderForge.SFN_Time,id:7736,x:30922,y:33386,varname:node_7736,prsc:2;n:type:ShaderForge.SFN_Tex2d,id:2737,x:31610,y:33184,varname:node_2737,prsc:2,ntxv:0,isnm:False|UVIN-2714-UVOUT,TEX-4542-TEX;n:type:ShaderForge.SFN_Panner,id:2714,x:31388,y:33249,varname:node_2714,prsc:2,spu:-0.9,spv:0|UVIN-5880-UVOUT,DIST-4744-OUT;n:type:ShaderForge.SFN_Multiply,id:5640,x:31222,y:33026,varname:node_5640,prsc:2|A-225-OUT,B-5880-UVOUT;n:type:ShaderForge.SFN_Multiply,id:6931,x:31956,y:32989,varname:node_6931,prsc:2|A-9175-B,B-2737-B,C-9091-OUT;n:type:ShaderForge.SFN_Vector1,id:9091,x:31830,y:33246,varname:node_9091,prsc:2,v1:4;n:type:ShaderForge.SFN_Vector2,id:225,x:31044,y:32967,varname:node_225,prsc:2,v1:1.1,v2:1;n:type:ShaderForge.SFN_Power,id:9285,x:32146,y:32955,varname:node_9285,prsc:2|VAL-6931-OUT,EXP-6204-OUT;n:type:ShaderForge.SFN_Vector1,id:6204,x:32028,y:33157,varname:node_6204,prsc:2,v1:2;n:type:ShaderForge.SFN_Tex2d,id:4735,x:31921,y:33420,varname:node_4735,prsc:2,ntxv:0,isnm:False|UVIN-29-OUT,TEX-4542-TEX;n:type:ShaderForge.SFN_TexCoord,id:5562,x:31433,y:33459,varname:node_5562,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:5507,x:32665,y:32639,varname:node_5507,prsc:2|A-1490-OUT,B-2575-OUT;n:type:ShaderForge.SFN_Multiply,id:2929,x:32570,y:33382,varname:node_2929,prsc:2|A-4735-A,B-7420-OUT,C-4490-OUT;n:type:ShaderForge.SFN_Vector1,id:7420,x:31921,y:33594,varname:node_7420,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:29,x:31654,y:33455,varname:node_29,prsc:2|A-5562-UVOUT,B-1713-OUT;n:type:ShaderForge.SFN_Tex2d,id:8569,x:31864,y:33701,varname:node_8569,prsc:2,ntxv:0,isnm:False|TEX-4542-TEX;n:type:ShaderForge.SFN_OneMinus,id:4490,x:32392,y:33626,varname:node_4490,prsc:2|IN-3374-OUT;n:type:ShaderForge.SFN_Multiply,id:3374,x:32165,y:33671,varname:node_3374,prsc:2|A-8569-R,B-6493-OUT;n:type:ShaderForge.SFN_Vector1,id:6493,x:32018,y:33916,varname:node_6493,prsc:2,v1:3;n:type:ShaderForge.SFN_Clamp01,id:2575,x:32794,y:33335,varname:node_2575,prsc:2|IN-2929-OUT;n:type:ShaderForge.SFN_Vector2,id:3436,x:31464,y:33652,varname:node_3436,prsc:2,v1:3,v2:1;n:type:ShaderForge.SFN_Append,id:1713,x:31509,y:33754,varname:node_1713,prsc:2|A-7263-OUT,B-955-OUT;n:type:ShaderForge.SFN_Vector1,id:955,x:31280,y:33821,varname:node_955,prsc:2,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:8957,x:31054,y:33674,ptovrint:False,ptlb:Item numbre,ptin:_Itemnumbre,varname:node_8957,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:5876,x:32665,y:32833,varname:node_5876,prsc:2|A-5507-OUT,B-7842-OUT;n:type:ShaderForge.SFN_Add,id:8897,x:32897,y:32652,varname:node_8897,prsc:2|A-5507-OUT,B-5876-OUT;n:type:ShaderForge.SFN_Floor,id:7263,x:31265,y:33639,varname:node_7263,prsc:2|IN-8957-OUT;proporder:797-4542-3239-7932-8957;pass:END;sub:END;*/

Shader "TIAB/Flat Shild" {
    Properties {
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _Texture ("Texture", 2D) = "white" {}
        _Speed ("Speed", Float ) = 1
        _speed ("speed", Float ) = 0.5
        _Itemnumbre ("Item numbre", Float ) = 1
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
            uniform float _speed;
            uniform float _Itemnumbre;
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
                float4 node_6921 = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float4 node_7736 = _Time;
                float node_4744 = (_speed*node_7736.g);
                float2 node_9106 = ((float2(1.1,1)*i.uv0)+node_4744*float2(1,0));
                float4 node_9175 = tex2D(_Texture,TRANSFORM_TEX(node_9106, _Texture));
                float2 node_2714 = (i.uv0+node_4744*float2(-0.9,0));
                float4 node_2737 = tex2D(_Texture,TRANSFORM_TEX(node_2714, _Texture));
                float2 node_29 = (i.uv0*float2(floor(_Itemnumbre),1.0));
                float4 node_4735 = tex2D(_Texture,TRANSFORM_TEX(node_29, _Texture));
                float4 node_8569 = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float node_5507 = ((node_6921.r+saturate(pow((node_9175.b*node_2737.b*4.0),2.0)))+saturate((node_4735.a*1.5*(1.0 - (node_8569.r*3.0)))));
                float4 node_1889 = _Time;
                float node_8758 = (node_1889.g*_Speed*0.1);
                float2 node_3925 = ((float2(1.1,1.1)*i.uv0)+node_8758*float2(1.3,1));
                float4 _MainTex = tex2D(_Texture,TRANSFORM_TEX(node_3925, _Texture));
                float2 node_1983 = (i.uv0+node_8758*float2(-0.9,-3.2));
                float4 node_3817 = tex2D(_Texture,TRANSFORM_TEX(node_1983, _Texture));
                float node_3412 = saturate((node_5507+(node_5507*saturate((_MainTex.g*node_3817.g*2.0)))));
                float3 emissive = (node_3412*i.vertexColor.rgb*_TintColor.rgb*2.0);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(node_3412*i.vertexColor.a*_TintColor.a));
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
