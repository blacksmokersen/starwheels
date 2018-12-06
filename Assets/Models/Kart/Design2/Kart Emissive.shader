// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:32986,y:32689,varname:node_4013,prsc:2|diff-1162-OUT,emission-6513-OUT;n:type:ShaderForge.SFN_Color,id:5212,x:31245,y:32689,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5019608,c2:0.5019608,c3:0.5019608,c4:1;n:type:ShaderForge.SFN_Tex2d,id:5509,x:31265,y:32945,varname:node_3243,prsc:2,ntxv:0,isnm:False|TEX-1906-TEX;n:type:ShaderForge.SFN_Sin,id:1754,x:31677,y:33076,varname:node_1754,prsc:2|IN-7415-OUT;n:type:ShaderForge.SFN_Time,id:4826,x:31273,y:33155,varname:node_4826,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7415,x:31486,y:33076,varname:node_7415,prsc:2|A-4826-T,B-607-OUT;n:type:ShaderForge.SFN_ValueProperty,id:607,x:31273,y:33317,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_4482,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_RemapRange,id:9487,x:31884,y:32979,varname:node_9487,prsc:2,frmn:-1,frmx:1,tomn:1.5,tomx:3|IN-1754-OUT;n:type:ShaderForge.SFN_Multiply,id:1162,x:31802,y:32789,varname:node_1162,prsc:2|A-8118-OUT,B-5832-OUT;n:type:ShaderForge.SFN_Add,id:5602,x:32270,y:32861,varname:node_5602,prsc:2|A-1162-OUT,B-725-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:1906,x:30856,y:33054,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_1906,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1294,x:31683,y:33448,varname:node_1294,prsc:2,ntxv:0,isnm:False|UVIN-8517-UVOUT,TEX-1906-TEX;n:type:ShaderForge.SFN_Multiply,id:725,x:32072,y:33440,varname:node_725,prsc:2|A-1294-A,B-6586-A,C-2455-OUT,D-5212-A;n:type:ShaderForge.SFN_Tex2d,id:6586,x:31683,y:33594,varname:node_6586,prsc:2,ntxv:0,isnm:False|UVIN-9622-UVOUT,TEX-1906-TEX;n:type:ShaderForge.SFN_TexCoord,id:5366,x:30834,y:33614,varname:node_5366,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:8517,x:31392,y:33508,varname:node_8517,prsc:2,spu:1.2,spv:0.9|UVIN-5692-OUT,DIST-813-OUT;n:type:ShaderForge.SFN_Panner,id:9622,x:31404,y:33636,varname:node_9622,prsc:2,spu:-0.85,spv:-1.1|UVIN-5366-UVOUT,DIST-813-OUT;n:type:ShaderForge.SFN_Multiply,id:5692,x:31138,y:33508,varname:node_5692,prsc:2|A-6461-OUT,B-5366-UVOUT;n:type:ShaderForge.SFN_Vector1,id:6461,x:30945,y:33461,varname:node_6461,prsc:2,v1:1.1;n:type:ShaderForge.SFN_Time,id:8248,x:30907,y:33804,varname:node_8248,prsc:2;n:type:ShaderForge.SFN_Multiply,id:813,x:31115,y:33788,varname:node_813,prsc:2|A-8248-T,B-8088-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8088,x:30948,y:34016,ptovrint:False,ptlb:Speed Wave,ptin:_SpeedWave,varname:node_8088,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.05;n:type:ShaderForge.SFN_Multiply,id:6513,x:32722,y:32939,varname:node_6513,prsc:2|A-3268-OUT,B-6646-OUT;n:type:ShaderForge.SFN_Clamp01,id:3268,x:32469,y:32861,varname:node_3268,prsc:2|IN-5602-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2455,x:31877,y:33814,ptovrint:False,ptlb:Power Wave,ptin:_PowerWave,varname:node_2455,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:6646,x:31972,y:33148,varname:node_6646,prsc:2|IN-1754-OUT,IMIN-2122-OUT,IMAX-8130-OUT,OMIN-6784-OUT,OMAX-7515-OUT;n:type:ShaderForge.SFN_Vector1,id:2122,x:31677,y:33200,varname:node_2122,prsc:2,v1:-1;n:type:ShaderForge.SFN_Vector1,id:8130,x:31677,y:33257,varname:node_8130,prsc:2,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:1004,x:31422,y:33340,ptovrint:False,ptlb:Glow Power,ptin:_GlowPower,varname:node_1004,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Vector1,id:2457,x:31606,y:33316,varname:node_2457,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Vector1,id:4068,x:31606,y:33378,varname:node_4068,prsc:2,v1:3;n:type:ShaderForge.SFN_Multiply,id:6784,x:31817,y:33257,varname:node_6784,prsc:2|A-1004-OUT,B-2457-OUT;n:type:ShaderForge.SFN_Multiply,id:7515,x:31817,y:33397,varname:node_7515,prsc:2|A-1004-OUT,B-4068-OUT;n:type:ShaderForge.SFN_Lerp,id:8118,x:31598,y:32619,varname:node_8118,prsc:2|A-4674-RGB,B-5212-RGB,T-5509-R;n:type:ShaderForge.SFN_Vector1,id:5832,x:31632,y:32914,varname:node_5832,prsc:2,v1:1;n:type:ShaderForge.SFN_Color,id:4674,x:31242,y:32499,ptovrint:False,ptlb:node_4674,ptin:_node_4674,varname:node_4674,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:5212-4674-607-1906-8088-2455-1004;pass:END;sub:END;*/

Shader "TIAB/Kart/Emissive" {
    Properties {
        _Color ("Color", Color) = (0.5019608,0.5019608,0.5019608,1)
        _node_4674 ("node_4674", Color) = (0.5,0.5,0.5,1)
        _Speed ("Speed", Float ) = 0
        _Texture ("Texture", 2D) = "white" {}
        _SpeedWave ("Speed Wave", Float ) = 0.05
        _PowerWave ("Power Wave", Float ) = 0.5
        _GlowPower ("Glow Power", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Color;
            uniform float _Speed;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _SpeedWave;
            uniform float _PowerWave;
            uniform float _GlowPower;
            uniform float4 _node_4674;
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
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 node_3243 = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float3 node_1162 = (lerp(_node_4674.rgb,_Color.rgb,node_3243.r)*1.0);
                float3 diffuseColor = node_1162;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 node_8248 = _Time;
                float node_813 = (node_8248.g*_SpeedWave);
                float2 node_8517 = ((1.1*i.uv0)+node_813*float2(1.2,0.9));
                float4 node_1294 = tex2D(_Texture,TRANSFORM_TEX(node_8517, _Texture));
                float2 node_9622 = (i.uv0+node_813*float2(-0.85,-1.1));
                float4 node_6586 = tex2D(_Texture,TRANSFORM_TEX(node_9622, _Texture));
                float4 node_4826 = _Time;
                float node_1754 = sin((node_4826.g*_Speed));
                float node_2122 = (-1.0);
                float node_6784 = (_GlowPower*1.5);
                float3 emissive = (saturate((node_1162+(node_1294.a*node_6586.a*_PowerWave*_Color.a)))*(node_6784 + ( (node_1754 - node_2122) * ((_GlowPower*3.0) - node_6784) ) / (1.0 - node_2122)));
/// Final Color:
                float3 finalColor = diffuse + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Color;
            uniform float _Speed;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float _SpeedWave;
            uniform float _PowerWave;
            uniform float _GlowPower;
            uniform float4 _node_4674;
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
                LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float4 node_3243 = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float3 node_1162 = (lerp(_node_4674.rgb,_Color.rgb,node_3243.r)*1.0);
                float3 diffuseColor = node_1162;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
