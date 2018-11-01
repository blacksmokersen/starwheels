// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:34605,y:32652,varname:node_4795,prsc:2|alpha-1499-OUT,refract-3630-OUT;n:type:ShaderForge.SFN_Tex2d,id:617,x:33049,y:33220,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_1026,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8269-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:249,x:31376,y:33130,varname:node_249,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:8673,x:31650,y:32996,varname:node_8673,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-249-UVOUT;n:type:ShaderForge.SFN_ArcTan2,id:4965,x:32119,y:33090,varname:node_4965,prsc:2,attp:2|A-3909-R,B-3909-G;n:type:ShaderForge.SFN_ComponentMask,id:3909,x:31905,y:33067,varname:node_3909,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-8673-OUT;n:type:ShaderForge.SFN_Length,id:42,x:32505,y:32852,varname:node_42,prsc:2|IN-8673-OUT;n:type:ShaderForge.SFN_Append,id:3879,x:32359,y:33115,varname:node_3879,prsc:2|A-6887-OUT,B-4965-OUT;n:type:ShaderForge.SFN_OneMinus,id:6887,x:32102,y:33242,varname:node_6887,prsc:2|IN-5575-OUT;n:type:ShaderForge.SFN_Distance,id:5575,x:31918,y:33227,varname:node_5575,prsc:2|A-249-UVOUT,B-6434-OUT;n:type:ShaderForge.SFN_Vector2,id:6434,x:31729,y:33266,varname:node_6434,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_Panner,id:8269,x:32864,y:33220,varname:node_8269,prsc:2,spu:0.5,spv:0.1|UVIN-3879-OUT,DIST-794-OUT;n:type:ShaderForge.SFN_OneMinus,id:5384,x:32764,y:32850,varname:node_5384,prsc:2|IN-42-OUT;n:type:ShaderForge.SFN_Multiply,id:7867,x:32953,y:32974,varname:node_7867,prsc:2|A-5384-OUT,B-1787-OUT,C-8132-OUT;n:type:ShaderForge.SFN_Vector1,id:1787,x:32764,y:32986,varname:node_1787,prsc:2,v1:3;n:type:ShaderForge.SFN_Clamp01,id:2004,x:33417,y:32863,varname:node_2004,prsc:2|IN-4782-OUT;n:type:ShaderForge.SFN_Time,id:4358,x:32359,y:33304,varname:node_4358,prsc:2;n:type:ShaderForge.SFN_Multiply,id:794,x:32647,y:33331,varname:node_794,prsc:2|A-4358-T,B-1134-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1134,x:32471,y:33507,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_1134,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Append,id:9260,x:33724,y:33115,varname:node_9260,prsc:2|A-617-R,B-617-R;n:type:ShaderForge.SFN_Multiply,id:3630,x:34130,y:33044,varname:node_3630,prsc:2|A-2004-OUT,B-9260-OUT,C-5634-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5634,x:33971,y:33253,ptovrint:False,ptlb:Strenght,ptin:_Strenght,varname:node_5634,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Vector1,id:1499,x:34257,y:32898,varname:node_1499,prsc:2,v1:0;n:type:ShaderForge.SFN_Power,id:4782,x:33212,y:32863,varname:node_4782,prsc:2|VAL-7867-OUT,EXP-3555-OUT;n:type:ShaderForge.SFN_Vector1,id:3555,x:32990,y:33103,varname:node_3555,prsc:2,v1:3;n:type:ShaderForge.SFN_Multiply,id:8132,x:32723,y:32725,varname:node_8132,prsc:2|A-1635-OUT,B-42-OUT;n:type:ShaderForge.SFN_Vector1,id:1635,x:32549,y:32712,varname:node_1635,prsc:2,v1:3;proporder:1134-617-5634;pass:END;sub:END;*/

Shader "TIAB/Item Projector/Distortion" {
    Properties {
        _Speed ("Speed", Float ) = 1
        _Texture ("Texture", 2D) = "white" {}
        _Strenght ("Strenght", Float ) = 0
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
            uniform float _Speed;
            uniform float _Strenght;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 projPos : TEXCOORD1;
                UNITY_FOG_COORDS(2)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                float2 node_8673 = (i.uv0*2.0+-1.0);
                float node_42 = length(node_8673);
                float4 node_4358 = _Time;
                float2 node_3909 = node_8673.rg;
                float2 node_8269 = (float2((1.0 - distance(i.uv0,float2(0.5,0.5))),((atan2(node_3909.r,node_3909.g)/6.28318530718)+0.5))+(node_4358.g*_Speed)*float2(0.5,0.1));
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_8269, _Texture));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + (saturate(pow(((1.0 - node_42)*3.0*(3.0*node_42)),3.0))*float2(_Texture_var.r,_Texture_var.r)*_Strenght);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
                float3 finalColor = 0;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,0.0),1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
