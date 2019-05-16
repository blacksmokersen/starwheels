using UnityEngine;
using UnityEditor.Animations;
using UnityEngine.Audio;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Intentor.Shortcuter.Util {
	/// <summary>
	/// Utility class for types.
	/// </summary>
	public static class TypeUtils {
		/// <summary>Shortcuter main namespace. It's used to exclude types from the Shortcuter system.</summary>
		private const string SHORTCUTER_NAMESPACE = "Intentor.Shortcuter";

		/// <summary>
		/// Gets all available shortcut types.
		/// </summary>
		/// <returns>The shortcut types.</returns>
		public static Dictionary<string, System.Type> GetShortcutTypes() {
			var types = new Dictionary<string, System.Type>();
			types.Add("Scene", null);
			types.Add("Prefab", typeof(UnityEngine.Object));
			types.Add("Script", typeof(UnityEngine.Object));
			types.Add("AnimatorController", typeof(AnimatorController));
			types.Add("Animation", typeof(Animation));
			types.Add("AudioMixer", typeof(AudioMixer));
			types.Add("Material", typeof(Material));
			types.Add("PhysicMaterial", typeof(PhysicMaterial));
			types.Add("PhysicsMaterial2D", typeof(PhysicsMaterial2D));
			types.Add("Shader", typeof(Shader));

			var scriptableObjects = GetTypesDerivedOf(typeof(ScriptableObject));
			for (var index = 0; index < scriptableObjects.Length; index++) {
				var type = scriptableObjects[index];

				if (string.IsNullOrEmpty(type.Namespace) || !type.Namespace.StartsWith(SHORTCUTER_NAMESPACE)) {
					types.Add(type.FullName, type);
				}
			}

			return types;
		}

		/// <summary>
		/// Gets a shortcut type from a type name.
		/// </summary>
		/// <param name="typeName">Type name.</param>
		public static Type GetShortcutType(string typeName) {
			var shortcutTypes = GetShortcutTypes();
			return shortcutTypes[typeName];
		}

		/// <summary>
		/// Gets all available types derived of a given type.
		/// </summary>
		/// <remarks>>
		/// All types from Unity are not considered on the search.
		/// </remarks>
		/// <param name="baseType">Base type.</param>
		/// <returns>The types derived of.</returns>
		public static Type[] GetTypesDerivedOf(Type baseType) {
			var types = new List<Type>();

			//Looks for assignable types in all available assemblies.
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int assemblyIndex = 0; assemblyIndex < assemblies.Length; assemblyIndex++) {
				var assembly = assemblies[assemblyIndex];

				if (assembly.FullName.StartsWith("Unity") ||
					assembly.FullName.StartsWith("Boo") ||
					assembly.FullName.StartsWith("Mono") ||
					assembly.FullName.StartsWith("System") ||
					assembly.FullName.StartsWith("mscorlib")) {
					continue;
				}

				try {
					var allTypes = assemblies[assemblyIndex].GetTypes();
					for (int typeIndex = 0; typeIndex < allTypes.Length; typeIndex++) {
						var type = allTypes[typeIndex];

						if (type.IsSubclassOf(baseType)) {
							types.Add(type);
						}
					}
				} catch (ReflectionTypeLoadException) {
					//If the assembly can't be read, just continue.
					continue;
				}
			}

			return types.ToArray();
		}
	}
}