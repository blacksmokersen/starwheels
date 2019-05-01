// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:0,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:34247,y:32183,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32161,y:32999,varname:_MainTex,prsc:2,ntxv:0,isnm:False|UVIN-8732-UVOUT,TEX-7972-TEX;n:type:ShaderForge.SFN_Multiply,id:2393,x:33670,y:32362,varname:node_2393,prsc:2|A-221-OUT,B-2053-RGB,C-2053-A;n:type:ShaderForge.SFN_VertexColor,id:2053,x:33322,y:32526,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32178,y:33195,ptovrint:True,ptlb:Color Middle,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4705883,c2:0.5882353,c3:0.5294118,c4:1;n:type:ShaderForge.SFN_Panner,id:8732,x:31922,y:32958,varname:node_8732,prsc:2,spu:1,spv:0|UVIN-7299-UVOUT,DIST-3697-OUT;n:type:ShaderForge.SFN_TexCoord,id:7299,x:31340,y:32706,varname:node_7299,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Time,id:2944,x:31538,y:32905,varname:node_2944,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3697,x:31723,y:32912,varname:node_3697,prsc:2|A-2944-T,B-5397-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5397,x:31538,y:33077,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_5397,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Tex2dAsset,id:7972,x:31993,y:32486,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_7972,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8359,x:32184,y:32782,varname:node_8359,prsc:2,ntxv:0,isnm:False|UVIN-2669-UVOUT,TEX-7972-TEX;n:type:ShaderForge.SFN_Panner,id:2669,x:31922,y:32804,varname:node_2669,prsc:2,spu:0.5,spv:0|UVIN-5711-OUT,DIST-3697-OUT;n:type:ShaderForge.SFN_Color,id:4145,x:32224,y:32003,ptovrint:False,ptlb:Color Border,ptin:_ColorBorder,varname:node_4145,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:352,x:32405,y:33064,varname:node_352,prsc:2|A-8359-R,B-6074-R,C-797-RGB,D-797-A;n:type:ShaderForge.SFN_Add,id:7444,x:32605,y:33010,varname:node_7444,prsc:2|A-7407-OUT,B-352-OUT;n:type:ShaderForge.SFN_Multiply,id:7407,x:32405,y:32913,varname:node_7407,prsc:2|A-8359-R,B-797-RGB,C-797-A;n:type:ShaderForge.SFN_Multiply,id:5711,x:31631,y:32574,varname:node_5711,prsc:2|A-9861-OUT,B-7299-UVOUT;n:type:ShaderForge.SFN_Vector2,id:9861,x:31340,y:32544,varname:node_9861,prsc:2,v1:0.9,v2:1;n:type:ShaderForge.SFN_Multiply,id:2986,x:32589,y:32212,varname:node_2986,prsc:2|A-4145-RGB,B-5232-G,C-4145-A;n:type:ShaderForge.SFN_Add,id:221,x:33363,y:32241,varname:node_221,prsc:2|A-6109-OUT,B-9029-OUT,C-7444-OUT;n:type:ShaderForge.SFN_Panner,id:1305,x:31952,y:32133,varname:node_1305,prsc:2,spu:0.2,spv:0|UVIN-2592-OUT,DIST-3697-OUT;n:type:ShaderForge.SFN_Multiply,id:2592,x:31685,y:32076,varname:node_2592,prsc:2|A-5105-OUT,B-7299-UVOUT;n:type:ShaderForge.SFN_Vector2,id:5105,x:31397,y:32109,varname:node_5105,prsc:2,v1:0.5,v2:1;n:type:ShaderForge.SFN_Tex2d,id:5232,x:32207,y:32315,varname:node_5232,prsc:2,ntxv:0,isnm:False|UVIN-1305-UVOUT,TEX-7972-TEX;n:type:ShaderForge.SFN_Add,id:9029,x:32800,y:32350,varname:node_9029,prsc:2|A-2986-OUT,B-7670-OUT;n:type:ShaderForge.SFN_Multiply,id:7670,x:32589,y:32379,varname:node_7670,prsc:2|A-4145-RGB,B-1436-G,C-5253-OUT,D-4145-A;n:type:ShaderForge.SFN_Vector1,id:5253,x:32444,y:32492,varname:node_5253,prsc:2,v1:0.25;n:type:ShaderForge.SFN_Panner,id:2941,x:31952,y:31955,varname:node_2941,prsc:2,spu:0.7,spv:0|UVIN-6780-OUT;n:type:ShaderForge.SFN_Tex2d,id:1436,x:32207,y:32171,varname:node_1436,prsc:2,ntxv:0,isnm:False|UVIN-2941-UVOUT,TEX-7972-TEX;n:type:ShaderForge.SFN_Multiply,id:6780,x:31673,y:31927,varname:node_6780,prsc:2|A-8534-OUT,B-7299-UVOUT;n:type:ShaderForge.SFN_Vector2,id:8534,x:31387,y:31964,varname:node_8534,prsc:2,v1:1.5,v2:1;n:type:ShaderForge.SFN_Tex2d,id:7606,x:32256,y:31523,varname:node_7606,prsc:2,ntxv:0,isnm:False|UVIN-5830-UVOUT,TEX-7972-TEX;n:type:ShaderForge.SFN_Tex2d,id:9217,x:32265,y:31391,varname:node_9217,prsc:2,ntxv:0,isnm:False|UVIN-5715-UVOUT,TEX-7972-TEX;n:type:ShaderForge.SFN_Panner,id:5830,x:31992,y:31561,varname:node_5830,prsc:2,spu:2,spv:0|UVIN-6028-OUT,DIST-3697-OUT;n:type:ShaderForge.SFN_Panner,id:5715,x:31992,y:31417,varname:node_5715,prsc:2,spu:3,spv:0|UVIN-1667-OUT,DIST-3697-OUT;n:type:ShaderForge.SFN_Add,id:7265,x:32763,y:31523,varname:node_7265,prsc:2|A-3336-OUT,B-4917-OUT;n:type:ShaderForge.SFN_Multiply,id:3336,x:32611,y:31345,varname:node_3336,prsc:2|A-8128-RGB,B-9217-B,C-8128-A;n:type:ShaderForge.SFN_Multiply,id:4917,x:32607,y:31661,varname:node_4917,prsc:2|A-8128-RGB,B-7606-B,C-8128-A;n:type:ShaderForge.SFN_Color,id:8128,x:32265,y:31230,ptovrint:False,ptlb:Color Particle,ptin:_ColorParticle,varname:node_8128,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:6028,x:31652,y:31709,varname:node_6028,prsc:2|A-2244-OUT,B-7299-UVOUT;n:type:ShaderForge.SFN_Vector2,id:2244,x:31314,y:31719,varname:node_2244,prsc:2,v1:0.5,v2:2;n:type:ShaderForge.SFN_Multiply,id:1667,x:31634,y:31547,varname:node_1667,prsc:2|A-6567-OUT,B-7299-UVOUT;n:type:ShaderForge.SFN_Vector2,id:6567,x:31297,y:31556,varname:node_6567,prsc:2,v1:1,v2:3;n:type:ShaderForge.SFN_Multiply,id:6109,x:32979,y:31321,varname:node_6109,prsc:2|A-2854-A,B-7265-OUT;n:type:ShaderForge.SFN_Tex2d,id:2854,x:32523,y:31071,varname:node_2854,prsc:2,ntxv:0,isnm:False|TEX-7972-TEX;proporder:7972-5397-797-4145-8128;pass:END;sub:END;*/

Shader "BSE/Item/Hook/Line" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        _Speed ("Speed", Float ) = 1
        _TintColor ("Color Middle", Color) = (0.4705883,0.5882353,0.5294118,1)
        _ColorBorder ("Color Border", Color) = (0.5,0.5,0.5,1)
        _ColorParticle ("Color Particle", Color) = (0.5,0.5,0.5,1)
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
            uniform float _Speed;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float4 _ColorBorder;
            uniform float4 _ColorParticle;
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
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_2854 = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float4 node_2944 = _Time;
                float node_3697 = (node_2944.g*_Speed);
                float2 node_5715 = ((float2(1,3)*i.uv0)+node_3697*float2(3,0));
                float4 node_9217 = tex2D(_Texture,TRANSFORM_TEX(node_5715, _Texture));
                float2 node_5830 = ((float2(0.5,2)*i.uv0)+node_3697*float2(2,0));
                float4 node_7606 = tex2D(_Texture,TRANSFORM_TEX(node_5830, _Texture));
                float3 node_7265 = ((_ColorParticle.rgb*node_9217.b*_ColorParticle.a)+(_ColorParticle.rgb*node_7606.b*_ColorParticle.a));
                float2 node_1305 = ((float2(0.5,1)*i.uv0)+node_3697*float2(0.2,0));
                float4 node_5232 = tex2D(_Texture,TRANSFORM_TEX(node_1305, _Texture));
                float4 node_8340 = _Time;
                float2 node_2941 = ((float2(1.5,1)*i.uv0)+node_8340.g*float2(0.7,0));
                float4 node_1436 = tex2D(_Texture,TRANSFORM_TEX(node_2941, _Texture));
                float2 node_2669 = ((float2(0.9,1)*i.uv0)+node_3697*float2(0.5,0));
                float4 node_8359 = tex2D(_Texture,TRANSFORM_TEX(node_2669, _Texture));
                float2 node_8732 = (i.uv0+node_3697*float2(1,0));
                float4 _MainTex = tex2D(_Texture,TRANSFORM_TEX(node_8732, _Texture));
                float3 emissive = (((node_2854.a*node_7265)+((_ColorBorder.rgb*node_5232.g*_ColorBorder.a)+(_ColorBorder.rgb*node_1436.g*0.25*_ColorBorder.a))+((node_8359.r*_TintColor.rgb*_TintColor.a)+(node_8359.r*_MainTex.r*_TintColor.rgb*_TintColor.a)))*i.vertexColor.rgb*i.vertexColor.a);
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
