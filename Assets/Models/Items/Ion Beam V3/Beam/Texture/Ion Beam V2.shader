// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:34221,y:32670,varname:node_4795,prsc:2|emission-9287-OUT;n:type:ShaderForge.SFN_Color,id:715,x:33011,y:32767,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Fresnel,id:3609,x:31532,y:32693,varname:node_3609,prsc:2|NRM-2028-OUT,EXP-8322-OUT;n:type:ShaderForge.SFN_NormalVector,id:2028,x:31049,y:32602,prsc:2,pt:True;n:type:ShaderForge.SFN_ValueProperty,id:8322,x:31147,y:32848,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_7144,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:4581,x:32018,y:32811,varname:node_4581,prsc:2|A-3609-OUT,B-5770-OUT;n:type:ShaderForge.SFN_Power,id:3859,x:32229,y:32817,varname:node_3859,prsc:2|VAL-4581-OUT,EXP-3306-OUT;n:type:ShaderForge.SFN_Multiply,id:6217,x:33442,y:32707,varname:node_6217,prsc:2|A-8960-OUT,B-715-RGB,C-715-A;n:type:ShaderForge.SFN_Clamp01,id:6614,x:32718,y:32797,varname:node_6614,prsc:2|IN-5022-OUT;n:type:ShaderForge.SFN_Vector1,id:5770,x:31836,y:32926,varname:node_5770,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:3306,x:32018,y:32946,varname:node_3306,prsc:2,v1:5;n:type:ShaderForge.SFN_Fresnel,id:8821,x:31697,y:32545,varname:node_8821,prsc:2|NRM-2028-OUT,EXP-1543-OUT;n:type:ShaderForge.SFN_Add,id:2027,x:33317,y:32381,varname:node_2027,prsc:2|A-7805-OUT,B-6254-OUT,C-6614-OUT,D-8216-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2682,x:31846,y:32633,ptovrint:False,ptlb:Interieur,ptin:_Interieur,varname:node_7919,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.25;n:type:ShaderForge.SFN_Tex2d,id:542,x:31996,y:32394,varname:node_2268,prsc:2,ntxv:0,isnm:False|UVIN-859-UVOUT,TEX-7382-TEX;n:type:ShaderForge.SFN_Panner,id:859,x:31674,y:32272,varname:node_859,prsc:2,spu:0,spv:-2|UVIN-1754-OUT,DIST-7486-OUT;n:type:ShaderForge.SFN_TexCoord,id:7434,x:30912,y:31974,varname:node_7434,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:6254,x:32914,y:32290,varname:node_6254,prsc:2|A-6513-OUT,B-1643-OUT;n:type:ShaderForge.SFN_Time,id:7980,x:30969,y:32346,varname:node_7980,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7486,x:31148,y:32388,varname:node_7486,prsc:2|A-7980-T,B-6167-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6167,x:30969,y:32495,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_8573,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Tex2dAsset,id:7382,x:31605,y:30946,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_3053,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2640,x:31996,y:32249,varname:node_9056,prsc:2,ntxv:0,isnm:False|UVIN-3782-UVOUT,TEX-7382-TEX;n:type:ShaderForge.SFN_Panner,id:3782,x:31674,y:32123,varname:node_3782,prsc:2,spu:0,spv:-0.5|UVIN-7434-UVOUT,DIST-7486-OUT;n:type:ShaderForge.SFN_Multiply,id:1754,x:31440,y:32193,varname:node_1754,prsc:2|A-7434-UVOUT,B-7171-OUT;n:type:ShaderForge.SFN_Vector2,id:7171,x:31243,y:32223,varname:node_7171,prsc:2,v1:2,v2:2;n:type:ShaderForge.SFN_Add,id:9556,x:32350,y:32330,varname:node_9556,prsc:2|A-2640-R,B-542-R;n:type:ShaderForge.SFN_Power,id:1643,x:32292,y:32652,varname:node_1643,prsc:2|VAL-8455-OUT,EXP-2921-OUT;n:type:ShaderForge.SFN_Vector1,id:2921,x:32073,y:32728,varname:node_2921,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:8455,x:32128,y:32573,varname:node_8455,prsc:2|A-8821-OUT,B-2682-OUT;n:type:ShaderForge.SFN_Vector1,id:2083,x:31911,y:32697,varname:node_2083,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Panner,id:6939,x:31680,y:31971,varname:node_6939,prsc:2,spu:0.45,spv:-1.05|UVIN-4959-OUT,DIST-7486-OUT;n:type:ShaderForge.SFN_Panner,id:8708,x:31680,y:31831,varname:node_8708,prsc:2,spu:-0.5,spv:-0.85|UVIN-7496-OUT,DIST-7486-OUT;n:type:ShaderForge.SFN_Multiply,id:7496,x:31437,y:31774,varname:node_7496,prsc:2|A-9140-OUT,B-7434-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4959,x:31448,y:31926,varname:node_4959,prsc:2|A-4823-OUT,B-7434-UVOUT;n:type:ShaderForge.SFN_Vector2,id:4823,x:31159,y:31866,varname:node_4823,prsc:2,v1:2,v2:2;n:type:ShaderForge.SFN_Vector2,id:9140,x:31159,y:31763,varname:node_9140,prsc:2,v1:3,v2:3;n:type:ShaderForge.SFN_Multiply,id:1218,x:32537,y:32128,varname:node_1218,prsc:2|A-5107-B,B-9530-B,C-7589-OUT;n:type:ShaderForge.SFN_Tex2d,id:5107,x:31982,y:31877,varname:node_5107,prsc:2,ntxv:0,isnm:False|UVIN-8708-UVOUT,TEX-7382-TEX;n:type:ShaderForge.SFN_Tex2d,id:9530,x:31996,y:32019,varname:node_9530,prsc:2,ntxv:0,isnm:False|UVIN-6939-UVOUT,TEX-7382-TEX;n:type:ShaderForge.SFN_Vector1,id:7589,x:32336,y:32208,varname:node_7589,prsc:2,v1:5;n:type:ShaderForge.SFN_SwitchProperty,id:9287,x:33813,y:32890,ptovrint:False,ptlb:Center,ptin:_Center,varname:node_9287,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-6217-OUT,B-6184-OUT;n:type:ShaderForge.SFN_Fresnel,id:6711,x:32712,y:33102,varname:node_6711,prsc:2|EXP-1636-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1636,x:32473,y:33152,ptovrint:False,ptlb:Fade Center,ptin:_FadeCenter,varname:node_1636,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:6184,x:33602,y:32937,varname:node_6184,prsc:2|A-6217-OUT,B-3059-OUT;n:type:ShaderForge.SFN_OneMinus,id:9687,x:32889,y:33102,varname:node_9687,prsc:2|IN-6711-OUT;n:type:ShaderForge.SFN_Multiply,id:586,x:33052,y:33157,varname:node_586,prsc:2|A-9687-OUT,B-6165-OUT;n:type:ShaderForge.SFN_Power,id:9369,x:33239,y:33213,varname:node_9369,prsc:2|VAL-586-OUT,EXP-2778-OUT;n:type:ShaderForge.SFN_Vector1,id:6165,x:32877,y:33239,varname:node_6165,prsc:2,v1:2;n:type:ShaderForge.SFN_Vector1,id:2778,x:33052,y:33301,varname:node_2778,prsc:2,v1:4;n:type:ShaderForge.SFN_Clamp01,id:3059,x:33395,y:33233,varname:node_3059,prsc:2|IN-9369-OUT;n:type:ShaderForge.SFN_Tex2d,id:2448,x:31940,y:31537,varname:node_2448,prsc:2,ntxv:0,isnm:False|UVIN-2309-UVOUT,TEX-7382-TEX;n:type:ShaderForge.SFN_Tex2d,id:2403,x:31951,y:31695,varname:node_2403,prsc:2,ntxv:0,isnm:False|UVIN-6262-UVOUT,TEX-7382-TEX;n:type:ShaderForge.SFN_Panner,id:2309,x:31640,y:31494,varname:node_2309,prsc:2,spu:-0.1,spv:0|UVIN-6192-OUT,DIST-7486-OUT;n:type:ShaderForge.SFN_Panner,id:6262,x:31657,y:31662,varname:node_6262,prsc:2,spu:0.05,spv:0|UVIN-7434-UVOUT,DIST-7486-OUT;n:type:ShaderForge.SFN_Multiply,id:6192,x:31423,y:31494,varname:node_6192,prsc:2|A-3002-OUT,B-7434-UVOUT;n:type:ShaderForge.SFN_Vector1,id:3002,x:31146,y:31507,varname:node_3002,prsc:2,v1:1.1;n:type:ShaderForge.SFN_Multiply,id:8600,x:32327,y:31620,varname:node_8600,prsc:2|A-2448-A,B-2403-A,C-8011-OUT;n:type:ShaderForge.SFN_Vector1,id:8011,x:32208,y:31819,varname:node_8011,prsc:2,v1:2;n:type:ShaderForge.SFN_Power,id:7952,x:32647,y:31666,varname:node_7952,prsc:2|VAL-8600-OUT,EXP-6717-OUT;n:type:ShaderForge.SFN_Vector1,id:6717,x:32499,y:31896,varname:node_6717,prsc:2,v1:3;n:type:ShaderForge.SFN_Multiply,id:8216,x:32804,y:31927,varname:node_8216,prsc:2|A-7952-OUT,B-3221-OUT;n:type:ShaderForge.SFN_Vector1,id:3221,x:32537,y:31931,varname:node_3221,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:5022,x:32482,y:32878,varname:node_5022,prsc:2|A-3859-OUT,B-8799-OUT;n:type:ShaderForge.SFN_Vector1,id:8799,x:32223,y:33022,varname:node_8799,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:1543,x:31325,y:32556,varname:node_1543,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:7805,x:32727,y:32449,varname:node_7805,prsc:2|A-9556-OUT,B-5537-OUT;n:type:ShaderForge.SFN_Vector1,id:5537,x:32424,y:32509,varname:node_5537,prsc:2,v1:0.25;n:type:ShaderForge.SFN_Vector2,id:1988,x:31102,y:31270,varname:node_1988,prsc:2,v1:6,v2:6;n:type:ShaderForge.SFN_Vector2,id:9235,x:31102,y:31167,varname:node_9235,prsc:2,v1:7,v2:7;n:type:ShaderForge.SFN_Multiply,id:4263,x:31380,y:31178,varname:node_4263,prsc:2|A-9235-OUT,B-7434-UVOUT;n:type:ShaderForge.SFN_Multiply,id:4147,x:31391,y:31330,varname:node_4147,prsc:2|A-1988-OUT,B-7434-UVOUT;n:type:ShaderForge.SFN_Panner,id:4324,x:31640,y:31186,varname:node_4324,prsc:2,spu:-0.5,spv:-0.85|UVIN-4263-OUT,DIST-5843-OUT;n:type:ShaderForge.SFN_Panner,id:4284,x:31640,y:31351,varname:node_4284,prsc:2,spu:0.45,spv:-1.05|UVIN-4147-OUT,DIST-5843-OUT;n:type:ShaderForge.SFN_Vector1,id:6241,x:31186,y:32529,varname:node_6241,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:8504,x:32267,y:31380,varname:node_8504,prsc:2|A-4294-B,B-7344-B,C-2908-OUT;n:type:ShaderForge.SFN_Tex2d,id:4294,x:31933,y:31015,varname:node_4294,prsc:2,ntxv:0,isnm:False|UVIN-4324-UVOUT,TEX-7382-TEX;n:type:ShaderForge.SFN_Tex2d,id:7344,x:31950,y:31296,varname:node_7344,prsc:2,ntxv:0,isnm:False|UVIN-4284-UVOUT,TEX-7382-TEX;n:type:ShaderForge.SFN_Multiply,id:5843,x:31440,y:32383,varname:node_5843,prsc:2|A-7486-OUT,B-6241-OUT;n:type:ShaderForge.SFN_Add,id:6513,x:32735,y:32093,varname:node_6513,prsc:2|A-8504-OUT,B-1218-OUT;n:type:ShaderForge.SFN_Vector1,id:2908,x:32079,y:31519,varname:node_2908,prsc:2,v1:2;n:type:ShaderForge.SFN_Clamp01,id:8960,x:33460,y:32471,varname:node_8960,prsc:2|IN-2027-OUT;proporder:715-7382-8322-6167-2682-9287-1636;pass:END;sub:END;*/

Shader "BSE/Item/IonBeam/Beam V2" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _Texture ("Texture", 2D) = "white" {}
        _Fresnel ("Fresnel", Float ) = 1
        _speed ("speed", Float ) = 0.1
        _Interieur ("Interieur", Float ) = 0.25
        [MaterialToggle] _Center ("Center", Float ) = 0
        _FadeCenter ("Fade Center", Float ) = 0
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
            uniform float _Fresnel;
            uniform float _Interieur;
            uniform float _speed;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform fixed _Center;
            uniform float _FadeCenter;
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
                float2 node_3782 = (i.uv0+node_7486*float2(0,-0.5));
                float4 node_9056 = tex2D(_Texture,TRANSFORM_TEX(node_3782, _Texture));
                float2 node_859 = ((i.uv0*float2(2,2))+node_7486*float2(0,-2));
                float4 node_2268 = tex2D(_Texture,TRANSFORM_TEX(node_859, _Texture));
                float node_5843 = (node_7486*1.5);
                float2 node_4324 = ((float2(7,7)*i.uv0)+node_5843*float2(-0.5,-0.85));
                float4 node_4294 = tex2D(_Texture,TRANSFORM_TEX(node_4324, _Texture));
                float2 node_4284 = ((float2(6,6)*i.uv0)+node_5843*float2(0.45,-1.05));
                float4 node_7344 = tex2D(_Texture,TRANSFORM_TEX(node_4284, _Texture));
                float2 node_8708 = ((float2(3,3)*i.uv0)+node_7486*float2(-0.5,-0.85));
                float4 node_5107 = tex2D(_Texture,TRANSFORM_TEX(node_8708, _Texture));
                float2 node_6939 = ((float2(2,2)*i.uv0)+node_7486*float2(0.45,-1.05));
                float4 node_9530 = tex2D(_Texture,TRANSFORM_TEX(node_6939, _Texture));
                float2 node_2309 = ((1.1*i.uv0)+node_7486*float2(-0.1,0));
                float4 node_2448 = tex2D(_Texture,TRANSFORM_TEX(node_2309, _Texture));
                float2 node_6262 = (i.uv0+node_7486*float2(0.05,0));
                float4 node_2403 = tex2D(_Texture,TRANSFORM_TEX(node_6262, _Texture));
                float3 node_6217 = (saturate((((node_9056.r+node_2268.r)*0.25)+(((node_4294.b*node_7344.b*2.0)+(node_5107.b*node_9530.b*5.0))*pow((pow(1.0-max(0,dot(normalDirection, viewDirection)),0.0)*_Interieur),2.0))+saturate((pow((pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnel)*1.0),5.0)*1.0))+(pow((node_2448.a*node_2403.a*2.0),3.0)*0.5)))*_Color.rgb*_Color.a);
                float3 emissive = lerp( node_6217, (node_6217*saturate(pow(((1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_FadeCenter))*2.0),4.0))), _Center );
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
