// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:34245,y:32724,varname:node_4013,prsc:2|diff-762-OUT,spec-1655-R,gloss-1655-A,emission-5286-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:6940,x:32271,y:32752,ptovrint:False,ptlb:Emissive,ptin:_Emissive,varname:node_6940,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7ba125ca47875aa4ea97d9396eadf790,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2975,x:32925,y:33218,varname:node_2975,prsc:2,tex:7ba125ca47875aa4ea97d9396eadf790,ntxv:0,isnm:False|UVIN-4188-UVOUT,TEX-6940-TEX;n:type:ShaderForge.SFN_TexCoord,id:8623,x:32056,y:33354,varname:node_8623,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:2954,x:32343,y:33277,varname:node_2954,prsc:2|A-1158-OUT,B-8623-UVOUT;n:type:ShaderForge.SFN_Vector1,id:1158,x:32056,y:33233,varname:node_1158,prsc:2,v1:1.1;n:type:ShaderForge.SFN_Panner,id:5899,x:32604,y:33273,varname:node_5899,prsc:2,spu:0,spv:1|UVIN-2954-OUT,DIST-6551-OUT;n:type:ShaderForge.SFN_Time,id:4396,x:32060,y:33649,varname:node_4396,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6551,x:32334,y:33601,varname:node_6551,prsc:2|A-4396-TSL,B-8458-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8458,x:32084,y:33865,ptovrint:False,ptlb:Speed_Wave,ptin:_Speed_Wave,varname:node_8458,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Panner,id:4188,x:32604,y:33444,varname:node_4188,prsc:2,spu:0.5,spv:1|UVIN-8623-UVOUT,DIST-6551-OUT;n:type:ShaderForge.SFN_Multiply,id:4684,x:33273,y:33133,varname:node_4684,prsc:2|A-8607-A,B-2975-A;n:type:ShaderForge.SFN_Add,id:9464,x:33620,y:33322,varname:node_9464,prsc:2|A-4684-OUT,B-1638-RGB;n:type:ShaderForge.SFN_Tex2d,id:1638,x:33251,y:33424,varname:node_1638,prsc:2,tex:7ba125ca47875aa4ea97d9396eadf790,ntxv:0,isnm:False|TEX-6940-TEX;n:type:ShaderForge.SFN_Multiply,id:3732,x:33849,y:33226,varname:node_3732,prsc:2|A-3448-OUT,B-9464-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3448,x:33663,y:33184,ptovrint:False,ptlb:Emissive_Power,ptin:_Emissive_Power,varname:node_3448,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.8;n:type:ShaderForge.SFN_Multiply,id:3822,x:32969,y:32672,varname:node_3822,prsc:2|A-5439-OUT,B-789-RGB;n:type:ShaderForge.SFN_Vector1,id:5439,x:32513,y:32495,varname:node_5439,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Color,id:789,x:32513,y:32597,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_789,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8867924,c2:0.842833,c3:0.6567283,c4:1;n:type:ShaderForge.SFN_Multiply,id:762,x:33222,y:32580,varname:node_762,prsc:2|A-8400-RGB,B-3822-OUT;n:type:ShaderForge.SFN_Tex2d,id:8400,x:32891,y:32437,ptovrint:False,ptlb:Albedo,ptin:_Albedo,varname:node_8400,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:1d39bcf2e3450ed4e90b3a13dd6f865f,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1655,x:33655,y:32755,ptovrint:False,ptlb:Metallic,ptin:_Metallic,varname:node_1655,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:eaa14c71728654d43bf1f41031e11438,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8607,x:32912,y:33021,varname:node_8607,prsc:2,tex:7ba125ca47875aa4ea97d9396eadf790,ntxv:0,isnm:False|UVIN-5899-UVOUT,TEX-6940-TEX;n:type:ShaderForge.SFN_ValueProperty,id:6386,x:33117,y:33692,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_6386,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:5880,x:33416,y:33807,varname:node_5880,prsc:2|A-6386-OUT,B-4909-T;n:type:ShaderForge.SFN_Time,id:4909,x:33093,y:33829,varname:node_4909,prsc:2;n:type:ShaderForge.SFN_Sin,id:7621,x:33618,y:33807,varname:node_7621,prsc:2|IN-5880-OUT;n:type:ShaderForge.SFN_RemapRange,id:9267,x:33901,y:33597,varname:node_9267,prsc:2,frmn:0,frmx:1,tomn:1.1,tomx:1.3|IN-7621-OUT;n:type:ShaderForge.SFN_Multiply,id:5286,x:34098,y:33422,varname:node_5286,prsc:2|A-3732-OUT,B-9267-OUT;proporder:789-8400-1655-6940-8458-3448-6386;pass:END;sub:END;*/

Shader "Shader Forge/Fils" {
    Properties {
        _Color ("Color", Color) = (0.8867924,0.842833,0.6567283,1)
        _Albedo ("Albedo", 2D) = "black" {}
        _Metallic ("Metallic", 2D) = "white" {}
        _Emissive ("Emissive", 2D) = "white" {}
        _Speed_Wave ("Speed_Wave", Float ) = 3
        _Emissive_Power ("Emissive_Power", Float ) = 0.8
        _Speed ("Speed", Float ) = 1
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Emissive; uniform float4 _Emissive_ST;
            uniform float _Speed_Wave;
            uniform float _Emissive_Power;
            uniform float4 _Color;
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform sampler2D _Metallic; uniform float4 _Metallic_ST;
            uniform float _Speed;
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
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Metallic_var = tex2D(_Metallic,TRANSFORM_TEX(i.uv0, _Metallic));
                float gloss = _Metallic_var.a;
                float perceptualRoughness = 1.0 - _Metallic_var.a;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metallic_var.r;
                float specularMonochrome;
                float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float3 diffuseColor = (_Albedo_var.rgb*(1.5*_Color.rgb)); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float4 node_4396 = _Time;
                float node_6551 = (node_4396.r*_Speed_Wave);
                float2 node_5899 = ((1.1*i.uv0)+node_6551*float2(0,1));
                float4 node_8607 = tex2D(_Emissive,TRANSFORM_TEX(node_5899, _Emissive));
                float2 node_4188 = (i.uv0+node_6551*float2(0.5,1));
                float4 node_2975 = tex2D(_Emissive,TRANSFORM_TEX(node_4188, _Emissive));
                float4 node_1638 = tex2D(_Emissive,TRANSFORM_TEX(i.uv0, _Emissive));
                float4 node_4909 = _Time;
                float3 emissive = ((_Emissive_Power*((node_8607.a*node_2975.a)+node_1638.rgb))*(sin((_Speed*node_4909.g))*0.1999999+1.1));
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
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
            Cull Off
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _Emissive; uniform float4 _Emissive_ST;
            uniform float _Speed_Wave;
            uniform float _Emissive_Power;
            uniform float4 _Color;
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform sampler2D _Metallic; uniform float4 _Metallic_ST;
            uniform float _Speed;
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
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Metallic_var = tex2D(_Metallic,TRANSFORM_TEX(i.uv0, _Metallic));
                float gloss = _Metallic_var.a;
                float perceptualRoughness = 1.0 - _Metallic_var.a;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metallic_var.r;
                float specularMonochrome;
                float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float3 diffuseColor = (_Albedo_var.rgb*(1.5*_Color.rgb)); // Need this for specular when using metallic
                diffuseColor = DiffuseAndSpecularFromMetallic( diffuseColor, specularColor, specularColor, specularMonochrome );
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
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
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
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
