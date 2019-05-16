using UnityEngine;
using System.Collections.Generic;

namespace Intentor.Shortcuter.ValueObjects {
	/// <summary>
	/// Represents a shortcut type, which contains GUIDs that points to actual resources on the project.
	/// </summary>
	[System.Serializable]
	public class ShortcutType {
		/// <summary>Item column title.</summary>
		public string columnTitle;
		/// <summary>Full qualified shortcut item type name.</summary>
		public string typeName;
		/// <summary>Indicates whether the item is folded out on the editor.</summary>
		public bool foldout;
		/// <summary>GUIDs of resources of the particular type.</summary>
		public List<string> guids;
	}
}