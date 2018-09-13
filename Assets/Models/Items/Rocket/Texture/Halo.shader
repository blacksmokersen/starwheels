// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-5375-OUT;n:type:ShaderForge.SFN_Tex2d,id:3817,x:31843,y:32858,varname:node_3817,prsc:2,ntxv:0,isnm:False|UVIN-4515-UVOUT,TEX-3403-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:3403,x:31466,y:33049,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_3403,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:3565,x:31841,y:33081,varname:node_3565,prsc:2,ntxv:0,isnm:False|TEX-3403-TEX;n:type:ShaderForge.SFN_Rotator,id:4515,x:31319,y:32809,varname:node_4515,prsc:2|UVIN-2660-UVOUT,SPD-1298-OUT;n:type:ShaderForge.SFN_TexCoord,id:2660,x:30861,y:32609,varname:node_2660,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:1298,x:30832,y:32841,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_1298,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Time,id:7157,x:31612,y:32372,varname:node_7157,prsc:2;n:type:ShaderForge.SFN_Multiply,id:682,x:31846,y:32446,varname:node_682,prsc:2|A-7157-T,B-1298-OUT;n:type:ShaderForge.SFN_Sin,id:9427,x:32018,y:32486,varname:node_9427,prsc:2|IN-682-OUT;n:type:ShaderForge.SFN_Multiply,id:5375,x:32452,y:32822,varname:node_5375,prsc:2|A-2728-OUT,B-2736-OUT,C-466-OUT;n:type:ShaderForge.SFN_RemapRange,id:2728,x:32235,y:32547,varname:node_2728,prsc:2,frmn:0,frmx:1,tomn:2,tomx:3|IN-9427-OUT;n:type:ShaderForge.SFN_Multiply,id:2736,x:32137,y:32720,varname:node_2736,prsc:2|A-2360-OUT,B-3817-RGB;n:type:ShaderForge.SFN_Color,id:274,x:31693,y:32658,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_274,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Multiply,id:466,x:32163,y:33037,varname:node_466,prsc:2|A-274-A,B-3565-A;n:type:ShaderForge.SFN_Multiply,id:2360,x:31927,y:32624,varname:node_2360,prsc:2|A-248-OUT,B-274-RGB;n:type:ShaderForge.SFN_Vector1,id:248,x:31694,y:32588,varname:node_248,prsc:2,v1:2;proporder:274-3403-1298;pass:END;sub:END;*/

Shader "BSE/Item/Rocket/Halo" {
    Properties {
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _Texture ("Texture", 2D) = "white" {}
        _Speed ("Speed", Float ) = 1
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
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _Speed;
            uniform float4 _Color;
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
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_7157 = _Time;
                float4 node_7584 = _Time;
                float node_4515_ang = node_7584.g;
                float node_4515_spd = _Speed;
                float node_4515_cos = cos(node_4515_spd*node_4515_ang);
                float node_4515_sin = sin(node_4515_spd*node_4515_ang);
                float2 node_4515_piv = float2(0.5,0.5);
                float2 node_4515 = (mul(i.uv0-node_4515_piv,float2x2( node_4515_cos, -node_4515_sin, node_4515_sin, node_4515_cos))+node_4515_piv);
                float4 node_3817 = tex2D(_Texture,TRANSFORM_TEX(node_4515, _Texture));
                float4 node_3565 = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float3 emissive = ((sin((node_7157.g*_Speed))*1.0+2.0)*((2.0*_Color.rgb)*node_3817.rgb)*(_Color.a*node_3565.a));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
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
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
