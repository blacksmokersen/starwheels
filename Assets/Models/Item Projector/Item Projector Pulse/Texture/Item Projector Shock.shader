// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:35899,y:32578,varname:node_4795,prsc:2|emission-1881-OUT,alpha-3661-OUT,refract-8083-OUT;n:type:ShaderForge.SFN_Tex2d,id:1570,x:32746,y:32382,varname:node_1570,prsc:2,ntxv:0,isnm:False|UVIN-2287-UVOUT,TEX-2940-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:2940,x:32502,y:32433,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_2940,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:7453,x:32746,y:32212,varname:node_7453,prsc:2,ntxv:0,isnm:False|UVIN-5844-OUT,TEX-2940-TEX;n:type:ShaderForge.SFN_Multiply,id:3201,x:33155,y:32300,varname:node_3201,prsc:2|A-7453-R,B-1570-R,C-4590-OUT,D-4590-OUT;n:type:ShaderForge.SFN_TexCoord,id:2287,x:32055,y:32123,varname:node_2287,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:5844,x:32251,y:32024,varname:node_5844,prsc:2|A-2795-OUT,B-2287-UVOUT;n:type:ShaderForge.SFN_Vector1,id:2795,x:32058,y:32016,varname:node_2795,prsc:2,v1:2;n:type:ShaderForge.SFN_Power,id:297,x:33310,y:32226,varname:node_297,prsc:2|VAL-3201-OUT,EXP-2820-OUT;n:type:ShaderForge.SFN_Vector1,id:2820,x:33089,y:32157,varname:node_2820,prsc:2,v1:10;n:type:ShaderForge.SFN_Vector1,id:4590,x:32906,y:32351,varname:node_4590,prsc:2,v1:5;n:type:ShaderForge.SFN_Clamp01,id:4840,x:33494,y:32317,varname:node_4840,prsc:2|IN-297-OUT;n:type:ShaderForge.SFN_Multiply,id:1355,x:33681,y:32366,varname:node_1355,prsc:2|A-4840-OUT,B-25-OUT;n:type:ShaderForge.SFN_Vector1,id:25,x:33458,y:32447,varname:node_25,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:1881,x:34621,y:32581,varname:node_1881,prsc:2|A-7697-OUT,B-7398-OUT,C-4610-RGB,D-2185-RGB,E-8035-G;n:type:ShaderForge.SFN_Vector1,id:7398,x:34014,y:32665,varname:node_7398,prsc:2,v1:2;n:type:ShaderForge.SFN_Color,id:4610,x:33802,y:32621,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_4610,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_VertexColor,id:2185,x:34332,y:32682,varname:node_2185,prsc:2;n:type:ShaderForge.SFN_Clamp01,id:7697,x:34097,y:32484,varname:node_7697,prsc:2|IN-1355-OUT;n:type:ShaderForge.SFN_Append,id:7180,x:34306,y:32795,varname:node_7180,prsc:2|A-7453-R,B-1570-R;n:type:ShaderForge.SFN_Multiply,id:8083,x:34833,y:32873,varname:node_8083,prsc:2|A-7180-OUT,B-2185-A,C-673-OUT,D-8035-G;n:type:ShaderForge.SFN_ValueProperty,id:673,x:34330,y:32994,ptovrint:False,ptlb:Refraction,ptin:_Refraction,varname:node_673,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:8716,x:34740,y:32714,varname:node_8716,prsc:2|A-4610-A,B-2185-A,C-7697-OUT,D-8035-G;n:type:ShaderForge.SFN_Tex2d,id:8035,x:32749,y:32032,varname:node_8035,prsc:2,ntxv:0,isnm:False|TEX-2940-TEX;n:type:ShaderForge.SFN_Clamp01,id:3661,x:35420,y:32743,varname:node_3661,prsc:2|IN-3245-OUT;n:type:ShaderForge.SFN_Multiply,id:3245,x:35058,y:32723,varname:node_3245,prsc:2|A-4267-OUT,B-8716-OUT;n:type:ShaderForge.SFN_Vector1,id:4267,x:34807,y:32644,varname:node_4267,prsc:2,v1:3;proporder:2940-4610-673;pass:END;sub:END;*/

Shader "TIAB/Item Projector/Shock V2" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _Refraction ("Refraction", Float ) = 0.1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
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
            uniform sampler2D _GrabTexture;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform float4 _Color;
            uniform float _Refraction;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float2 node_5844 = (2.0*i.uv0);
                float4 node_7453 = tex2D(_Texture,TRANSFORM_TEX(node_5844, _Texture));
                float4 node_1570 = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float4 node_8035 = tex2D(_Texture,TRANSFORM_TEX(i.uv0, _Texture));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (float2(node_7453.r,node_1570.r)*i.vertexColor.a*_Refraction*node_8035.g);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float node_4590 = 5.0;
                float node_1355 = (saturate(pow((node_7453.r*node_1570.r*node_4590*node_4590),10.0))*0.2);
                float node_7697 = saturate(node_1355);
                float3 emissive = (node_7697*2.0*_Color.rgb*i.vertexColor.rgb*node_8035.g);
                float3 finalColor = emissive;
                float node_3661 = saturate((3.0*(_Color.a*i.vertexColor.a*node_7697*node_8035.g)));
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,node_3661),1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0.5,0.5,0.5,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
