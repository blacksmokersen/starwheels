using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace Bolt.Utils
{
	public static class MenuUtililies
	{
		// ======= PUBLIC METHODS =====================================================================================

		[MenuItem("Bolt/Utils/Find Missing Scripts", priority = 25)]
		public static void FindMissingScriptsMenu()
		{
			BoltLog.Info("Searching for Missing Scripts");
			if (FindMissingComponents() == 0)
			{
				BoltLog.Info("Not found any prefab with missing scripts");
			}
		}

		// ======= PRIVATE METHODS =====================================================================================

		public static int FindMissingComponents()
		{
			int missingScriptsCount = 0;
			List<Component> components = new List<Component>();

			var iter = AssetDatabase.FindAssets("t:Prefab").GetEnumerator();

			while (iter.MoveNext())
			{
				var guid = (string) iter.Current;
				var path = AssetDatabase.GUIDToAssetPath(guid);
				var go = AssetDatabase.LoadAssetAtPath<GameObject>(path);

				go.GetComponentsInChildren(true, components);
				for (int j = 0; j < components.Count; ++j)
				{
					if (components[j] == null)
					{
						++missingScriptsCount;
						BoltLog.Error("Missing script: " + path);
					}
				}
				components.Clear();
			}

			if (missingScriptsCount != 0)
			{
				BoltLog.Info("Found {0} Missing Scripts", missingScriptsCount);
			}

			return missingScriptsCount;
		}
	}
}