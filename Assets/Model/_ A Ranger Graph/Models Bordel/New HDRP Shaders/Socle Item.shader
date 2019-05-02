// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "NewShader/Socle"
{
    Properties
    {
		_BaseTexture("Base Texture", 2D) = "white" {}
		_Color("Color", Color) = (0,0,0,0)
		_Metalic("Metalic", 2D) = "white" {}
		_Normal("Normal", 2D) = "white" {}
		_EmissivePower("Emissive Power", Float) = 1
		_Speed("Speed", Float) = 1
		_Texture0("Texture 0", 2D) = "white" {}
		[HDR]_ColorEmissive("ColorEmissive", Color) = (0,0,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
    }

    SubShader
    {
        Tags { "RenderPipeline"="HDRenderPipeline" "RenderType"="Opaque" "Queue"="Geometry" }
        
		Cull Back
		Blend One Zero
		ZTest LEqual
		ZWrite On
		ZClip [_ZClip]

		HLSLINCLUDE
		#pragma target 4.5
		#pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
		#pragma multi_compile_instancing
		#pragma instancing_options renderinglayer
		#pragma multi_compile _ LOD_FADE_CROSSFADE

		struct GlobalSurfaceDescription
		{
			//Standard
			float3 Albedo;
			float3 Normal;
			float3 Specular;
			float Metallic;
			float3 Emission;
			float Smoothness;
			float Occlusion;
			float Alpha;
			float AlphaClipThreshold;
			float CoatMask;
			//SSS
			uint DiffusionProfile;
			float SubsurfaceMask;
			//Transmission
			float Thickness;
			// Anisotropic
			float3 TangentWS;
			float Anisotropy; 
			//Iridescence
			float IridescenceThickness;
			float IridescenceMask;
			// Transparency
			float IndexOfRefraction;
			float3 TransmittanceColor;
			float TransmittanceAbsorptionDistance;
			float TransmittanceMask;
		};

		struct AlphaSurfaceDescription
		{
			float Alpha;
			float AlphaClipThreshold;
		};

		ENDHLSL
		
        Pass
        {
			
            Name "GBuffer"
            Tags { "LightMode"="GBuffer" }    
			Stencil
			{
				Ref 2
				WriteMask 7
				Comp Always
				Pass Replace
				Fail Keep
				ZFail Keep
			}

     
            HLSLPROGRAM
        	//#define UNITY_MATERIAL_LIT
			#pragma vertex Vert
			#pragma fragment Frag
			
			#define _NORMALMAP 1

		
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Wind.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
            #define SHADERPASS SHADERPASS_GBUFFER
            #pragma multi_compile _ LIGHTMAP_ON
            #pragma multi_compile _ DIRLIGHTMAP_COMBINED
            #pragma multi_compile _ DYNAMICLIGHTMAP_ON
            #pragma multi_compile _ SHADOWS_SHADOWMASK
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD2
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_TANGENT_TO_WORLD
            #define VARYINGS_NEED_TEXCOORD1
            #define VARYINGS_NEED_TEXCOORD2
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

			CBUFFER_START(UnityPerMaterial)
			uniform float4 _Color;
			uniform sampler2D _BaseTexture;
			uniform float4 _BaseTexture_ST;
			uniform sampler2D _Normal;
			uniform float4 _Normal_ST;
			uniform float4 _ColorEmissive;
			uniform sampler2D _Texture0;
			uniform float _Speed;
			uniform float4 _Texture0_ST;
			uniform float _EmissivePower;
			uniform sampler2D _Metalic;
			uniform float4 _Metalic_ST;
			CBUFFER_END
			
            //float3x3 BuildWorldToTangent(float4 tangentWS, float3 normalWS)
            //{
        	//    float3 unnormalizedNormalWS = normalWS;
            //    float renormFactor = 1.0 / length(unnormalizedNormalWS);
            //    float3x3 worldToTangent = CreateWorldToTangent(unnormalizedNormalWS, tangentWS.xyz, tangentWS.w > 0.0 ? 1.0 : -1.0);
            //    worldToTangent[0] = worldToTangent[0] * renormFactor;
            //    worldToTangent[1] = worldToTangent[1] * renormFactor;
            //    worldToTangent[2] = worldToTangent[2] * renormFactor;
            //    return worldToTangent;
            //}

            struct AttributesMesh 
			{
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float4 uv1 : TEXCOORD1;
                float4 uv2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				#if UNITY_ANY_INSTANCING_ENABLED
				uint instanceID : INSTANCEID_SEMANTIC;
				#endif
            };

            struct PackedVaryingsMeshToPS 
			{
                float4 positionCS : SV_Position;
                float3 interp00 : TEXCOORD0;
                float3 interp01 : TEXCOORD1;
                float4 interp02 : TEXCOORD2;
                float4 interp03 : TEXCOORD3;
				float4 interp04 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				#if UNITY_ANY_INSTANCING_ENABLED
				uint instanceID : INSTANCEID_SEMANTIC;
				#endif
            };
        
			void BuildSurfaceData ( FragInputs fragInputs, GlobalSurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData )
			{
				ZERO_INITIALIZE ( SurfaceData, surfaceData );

				float3 normalTS = float3( 0.0f, 0.0f, 1.0f );
				normalTS = surfaceDescription.Normal;
				GetNormalWS ( fragInputs, normalTS, surfaceData.normalWS );

				surfaceData.ambientOcclusion = 1.0f;

				surfaceData.baseColor = surfaceDescription.Albedo;
				surfaceData.perceptualSmoothness = surfaceDescription.Smoothness;
				surfaceData.ambientOcclusion = surfaceDescription.Occlusion;

				surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;

#ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
				surfaceData.specularColor = surfaceDescription.Specular;
#else
				surfaceData.metallic = surfaceDescription.Metallic;
#endif

#if defined(_MATERIAL_FEATURE_SUBSURFACE_SCATTERING) || defined(_MATERIAL_FEATURE_TRANSMISSION)
				surfaceData.diffusionProfile = surfaceDescription.DiffusionProfile;
#endif

#ifdef _MATERIAL_FEATURE_SUBSURFACE_SCATTERING
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SUBSURFACE_SCATTERING;
				surfaceData.subsurfaceMask = surfaceDescription.SubsurfaceMask;
#else
				surfaceData.subsurfaceMask = 1.0f;
#endif

#ifdef _MATERIAL_FEATURE_TRANSMISSION
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_TRANSMISSION;
				surfaceData.thickness = surfaceDescription.Thickness;
#endif

				surfaceData.tangentWS = normalize ( fragInputs.worldToTangent[ 0 ].xyz );
				surfaceData.tangentWS = Orthonormalize ( surfaceData.tangentWS, surfaceData.normalWS );

#ifdef _MATERIAL_FEATURE_ANISOTROPY
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_ANISOTROPY;
				surfaceData.anisotropy = surfaceDescription.Anisotropy;

#else
				surfaceData.anisotropy = 0;
#endif

#ifdef _MATERIAL_FEATURE_CLEAR_COAT
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_CLEAR_COAT;
				surfaceData.coatMask = surfaceDescription.CoatMask;
#else
				surfaceData.coatMask = 0.0f;
#endif

#ifdef _MATERIAL_FEATURE_IRIDESCENCE
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_IRIDESCENCE;
				surfaceData.iridescenceThickness = surfaceDescription.IridescenceThickness;
				surfaceData.iridescenceMask = surfaceDescription.IridescenceMask;
#else
				surfaceData.iridescenceThickness = 0.0;
				surfaceData.iridescenceMask = 1.0;
#endif

				//ASE CUSTOM TAG
#ifdef _MATERIAL_FEATURE_TRANSPARENCY
				surfaceData.ior = surfaceDescription.IndexOfRefraction;
				surfaceData.transmittanceColor = surfaceDescription.TransmittanceColor;
				surfaceData.atDistance = surfaceDescription.TransmittanceAbsorptionDistance;
				surfaceData.transmittanceMask = surfaceDescription.TransmittanceMask;
#else
				surfaceData.ior = 1.0;
				surfaceData.transmittanceColor = float3( 1.0, 1.0, 1.0 );
				surfaceData.atDistance = 1000000.0;
				surfaceData.transmittanceMask = 0.0;
#endif

				surfaceData.specularOcclusion = 1.0;

#if defined(_BENTNORMALMAP) && defined(_ENABLESPECULAROCCLUSION)
				surfaceData.specularOcclusion = GetSpecularOcclusionFromBentAO ( V, bentNormalWS, surfaceData );
#elif defined(_MASKMAP)
				surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion ( NdotV, surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness ( surfaceData.perceptualSmoothness ) );
#endif
			}

            void GetSurfaceAndBuiltinData( GlobalSurfaceDescription surfaceDescription , FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
            {
        
#if _ALPHATEST_ON
				DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
#endif
				BuildSurfaceData( fragInputs, surfaceDescription, V, surfaceData );
        
                // Builtin Data
                // For back lighting we use the oposite vertex normal 
                InitBuiltinData(surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.worldToTangent[2], fragInputs.positionRWS, fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
        
				builtinData.emissiveColor =             surfaceDescription.Emission;
                builtinData.distortion =                float2(0.0, 0.0);           // surfaceDescription.Distortion -- if distortion pass
                builtinData.distortionBlur =            0.0;                        // surfaceDescription.DistortionBlur -- if distortion pass
                builtinData.depthOffset =               0.0;                        // ApplyPerPixelDisplacement(input, V, layerTexCoord, blendMasks); #ifdef _DEPTHOFFSET_ON : ApplyDepthOffsetPositionInput(V, depthOffset, GetWorldToHClipMatrix(), posInput);
        
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);            
            }
        
			PackedVaryingsMeshToPS Vert ( AttributesMesh inputMesh  )
			{
				PackedVaryingsMeshToPS outputPackedVaryingsMeshToPS;

				UNITY_SETUP_INSTANCE_ID ( inputMesh );
				UNITY_TRANSFER_INSTANCE_ID ( inputMesh, outputPackedVaryingsMeshToPS );

				#if UNITY_ANY_INSTANCING_ENABLED
				outputPackedVaryingsMeshToPS.instanceID = inputMesh.instanceID;
				#endif

				outputPackedVaryingsMeshToPS.ase_texcoord5.xy = inputMesh.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				outputPackedVaryingsMeshToPS.ase_texcoord5.zw = 0;
				inputMesh.positionOS.xyz +=  float3( 0, 0, 0 ) ;
				inputMesh.normalOS =  inputMesh.normalOS ;

				float3 positionRWS = TransformObjectToWorld ( inputMesh.positionOS.xyz );
				float3 normalWS = TransformObjectToWorldNormal ( inputMesh.normalOS );
				float4 tangentWS = float4( TransformObjectToWorldDir ( inputMesh.tangentOS.xyz ), inputMesh.tangentOS.w );
				float4 positionCS = TransformWorldToHClip ( positionRWS );

				outputPackedVaryingsMeshToPS.positionCS = positionCS;
				outputPackedVaryingsMeshToPS.interp00.xyz = positionRWS;
				outputPackedVaryingsMeshToPS.interp01.xyz = normalWS;
				outputPackedVaryingsMeshToPS.interp02.xyzw = tangentWS;
				outputPackedVaryingsMeshToPS.interp03 = inputMesh.uv1;
				outputPackedVaryingsMeshToPS.interp04 = inputMesh.uv2;
			
				return outputPackedVaryingsMeshToPS;
			}

			void Frag ( PackedVaryingsMeshToPS packedInput, 
						OUTPUT_GBUFFER ( outGBuffer )
						#ifdef _DEPTHOFFSET_ON
						, out float outputDepth : SV_Depth
						#endif
						 
						)
			{
				FragInputs input;
				ZERO_INITIALIZE ( FragInputs, input );
				input.worldToTangent = k_identity3x3;
				
				float3 positionRWS = packedInput.interp00.xyz;
				float3 normalWS = packedInput.interp01.xyz;
				float4 tangentWS = packedInput.interp02.xyzw;
			
				input.positionSS = packedInput.positionCS;
				input.positionRWS = positionRWS;
				input.worldToTangent = BuildWorldToTangent ( tangentWS, normalWS );
				input.texCoord1 = packedInput.interp03;
				input.texCoord2 = packedInput.interp04;

				// input.positionSS is SV_Position
				PositionInputs posInput = GetPositionInput ( input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS );

				float3 normalizedWorldViewDir = GetWorldSpaceNormalizeViewDir ( input.positionRWS );

				SurfaceData surfaceData;
				BuiltinData builtinData;

				GlobalSurfaceDescription surfaceDescription = ( GlobalSurfaceDescription ) 0;
				float2 uv_BaseTexture = packedInput.ase_texcoord5.xy * _BaseTexture_ST.xy + _BaseTexture_ST.zw;
				
				float2 uv_Normal = packedInput.ase_texcoord5.xy * _Normal_ST.xy + _Normal_ST.zw;
				
				float2 uv39 = packedInput.ase_texcoord5.xy * float2( 1,1 ) + float2( 0,0 );
				float temp_output_45_0 = ( _Time.y * _Speed );
				float temp_output_37_0 = ( 0.2 * temp_output_45_0 );
				float cos34 = cos( -temp_output_37_0 );
				float sin34 = sin( -temp_output_37_0 );
				float2 rotator34 = mul( ( 1.1 * uv39 ) - float2( 0.25,0.25 ) , float2x2( cos34 , -sin34 , sin34 , cos34 )) + float2( 0.25,0.25 );
				float cos35 = cos( temp_output_37_0 );
				float sin35 = sin( temp_output_37_0 );
				float2 rotator35 = mul( uv39 - float2( 0,0 ) , float2x2( cos35 , -sin35 , sin35 , cos35 )) + float2( 0,0 );
				float2 uv_Texture0 = packedInput.ase_texcoord5.xy * _Texture0_ST.xy + _Texture0_ST.zw;
				float4 tex2DNode18 = tex2D( _Texture0, uv_Texture0 );
				float clampResult52 = clamp( pow( ( 3.0 * tex2DNode18.g ) , 3.0 ) , 0.0 , 1.0 );
				float lerpResult51 = lerp( 0.0 , ( tex2D( _Texture0, rotator34 ).a * tex2D( _Texture0, rotator35 ).a ) , clampResult52);
				float temp_output_19_0 = (0.9 + (sin( temp_output_45_0 ) - 0.0) * (1.3 - 0.9) / (1.0 - 0.0));
				
				float2 uv_Metalic = packedInput.ase_texcoord5.xy * _Metalic_ST.xy + _Metalic_ST.zw;
				float4 tex2DNode11 = tex2D( _Metalic, uv_Metalic );
				
				surfaceDescription.Albedo = ( _Color * tex2D( _BaseTexture, uv_BaseTexture ) ).rgb;
				surfaceDescription.Normal = UnpackNormalmapRGorAG( tex2D( _Normal, uv_Normal ), 1.0f );
				surfaceDescription.Emission = ( _ColorEmissive * ( ( lerpResult51 + tex2DNode18 ) * _EmissivePower * temp_output_19_0 ) ).rgb;
				surfaceDescription.Specular = 0;
				surfaceDescription.Metallic = tex2DNode11.r;
				surfaceDescription.Smoothness = tex2DNode11.a;
				surfaceDescription.Occlusion = 1;
				surfaceDescription.Alpha = 1;
				surfaceDescription.AlphaClipThreshold = 0;

#ifdef _MATERIAL_FEATURE_CLEAR_COAT
				surfaceDescription.CoatMask = 0;
#endif

#if defined(_MATERIAL_FEATURE_SUBSURFACE_SCATTERING) || defined(_MATERIAL_FEATURE_TRANSMISSION)
				surfaceDescription.DiffusionProfile = 0;
#endif

#ifdef _MATERIAL_FEATURE_SUBSURFACE_SCATTERING
				surfaceDescription.SubsurfaceMask = 1;
#endif

#ifdef _MATERIAL_FEATURE_TRANSMISSION
				surfaceDescription.Thickness = 0;
#endif

#ifdef _MATERIAL_FEATURE_ANISOTROPY
				surfaceDescription.Anisotropy = 0;
#endif

#ifdef _MATERIAL_FEATURE_IRIDESCENCE
				surfaceDescription.IridescenceThickness = 0;
				surfaceDescription.IridescenceMask = 1;
#endif

#ifdef _MATERIAL_FEATURE_TRANSPARENCY
				surfaceDescription.IndexOfRefraction = 1;
				surfaceDescription.TransmittanceColor = float3( 1, 1, 1 );
				surfaceDescription.TransmittanceAbsorptionDistance = 1000000;
				surfaceDescription.TransmittanceMask = 0;
#endif
				GetSurfaceAndBuiltinData ( surfaceDescription, input, normalizedWorldViewDir, posInput, surfaceData, builtinData );
				ENCODE_INTO_GBUFFER ( surfaceData, builtinData, posInput.positionSS, outGBuffer );
#ifdef _DEPTHOFFSET_ON
				outputDepth = posInput.deviceDepth;
#endif
			}

            ENDHLSL
        }
        
		
		
        Pass
        {
			
            Name "META"
            Tags { "LightMode"="Meta" }
            Cull Off
            HLSLPROGRAM
			//#define UNITY_MATERIAL_LIT
			#pragma vertex Vert
			#pragma fragment Frag

			#define _NORMALMAP 1

        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Wind.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
			#define SHADERPASS SHADERPASS_LIGHT_TRANSPORT
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD2
			#define ATTRIBUTES_NEED_COLOR
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
			CBUFFER_START(UnityPerMaterial)
			uniform float4 _Color;
			uniform sampler2D _BaseTexture;
			uniform float4 _BaseTexture_ST;
			uniform sampler2D _Normal;
			uniform float4 _Normal_ST;
			uniform float4 _ColorEmissive;
			uniform sampler2D _Texture0;
			uniform float _Speed;
			uniform float4 _Texture0_ST;
			uniform float _EmissivePower;
			uniform sampler2D _Metalic;
			uniform float4 _Metalic_ST;
			CBUFFER_END
			
            struct AttributesMesh 
			{
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 color : COLOR;
				
            };

            struct PackedVaryingsMeshToPS
			{
                float4 positionCS : SV_Position;
				float4 ase_texcoord : TEXCOORD0;
            };
            
			void BuildSurfaceData ( FragInputs fragInputs, GlobalSurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData )
			{
				ZERO_INITIALIZE ( SurfaceData, surfaceData );

				float3 normalTS = float3( 0.0f, 0.0f, 1.0f );
				normalTS = surfaceDescription.Normal;
				GetNormalWS ( fragInputs, normalTS, surfaceData.normalWS );

				surfaceData.ambientOcclusion = 1.0f;

				surfaceData.baseColor = surfaceDescription.Albedo;
				surfaceData.perceptualSmoothness = surfaceDescription.Smoothness;
				surfaceData.ambientOcclusion = surfaceDescription.Occlusion;

				surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;

#ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
				surfaceData.specularColor = surfaceDescription.Specular;
#else
				surfaceData.metallic = surfaceDescription.Metallic;
#endif

#if defined(_MATERIAL_FEATURE_SUBSURFACE_SCATTERING) || defined(_MATERIAL_FEATURE_TRANSMISSION)
				surfaceData.diffusionProfile = surfaceDescription.DiffusionProfile;
#endif

#ifdef _MATERIAL_FEATURE_SUBSURFACE_SCATTERING
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SUBSURFACE_SCATTERING;
				surfaceData.subsurfaceMask = surfaceDescription.SubsurfaceMask;

#else
				surfaceData.subsurfaceMask = 1.0f;
#endif

#ifdef _MATERIAL_FEATURE_TRANSMISSION
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_TRANSMISSION;
				surfaceData.thickness = surfaceDescription.Thickness;
#endif

				surfaceData.tangentWS = normalize ( fragInputs.worldToTangent[ 0 ].xyz );
				surfaceData.tangentWS = Orthonormalize ( surfaceData.tangentWS, surfaceData.normalWS );

#ifdef _MATERIAL_FEATURE_ANISOTROPY
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_ANISOTROPY;
				surfaceData.anisotropy = surfaceDescription.Anisotropy;

#else
				surfaceData.anisotropy = 0;
#endif

#ifdef _MATERIAL_FEATURE_CLEAR_COAT
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_CLEAR_COAT;
				surfaceData.coatMask = surfaceDescription.CoatMask;
#else
				surfaceData.coatMask = 0.0f;
#endif

#ifdef _MATERIAL_FEATURE_IRIDESCENCE
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_IRIDESCENCE;
				surfaceData.iridescenceThickness = surfaceDescription.IridescenceThickness;
				surfaceData.iridescenceMask = surfaceDescription.IridescenceMask;
#else
				surfaceData.iridescenceThickness = 0.0;
				surfaceData.iridescenceMask = 1.0;
#endif

				//ASE CUSTOM TAG
#ifdef _MATERIAL_FEATURE_TRANSPARENCY
				surfaceData.ior = surfaceDescription.IndexOfRefraction;
				surfaceData.transmittanceColor = surfaceDescription.TransmittanceColor;
				surfaceData.atDistance = surfaceDescription.TransmittanceAbsorptionDistance;
				surfaceData.transmittanceMask = surfaceDescription.TransmittanceMask;
#else
				surfaceData.ior = 1.0;
				surfaceData.transmittanceColor = float3( 1.0, 1.0, 1.0 );
				surfaceData.atDistance = 1000000.0;
				surfaceData.transmittanceMask = 0.0;
#endif

				surfaceData.specularOcclusion = 1.0;

#if defined(_BENTNORMALMAP) && defined(_ENABLESPECULAROCCLUSION)
				surfaceData.specularOcclusion = GetSpecularOcclusionFromBentAO ( V, bentNormalWS, surfaceData );
#elif defined(_MASKMAP)
				surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion ( NdotV, surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness ( surfaceData.perceptualSmoothness ) );
#endif
			}

            void GetSurfaceAndBuiltinData( GlobalSurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
            {
#if _ALPHATEST_ON
				DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
#endif
				BuildSurfaceData (fragInputs, surfaceDescription, V, surfaceData);
        
               // Builtin Data
                // For back lighting we use the oposite vertex normal 
                InitBuiltinData(surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.worldToTangent[2], fragInputs.positionRWS, fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
        
		        builtinData.emissiveColor =             surfaceDescription.Emission;
                builtinData.distortion =                float2(0.0, 0.0);           // surfaceDescription.Distortion -- if distortion pass
                builtinData.distortionBlur =            0.0;                        // surfaceDescription.DistortionBlur -- if distortion pass
                builtinData.depthOffset =               0.0;                        // ApplyPerPixelDisplacement(input, V, layerTexCoord, blendMasks); #ifdef _DEPTHOFFSET_ON : ApplyDepthOffsetPositionInput(V, depthOffset, GetWorldToHClipMatrix(), posInput);
        
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
            }
        
           
			CBUFFER_START ( UnityMetaPass )
				bool4 unity_MetaVertexControl;
				bool4 unity_MetaFragmentControl;
			CBUFFER_END


			float unity_OneOverOutputBoost;
			float unity_MaxOutputValue;

			PackedVaryingsMeshToPS Vert ( AttributesMesh inputMesh  )
			{
				PackedVaryingsMeshToPS outputPackedVaryingsMeshToPS;

				UNITY_SETUP_INSTANCE_ID ( inputMesh );
				UNITY_TRANSFER_INSTANCE_ID ( inputMesh, outputPackedVaryingsMeshToPS );

				outputPackedVaryingsMeshToPS.ase_texcoord.xy = inputMesh.uv0;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				outputPackedVaryingsMeshToPS.ase_texcoord.zw = 0;
				inputMesh.positionOS.xyz +=  float3( 0, 0, 0 ) ;
				inputMesh.normalOS =  inputMesh.normalOS ;

				float2 uv;

				if ( unity_MetaVertexControl.x )
				{
					uv = inputMesh.uv1 * unity_LightmapST.xy + unity_LightmapST.zw;
				}
				else if ( unity_MetaVertexControl.y )
				{
					uv = inputMesh.uv2 * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
				}

				outputPackedVaryingsMeshToPS.positionCS = float4( uv * 2.0 - 1.0, inputMesh.positionOS.z > 0 ? 1.0e-4 : 0.0, 1.0 );

				return outputPackedVaryingsMeshToPS;
			}

			float4 Frag ( PackedVaryingsMeshToPS packedInput  ) : SV_Target
			{
				FragInputs input;
				ZERO_INITIALIZE ( FragInputs, input );
				input.worldToTangent = k_identity3x3;
				input.positionSS = packedInput.positionCS;

				PositionInputs posInput = GetPositionInput ( input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS );

				float3 V = 0;

				SurfaceData surfaceData;
				BuiltinData builtinData;

				GlobalSurfaceDescription surfaceDescription = ( GlobalSurfaceDescription ) 0;
				float2 uv_BaseTexture = packedInput.ase_texcoord.xy * _BaseTexture_ST.xy + _BaseTexture_ST.zw;
				
				float2 uv_Normal = packedInput.ase_texcoord.xy * _Normal_ST.xy + _Normal_ST.zw;
				
				float2 uv39 = packedInput.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float temp_output_45_0 = ( _Time.y * _Speed );
				float temp_output_37_0 = ( 0.2 * temp_output_45_0 );
				float cos34 = cos( -temp_output_37_0 );
				float sin34 = sin( -temp_output_37_0 );
				float2 rotator34 = mul( ( 1.1 * uv39 ) - float2( 0.25,0.25 ) , float2x2( cos34 , -sin34 , sin34 , cos34 )) + float2( 0.25,0.25 );
				float cos35 = cos( temp_output_37_0 );
				float sin35 = sin( temp_output_37_0 );
				float2 rotator35 = mul( uv39 - float2( 0,0 ) , float2x2( cos35 , -sin35 , sin35 , cos35 )) + float2( 0,0 );
				float2 uv_Texture0 = packedInput.ase_texcoord.xy * _Texture0_ST.xy + _Texture0_ST.zw;
				float4 tex2DNode18 = tex2D( _Texture0, uv_Texture0 );
				float clampResult52 = clamp( pow( ( 3.0 * tex2DNode18.g ) , 3.0 ) , 0.0 , 1.0 );
				float lerpResult51 = lerp( 0.0 , ( tex2D( _Texture0, rotator34 ).a * tex2D( _Texture0, rotator35 ).a ) , clampResult52);
				float temp_output_19_0 = (0.9 + (sin( temp_output_45_0 ) - 0.0) * (1.3 - 0.9) / (1.0 - 0.0));
				
				float2 uv_Metalic = packedInput.ase_texcoord.xy * _Metalic_ST.xy + _Metalic_ST.zw;
				float4 tex2DNode11 = tex2D( _Metalic, uv_Metalic );
				
				surfaceDescription.Albedo = ( _Color * tex2D( _BaseTexture, uv_BaseTexture ) ).rgb;
				surfaceDescription.Normal = UnpackNormalmapRGorAG( tex2D( _Normal, uv_Normal ), 1.0f );
				surfaceDescription.Emission = ( _ColorEmissive * ( ( lerpResult51 + tex2DNode18 ) * _EmissivePower * temp_output_19_0 ) ).rgb;
				surfaceDescription.Specular = 0;
				surfaceDescription.Metallic = tex2DNode11.r;
				surfaceDescription.Smoothness = tex2DNode11.a;
				surfaceDescription.Occlusion = 1;
				surfaceDescription.Alpha = 1;
				surfaceDescription.AlphaClipThreshold = 0;

#ifdef _MATERIAL_FEATURE_CLEAR_COAT
				surfaceDescription.CoatMask = 0;
#endif

#if defined(_MATERIAL_FEATURE_SUBSURFACE_SCATTERING) || defined(_MATERIAL_FEATURE_TRANSMISSION)
				surfaceDescription.DiffusionProfile = 0;
#endif

#ifdef _MATERIAL_FEATURE_SUBSURFACE_SCATTERING
				surfaceDescription.SubsurfaceMask = 1;
#endif

#ifdef _MATERIAL_FEATURE_TRANSMISSION
				surfaceDescription.Thickness = 0;
#endif

#ifdef _MATERIAL_FEATURE_ANISOTROPY
				surfaceDescription.Anisotropy = 0;
#endif

#ifdef _MATERIAL_FEATURE_IRIDESCENCE
				surfaceDescription.IridescenceThickness = 0;
				surfaceDescription.IridescenceMask = 1;
#endif

#ifdef _MATERIAL_FEATURE_TRANSPARENCY
				surfaceDescription.IndexOfRefraction = 1;
				surfaceDescription.TransmittanceColor = float3( 1, 1, 1 );
				surfaceDescription.TransmittanceAbsorptionDistance = 1000000;
				surfaceDescription.TransmittanceMask = 0;
#endif

				GetSurfaceAndBuiltinData ( surfaceDescription, input, V, posInput, surfaceData, builtinData );

				BSDFData bsdfData = ConvertSurfaceDataToBSDFData ( input.positionSS.xy, surfaceData );

				LightTransportData lightTransportData = GetLightTransportData ( surfaceData, builtinData, bsdfData );

				float4 res = float4( 0.0, 0.0, 0.0, 1.0 );
				if ( unity_MetaFragmentControl.x )
				{
					res.rgb = clamp ( pow ( abs ( lightTransportData.diffuseColor ), saturate ( unity_OneOverOutputBoost ) ), 0, unity_MaxOutputValue );
				}

				if ( unity_MetaFragmentControl.y )
				{
					res.rgb = lightTransportData.emissiveColor;
				}

				return res;
			}
       
            ENDHLSL
        }

		
		Pass
        {
			
            Name "ShadowCaster"
            Tags { "LightMode"="ShadowCaster" }
            ColorMask 0
			

            HLSLPROGRAM
			//#define UNITY_MATERIAL_LIT
			#pragma vertex Vert
			#pragma fragment Frag

			
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Wind.hlsl"
        
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        
        
             #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
            #define SHADERPASS SHADERPASS_SHADOWS
            #define USE_LEGACY_UNITY_MATRIX_VARIABLES
        
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
            #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
			CBUFFER_START(UnityPerMaterial)
						CBUFFER_END
			
            struct AttributesMesh 
			{
                float4 positionOS : POSITION;
				float3 normalOS : NORMAL;
				
				#if UNITY_ANY_INSTANCING_ENABLED
				uint instanceID : INSTANCEID_SEMANTIC;
				#endif 
            };

            struct PackedVaryingsMeshToPS 
			{
                float4 positionCS : SV_Position;
				
				#if UNITY_ANY_INSTANCING_ENABLED
				uint instanceID : INSTANCEID_SEMANTIC;
				#endif 
            };
        
            void BuildSurfaceData(FragInputs fragInputs, AlphaSurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
            {
                ZERO_INITIALIZE(SurfaceData, surfaceData);
                surfaceData.ambientOcclusion =      1.0f;
                surfaceData.subsurfaceMask =        1.0f;
        
                surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;
        #ifdef _MATERIAL_FEATURE_SUBSURFACE_SCATTERING
                surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SUBSURFACE_SCATTERING;
        #endif
        #ifdef _MATERIAL_FEATURE_TRANSMISSION
                surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_TRANSMISSION;
        #endif
        #ifdef _MATERIAL_FEATURE_ANISOTROPY
                surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_ANISOTROPY;
        #endif
        #ifdef _MATERIAL_FEATURE_CLEAR_COAT
                surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_CLEAR_COAT;
        #endif
        #ifdef _MATERIAL_FEATURE_IRIDESCENCE
                surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_IRIDESCENCE;
        #endif
        #ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
                surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
        #endif
        
                float3 normalTS = float3(0.0f, 0.0f, 1.0f);
                GetNormalWS(fragInputs, normalTS, surfaceData.normalWS);
                surfaceData.tangentWS = normalize(fragInputs.worldToTangent[0].xyz);
                surfaceData.tangentWS = Orthonormalize(surfaceData.tangentWS, surfaceData.normalWS);
                surfaceData.anisotropy = 0;
                surfaceData.coatMask = 0.0f;
                surfaceData.iridescenceThickness = 0.0;
                surfaceData.iridescenceMask = 1.0;
                surfaceData.ior = 1.0;
                surfaceData.transmittanceColor = float3(1.0, 1.0, 1.0);
                surfaceData.atDistance = 1000000.0;
                surfaceData.transmittanceMask = 0.0;
                surfaceData.specularOcclusion = 1.0;
        #if defined(_BENTNORMALMAP) && defined(_ENABLESPECULAROCCLUSION)
                surfaceData.specularOcclusion = GetSpecularOcclusionFromBentAO(V, bentNormalWS, surfaceData);
        #elif defined(_MASKMAP)
                surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion(NdotV, surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness(surfaceData.perceptualSmoothness));
        #endif
            }
        
            void GetSurfaceAndBuiltinData( AlphaSurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
            {
#if _ALPHATEST_ON
				DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
#endif
                BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
                // Builtin Data
                // For back lighting we use the oposite vertex normal 
                InitBuiltinData(surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.worldToTangent[2], fragInputs.positionRWS, fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
                builtinData.distortion =                float2(0.0, 0.0);           // surfaceDescription.Distortion -- if distortion pass
                builtinData.distortionBlur =            0.0;                        // surfaceDescription.DistortionBlur -- if distortion pass
                builtinData.depthOffset =               0.0;                        // ApplyPerPixelDisplacement(input, V, layerTexCoord, blendMasks); #ifdef _DEPTHOFFSET_ON : ApplyDepthOffsetPositionInput(V, depthOffset, GetWorldToHClipMatrix(), posInput);
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);            
            }

			PackedVaryingsMeshToPS Vert( AttributesMesh inputMesh  )
			{
				PackedVaryingsMeshToPS outputPackedVaryingsMeshToPS;

				UNITY_SETUP_INSTANCE_ID ( inputMesh );
				UNITY_TRANSFER_INSTANCE_ID ( inputMesh, outputPackedVaryingsMeshToPS );

				
				inputMesh.positionOS.xyz +=  float3( 0, 0, 0 ) ;
				inputMesh.normalOS =  inputMesh.normalOS ;

				float3 positionRWS = TransformObjectToWorld ( inputMesh.positionOS.xyz );
				float4 positionCS = TransformWorldToHClip ( positionRWS );

				outputPackedVaryingsMeshToPS.positionCS = positionCS;
				return outputPackedVaryingsMeshToPS;
			}

			void Frag(  PackedVaryingsMeshToPS packedInput
							#ifdef WRITE_NORMAL_BUFFER
							, out float4 outNormalBuffer : SV_Target0
								#ifdef WRITE_MSAA_DEPTH
							, out float1 depthColor : SV_Target1
								#endif
							#else
							, out float4 outColor : SV_Target0
							#endif

							#ifdef _DEPTHOFFSET_ON
							, out float outputDepth : SV_Depth
							#endif
							 
						)
				{
						FragInputs input;
						ZERO_INITIALIZE(FragInputs, input);
						input.worldToTangent = k_identity3x3;
						input.positionSS = packedInput.positionCS;       // input.positionCS is SV_Position

						// input.positionSS is SV_Position
						PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

						float3 V = float3(1.0, 1.0, 1.0); // Avoid the division by 0

						SurfaceData surfaceData;
						BuiltinData builtinData;
						AlphaSurfaceDescription surfaceDescription = (AlphaSurfaceDescription)0;
						
						surfaceDescription.Alpha = 1;
						surfaceDescription.AlphaClipThreshold = 0;

						GetSurfaceAndBuiltinData(surfaceDescription,input, V, posInput, surfaceData, builtinData);

					#ifdef _DEPTHOFFSET_ON
						outputDepth = posInput.deviceDepth;
					#endif

					#ifdef WRITE_NORMAL_BUFFER
						EncodeIntoNormalBuffer(ConvertSurfaceDataToNormalData(surfaceData), posInput.positionSS, outNormalBuffer);
						#ifdef WRITE_MSAA_DEPTH
						// In case we are rendering in MSAA, reading the an MSAA depth buffer is way too expensive. To avoid that, we export the depth to a color buffer
						depthColor = packedInput.positionCS.z;
						#endif
					#elif defined(SCENESELECTIONPASS)
						// We use depth prepass for scene selection in the editor, this code allow to output the outline correctly
						outColor = float4(_ObjectId, _PassValue, 1.0, 1.0);
					#else
						outColor = float4(0.0, 0.0, 0.0, 0.0);
					#endif
				}
            ENDHLSL
        }
		
		
        Pass
        {
			
            Name "SceneSelectionPass"
            Tags { "LightMode"="SceneSelectionPass" }

            ColorMask 0
        
            HLSLPROGRAM
				//#define UNITY_MATERIAL_LIT
				#pragma vertex Vert
				#pragma fragment Frag
        
				

				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Wind.hlsl"
        
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
                #define SHADERPASS SHADERPASS_DEPTH_ONLY
                #define SCENESELECTIONPASS
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
				int _ObjectId;
				int _PassValue;
        
				struct AttributesMesh 
				{
					float3 positionOS : POSITION;
					float3 normalOS : NORMAL;
					
					#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : INSTANCEID_SEMANTIC;
					#endif
				};
        
				struct PackedVaryingsMeshToPS 
				{
					float4 positionCS : SV_Position; 
					
					#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : INSTANCEID_SEMANTIC; 
					#endif 
				};

        
                CBUFFER_START(UnityPerMaterial)
								CBUFFER_END
				                
        
				void BuildSurfaceData(FragInputs fragInputs, AlphaSurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
				{
					ZERO_INITIALIZE(SurfaceData, surfaceData);
					surfaceData.ambientOcclusion =      1.0f;
					surfaceData.subsurfaceMask =        1.0f;
					surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;
			#ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
					surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
			#endif
					float3 normalTS =                   float3(0.0f, 0.0f, 1.0f);
					GetNormalWS(fragInputs, normalTS, surfaceData.normalWS);
					surfaceData.tangentWS = normalize(fragInputs.worldToTangent[0].xyz);    // The tangent is not normalize in worldToTangent for mikkt. TODO: Check if it expected that we normalize with Morten. Tag: SURFACE_GRADIENT
					surfaceData.tangentWS = Orthonormalize(surfaceData.tangentWS, surfaceData.normalWS);
					surfaceData.anisotropy = 0;
					surfaceData.coatMask = 0.0f;
					surfaceData.iridescenceThickness = 0.0;
					surfaceData.iridescenceMask = 1.0;
					surfaceData.ior = 1.0;
					surfaceData.transmittanceColor = float3(1.0, 1.0, 1.0);
					surfaceData.atDistance = 1000000.0;
					surfaceData.transmittanceMask = 0.0;
					surfaceData.specularOcclusion = 1.0;
			#if defined(_BENTNORMALMAP) && defined(_ENABLESPECULAROCCLUSION)
					surfaceData.specularOcclusion = GetSpecularOcclusionFromBentAO(V, bentNormalWS, surfaceData);
			#elif defined(_MASKMAP)
					surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion(NdotV, surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness(surfaceData.perceptualSmoothness));
			#endif
        
				}
        
				void GetSurfaceAndBuiltinData(AlphaSurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
				{
				#if _ALPHATEST_ON
					DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif

					BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
					InitBuiltinData(surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.worldToTangent[2], fragInputs.positionRWS, fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
					builtinData.distortion =                float2(0.0, 0.0);           
					builtinData.distortionBlur =            0.0;                        
					builtinData.depthOffset =               0.0;                        
					PostInitBuiltinData(V, posInput, surfaceData, builtinData);
				}
        
       
				PackedVaryingsMeshToPS Vert(AttributesMesh inputMesh )
				{
					PackedVaryingsMeshToPS outputPackedVaryingsMeshToPS;
					
					UNITY_SETUP_INSTANCE_ID(inputMesh);
					UNITY_TRANSFER_INSTANCE_ID(inputMesh, outputPackedVaryingsMeshToPS);
					
					
				
					inputMesh.positionOS.xyz +=  float3( 0, 0, 0 ) ;
					inputMesh.normalOS =  inputMesh.normalOS ;

					float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
					
					outputPackedVaryingsMeshToPS.positionCS = TransformWorldToHClip(positionRWS);
			
					return outputPackedVaryingsMeshToPS;
				}

				void Frag(  PackedVaryingsMeshToPS packedInput
							#ifdef WRITE_NORMAL_BUFFER
							, out float4 outNormalBuffer : SV_Target0
								#ifdef WRITE_MSAA_DEPTH
							, out float1 depthColor : SV_Target1
								#endif
							#elif defined(SCENESELECTIONPASS)
							, out float4 outColor : SV_Target0
							#endif

							#ifdef _DEPTHOFFSET_ON
							, out float outputDepth : SV_Depth
							#endif
							
						)
				{
					
					FragInputs input;
					ZERO_INITIALIZE(FragInputs, input);
					input.worldToTangent = k_identity3x3;
					input.positionSS = packedInput.positionCS;
					

					// input.positionSS is SV_Position
					PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

				
					float3 V = float3(1.0, 1.0, 1.0); // Avoid the division by 0
				
					SurfaceData surfaceData;
					BuiltinData builtinData;
					AlphaSurfaceDescription surfaceDescription = ( AlphaSurfaceDescription ) 0;
					
					surfaceDescription.Alpha = 1;
					surfaceDescription.AlphaClipThreshold = 0;
					GetSurfaceAndBuiltinData(surfaceDescription, input, V, posInput, surfaceData, builtinData);

				#ifdef _DEPTHOFFSET_ON
					outputDepth = posInput.deviceDepth;
				#endif

				#ifdef WRITE_NORMAL_BUFFER
					EncodeIntoNormalBuffer(ConvertSurfaceDataToNormalData(surfaceData), posInput.positionSS, outNormalBuffer);
					#ifdef WRITE_MSAA_DEPTH
					// In case we are rendering in MSAA, reading the an MSAA depth buffer is way too expensive. To avoid that, we export the depth to a color buffer
					depthColor = packedInput.positionCS.z;
					#endif
				#elif defined(SCENESELECTIONPASS)
					// We use depth prepass for scene selection in the editor, this code allow to output the outline correctly
					outColor = float4(_ObjectId, _PassValue, 1.0, 1.0);
				#endif
				}

            ENDHLSL
        }
		
        Pass
        {
			
            Name "DepthOnly"
            Tags { "LightMode"="DepthOnly" }
			ColorMask 0
            
            HLSLPROGRAM
				//#define UNITY_MATERIAL_LIT
				#pragma vertex Vert
				#pragma fragment Frag
        
				

        
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Wind.hlsl"
        
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
				#define SHADERPASS SHADERPASS_DEPTH_ONLY
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
				struct AttributesMesh 
				{
					float3 positionOS : POSITION;
					float3 normalOS : NORMAL;
					
					#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : INSTANCEID_SEMANTIC;
					#endif
				};

				struct PackedVaryingsMeshToPS 
				{
					float4 positionCS : SV_Position;
					
					#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : INSTANCEID_SEMANTIC;
					#endif
				};

				CBUFFER_START(UnityPerMaterial)
								CBUFFER_END
				        
				void BuildSurfaceData(FragInputs fragInputs, AlphaSurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
				{
					ZERO_INITIALIZE(SurfaceData, surfaceData);
					surfaceData.ambientOcclusion =      1.0f;
					surfaceData.subsurfaceMask =        1.0f;

					surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;
			#ifdef _MATERIAL_FEATURE_SUBSURFACE_SCATTERING
					surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SUBSURFACE_SCATTERING;
			#endif
			#ifdef _MATERIAL_FEATURE_TRANSMISSION
					surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_TRANSMISSION;
			#endif
			#ifdef _MATERIAL_FEATURE_ANISOTROPY
					surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_ANISOTROPY;
			#endif
			#ifdef _MATERIAL_FEATURE_CLEAR_COAT
					surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_CLEAR_COAT;
			#endif
			#ifdef _MATERIAL_FEATURE_IRIDESCENCE
					surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_IRIDESCENCE;
			#endif
			#ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
					surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
			#endif
					float3 normalTS =                   float3(0.0f, 0.0f, 1.0f);
					GetNormalWS(fragInputs, normalTS, surfaceData.normalWS);
					surfaceData.tangentWS = normalize(fragInputs.worldToTangent[0].xyz);    // The tangent is not normalize in worldToTangent for mikkt. TODO: Check if it expected that we normalize with Morten. Tag: SURFACE_GRADIENT
					surfaceData.tangentWS = Orthonormalize(surfaceData.tangentWS, surfaceData.normalWS);
					surfaceData.anisotropy = 0;
					surfaceData.coatMask = 0.0f;
					surfaceData.iridescenceThickness = 0.0;
					surfaceData.iridescenceMask = 1.0;
					surfaceData.ior = 1.0;
					surfaceData.transmittanceColor = float3(1.0, 1.0, 1.0);
					surfaceData.atDistance = 1000000.0;
					surfaceData.transmittanceMask = 0.0;
					surfaceData.specularOcclusion = 1.0;
			#if defined(_BENTNORMALMAP) && defined(_ENABLESPECULAROCCLUSION)
					surfaceData.specularOcclusion = GetSpecularOcclusionFromBentAO(V, bentNormalWS, surfaceData);
			#elif defined(_MASKMAP)
					surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion(NdotV, surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness(surfaceData.perceptualSmoothness));
			#endif
				}
        
            void GetSurfaceAndBuiltinData(AlphaSurfaceDescription surfaceDescription,FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
            {
				#if _ALPHATEST_ON
					DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif
                BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
                InitBuiltinData(surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.worldToTangent[2], fragInputs.positionRWS, fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
                builtinData.distortion =                float2(0.0, 0.0);           // surfaceDescription.Distortion -- if distortion pass
                builtinData.distortionBlur =            0.0;                        // surfaceDescription.DistortionBlur -- if distortion pass
                builtinData.depthOffset =               0.0;                        // ApplyPerPixelDisplacement(input, V, layerTexCoord, blendMasks); #ifdef _DEPTHOFFSET_ON : ApplyDepthOffsetPositionInput(V, depthOffset, GetWorldToHClipMatrix(), posInput);
                PostInitBuiltinData(V, posInput, surfaceData, builtinData);
            }

			PackedVaryingsMeshToPS Vert(AttributesMesh inputMesh  )
			{
				PackedVaryingsMeshToPS outputPackedVaryingsMeshToPS;
				UNITY_SETUP_INSTANCE_ID(inputMesh);
				UNITY_TRANSFER_INSTANCE_ID(inputMesh, outputPackedVaryingsMeshToPS);

				
				inputMesh.positionOS.xyz +=  float3( 0, 0, 0 ) ;
				inputMesh.normalOS =  inputMesh.normalOS ;

				float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
				outputPackedVaryingsMeshToPS.positionCS = TransformWorldToHClip(positionRWS);
				return outputPackedVaryingsMeshToPS;
			}

			void Frag(  PackedVaryingsMeshToPS packedInput
						#ifdef WRITE_NORMAL_BUFFER
						, out float4 outNormalBuffer : SV_Target0
							#ifdef WRITE_MSAA_DEPTH
						, out float1 depthColor : SV_Target1
							#endif
						#else
						, out float4 outColor : SV_Target0
						#endif

						#ifdef _DEPTHOFFSET_ON
						, out float outputDepth : SV_Depth
						#endif
						
					)
			{
							
				FragInputs input;
				ZERO_INITIALIZE(FragInputs, input);
				input.worldToTangent = k_identity3x3;
				input.positionSS = packedInput.positionCS;
				
				PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

				float3 V = float3(1.0, 1.0, 1.0);

				SurfaceData surfaceData;
				BuiltinData builtinData;
				AlphaSurfaceDescription surfaceDescription = ( AlphaSurfaceDescription ) 0;
				
				surfaceDescription.Alpha = 1;
				surfaceDescription.AlphaClipThreshold = 0;

				GetSurfaceAndBuiltinData(surfaceDescription, input, V, posInput, surfaceData, builtinData);

			#ifdef _DEPTHOFFSET_ON
				outputDepth = posInput.deviceDepth;
			#endif

			#ifdef WRITE_NORMAL_BUFFER
				EncodeIntoNormalBuffer(ConvertSurfaceDataToNormalData(surfaceData), posInput.positionSS, outNormalBuffer);
				#ifdef WRITE_MSAA_DEPTH
				depthColor = packedInput.positionCS.z;
				#endif
			#elif defined(SCENESELECTIONPASS)
				outColor = float4(_ObjectId, _PassValue, 1.0, 1.0);
			#else
				outColor = float4(0.0, 0.0, 0.0, 0.0);
			#endif
			}
        
            ENDHLSL
        }

		
        Pass
        {
			
            Name "Motion Vectors"
            Tags { "LightMode"="MotionVectors" }
        
			Stencil
			{
				Ref 128
				WriteMask 128
				Comp Always
				Pass Replace
				Fail Keep
				ZFail Keep
			}

             
            HLSLPROGRAM
				//#define UNITY_MATERIAL_LIT
				#pragma vertex Vert
				#pragma fragment Frag
        
				
        
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Wind.hlsl"
        
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
                #define SHADERPASS SHADERPASS_VELOCITY
				#pragma multi_compile _ WRITE_NORMAL_BUFFER
                #pragma multi_compile _ WRITE_MSAA_DEPTH

                #define VARYINGS_NEED_POSITION_WS
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
                #include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
            
				struct AttributesMesh
				{
					float3 positionOS : POSITION;
					float3 normalOS : NORMAL;
					
					#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : INSTANCEID_SEMANTIC;
					#endif
				};
        
				struct VaryingsMeshToPS 
				{
					float4 positionCS : SV_Position;
					float3 positionRWS;
					#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : INSTANCEID_SEMANTIC;
					#endif
				};

				struct AttributesPass
				{
					float3 previousPositionOS : TEXCOORD4;
				};

				struct VaryingsPassToPS
				{
					float4 positionCS;
					float4 previousPositionCS;
				};

				#define VARYINGS_NEED_PASS

				struct VaryingsToPS
				{
					VaryingsMeshToPS vmesh;
					VaryingsPassToPS vpass;
				};

				struct PackedVaryingsToPS
				{
					float3 vmeshInterp00 : TEXCOORD0;
					float4 vmeshPositionCS : SV_Position;
					float3 vpassInterpolators0 : TEXCOORD1;
					float3 vpassInterpolators1 : TEXCOORD2;
					
					#if UNITY_ANY_INSTANCING_ENABLED
					uint vmeshInstanceID : INSTANCEID_SEMANTIC;
					#endif
				};

                CBUFFER_START(UnityPerMaterial)
								CBUFFER_END
				            
				FragInputs BuildFragInputs(VaryingsMeshToPS input)
				{
					FragInputs output;
					ZERO_INITIALIZE(FragInputs, output);
					output.worldToTangent = k_identity3x3;
					output.positionSS = input.positionCS;
					output.positionRWS = input.positionRWS;
					return output;
				}
                
				void BuildSurfaceData(FragInputs fragInputs, AlphaSurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
				{
					ZERO_INITIALIZE(SurfaceData, surfaceData);
					surfaceData.ambientOcclusion =      1.0f;
					surfaceData.subsurfaceMask =        1.0f;
					surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;
			#ifdef _MATERIAL_FEATURE_SUBSURFACE_SCATTERING
					surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SUBSURFACE_SCATTERING;
			#endif
			#ifdef _MATERIAL_FEATURE_TRANSMISSION
					surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_TRANSMISSION;
			#endif
			#ifdef _MATERIAL_FEATURE_ANISOTROPY
					surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_ANISOTROPY;
			#endif
			#ifdef _MATERIAL_FEATURE_CLEAR_COAT
					surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_CLEAR_COAT;
			#endif
			#ifdef _MATERIAL_FEATURE_IRIDESCENCE
					surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_IRIDESCENCE;
			#endif
			#ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
					surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
			#endif
        
					float3 normalTS =                   float3(0.0f, 0.0f, 1.0f);
					GetNormalWS(fragInputs, normalTS, surfaceData.normalWS);
					surfaceData.tangentWS = normalize(fragInputs.worldToTangent[0].xyz);    // The tangent is not normalize in worldToTangent for mikkt. TODO: Check if it expected that we normalize with Morten. Tag: SURFACE_GRADIENT
					surfaceData.tangentWS = Orthonormalize(surfaceData.tangentWS, surfaceData.normalWS);
					surfaceData.anisotropy = 0;
					surfaceData.coatMask = 0.0f;
					surfaceData.iridescenceThickness = 0.0;
					surfaceData.iridescenceMask = 1.0;
					surfaceData.ior = 1.0;
					surfaceData.transmittanceColor = float3(1.0, 1.0, 1.0);
					surfaceData.atDistance = 1000000.0;
					surfaceData.transmittanceMask = 0.0;
					surfaceData.specularOcclusion = 1.0;
			#if defined(_BENTNORMALMAP) && defined(_ENABLESPECULAROCCLUSION)
					surfaceData.specularOcclusion = GetSpecularOcclusionFromBentAO(V, bentNormalWS, surfaceData);
			#elif defined(_MASKMAP)
					surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion(NdotV, surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness(surfaceData.perceptualSmoothness));
			#endif
				}
        
				void GetSurfaceAndBuiltinData(AlphaSurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
				{
				#if _ALPHATEST_ON
					DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif
					BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
					InitBuiltinData(surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.worldToTangent[2], fragInputs.positionRWS, fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
					builtinData.distortion = float2(0.0, 0.0);
					builtinData.distortionBlur = 0.0;
					builtinData.depthOffset = 0.0;
					PostInitBuiltinData(V, posInput, surfaceData, builtinData);
				}
        

				VaryingsMeshToPS UnpackVaryingsMeshToPS(PackedVaryingsToPS input)
				{
					VaryingsMeshToPS output;
					output.positionCS = input.vmeshPositionCS;
					output.positionRWS = input.vmeshInterp00.xyz;
					#if UNITY_ANY_INSTANCING_ENABLED
					output.instanceID = input.vmeshInstanceID;
					#endif
					return output;
				}

				VaryingsPassToPS UnpackVaryingsPassToPS(PackedVaryingsToPS input)
				{
					VaryingsPassToPS output;
					output.positionCS = float4(input.vpassInterpolators0.xy, 0.0, input.vpassInterpolators0.z);
					output.previousPositionCS = float4(input.vpassInterpolators1.xy, 0.0, input.vpassInterpolators1.z);

					return output;
				}

				PackedVaryingsToPS PackVaryingsToPS(VaryingsToPS varyingsType)
				{
					PackedVaryingsToPS outputPackedVaryingsToPS;
					
					outputPackedVaryingsToPS.vmeshPositionCS = varyingsType.vmesh.positionCS;
					outputPackedVaryingsToPS.vmeshInterp00.xyz = varyingsType.vmesh.positionRWS;
					#if UNITY_ANY_INSTANCING_ENABLED
					outputPackedVaryingsToPS.vmeshInstanceID = varyingsType.vmesh.instanceID;
					#endif
					outputPackedVaryingsToPS.vpassInterpolators0 = float3(varyingsType.vpass.positionCS.xyw);
					outputPackedVaryingsToPS.vpassInterpolators1 = float3(varyingsType.vpass.previousPositionCS.xyw);
					return outputPackedVaryingsToPS;
				}

				float3 TransformPreviousObjectToWorldNormal(float3 normalOS)
				{
				#ifdef UNITY_ASSUME_UNIFORM_SCALING
					return normalize(mul((float3x3)unity_MatrixPreviousM, normalOS));
				#else
					return normalize(mul(normalOS, (float3x3)unity_MatrixPreviousMI));
				#endif
				}

				float3 TransformPreviousObjectToWorld(float3 positionOS)
				{
					float4x4 previousModelMatrix = ApplyCameraTranslationToMatrix(unity_MatrixPreviousM);
					return mul(previousModelMatrix, float4(positionOS, 1.0)).xyz;
				}

				void VelocityPositionZBias(VaryingsToPS input)
				{
				#if defined(UNITY_REVERSED_Z)
					input.vmesh.positionCS.z -= unity_MotionVectorsParams.z * input.vmesh.positionCS.w;
				#else
					input.vmesh.positionCS.z += unity_MotionVectorsParams.z * input.vmesh.positionCS.w;
				#endif
				}

				PackedVaryingsToPS Vert(AttributesMesh inputMesh,
										AttributesPass inputPass
										
										)
				{
					PackedVaryingsToPS outputPackedVaryingsToPS;
					VaryingsToPS varyingsType;
					VaryingsMeshToPS outputVaryingsMeshToPS;

					UNITY_SETUP_INSTANCE_ID(inputMesh);
					UNITY_TRANSFER_INSTANCE_ID(inputMesh, outputVaryingsMeshToPS);

					
					inputMesh.positionOS.xyz +=  float3( 0, 0, 0 ) ;
					inputMesh.normalOS =  inputMesh.normalOS ;

					float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
					outputVaryingsMeshToPS.positionRWS = positionRWS;
					outputVaryingsMeshToPS.positionCS = TransformWorldToHClip(positionRWS);
					

					varyingsType.vmesh = outputVaryingsMeshToPS;

					VelocityPositionZBias(varyingsType);
					varyingsType.vpass.positionCS = mul(_NonJitteredViewProjMatrix, float4(varyingsType.vmesh.positionRWS, 1.0));
					bool forceNoMotion = unity_MotionVectorsParams.y == 0.0;
					if (forceNoMotion)
					{
						varyingsType.vpass.previousPositionCS = float4(0.0, 0.0, 0.0, 1.0);
					}
					else
					{
						bool hasDeformation = unity_MotionVectorsParams.x > 0.0; // Skin or morph target

						float3 previousPositionRWS = TransformPreviousObjectToWorld(hasDeformation ? inputPass.previousPositionOS : inputMesh.positionOS);

						float3 normalWS = float3(0.0, 0.0, 0.0);

						varyingsType.vpass.previousPositionCS = mul(_PrevViewProjMatrix, float4(previousPositionRWS, 1.0));
					}

					outputPackedVaryingsToPS.vmeshPositionCS = varyingsType.vmesh.positionCS;
					outputPackedVaryingsToPS.vmeshInterp00.xyz = varyingsType.vmesh.positionRWS;
					
					#if UNITY_ANY_INSTANCING_ENABLED
					outputPackedVaryingsToPS.vmeshInstanceID = varyingsType.vmesh.instanceID;
					#endif

					outputPackedVaryingsToPS.vpassInterpolators0 = float3(varyingsType.vpass.positionCS.xyw);
					outputPackedVaryingsToPS.vpassInterpolators1 = float3(varyingsType.vpass.previousPositionCS.xyw);
					
					return outputPackedVaryingsToPS;
				}

				void Frag(  PackedVaryingsToPS packedInput
							, out float4 outVelocity : SV_Target0
							#ifdef WRITE_NORMAL_BUFFER
							, out float4 outNormalBuffer : SV_Target1
								#ifdef WRITE_MSAA_DEPTH
								, out float1 depthColor : SV_Target2
								#endif
							#endif
							#ifdef _DEPTHOFFSET_ON
							, out float outputDepth : SV_Depth
							#endif
							
						)
				{
					
					VaryingsMeshToPS unpacked= UnpackVaryingsMeshToPS(packedInput);
					FragInputs input = BuildFragInputs(unpacked);
					

					PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

					float3 V = GetWorldSpaceNormalizeViewDir(input.positionRWS);

					SurfaceData surfaceData;
					BuiltinData builtinData;
					
					AlphaSurfaceDescription surfaceDescription = (AlphaSurfaceDescription)0;
                    
					surfaceDescription.Alpha = 1;
					surfaceDescription.AlphaClipThreshold = 0;
	
					GetSurfaceAndBuiltinData(surfaceDescription,input, V, posInput, surfaceData, builtinData);

					VaryingsPassToPS inputPass = UnpackVaryingsPassToPS(packedInput);
				#ifdef _DEPTHOFFSET_ON
					inputPass.positionCS.w += builtinData.depthOffset;
					inputPass.previousPositionCS.w += builtinData.depthOffset;
				#endif

					float2 velocity = CalculateVelocity(inputPass.positionCS, inputPass.previousPositionCS);

					EncodeVelocity(velocity * 0.5, outVelocity);

					bool forceNoMotion = unity_MotionVectorsParams.y == 0.0;
					if (forceNoMotion)
						outVelocity = float4(0.0, 0.0, 0.0, 0.0);

				#ifdef WRITE_NORMAL_BUFFER
					EncodeIntoNormalBuffer(ConvertSurfaceDataToNormalData(surfaceData), posInput.positionSS, outNormalBuffer);

					#ifdef WRITE_MSAA_DEPTH
					depthColor = packedInput.vmesh.positionCS.z;
					#endif
				#endif

				#ifdef _DEPTHOFFSET_ON
					outputDepth = posInput.deviceDepth;
				#endif
				}

            ENDHLSL
        }

		
        Pass
        {
            
            
			Name "Forward"
			Tags { "LightMode"="Forward" }
			Stencil
			{
				Ref 2
				WriteMask 7
				Comp Always
				Pass Replace
				Fail Keep
				ZFail Keep
			}


            HLSLPROGRAM
                //#define UNITY_MATERIAL_LIT
				#pragma vertex Vert
				#pragma fragment Frag
        
				#define _NORMALMAP 1

        
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Wind.hlsl"
        
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/NormalSurfaceGradient.hlsl"
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
                #define SHADERPASS SHADERPASS_FORWARD
                #pragma multi_compile _ LIGHTMAP_ON
                #pragma multi_compile _ DIRLIGHTMAP_COMBINED
                #pragma multi_compile _ DYNAMICLIGHTMAP_ON
                #pragma multi_compile _ SHADOWS_SHADOWMASK
                #define LIGHTLOOP_TILE_PASS
                #pragma multi_compile USE_FPTL_LIGHTLIST USE_CLUSTERED_LIGHTLIST
				#pragma multi_compile SHADOW_LOW SHADOW_MEDIUM SHADOW_HIGH
        
                #define ATTRIBUTES_NEED_NORMAL
                #define ATTRIBUTES_NEED_TANGENT
                #define ATTRIBUTES_NEED_TEXCOORD1
                #define ATTRIBUTES_NEED_TEXCOORD2
                #define VARYINGS_NEED_POSITION_WS
                #define VARYINGS_NEED_TANGENT_TO_WORLD
                #define VARYINGS_NEED_TEXCOORD1
                #define VARYINGS_NEED_TEXCOORD2
        
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Lighting.hlsl"
        
				#define HAS_LIGHTLOOP
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Lit/Lit.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoop.hlsl"
        
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
				//float3x3 BuildWorldToTangent(float4 tangentWS, float3 normalWS)
				//{
        		//	float3 unnormalizedNormalWS = normalWS;
				//	float renormFactor = 1.0 / length(unnormalizedNormalWS);
				//	float3x3 worldToTangent = CreateWorldToTangent(unnormalizedNormalWS, tangentWS.xyz, tangentWS.w > 0.0 ? 1.0 : -1.0);
				//	worldToTangent[0] = worldToTangent[0] * renormFactor;
				//	worldToTangent[1] = worldToTangent[1] * renormFactor;
				//	worldToTangent[2] = worldToTangent[2] * renormFactor;
				//	return worldToTangent;
				//}
        
				struct AttributesMesh 
				{
					float3 positionOS : POSITION;
					float3 normalOS : NORMAL;
					float4 tangentOS : TANGENT;
					float4 uv1 : TEXCOORD1;
					float4 uv2 : TEXCOORD2;
					float4 ase_texcoord : TEXCOORD0;
					#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : INSTANCEID_SEMANTIC;
					#endif
				};
        
				struct PackedVaryingsMeshToPS 
				{
					float4 positionCS : SV_Position;
					float3 interp00 : TEXCOORD0;
					float3 interp01 : TEXCOORD1;
					float4 interp02 : TEXCOORD2;
					float4 interp03 : TEXCOORD3;
					float4 interp04 : TEXCOORD4;
					float4 ase_texcoord5 : TEXCOORD5;
					#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : INSTANCEID_SEMANTIC;
					#endif
				};

        
				CBUFFER_START(UnityPerMaterial)
				uniform float4 _Color;
				uniform sampler2D _BaseTexture;
				uniform float4 _BaseTexture_ST;
				uniform sampler2D _Normal;
				uniform float4 _Normal_ST;
				uniform float4 _ColorEmissive;
				uniform sampler2D _Texture0;
				uniform float _Speed;
				uniform float4 _Texture0_ST;
				uniform float _EmissivePower;
				uniform sampler2D _Metalic;
				uniform float4 _Metalic_ST;
				CBUFFER_END
				                
        
				void BuildSurfaceData ( FragInputs fragInputs, GlobalSurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData )
			{
				ZERO_INITIALIZE ( SurfaceData, surfaceData );

				float3 normalTS = float3( 0.0f, 0.0f, 1.0f );
				normalTS = surfaceDescription.Normal;
				GetNormalWS ( fragInputs, normalTS, surfaceData.normalWS );

				surfaceData.ambientOcclusion = 1.0f;

				surfaceData.baseColor = surfaceDescription.Albedo;
				surfaceData.perceptualSmoothness = surfaceDescription.Smoothness;
				surfaceData.ambientOcclusion = surfaceDescription.Occlusion;

				surfaceData.materialFeatures = MATERIALFEATUREFLAGS_LIT_STANDARD;

#ifdef _MATERIAL_FEATURE_SPECULAR_COLOR
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR;
				surfaceData.specularColor = surfaceDescription.Specular;
#else
				surfaceData.metallic = surfaceDescription.Metallic;
#endif

#if defined(_MATERIAL_FEATURE_SUBSURFACE_SCATTERING) || defined(_MATERIAL_FEATURE_TRANSMISSION)
				surfaceData.diffusionProfile = surfaceDescription.DiffusionProfile;
#endif

#ifdef _MATERIAL_FEATURE_SUBSURFACE_SCATTERING
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_SUBSURFACE_SCATTERING;
				surfaceData.subsurfaceMask = surfaceDescription.SubsurfaceMask;
#else
				surfaceData.subsurfaceMask = 1.0f;
#endif

#ifdef _MATERIAL_FEATURE_TRANSMISSION
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_TRANSMISSION;
				surfaceData.thickness = surfaceDescription.Thickness;
#endif

				surfaceData.tangentWS = normalize ( fragInputs.worldToTangent[ 0 ].xyz );
				surfaceData.tangentWS = Orthonormalize ( surfaceData.tangentWS, surfaceData.normalWS );

#ifdef _MATERIAL_FEATURE_ANISOTROPY
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_ANISOTROPY;
				surfaceData.anisotropy = surfaceDescription.Anisotropy;

#else
				surfaceData.anisotropy = 0;
#endif

#ifdef _MATERIAL_FEATURE_CLEAR_COAT
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_CLEAR_COAT;
				surfaceData.coatMask = surfaceDescription.CoatMask;
#else
				surfaceData.coatMask = 0.0f;
#endif

#ifdef _MATERIAL_FEATURE_IRIDESCENCE
				surfaceData.materialFeatures |= MATERIALFEATUREFLAGS_LIT_IRIDESCENCE;
				surfaceData.iridescenceThickness = surfaceDescription.IridescenceThickness;
				surfaceData.iridescenceMask = surfaceDescription.IridescenceMask;
#else
				surfaceData.iridescenceThickness = 0.0;
				surfaceData.iridescenceMask = 1.0;
#endif

				//ASE CUSTOM TAG
#ifdef _MATERIAL_FEATURE_TRANSPARENCY
				surfaceData.ior = surfaceDescription.IndexOfRefraction;
				surfaceData.transmittanceColor = surfaceDescription.TransmittanceColor;
				surfaceData.atDistance = surfaceDescription.TransmittanceAbsorptionDistance;
				surfaceData.transmittanceMask = surfaceDescription.TransmittanceMask;
#else
				surfaceData.ior = 1.0;
				surfaceData.transmittanceColor = float3( 1.0, 1.0, 1.0 );
				surfaceData.atDistance = 1000000.0;
				surfaceData.transmittanceMask = 0.0;
#endif

				surfaceData.specularOcclusion = 1.0;

#if defined(_BENTNORMALMAP) && defined(_ENABLESPECULAROCCLUSION)
				surfaceData.specularOcclusion = GetSpecularOcclusionFromBentAO ( V, bentNormalWS, surfaceData );
#elif defined(_MASKMAP)
				surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion ( NdotV, surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness ( surfaceData.perceptualSmoothness ) );
#endif
			}
        
				void GetSurfaceAndBuiltinData( GlobalSurfaceDescription surfaceDescription , FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
				{
				#if _ALPHATEST_ON
					DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif
		
					BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
					InitBuiltinData(surfaceDescription.Alpha, surfaceData.normalWS, -fragInputs.worldToTangent[2], fragInputs.positionRWS, fragInputs.texCoord1, fragInputs.texCoord2, builtinData);
        
					builtinData.emissiveColor =             surfaceDescription.Emission;
					builtinData.distortion =                float2(0.0, 0.0);           // surfaceDescription.Distortion -- if distortion pass
					builtinData.distortionBlur =            0.0;                        // surfaceDescription.DistortionBlur -- if distortion pass
        
					builtinData.depthOffset =               0.0;                        // ApplyPerPixelDisplacement(input, V, layerTexCoord, blendMasks); #ifdef _DEPTHOFFSET_ON : ApplyDepthOffsetPositionInput(V, depthOffset, GetWorldToHClipMatrix(), posInput);
        
					PostInitBuiltinData(V, posInput, surfaceData, builtinData);
				}
        
			
				PackedVaryingsMeshToPS Vert(AttributesMesh inputMesh  )
				{
					PackedVaryingsMeshToPS outputPackedVaryingsMeshToPS;

					UNITY_SETUP_INSTANCE_ID(inputMesh);
					UNITY_TRANSFER_INSTANCE_ID(inputMesh, outputPackedVaryingsMeshToPS);

					outputPackedVaryingsMeshToPS.ase_texcoord5.xy = inputMesh.ase_texcoord.xy;
					
					//setting value to unused interpolator channels and avoid initialization warnings
					outputPackedVaryingsMeshToPS.ase_texcoord5.zw = 0;
					inputMesh.positionOS.xyz +=  float3( 0, 0, 0 ) ;
					inputMesh.normalOS =  inputMesh.normalOS ;

					float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
					float3 normalWS = TransformObjectToWorldNormal(inputMesh.normalOS);
					float4 tangentWS = float4(TransformObjectToWorldDir(inputMesh.tangentOS.xyz), inputMesh.tangentOS.w);

					outputPackedVaryingsMeshToPS.positionCS = TransformWorldToHClip(positionRWS);
					outputPackedVaryingsMeshToPS.interp00.xyz = positionRWS;
					outputPackedVaryingsMeshToPS.interp01.xyz = normalWS;
					outputPackedVaryingsMeshToPS.interp02.xyzw = tangentWS;
					outputPackedVaryingsMeshToPS.interp03.xyzw = inputMesh.uv1;
					outputPackedVaryingsMeshToPS.interp04.xyzw = inputMesh.uv2;

					return outputPackedVaryingsMeshToPS;
				}

				void Frag(PackedVaryingsMeshToPS packedInput,
						#ifdef OUTPUT_SPLIT_LIGHTING
							out float4 outColor : SV_Target0,  // outSpecularLighting
							out float4 outDiffuseLighting : SV_Target1,
							OUTPUT_SSSBUFFER(outSSSBuffer)
						#else
							out float4 outColor : SV_Target0
						#endif
						#ifdef _DEPTHOFFSET_ON
							, out float outputDepth : SV_Depth
						#endif,
						 
						  )
				{
					FragInputs input;
					ZERO_INITIALIZE(FragInputs, input);
        
					input.worldToTangent = k_identity3x3;
					input.positionSS = packedInput.positionCS;
					float3 positionRWS = packedInput.interp00.xyz;
					float3 normalWS = packedInput.interp01.xyz;
					float4 tangentWS = packedInput.interp02.xyzw;
						
					input.positionRWS = positionRWS;
					input.worldToTangent = BuildWorldToTangent(tangentWS, normalWS);
					input.texCoord1 = packedInput.interp03.xyzw;
					input.texCoord2 = packedInput.interp04.xyzw;

					// input.positionSS is SV_Position
					PositionInputs posInput = GetPositionInput_Stereo(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS.xyz, uint2(input.positionSS.xy) / GetTileSize(), unity_StereoEyeIndex);

					float3 normalizedWorldViewDir = GetWorldSpaceNormalizeViewDir ( input.positionRWS );

					SurfaceData surfaceData;
					BuiltinData builtinData;
					GlobalSurfaceDescription surfaceDescription = ( GlobalSurfaceDescription ) 0;
					float2 uv_BaseTexture = packedInput.ase_texcoord5.xy * _BaseTexture_ST.xy + _BaseTexture_ST.zw;
					
					float2 uv_Normal = packedInput.ase_texcoord5.xy * _Normal_ST.xy + _Normal_ST.zw;
					
					float2 uv39 = packedInput.ase_texcoord5.xy * float2( 1,1 ) + float2( 0,0 );
					float temp_output_45_0 = ( _Time.y * _Speed );
					float temp_output_37_0 = ( 0.2 * temp_output_45_0 );
					float cos34 = cos( -temp_output_37_0 );
					float sin34 = sin( -temp_output_37_0 );
					float2 rotator34 = mul( ( 1.1 * uv39 ) - float2( 0.25,0.25 ) , float2x2( cos34 , -sin34 , sin34 , cos34 )) + float2( 0.25,0.25 );
					float cos35 = cos( temp_output_37_0 );
					float sin35 = sin( temp_output_37_0 );
					float2 rotator35 = mul( uv39 - float2( 0,0 ) , float2x2( cos35 , -sin35 , sin35 , cos35 )) + float2( 0,0 );
					float2 uv_Texture0 = packedInput.ase_texcoord5.xy * _Texture0_ST.xy + _Texture0_ST.zw;
					float4 tex2DNode18 = tex2D( _Texture0, uv_Texture0 );
					float clampResult52 = clamp( pow( ( 3.0 * tex2DNode18.g ) , 3.0 ) , 0.0 , 1.0 );
					float lerpResult51 = lerp( 0.0 , ( tex2D( _Texture0, rotator34 ).a * tex2D( _Texture0, rotator35 ).a ) , clampResult52);
					float temp_output_19_0 = (0.9 + (sin( temp_output_45_0 ) - 0.0) * (1.3 - 0.9) / (1.0 - 0.0));
					
					float2 uv_Metalic = packedInput.ase_texcoord5.xy * _Metalic_ST.xy + _Metalic_ST.zw;
					float4 tex2DNode11 = tex2D( _Metalic, uv_Metalic );
					
					surfaceDescription.Albedo = ( _Color * tex2D( _BaseTexture, uv_BaseTexture ) ).rgb;
					surfaceDescription.Normal = UnpackNormalmapRGorAG( tex2D( _Normal, uv_Normal ), 1.0f );
					surfaceDescription.Emission = ( _ColorEmissive * ( ( lerpResult51 + tex2DNode18 ) * _EmissivePower * temp_output_19_0 ) ).rgb;
					surfaceDescription.Specular = 0;
					surfaceDescription.Metallic = tex2DNode11.r;
					surfaceDescription.Smoothness = tex2DNode11.a;
					surfaceDescription.Occlusion = 1;
					surfaceDescription.Alpha = 1;
					surfaceDescription.AlphaClipThreshold = 0;

	#ifdef _MATERIAL_FEATURE_CLEAR_COAT
					surfaceDescription.CoatMask = 0;
	#endif

	#if defined(_MATERIAL_FEATURE_SUBSURFACE_SCATTERING) || defined(_MATERIAL_FEATURE_TRANSMISSION)
					surfaceDescription.DiffusionProfile = 0;
	#endif

	#ifdef _MATERIAL_FEATURE_SUBSURFACE_SCATTERING
					surfaceDescription.SubsurfaceMask = 1;
	#endif

	#ifdef _MATERIAL_FEATURE_TRANSMISSION
					surfaceDescription.Thickness = 0;
	#endif

	#ifdef _MATERIAL_FEATURE_ANISOTROPY
					surfaceDescription.Anisotropy = 0;
	#endif

	#ifdef _MATERIAL_FEATURE_IRIDESCENCE
					surfaceDescription.IridescenceThickness = 0;
					surfaceDescription.IridescenceMask = 1;
	#endif

	#ifdef _MATERIAL_FEATURE_TRANSPARENCY
					surfaceDescription.IndexOfRefraction = 1;
					surfaceDescription.TransmittanceColor = float3( 1, 1, 1 );
					surfaceDescription.TransmittanceAbsorptionDistance = 1000000;
					surfaceDescription.TransmittanceMask = 0;
	#endif
					GetSurfaceAndBuiltinData(surfaceDescription, input, normalizedWorldViewDir, posInput, surfaceData, builtinData);

					BSDFData bsdfData = ConvertSurfaceDataToBSDFData(input.positionSS.xy, surfaceData);

					PreLightData preLightData = GetPreLightData(normalizedWorldViewDir, posInput, bsdfData);

					outColor = float4(0.0, 0.0, 0.0, 0.0);

					{
				#ifdef _SURFACE_TYPE_TRANSPARENT
						uint featureFlags = LIGHT_FEATURE_MASK_FLAGS_TRANSPARENT;
				#else
						uint featureFlags = LIGHT_FEATURE_MASK_FLAGS_OPAQUE;
				#endif
						float3 diffuseLighting;
						float3 specularLighting;

						LightLoop(normalizedWorldViewDir, posInput, preLightData, bsdfData, builtinData, featureFlags, diffuseLighting, specularLighting);

				#ifdef OUTPUT_SPLIT_LIGHTING
						if (_EnableSubsurfaceScattering != 0 && ShouldOutputSplitLighting(bsdfData))
						{
							outColor = float4(specularLighting, 1.0);
							outDiffuseLighting = float4(TagLightingForSSS(diffuseLighting), 1.0);
						}
						else
						{
							outColor = float4(diffuseLighting + specularLighting, 1.0);
							outDiffuseLighting = 0;
						}
						ENCODE_INTO_SSSBUFFER(surfaceData, posInput.positionSS, outSSSBuffer);
				#else
						outColor = ApplyBlendMode(diffuseLighting, specularLighting, builtinData.opacity);
						outColor = EvaluateAtmosphericScattering(posInput, normalizedWorldViewDir, outColor);
				#endif
					}

				#ifdef _DEPTHOFFSET_ON
					outputDepth = posInput.deviceDepth;
				#endif
				}

            ENDHLSL
        }
    }
    FallBack "Hidden/InternalErrorShader"
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
274;88;1185;728;135.7638;181.7505;1;True;True
Node;AmplifyShaderEditor.TimeNode;46;-2619.24,124.307;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;24;-2484.034,341.5777;Float;False;Property;_Speed;Speed;5;0;Create;True;0;0;False;0;1;1.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;38;-2375.002,-13.42557;Float;False;Constant;_Float2;Float 2;5;0;Create;True;0;0;False;0;0.2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;45;-2336.24,153.307;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;41;-2032.103,-415.2384;Float;False;Constant;_Float3;Float 3;5;0;Create;True;0;0;False;0;1.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;36;-1700.464,42.89648;Float;True;Property;_Texture0;Texture 0;6;0;Create;True;0;0;False;0;None;84f07d0eb72faae40853854429997125;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;39;-2153.103,-262.2384;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;-1979.501,-16.22557;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-1135.031,-18.09985;Float;False;Constant;_Float0;Float 0;5;0;Create;True;0;0;False;0;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;18;-1296.033,101.5774;Float;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;40;-1823.103,-382.2384;Float;False;2;2;0;FLOAT;0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NegateNode;42;-1846.103,-136.2384;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;-910.1317,54.70013;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;-916.6309,147.0001;Float;False;Constant;_Float1;Float 1;5;0;Create;True;0;0;False;0;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RotatorNode;35;-1651.464,-190.1035;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RotatorNode;34;-1648.464,-373.1035;Float;False;3;0;FLOAT2;0,0;False;1;FLOAT2;0.25,0.25;False;2;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;31;-1331.068,-410.606;Float;True;Property;_TextureSample2;Texture Sample 2;6;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;27;-727.9313,123.0002;Float;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;29;-1329.068,-211.606;Float;True;Property;_TextureSample1;Texture Sample 1;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;52;-564.8088,136.7614;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;-627.0326,-74.32257;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;50;-1438.484,368.8895;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;51;-350.9147,-4.676156;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;13;-77.96294,13.52198;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;19;-995.0326,398.6774;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0.9;False;4;FLOAT;1.3;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;17;-190.4332,138.2774;Float;False;Property;_EmissivePower;Emissive Power;4;0;Create;True;0;0;False;0;1;3.36;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;53;261.1851,0.04812622;Float;False;Property;_ColorEmissive;ColorEmissive;7;1;[HDR];Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;324.6674,186.1773;Float;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;9;-398.8148,-809.6978;Float;False;Property;_Color;Color;1;0;Create;True;0;0;False;0;0,0,0,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;8;-455.8148,-618.6979;Float;True;Property;_BaseTexture;Base Texture;0;0;Create;True;0;0;False;0;None;4960c844b255c6846b962a7423700e8f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;54;528.5442,57.32729;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;12;-445.8148,-414.6982;Float;True;Property;_Normal;Normal;3;0;Create;True;0;0;False;0;None;a5b1b1698f1be464d8770f1d5015a104;True;0;True;white;Auto;True;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;11;-175.7359,365.8555;Float;True;Property;_Metalic;Metalic;2;0;Create;True;0;0;False;0;None;a72e426242717e444ad0edcf7c38f7e1;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-126.8148,-632.698;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;49;-437.1386,431.7903;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;2;0,0;Float;False;False;2;Float;ASEMaterialInspector;0;1;Hidden/Templates/HDSRPPBR;bb308bce79762c34e823049efce65141;0;1;META;0;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;False;False;True;1;False;-1;True;3;False;-1;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;True;2;False;-1;False;False;False;False;False;True;1;LightMode=Meta;False;0;;0;0;Standard;0;22;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;11;FLOAT;0;False;12;INT;0;False;13;FLOAT;0;False;14;FLOAT;0;False;15;FLOAT;0;False;16;FLOAT;0;False;17;FLOAT;0;False;18;FLOAT;0;False;19;FLOAT3;0,0,0;False;20;FLOAT;0;False;21;FLOAT;0;False;9;FLOAT3;0,0,0;False;10;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;739.7886,-86.89916;Float;False;True;2;Float;ASEMaterialInspector;0;2;NewShader/Socle;bb308bce79762c34e823049efce65141;0;0;GBuffer;22;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;False;False;True;1;False;-1;True;3;False;-1;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;False;True;True;2;False;-1;255;False;-1;7;False;-1;7;False;-1;3;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;False;False;True;1;LightMode=GBuffer;False;0;;0;0;Standard;0;22;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;11;FLOAT;0;False;12;INT;0;False;13;FLOAT;0;False;14;FLOAT;0;False;15;FLOAT;0;False;16;FLOAT;0;False;17;FLOAT;0;False;18;FLOAT;0;False;19;FLOAT3;0,0,0;False;20;FLOAT;0;False;21;FLOAT;0;False;9;FLOAT3;0,0,0;False;10;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;6;0,0;Float;False;False;2;Float;ASEMaterialInspector;0;1;Hidden/Templates/HDSRPPBR;bb308bce79762c34e823049efce65141;0;5;Motion Vectors;0;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;False;False;True;1;False;-1;True;3;False;-1;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;False;True;True;128;False;-1;255;False;-1;128;False;-1;7;False;-1;3;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;False;False;True;1;LightMode=MotionVectors;False;0;;0;0;Standard;0;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;3;0,0;Float;False;False;2;Float;ASEMaterialInspector;0;1;Hidden/Templates/HDSRPPBR;bb308bce79762c34e823049efce65141;0;2;ShadowCaster;0;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;False;False;True;1;False;-1;True;3;False;-1;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;True;False;False;False;False;0;False;-1;False;False;False;False;True;1;LightMode=ShadowCaster;False;0;;0;0;Standard;0;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;5;0,0;Float;False;False;2;Float;ASEMaterialInspector;0;1;Hidden/Templates/HDSRPPBR;bb308bce79762c34e823049efce65141;0;4;DepthOnly;0;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;False;False;True;1;False;-1;True;3;False;-1;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;True;False;False;False;False;0;False;-1;False;False;False;False;True;1;LightMode=DepthOnly;False;0;;0;0;Standard;0;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;4;0,0;Float;False;False;2;Float;ASEMaterialInspector;0;1;Hidden/Templates/HDSRPPBR;bb308bce79762c34e823049efce65141;0;3;SceneSelectionPass;0;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;False;False;True;1;False;-1;True;3;False;-1;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;True;False;False;False;False;0;False;-1;False;False;False;False;True;1;LightMode=SceneSelectionPass;False;0;;0;0;Standard;0;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;7;0,0;Float;False;False;2;Float;ASEMaterialInspector;0;1;Hidden/Templates/HDSRPPBR;bb308bce79762c34e823049efce65141;0;6;Forward;0;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;False;False;True;1;False;-1;True;3;False;-1;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;False;True;True;2;False;-1;255;False;-1;7;False;-1;7;False;-1;3;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;False;False;True;1;LightMode=Forward;False;0;;0;0;Standard;0;22;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;11;FLOAT;0;False;12;INT;0;False;13;FLOAT;0;False;14;FLOAT;0;False;15;FLOAT;0;False;16;FLOAT;0;False;17;FLOAT;0;False;18;FLOAT;0;False;19;FLOAT3;0,0,0;False;20;FLOAT;0;False;21;FLOAT;0;False;9;FLOAT3;0,0,0;False;10;FLOAT3;0,0,0;False;0
WireConnection;45;0;46;2
WireConnection;45;1;24;0
WireConnection;37;0;38;0
WireConnection;37;1;45;0
WireConnection;18;0;36;0
WireConnection;40;0;41;0
WireConnection;40;1;39;0
WireConnection;42;0;37;0
WireConnection;25;0;26;0
WireConnection;25;1;18;2
WireConnection;35;0;39;0
WireConnection;35;2;37;0
WireConnection;34;0;40;0
WireConnection;34;2;42;0
WireConnection;31;0;36;0
WireConnection;31;1;34;0
WireConnection;27;0;25;0
WireConnection;27;1;28;0
WireConnection;29;0;36;0
WireConnection;29;1;35;0
WireConnection;52;0;27;0
WireConnection;14;0;31;4
WireConnection;14;1;29;4
WireConnection;50;0;45;0
WireConnection;51;1;14;0
WireConnection;51;2;52;0
WireConnection;13;0;51;0
WireConnection;13;1;18;0
WireConnection;19;0;50;0
WireConnection;16;0;13;0
WireConnection;16;1;17;0
WireConnection;16;2;19;0
WireConnection;54;0;53;0
WireConnection;54;1;16;0
WireConnection;10;0;9;0
WireConnection;10;1;8;0
WireConnection;49;1;19;0
WireConnection;1;0;10;0
WireConnection;1;1;12;0
WireConnection;1;2;54;0
WireConnection;1;4;11;0
WireConnection;1;5;11;4
ASEEND*/
//CHKSM=300443EFF8C9DCD7D90A27459939D8E485826B75