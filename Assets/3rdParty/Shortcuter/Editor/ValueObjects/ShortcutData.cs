using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;

namespace Intentor.Shortcuter.ValueObjects {
	/// <summary>
	/// Shortcut data.
	/// </summary>
	public class ShortcutData : ScriptableObject {
		/// <summary>Quantity of columns to display.</summary>
		public int columns = 3;
		/// <summary>Shortcut types to be displayed.</summary>
		public List<ShortcutType> types;
	}
}