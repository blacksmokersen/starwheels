// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:False,igpj:True,qofs:1,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:34221,y:32670,varname:node_4795,prsc:2|emission-6217-OUT;n:type:ShaderForge.SFN_Color,id:715,x:33011,y:32767,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Fresnel,id:3609,x:31532,y:32693,varname:node_3609,prsc:2|NRM-2028-OUT,EXP-8322-OUT;n:type:ShaderForge.SFN_NormalVector,id:2028,x:31049,y:32602,prsc:2,pt:True;n:type:ShaderForge.SFN_ValueProperty,id:8322,x:31147,y:32848,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_7144,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:4581,x:32018,y:32811,varname:node_4581,prsc:2|A-3609-OUT,B-5770-OUT;n:type:ShaderForge.SFN_Power,id:3859,x:32229,y:32817,varname:node_3859,prsc:2|VAL-4581-OUT,EXP-3306-OUT;n:type:ShaderForge.SFN_Multiply,id:6217,x:33937,y:32668,varname:node_6217,prsc:2|A-1716-OUT,B-715-RGB,C-715-A,D-1443-A,E-6409-OUT;n:type:ShaderForge.SFN_Clamp01,id:6614,x:32718,y:32797,varname:node_6614,prsc:2|IN-5022-OUT;n:type:ShaderForge.SFN_Vector1,id:5770,x:31836,y:32926,varname:node_5770,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:3306,x:32018,y:32946,varname:node_3306,prsc:2,v1:5;n:type:ShaderForge.SFN_Fresnel,id:8821,x:31726,y:32436,varname:node_8821,prsc:2|NRM-2028-OUT,EXP-1543-OUT;n:type:ShaderForge.SFN_Add,id:2027,x:33317,y:32381,varname:node_2027,prsc:2|A-6254-OUT,B-6614-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2682,x:31846,y:32633,ptovrint:False,ptlb:Interieur,ptin:_Interieur,varname:node_7919,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.25;n:type:ShaderForge.SFN_TexCoord,id:7434,x:30912,y:31974,varname:node_7434,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:6254,x:32914,y:32290,varname:node_6254,prsc:2|A-6513-OUT,B-1643-OUT;n:type:ShaderForge.SFN_Time,id:7980,x:30969,y:32346,varname:node_7980,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7486,x:31148,y:32388,varname:node_7486,prsc:2|A-7980-T,B-6167-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6167,x:30969,y:32495,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_8573,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Tex2dAsset,id:7382,x:31605,y:30946,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_3053,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Power,id:1643,x:32292,y:32652,varname:node_1643,prsc:2|VAL-8455-OUT,EXP-2921-OUT;n:type:ShaderForge.SFN_Vector1,id:2921,x:32073,y:32728,varname:node_2921,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:8455,x:32128,y:32573,varname:node_8455,prsc:2|A-8821-OUT,B-2682-OUT;n:type:ShaderForge.SFN_Panner,id:6939,x:31680,y:31971,varname:node_6939,prsc:2,spu:0.45,spv:-1.05|UVIN-4959-OUT,DIST-7486-OUT;n:type:ShaderForge.SFN_Panner,id:8708,x:31680,y:31831,varname:node_8708,prsc:2,spu:-0.5,spv:0.85|UVIN-7496-OUT,DIST-7486-OUT;n:type:ShaderForge.SFN_Multiply,id:7496,x:31437,y:31774,varname:node_7496,prsc:2|A-9140-OUT,B-7434-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4959,x:31448,y:31926,varname:node_4959,prsc:2|A-4823-OUT,B-7434-UVOUT;n:type:ShaderForge.SFN_Vector2,id:4823,x:31159,y:31866,varname:node_4823,prsc:2,v1:2,v2:2;n:type:ShaderForge.SFN_Vector2,id:9140,x:31159,y:31763,varname:node_9140,prsc:2,v1:3,v2:3;n:type:ShaderForge.SFN_Multiply,id:1218,x:32537,y:32128,varname:node_1218,prsc:2|A-5107-B,B-9530-B,C-7589-OUT;n:type:ShaderForge.SFN_Tex2d,id:5107,x:31982,y:31877,varname:node_5107,prsc:2,ntxv:0,isnm:False|UVIN-8708-UVOUT,TEX-7382-TEX;n:type:ShaderForge.SFN_Tex2d,id:9530,x:31996,y:32019,varname:node_9530,prsc:2,ntxv:0,isnm:False|UVIN-6939-UVOUT,TEX-7382-TEX;n:type:ShaderForge.SFN_Vector1,id:7589,x:32336,y:32208,varname:node_7589,prsc:2,v1:5;n:type:ShaderForge.SFN_Multiply,id:5022,x:32482,y:32878,varname:node_5022,prsc:2|A-3859-OUT,B-8799-OUT;n:type:ShaderForge.SFN_Vector1,id:8799,x:32223,y:33022,varname:node_8799,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:1543,x:31532,y:32571,varname:node_1543,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector2,id:1988,x:31102,y:31270,varname:node_1988,prsc:2,v1:6,v2:6;n:type:ShaderForge.SFN_Vector2,id:9235,x:31102,y:31167,varname:node_9235,prsc:2,v1:7,v2:7;n:type:ShaderForge.SFN_Multiply,id:4263,x:31380,y:31178,varname:node_4263,prsc:2|A-9235-OUT,B-7434-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4147,x:31391,y:31330,varname:node_4147,prsc:2|A-1988-OUT,B-7434-UVOUT;n:type:ShaderForge.SFN_Panner,id:4324,x:31640,y:31186,varname:node_4324,prsc:2,spu:-0.5,spv:-0.85|UVIN-4263-OUT,DIST-5843-OUT;n:type:ShaderForge.SFN_Panner,id:4284,x:31640,y:31351,varname:node_4284,prsc:2,spu:0.45,spv:1.05|UVIN-4147-OUT,DIST-5843-OUT;n:type:ShaderForge.SFN_Vector1,id:6241,x:31186,y:32529,varname:node_6241,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:8504,x:32267,y:31380,varname:node_8504,prsc:2|A-4294-B,B-7344-B,C-2908-OUT;n:type:ShaderForge.SFN_Tex2d,id:4294,x:31933,y:31015,varname:node_4294,prsc:2,ntxv:0,isnm:False|UVIN-4324-UVOUT,TEX-7382-TEX;n:type:ShaderForge.SFN_Tex2d,id:7344,x:31950,y:31296,varname:node_7344,prsc:2,ntxv:0,isnm:False|UVIN-4284-UVOUT,TEX-7382-TEX;n:type:ShaderForge.SFN_Multiply,id:5843,x:31440,y:32383,varname:node_5843,prsc:2|A-7486-OUT,B-6241-OUT;n:type:ShaderForge.SFN_Add,id:6513,x:32735,y:32093,varname:node_6513,prsc:2|A-8504-OUT,B-1218-OUT;n:type:ShaderForge.SFN_Vector1,id:2908,x:32079,y:31519,varname:node_2908,prsc:2,v1:2;n:type:ShaderForge.SFN_Clamp01,id:8960,x:33532,y:32413,varname:node_8960,prsc:2|IN-2027-OUT;n:type:ShaderForge.SFN_Tex2d,id:1443,x:32657,y:33262,varname:node_1443,prsc:2,ntxv:0,isnm:False|UVIN-4394-OUT,TEX-7382-TEX;n:type:ShaderForge.SFN_TexCoord,id:6960,x:32191,y:33205,varname:node_6960,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:4394,x:32448,y:33103,varname:node_4394,prsc:2|A-82-OUT,B-6960-UVOUT;n:type:ShaderForge.SFN_Vector1,id:82,x:32240,y:33108,varname:node_82,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector1,id:6409,x:33200,y:32989,varname:node_6409,prsc:2,v1:2;n:type:ShaderForge.SFN_Add,id:1716,x:33732,y:32453,varname:node_1716,prsc:2|A-8960-OUT,B-9490-OUT;n:type:ShaderForge.SFN_Vector1,id:9490,x:33329,y:32630,varname:node_9490,prsc:2,v1:0.05;proporder:715-7382-8322-6167-2682;pass:END;sub:END;*/

Shader "BSE/Cloak/Exterieur" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _Texture ("Texture", 2D) = "white" {}
        _Fresnel ("Fresnel", Float ) = 1
        _speed ("speed", Float ) = 0.1
        _Interieur ("Interieur", Float ) = 0.25
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent+1"
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
            uniform float _Fresnel;
            uniform float _Interieur;
            uniform float _speed;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
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
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
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
                float4 node_7980 = _Time;
                float node_7486 = (node_7980.g*_speed);
                float node_5843 = (node_7486*1.5);
                float2 node_4324 = ((float2(7,7)*i.uv0)+node_5843*float2(-0.5,-0.85));
                float4 node_4294 = tex2D(_Texture,TRANSFORM_TEX(node_4324, _Texture));
                float2 node_4284 = ((float2(6,6)*i.uv0)+node_5843*float2(0.45,1.05));
                float4 node_7344 = tex2D(_Texture,TRANSFORM_TEX(node_4284, _Texture));
                float2 node_8708 = ((float2(3,3)*i.uv0)+node_7486*float2(-0.5,0.85));
                float4 node_5107 = tex2D(_Texture,TRANSFORM_TEX(node_8708, _Texture));
                float2 node_6939 = ((float2(2,2)*i.uv0)+node_7486*float2(0.45,-1.05));
                float4 node_9530 = tex2D(_Texture,TRANSFORM_TEX(node_6939, _Texture));
                float2 node_4394 = (2.0*i.uv0);
                float4 node_1443 = tex2D(_Texture,TRANSFORM_TEX(node_4394, _Texture));
                float3 emissive = ((saturate(((((node_4294.b*node_7344.b*2.0)+(node_5107.b*node_9530.b*5.0))*pow((pow(1.0-max(0,dot(normalDirection, viewDirection)),0.0)*_Interieur),2.0))+saturate((pow((pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel)*1.0),5.0)*1.0))))+0.05)*_Color.rgb*_Color.a*node_1443.a*2.0);
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
