using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using System.Collections.Generic;
using Intentor.Shortcuter.Util;
using Intentor.Shortcuter.ValueObjects;
using UnityEditor.Animations;

namespace Intentor.Shortcuter.Partials {
	/// <summary>
	/// Lists available items.
	/// </summary>
	public class ListItemsEditor : PartialEditor {
		public ListItemsEditor(ShortcutData editorItem) : base(editorItem) { }

		public override void OnInspectorGUI() {
			EditorGUI.indentLevel = 1;

			for (var index = 0; index < this.editorItem.types.Count; index++) {
				var shortcutType = this.editorItem.types[index];

				EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);

				//Foldout.
				shortcutType.foldout = EditorGUILayout.Foldout(shortcutType.foldout, shortcutType.columnTitle);

				//Move up.
				if (GUILayout.Button(new GUIContent('\u25B2'.ToString(), "Move the item up."),
					EditorStyles.toolbarButton, GUILayout.Width(30))) {
					if (index > 0) {
						this.editorItem.types.RemoveAt(index);
						this.editorItem.types.Insert(index - 1, shortcutType);
					}
				}

				//Move down.
				if (GUILayout.Button(new GUIContent('\u25BC'.ToString(), "Move the item down."),
					EditorStyles.toolbarButton, GUILayout.Width(30))) {
					if (index < this.editorItem.types.Count - 1) {
						this.editorItem.types.RemoveAt(index);
						this.editorItem.types.Insert(index + 1, shortcutType);
					}
				}

				//Remove.
				if (GUILayout.Button(new GUIContent("X", "Remove the current item."),
					EditorStyles.toolbarButton, GUILayout.Width(30))) {
					this.editorItem.types.RemoveAt(index--);
					continue;
				}

				EditorGUILayout.EndHorizontal();

				if (shortcutType.foldout) {
					//Type name.
					EditorGUILayout.LabelField(shortcutType.typeName);

					//Title.
					shortcutType.columnTitle = EditorGUILayout.TextField(
						new GUIContent("Column title", "Item column title."), shortcutType.columnTitle);
					//If no title is provided, the type name is used instead.
					if (string.IsNullOrEmpty(shortcutType.columnTitle)) {
						shortcutType.columnTitle = shortcutType.typeName;
					}

					this.DrawTypeObjects(shortcutType);
				}
			}

			EditorGUI.indentLevel = 0;
		}

		/// <summary>
		/// Draws the object list of the shortcut type.
		/// </summary>
		/// <param name="shortcutType">Shortcut type to be drawn.</param>
		private void DrawTypeObjects(ShortcutType shortcutType) {
			var guids = AssetUtils.GetAssetsGuid(shortcutType.typeName);

			if (guids.Length == 0) {
				EditorGUILayout.HelpBox("There are no objects for the selected type.", MessageType.Info);
			}

			foreach (var guid in guids) {
				var exists = false;

				//Checks whether the asset exists.
				foreach (var guidOnItem in shortcutType.guids) {
					if (guidOnItem == guid) {
						exists = true;
						break;
					}
				}

				//Check asset selection.
				EditorGUILayout.BeginHorizontal();
				var selected = EditorGUILayout.Toggle(exists, GUILayout.Width(30));
				EditorGUILayout.LabelField(AssetDatabase.GUIDToAssetPath(guid));
				EditorGUILayout.EndHorizontal();

				if (selected && !exists) {
					shortcutType.guids.Add(guid);
				} else if (!selected && exists) {
					shortcutType.guids.Remove(guid);
				}
			}

			//Removes any empty elements from objects.
			shortcutType.guids.Remove(string.Empty);
		}
	}
}
