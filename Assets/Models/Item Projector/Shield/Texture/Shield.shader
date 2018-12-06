// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33388,y:32696,varname:node_3138,prsc:2|emission-8439-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32571,y:32895,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Fresnel,id:1287,x:31468,y:32629,varname:node_1287,prsc:2|NRM-8967-OUT,EXP-7144-OUT;n:type:ShaderForge.SFN_NormalVector,id:8967,x:31069,y:32563,prsc:2,pt:True;n:type:ShaderForge.SFN_ValueProperty,id:7144,x:31083,y:32784,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_7144,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:1000,x:31954,y:32747,varname:node_1000,prsc:2|A-1539-OUT,B-5884-OUT;n:type:ShaderForge.SFN_Power,id:4377,x:32165,y:32753,varname:node_4377,prsc:2|VAL-1000-OUT,EXP-849-OUT;n:type:ShaderForge.SFN_Multiply,id:8439,x:33093,y:32766,varname:node_8439,prsc:2|A-5473-OUT,B-7241-RGB;n:type:ShaderForge.SFN_Clamp01,id:734,x:32356,y:32753,varname:node_734,prsc:2|IN-4377-OUT;n:type:ShaderForge.SFN_Vector1,id:5884,x:31772,y:32862,varname:node_5884,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Vector1,id:849,x:31954,y:32882,varname:node_849,prsc:2,v1:5;n:type:ShaderForge.SFN_Depth,id:6722,x:30761,y:32846,varname:node_6722,prsc:2;n:type:ShaderForge.SFN_DepthBlend,id:4180,x:31300,y:32847,varname:node_4180,prsc:2|DIST-5192-OUT;n:type:ShaderForge.SFN_Multiply,id:5192,x:31114,y:32852,varname:node_5192,prsc:2|A-6722-OUT,B-655-OUT;n:type:ShaderForge.SFN_ValueProperty,id:655,x:30761,y:32995,ptovrint:False,ptlb:node_655,ptin:_node_655,varname:node_655,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_OneMinus,id:2856,x:31468,y:32847,varname:node_2856,prsc:2|IN-4180-OUT;n:type:ShaderForge.SFN_Add,id:1539,x:31641,y:32744,varname:node_1539,prsc:2|A-1287-OUT,B-2856-OUT;n:type:ShaderForge.SFN_Fresnel,id:785,x:31667,y:32456,varname:node_785,prsc:2|NRM-8967-OUT,EXP-7582-OUT;n:type:ShaderForge.SFN_Multiply,id:7582,x:31377,y:32473,varname:node_7582,prsc:2|A-7919-OUT,B-7144-OUT;n:type:ShaderForge.SFN_Add,id:5473,x:32804,y:32679,varname:node_5473,prsc:2|A-9140-OUT,B-734-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7919,x:31069,y:32452,ptovrint:False,ptlb:Int,ptin:_Int,varname:node_7919,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.25;n:type:ShaderForge.SFN_Tex2d,id:2268,x:31940,y:32303,varname:node_2268,prsc:2,ntxv:0,isnm:False|UVIN-9049-UVOUT,TEX-3053-TEX;n:type:ShaderForge.SFN_Panner,id:9049,x:31618,y:32179,varname:node_9049,prsc:2,spu:1,spv:0|UVIN-4623-UVOUT,DIST-7334-OUT;n:type:ShaderForge.SFN_TexCoord,id:4623,x:31187,y:31982,varname:node_4623,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:9140,x:32487,y:32379,varname:node_9140,prsc:2|A-3040-RGB,B-6913-OUT,C-785-OUT;n:type:ShaderForge.SFN_Color,id:3040,x:32169,y:32121,ptovrint:False,ptlb:node_3040,ptin:_node_3040,varname:node_3040,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Time,id:16,x:31187,y:32155,varname:node_16,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7334,x:31373,y:32218,varname:node_7334,prsc:2|A-16-T,B-8573-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8573,x:31187,y:32314,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_8573,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Tex2dAsset,id:3053,x:31758,y:32308,ptovrint:False,ptlb:node_3053,ptin:_node_3053,varname:node_3053,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9056,x:31925,y:32154,varname:node_9056,prsc:2,ntxv:0,isnm:False|UVIN-8012-UVOUT,TEX-3053-TEX;n:type:ShaderForge.SFN_Panner,id:8012,x:31647,y:32041,varname:node_8012,prsc:2,spu:0.25,spv:0|UVIN-920-OUT,DIST-7334-OUT;n:type:ShaderForge.SFN_Multiply,id:920,x:31397,y:31858,varname:node_920,prsc:2|A-2541-OUT,B-4623-UVOUT;n:type:ShaderForge.SFN_Vector2,id:2541,x:31165,y:31836,varname:node_2541,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_Add,id:6913,x:32169,y:32313,varname:node_6913,prsc:2|A-9056-RGB,B-2268-RGB;proporder:7241-7144-655-7919-3040-8573-3053;pass:END;sub:END;*/

Shader "BSE/Item/Shield" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _Fresnel ("Fresnel", Float ) = 1
        _node_655 ("node_655", Float ) = 0.1
        _Int ("Int", Float ) = 0.25
        _node_3040 ("node_3040", Color) = (0.5,0.5,0.5,1)
        _speed ("speed", Float ) = 0.1
        _node_3053 ("node_3053", 2D) = "white" {}
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
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _Color;
            uniform float _Fresnel;
            uniform float _node_655;
            uniform float _Int;
            uniform float4 _node_3040;
            uniform float _speed;
            uniform sampler2D _node_3053; uniform float4 _node_3053_ST;
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
                float4 projPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float4 node_16 = _Time;
                float node_7334 = (node_16.g*_speed);
                float2 node_8012 = ((float2(0.5,0.5)*i.uv0)+node_7334*float2(0.25,0));
                float4 node_9056 = tex2D(_node_3053,TRANSFORM_TEX(node_8012, _node_3053));
                float2 node_9049 = (i.uv0+node_7334*float2(1,0));
                float4 node_2268 = tex2D(_node_3053,TRANSFORM_TEX(node_9049, _node_3053));
                float node_5192 = (partZ*_node_655);
                float node_4180 = saturate((sceneZ-partZ)/node_5192);
                float3 emissive = (((_node_3040.rgb*(node_9056.rgb+node_2268.rgb)*pow(1.0-max(0,dot(normalDirection, viewDirection)),(_Int*_Fresnel)))+saturate(pow(((pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel)+(1.0 - node_4180))*1.5),5.0)))*_Color.rgb);
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
