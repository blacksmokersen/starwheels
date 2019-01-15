using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Intentor.Shortcuter.Partials;
using Intentor.Shortcuter.Util;
using Intentor.Shortcuter.ValueObjects;

namespace Intentor.Shortcuter.Inspectors {
	/// <summary>
	/// Shortcut data editor.
	/// </summary>
	[CustomEditor(typeof(ShortcutData))]
	public class ShortcutDataEditor : Editor {
		/// <summary>Object to be edited.</summary>
		private ShortcutData editorItem;
		/// <summary>Add item editor.</summary>
		private AddItemEditor addItemEditor;
		/// <summary>List items editor.</summary>
		private ListItemsEditor listItemsEditor;

		protected void OnEnable() {
			this.editorItem = (ShortcutData)this.target;
			this.addItemEditor = new AddItemEditor(this.editorItem);
			this.listItemsEditor = new ListItemsEditor(this.editorItem);
		}

		public override void OnInspectorGUI() {
			//Quantity of columns.
			this.editorItem.columns = EditorGUILayout.IntSlider(
				new GUIContent("Columns", "Quantity of columns to display."), this.editorItem.columns, 1, 10);

			EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
			EditorGUILayout.LabelField("Shortcut types");

			//Collapse all.
			if (GUILayout.Button(new GUIContent('\u229F'.ToString(), "Collapse all types."),
				EditorStyles.toolbarButton, GUILayout.Width(30))) {
				foreach (var type in this.editorItem.types) {
					type.foldout = false;
				}
			}

			//Add new item.
			if (GUILayout.Button(new GUIContent("+", "Add a new shortcut."),
				EditorStyles.toolbarButton, GUILayout.Width(30))) {
				this.addItemEditor.addMode = true;
			}

			EditorGUILayout.EndHorizontal();

			this.addItemEditor.OnInspectorGUI();
			this.listItemsEditor.OnInspectorGUI();

			if (GUI.changed) EditorUtility.SetDirty(target);
		}
	}
}