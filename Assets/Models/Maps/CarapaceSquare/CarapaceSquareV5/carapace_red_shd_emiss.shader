// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "ArchwallEmissive"
{
    Properties
    {
		[HDR]_ColorEmissive("Color Emissive", Color) = (1.515717,0.2734234,0,0)
		_Speed("Speed", Float) = 0
		_Tiling("Tiling", Vector) = (0,0,0,0)
		_Offset("Offset", Vector) = (0,0,0,0)
		_TextureSample0("Texture Sample 0", 2D) = "white" {}
    }

    SubShader
    {
        Tags { "RenderPipeline"="HDRenderPipeline" "RenderType"="Opaque" "Queue"="Geometry" }

		Blend One Zero
		Cull Back
		ZTest LEqual
		ZWrite On
		Offset 0 , 0

		HLSLINCLUDE
		#pragma target 4.5
		#pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
		#pragma multi_compile_instancing
		ENDHLSL

		
        Pass
        {
			
            Name "Depth prepass"
            Tags { "LightMode"="DepthForwardOnly" }
            ColorMask 0
			
        
            HLSLPROGRAM
        
				#pragma vertex Vert
				#pragma fragment Frag
        
				
        
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Wind.hlsl"
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
        
                #define SHADERPASS SHADERPASS_DEPTH_ONLY
        
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
        
				struct AttributesMesh 
				{
					float3 positionOS : POSITION;
					float4 normalOS : NORMAL;
					
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
				                
                struct SurfaceDescription
                {
                    float Alpha;
                    float AlphaClipThreshold;
                };

				void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
				{
					ZERO_INITIALIZE(SurfaceData, surfaceData);
				}
        
				void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
				{ 
				#if _ALPHATEST_ON
					DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif

					BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
					ZERO_INITIALIZE(BuiltinData, builtinData);
					builtinData.opacity =  surfaceDescription.Alpha;
					builtinData.distortion = float2(0.0, 0.0);
					builtinData.distortionBlur =0.0;
				}

				PackedVaryingsMeshToPS Vert(AttributesMesh inputMesh   )
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

					float3 V = float3(1.0, 1.0, 1.0); // Avoid the division by 0

					SurfaceData surfaceData;
					BuiltinData builtinData;
					SurfaceDescription surfaceDescription = (SurfaceDescription)0;
					
					surfaceDescription.Alpha = 1;
					surfaceDescription.AlphaClipThreshold =  0;

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
				#else
					outColor = float4(0.0, 0.0, 0.0, 0.0);
				#endif
				}
        
            ENDHLSL
        }
		
        Pass
        {
			
            Name "Forward Unlit"
            Tags { "LightMode"="ForwardOnly" }
        
            ColorMask RGBA
			
            HLSLPROGRAM
        
				#pragma vertex Vert
				#pragma fragment Frag

				
        
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Wind.hlsl"
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"

                #define SHADERPASS SHADERPASS_FORWARD_UNLIT
                #pragma multi_compile _ LIGHTMAP_ON
                #pragma multi_compile _ DIRLIGHTMAP_COMBINED
                #pragma multi_compile _ DYNAMICLIGHTMAP_ON
        
        
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
		        #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"
	        
				struct AttributesMesh 
				{
					float3 positionOS : POSITION;
					float4 normalOS : NORMAL;
					float4 ase_texcoord : TEXCOORD0;
					#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : INSTANCEID_SEMANTIC;
					#endif
				};

				struct PackedVaryingsMeshToPS 
				{
					float4 positionCS : SV_Position;
					float4 ase_texcoord : TEXCOORD0;
					#if UNITY_ANY_INSTANCING_ENABLED
					uint instanceID : INSTANCEID_SEMANTIC; 
					#endif 
				};

				CBUFFER_START(UnityPerMaterial)
				uniform float4 _ColorEmissive;
				uniform sampler2D _TextureSample0;
				uniform float _Speed;
				uniform float2 _Tiling;
				uniform float2 _Offset;
				CBUFFER_END
				                
		            
				struct SurfaceDescription
				{
					float3 Color;
					float Alpha;
					float AlphaClipThreshold;
				};
        
		
				void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
				{
					ZERO_INITIALIZE(SurfaceData, surfaceData);
					surfaceData.color = surfaceDescription.Color;
				}
        
				void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription , FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
				{
				#if _ALPHATEST_ON
					DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif
					BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
					ZERO_INITIALIZE(BuiltinData, builtinData); 
					builtinData.opacity = surfaceDescription.Alpha;
					builtinData.distortion = float2(0.0, 0.0); 
					builtinData.distortionBlur = 0.0;
				}
        
         
				PackedVaryingsMeshToPS Vert(AttributesMesh inputMesh )
				{
					PackedVaryingsMeshToPS outputPackedVaryingsMeshToPS;
					UNITY_SETUP_INSTANCE_ID(inputMesh);
					UNITY_TRANSFER_INSTANCE_ID(inputMesh, outputPackedVaryingsMeshToPS);

					outputPackedVaryingsMeshToPS.ase_texcoord.xy = inputMesh.ase_texcoord.xy;
					
					//setting value to unused interpolator channels and avoid initialization warnings
					outputPackedVaryingsMeshToPS.ase_texcoord.zw = 0;
					inputMesh.positionOS.xyz +=  float3( 0, 0, 0 ) ;
					inputMesh.normalOS =  inputMesh.normalOS ;

					float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
					outputPackedVaryingsMeshToPS.positionCS = TransformWorldToHClip(positionRWS);
					return outputPackedVaryingsMeshToPS;
				}

				float4 Frag(PackedVaryingsMeshToPS packedInput) : SV_Target
				{
					
					FragInputs input;
					ZERO_INITIALIZE(FragInputs, input);
					input.worldToTangent = k_identity3x3;
					input.positionSS = packedInput.positionCS;
				
					PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

					float3 V = float3(1.0, 1.0, 1.0);

					SurfaceData surfaceData;
					BuiltinData builtinData;
					SurfaceDescription surfaceDescription = (SurfaceDescription)0;
					float2 uv14 = packedInput.ase_texcoord.xy * _Tiling + _Offset;
					float2 panner15 = ( ( _Speed * _Time.y ) * float2( 1,0 ) + uv14);
					
					surfaceDescription.Color =  ( _ColorEmissive * tex2D( _TextureSample0, panner15 ).r ).rgb;
					surfaceDescription.Alpha = 1;
					surfaceDescription.AlphaClipThreshold =  0;

					GetSurfaceAndBuiltinData(surfaceDescription, input, V, posInput, surfaceData, builtinData);

					BSDFData bsdfData = ConvertSurfaceDataToBSDFData(input.positionSS.xy, surfaceData);

					float4 outColor = ApplyBlendMode(bsdfData.color + builtinData.emissiveColor, builtinData.opacity);
					outColor = EvaluateAtmosphericScattering(posInput, V, outColor);

					return outColor;
				}

            ENDHLSL
        }

		
        Pass
        {
			
            Name "ShadowCaster"
            Tags { "LightMode"="ShadowCaster" }
            
            ZClip [_ZClip]
            ColorMask 0
        
            HLSLPROGRAM
        
				#pragma instancing_options renderinglayer
                #pragma multi_compile _ LOD_FADE_CROSSFADE

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
				                
			    
				struct SurfaceDescription
                {
                    float Alpha;
                    float AlphaClipThreshold;
                };
                    
            
				void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
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
					// If we have bent normal and ambient occlusion, process a specular occlusion
					surfaceData.specularOcclusion = GetSpecularOcclusionFromBentAO(V, bentNormalWS, surfaceData);
			#elif defined(_MASKMAP)
					surfaceData.specularOcclusion = GetSpecularOcclusionFromAmbientOcclusion(NdotV, surfaceData.ambientOcclusion, PerceptualSmoothnessToRoughness(surfaceData.perceptualSmoothness));
			#endif
				}
        
				void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
				{
					#if _ALPHATEST_ON
						DoAlphaTest(surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold);
					#endif
					DoAlphaTest(surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold);
        
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
						SurfaceDescription surfaceDescription = (SurfaceDescription)0;
						
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
			
            Name "META"
            Tags { "LightMode"="Meta" }
        
            Cull Off
        
            HLSLPROGRAM
        
				#pragma vertex Vert
				#pragma fragment Frag
        
				
				
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Wind.hlsl"
        
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
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

				struct AttributesMesh
				{
					float3 positionOS : POSITION;
					float3 normalOS : NORMAL;
					float4 tangentOS : TANGENT;
					float4 uv0 : TEXCOORD0;
					float4 uv1 : TEXCOORD1;
					float4 uv2 : TEXCOORD2;
					float4 color : COLOR;
					
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
				                
                struct SurfaceDescription
                {
                    float3 Color;
                    float Alpha;
                    float AlphaClipThreshold;
                };
                    
				void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
				{
					ZERO_INITIALIZE(SurfaceData, surfaceData);
					surfaceData.color = surfaceDescription.Color;
				}
        
				void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
				{
       
				#if _ALPHATEST_ON
					DoAlphaTest(surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold);
				#endif

					BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
					ZERO_INITIALIZE(BuiltinData, builtinData);
					builtinData.opacity = surfaceDescription.Alpha;
					builtinData.distortion = float2(0.0, 0.0);
					builtinData.distortionBlur = 0.0;
				}
       
				CBUFFER_START(UnityMetaPass)
				bool4 unity_MetaVertexControl;
				bool4 unity_MetaFragmentControl;
				CBUFFER_END

				float unity_OneOverOutputBoost;
				float unity_MaxOutputValue;

				PackedVaryingsMeshToPS Vert(AttributesMesh inputMesh   )
				{
					PackedVaryingsMeshToPS outputPackedVaryingsMeshToPS;

					UNITY_SETUP_INSTANCE_ID(inputMesh);
					UNITY_TRANSFER_INSTANCE_ID(inputMesh, outputPackedVaryingsMeshToPS);

					
					inputMesh.positionOS.xyz +=  float3( 0, 0, 0 ) ;
					inputMesh.normalOS =  inputMesh.normalOS ;

					float2 uv;

					if (unity_MetaVertexControl.x)
					{
						uv = inputMesh.uv1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
					}
					else if (unity_MetaVertexControl.y)
					{
						uv = inputMesh.uv2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
					}

					outputPackedVaryingsMeshToPS.positionCS = float4(uv * 2.0 - 1.0, inputMesh.positionOS.z > 0 ? 1.0e-4 : 0.0, 1.0);
					return outputPackedVaryingsMeshToPS;
				}

				float4 Frag(PackedVaryingsMeshToPS packedInput) : SV_Target
				{			
					FragInputs input;
					ZERO_INITIALIZE(FragInputs, input);
					input.worldToTangent = k_identity3x3;
					input.positionSS = packedInput.positionCS;
                
				
					PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

					float3 V = float3(1.0, 1.0, 1.0); // Avoid the division by 0
		
					SurfaceData surfaceData;
					BuiltinData builtinData;
					SurfaceDescription surfaceDescription = (SurfaceDescription)0;
					
					surfaceDescription.Color =  float3( 1, 1, 1 );
					surfaceDescription.Alpha = 1;
					surfaceDescription.AlphaClipThreshold =  0;

					GetSurfaceAndBuiltinData(surfaceDescription,input, V, posInput, surfaceData, builtinData);
					BSDFData bsdfData = ConvertSurfaceDataToBSDFData(input.positionSS.xy, surfaceData);
					LightTransportData lightTransportData = GetLightTransportData(surfaceData, builtinData, bsdfData);
					float4 res = float4(0.0, 0.0, 0.0, 1.0);
					if (unity_MetaFragmentControl.x)
					{
						res.rgb = clamp(pow(abs(lightTransportData.diffuseColor), saturate(unity_OneOverOutputBoost)), 0, unity_MaxOutputValue);
					}

					if (unity_MetaFragmentControl.y)
					{
						res.rgb = lightTransportData.emissiveColor;
					}

					return res;
				}

            ENDHLSL
		}
    }
    FallBack "Hidden/InternalErrorShader"
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=16103
226;18;1070;993;1135.906;639.9521;1.917792;True;False
Node;AmplifyShaderEditor.RangedFloatNode;25;-1550.932,650.465;Float;False;Property;_Speed;Speed;1;0;Create;True;0;0;False;0;0;1.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;30;-1428.062,397.2174;Float;False;Property;_Tiling;Tiling;2;0;Create;True;0;0;False;0;0,0;1,1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;28;-1425.062,527.2174;Float;False;Property;_Offset;Offset;3;0;Create;True;0;0;False;0;0,0;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TimeNode;23;-1616.053,811.2308;Float;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-1155.87,457.6394;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.3,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;-1279.053,781.2308;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;15;-679.9872,537.2446;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0;False;1;FLOAT;0.25;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ColorNode;19;-367.4335,93.74384;Float;False;Property;_ColorEmissive;Color Emissive;0;1;[HDR];Create;True;0;0;False;0;1.515717,0.2734234,0,0;0,0.6035655,2.023601,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;17;-411.5878,498.698;Float;True;Property;_TextureSample0;Texture Sample 0;4;0;Create;True;0;0;False;0;None;b3f9d7cead126d6469efb7b38ac2f20c;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;172.6793,144.4482;Float;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;31;-1163.062,614.2174;Float;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;10;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;32;502.682,138.1486;Float;False;False;2;Float;ASEMaterialInspector;0;1;Hidden/Templates/HDSRPUnlit;dfe2f27ac20b08c469b2f95c236be0c3;0;0;Depth prepass;0;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;False;False;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;True;False;False;False;False;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;False;False;True;1;LightMode=DepthForwardOnly;False;0;;0;0;Standard;0;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;33;502.682,138.1486;Float;False;True;2;Float;ASEMaterialInspector;0;3;ArchwallEmissive;dfe2f27ac20b08c469b2f95c236be0c3;0;1;Forward Unlit;5;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;False;False;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;False;False;True;1;LightMode=ForwardOnly;False;0;;0;0;Standard;0;5;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;34;502.682,138.1486;Float;False;False;2;Float;ASEMaterialInspector;0;1;Hidden/Templates/HDSRPUnlit;dfe2f27ac20b08c469b2f95c236be0c3;0;2;ShadowCaster;1;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;False;False;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;True;False;False;False;False;0;False;-1;False;False;False;False;True;1;LightMode=ShadowCaster;False;0;;0;0;Standard;0;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;35;502.682,138.1486;Float;False;False;2;Float;ASEMaterialInspector;0;1;Hidden/Templates/HDSRPUnlit;dfe2f27ac20b08c469b2f95c236be0c3;0;3;META;1;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;False;False;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;True;2;False;-1;False;False;False;False;False;True;1;LightMode=Meta;False;0;;0;0;Standard;0;5;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;0
WireConnection;14;0;30;0
WireConnection;14;1;28;0
WireConnection;24;0;25;0
WireConnection;24;1;23;2
WireConnection;15;0;14;0
WireConnection;15;1;24;0
WireConnection;17;1;15;0
WireConnection;21;0;19;0
WireConnection;21;1;17;1
WireConnection;31;0;25;0
WireConnection;33;0;21;0
ASEEND*/
//CHKSM=999351364111EEA83DF6D76AB59E1DDCEED6A401