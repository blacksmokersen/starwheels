using UnityEngine;
using Intentor.Shortcuter.ValueObjects;

namespace Intentor.Shortcuter.Partials {
	/// <summary>
	/// Partial editor base type.
	/// </summary>
	public abstract class PartialEditor {
		/// <summary>Object to be edited.</summary>
		protected ShortcutData editorItem;

		/// <summary>
		/// Initializes a new instance of the <see cref="Intentor.Shortcuter.Partials.PartialEditor"/> class.
		/// </summary>
		/// <param name="editorItem">Object to be edited.</param>
		public PartialEditor(ShortcutData editorItem) {
			this.editorItem = editorItem;
		}

		/// <summary>
		/// Called when the partial window needs to be drawn.
		/// </summary>
		public abstract void OnInspectorGUI();
	}
}
