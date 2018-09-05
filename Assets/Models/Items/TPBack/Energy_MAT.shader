// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32830,y:33081,varname:node_3138,prsc:2|emission-4363-OUT;n:type:ShaderForge.SFN_Rotator,id:1046,x:32100,y:33206,varname:node_1046,prsc:2|UVIN-6332-UVOUT,SPD-1908-OUT;n:type:ShaderForge.SFN_TexCoord,id:6332,x:31440,y:33184,varname:node_6332,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Color,id:4464,x:32161,y:32918,ptovrint:False,ptlb:Color_copy,ptin:_Color_copy,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.3973834,c2:0.7684795,c3:0.8867924,c4:1;n:type:ShaderForge.SFN_Blend,id:4363,x:32439,y:33160,varname:node_4363,prsc:2,blmd:12,clmp:True|SRC-4464-RGB,DST-1046-UVOUT;n:type:ShaderForge.SFN_Slider,id:1908,x:31739,y:33587,ptovrint:False,ptlb:node_1908,ptin:_node_1908,varname:node_1908,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:10;n:type:ShaderForge.SFN_TexCoord,id:2538,x:32157,y:33569,varname:node_2538,prsc:2,uv:0,uaff:False;proporder:4464-1908;pass:END;sub:END;*/

Shader "Shader Forge/Energy_MAT" {
    Properties {
        _Color_copy ("Color_copy", Color) = (0.3973834,0.7684795,0.8867924,1)
        _node_1908 ("node_1908", Range(0, 10)) = 1
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color_copy;
            uniform float _node_1908;
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
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_1344 = _Time;
                float node_1046_ang = node_1344.g;
                float node_1046_spd = _node_1908;
                float node_1046_cos = cos(node_1046_spd*node_1046_ang);
                float node_1046_sin = sin(node_1046_spd*node_1046_ang);
                float2 node_1046_piv = float2(0.5,0.5);
                float2 node_1046 = (mul(i.uv0-node_1046_piv,float2x2( node_1046_cos, -node_1046_sin, node_1046_sin, node_1046_cos))+node_1046_piv);
                float3 emissive = saturate((_Color_copy.rgb > 0.5 ?  (1.0-(1.0-2.0*(_Color_copy.rgb-0.5))*(1.0-float3(node_1046,0.0))) : (2.0*_Color_copy.rgb*float3(node_1046,0.0))) );
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
