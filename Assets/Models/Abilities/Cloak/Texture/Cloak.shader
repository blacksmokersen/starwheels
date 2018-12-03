// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:False,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:34596,y:32646,varname:node_2865,prsc:2|diff-6343-OUT,spec-2695-R,gloss-2695-A,normal-5964-RGB,emission-6857-OUT,alpha-1079-OUT,refract-3788-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:33888,y:32629,varname:node_6343,prsc:2|A-7736-RGB,B-6665-RGB;n:type:ShaderForge.SFN_Color,id:6665,x:33695,y:32722,ptovrint:False,ptlb:Color,ptin:_Color,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5019608,c2:0.5019608,c3:0.5019608,c4:1;n:type:ShaderForge.SFN_Tex2d,id:7736,x:33695,y:32537,ptovrint:True,ptlb:Base Color,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5964,x:34181,y:32895,ptovrint:True,ptlb:Normal Map,ptin:_BumpMap,varname:_BumpMap,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_ValueProperty,id:9861,x:32890,y:33305,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_9752,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Append,id:8287,x:32876,y:33785,varname:node_8287,prsc:2|A-7959-OUT,B-878-OUT;n:type:ShaderForge.SFN_Multiply,id:6194,x:33318,y:33817,varname:node_6194,prsc:2|A-8287-OUT,B-1446-OUT,C-3728-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1446,x:32812,y:34038,ptovrint:False,ptlb:Refraction Power,ptin:_RefractionPower,varname:node_3333,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Tex2dAsset,id:9870,x:32047,y:34008,ptovrint:False,ptlb:Refraction,ptin:_Refraction,varname:node_758,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:372,x:32327,y:33815,varname:node_204,prsc:2,ntxv:0,isnm:False|UVIN-3588-UVOUT,TEX-9870-TEX;n:type:ShaderForge.SFN_Tex2d,id:1300,x:32376,y:34038,varname:node_5990,prsc:2,ntxv:0,isnm:False|UVIN-7982-UVOUT,TEX-9870-TEX;n:type:ShaderForge.SFN_Multiply,id:7959,x:32608,y:33830,varname:node_7959,prsc:2|A-372-R,B-1300-R;n:type:ShaderForge.SFN_Multiply,id:878,x:32590,y:34037,varname:node_878,prsc:2|A-372-G,B-1300-G;n:type:ShaderForge.SFN_Panner,id:3588,x:31918,y:33695,varname:node_3588,prsc:2,spu:-0.8,spv:1.1|UVIN-9348-OUT,DIST-1518-OUT;n:type:ShaderForge.SFN_Panner,id:7982,x:31918,y:33840,varname:node_7982,prsc:2,spu:1.05,spv:-0.9|UVIN-9146-UVOUT,DIST-1518-OUT;n:type:ShaderForge.SFN_TexCoord,id:9146,x:31485,y:33734,varname:node_9146,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector1,id:702,x:31474,y:33608,varname:node_702,prsc:2,v1:1.1;n:type:ShaderForge.SFN_Multiply,id:9348,x:31738,y:33632,varname:node_9348,prsc:2|A-702-OUT,B-9146-UVOUT;n:type:ShaderForge.SFN_Time,id:6163,x:31440,y:34044,varname:node_6163,prsc:2;n:type:ShaderForge.SFN_Multiply,id:1518,x:31652,y:34067,varname:node_1518,prsc:2|A-6163-T,B-4182-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4182,x:31509,y:34266,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_4332,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Clamp01,id:1471,x:33808,y:33650,varname:node_1471,prsc:2|IN-6194-OUT;n:type:ShaderForge.SFN_Fresnel,id:8207,x:32709,y:34294,varname:node_8207,prsc:2|NRM-6461-OUT,EXP-8431-OUT;n:type:ShaderForge.SFN_NormalVector,id:6461,x:32311,y:34273,prsc:2,pt:False;n:type:ShaderForge.SFN_ValueProperty,id:8431,x:32308,y:34579,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_362,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Vector1,id:6479,x:32862,y:34446,varname:node_6479,prsc:2,v1:0.01;n:type:ShaderForge.SFN_Add,id:3728,x:32965,y:34285,varname:node_3728,prsc:2|A-8207-OUT,B-6479-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1344,x:31852,y:33421,ptovrint:False,ptlb:Height Effect,ptin:_HeightEffect,varname:node_3381,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_FragmentPosition,id:3599,x:32441,y:33331,varname:node_3599,prsc:2;n:type:ShaderForge.SFN_Add,id:326,x:32888,y:33450,varname:node_326,prsc:2|A-5265-OUT,B-4100-OUT;n:type:ShaderForge.SFN_Floor,id:6352,x:33423,y:33468,varname:node_6352,prsc:2|IN-4661-OUT;n:type:ShaderForge.SFN_Clamp01,id:4661,x:33128,y:33450,varname:node_4661,prsc:2|IN-326-OUT;n:type:ShaderForge.SFN_Lerp,id:1079,x:34154,y:33226,varname:node_1079,prsc:2|A-9101-OUT,B-9861-OUT,T-6352-OUT;n:type:ShaderForge.SFN_Vector1,id:9101,x:32890,y:33216,varname:node_9101,prsc:2,v1:1;n:type:ShaderForge.SFN_Lerp,id:3788,x:34065,y:33548,varname:node_3788,prsc:2|A-6592-OUT,B-1471-OUT,T-6352-OUT;n:type:ShaderForge.SFN_Vector1,id:6592,x:33808,y:33453,varname:node_6592,prsc:2,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:6543,x:31897,y:33570,ptovrint:False,ptlb:Height objet,ptin:_Heightobjet,varname:_HeightEffect_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:4100,x:32362,y:33532,varname:node_4100,prsc:2|A-1344-OUT,B-6543-OUT;n:type:ShaderForge.SFN_Multiply,id:4797,x:33768,y:33070,varname:node_4797,prsc:2|A-4273-RGB,B-613-OUT;n:type:ShaderForge.SFN_Color,id:4273,x:33529,y:33094,ptovrint:False,ptlb:Separation Color,ptin:_SeparationColor,varname:node_4421,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Add,id:3579,x:32888,y:33654,varname:node_3579,prsc:2|A-5265-OUT,B-4100-OUT,C-9857-OUT;n:type:ShaderForge.SFN_Vector1,id:9857,x:32693,y:33741,varname:node_9857,prsc:2,v1:0.01;n:type:ShaderForge.SFN_Clamp01,id:2170,x:33068,y:33672,varname:node_2170,prsc:2|IN-3579-OUT;n:type:ShaderForge.SFN_Power,id:7206,x:33252,y:33645,varname:node_7206,prsc:2|VAL-2170-OUT,EXP-9185-OUT;n:type:ShaderForge.SFN_Vector1,id:9185,x:33004,y:33597,varname:node_9185,prsc:2,v1:50;n:type:ShaderForge.SFN_Clamp01,id:613,x:33413,y:33629,varname:node_613,prsc:2|IN-7206-OUT;n:type:ShaderForge.SFN_Lerp,id:6857,x:34176,y:33079,varname:node_6857,prsc:2|A-4797-OUT,B-6323-OUT,T-6352-OUT;n:type:ShaderForge.SFN_Vector1,id:6323,x:33893,y:33117,varname:node_6323,prsc:2,v1:0;n:type:ShaderForge.SFN_OneMinus,id:5265,x:32631,y:33383,varname:node_5265,prsc:2|IN-3599-Y;n:type:ShaderForge.SFN_Tex2d,id:2695,x:34037,y:32700,ptovrint:False,ptlb:Metalic,ptin:_Metalic,varname:node_2695,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;proporder:6665-7736-2695-5964-9870-1344-6543-4273-9861-1446-8431-4182;pass:END;sub:END;*/

Shader "BSE/Cloak" {
    Properties {
        _Color ("Color", Color) = (0.5019608,0.5019608,0.5019608,1)
        _MainTex ("Base Color", 2D) = "white" {}
        _Metalic ("Metalic", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _Refraction ("Refraction", 2D) = "white" {}
        _HeightEffect ("Height Effect", Float ) = 0
        _Heightobjet ("Height objet", Float ) = 0
        _SeparationColor ("Separation Color", Color) = (0.5,0.5,0.5,1)
        _Opacity ("Opacity", Float ) = 1
        _RefractionPower ("Refraction Power", Float ) = 0.5
        _Fresnel ("Fresnel", Float ) = 3
        _speed ("speed", Float ) = 0.5
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
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _Opacity;
            uniform float _RefractionPower;
            uniform sampler2D _Refraction; uniform float4 _Refraction_ST;
            uniform float _speed;
            uniform float _Fresnel;
            uniform float _HeightEffect;
            uniform float _Heightobjet;
            uniform float4 _SeparationColor;
            uniform sampler2D _Metalic; uniform float4 _Metalic_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 projPos : TEXCOORD7;
                UNITY_FOG_COORDS(8)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD9;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float node_6592 = 0.0;
                float4 node_6163 = _Time;
                float node_1518 = (node_6163.g*_speed);
                float2 node_3588 = ((1.1*i.uv0)+node_1518*float2(-0.8,1.1));
                float4 node_204 = tex2D(_Refraction,TRANSFORM_TEX(node_3588, _Refraction));
                float2 node_7982 = (i.uv0+node_1518*float2(1.05,-0.9));
                float4 node_5990 = tex2D(_Refraction,TRANSFORM_TEX(node_7982, _Refraction));
                float node_5265 = (1.0 - i.posWorld.g);
                float node_4100 = (_HeightEffect+_Heightobjet);
                float node_6352 = floor(saturate((node_5265+node_4100)));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + lerp(float2(node_6592,node_6592),saturate((float2((node_204.r*node_5990.r),(node_204.g*node_5990.g))*_RefractionPower*(pow(1.0-max(0,dot(i.normalDir, viewDirection)),_Fresnel)+0.01))),node_6352);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Metalic_var = tex2D(_Metalic,TRANSFORM_TEX(i.uv0, _Metalic));
                float gloss = _Metalic_var.a;
                float perceptualRoughness = 1.0 - _Metalic_var.a;
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
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metalic_var.r;
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuseColor = (_MainTex_var.rgb*_Color.rgb); // Need this for specular when using metallic
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
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float node_6323 = 0.0;
                float3 emissive = lerp((_SeparationColor.rgb*saturate(pow(saturate((node_5265+node_4100+0.01)),50.0))),float3(node_6323,node_6323,node_6323),node_6352);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,lerp(1.0,_Opacity,node_6352)),1);
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _BumpMap; uniform float4 _BumpMap_ST;
            uniform float _Opacity;
            uniform float _RefractionPower;
            uniform sampler2D _Refraction; uniform float4 _Refraction_ST;
            uniform float _speed;
            uniform float _Fresnel;
            uniform float _HeightEffect;
            uniform float _Heightobjet;
            uniform float4 _SeparationColor;
            uniform sampler2D _Metalic; uniform float4 _Metalic_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 projPos : TEXCOORD7;
                LIGHTING_COORDS(8,9)
                UNITY_FOG_COORDS(10)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _BumpMap_var = UnpackNormal(tex2D(_BumpMap,TRANSFORM_TEX(i.uv0, _BumpMap)));
                float3 normalLocal = _BumpMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float node_6592 = 0.0;
                float4 node_6163 = _Time;
                float node_1518 = (node_6163.g*_speed);
                float2 node_3588 = ((1.1*i.uv0)+node_1518*float2(-0.8,1.1));
                float4 node_204 = tex2D(_Refraction,TRANSFORM_TEX(node_3588, _Refraction));
                float2 node_7982 = (i.uv0+node_1518*float2(1.05,-0.9));
                float4 node_5990 = tex2D(_Refraction,TRANSFORM_TEX(node_7982, _Refraction));
                float node_5265 = (1.0 - i.posWorld.g);
                float node_4100 = (_HeightEffect+_Heightobjet);
                float node_6352 = floor(saturate((node_5265+node_4100)));
                float2 sceneUVs = (i.projPos.xy / i.projPos.w) + lerp(float2(node_6592,node_6592),saturate((float2((node_204.r*node_5990.r),(node_204.g*node_5990.g))*_RefractionPower*(pow(1.0-max(0,dot(i.normalDir, viewDirection)),_Fresnel)+0.01))),node_6352);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Metalic_var = tex2D(_Metalic,TRANSFORM_TEX(i.uv0, _Metalic));
                float gloss = _Metalic_var.a;
                float perceptualRoughness = 1.0 - _Metalic_var.a;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float3 specularColor = _Metalic_var.r;
                float specularMonochrome;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuseColor = (_MainTex_var.rgb*_Color.rgb); // Need this for specular when using metallic
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
                fixed4 finalRGBA = fixed4(finalColor * lerp(1.0,_Opacity,node_6352),0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _HeightEffect;
            uniform float _Heightobjet;
            uniform float4 _SeparationColor;
            uniform sampler2D _Metalic; uniform float4 _Metalic_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float node_5265 = (1.0 - i.posWorld.g);
                float node_4100 = (_HeightEffect+_Heightobjet);
                float node_6323 = 0.0;
                float node_6352 = floor(saturate((node_5265+node_4100)));
                o.Emission = lerp((_SeparationColor.rgb*saturate(pow(saturate((node_5265+node_4100+0.01)),50.0))),float3(node_6323,node_6323,node_6323),node_6352);
                
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffColor = (_MainTex_var.rgb*_Color.rgb);
                float specularMonochrome;
                float3 specColor;
                float4 _Metalic_var = tex2D(_Metalic,TRANSFORM_TEX(i.uv0, _Metalic));
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, _Metalic_var.r, specColor, specularMonochrome );
                float roughness = 1.0 - _Metalic_var.a;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
