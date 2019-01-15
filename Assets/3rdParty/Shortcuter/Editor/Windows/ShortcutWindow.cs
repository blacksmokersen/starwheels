using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using Intentor.Shortcuter.Util;
using Intentor.Shortcuter.ValueObjects;

namespace Intentor.Shortcuter.Windows {
	/// <summary>
	/// Shortcut window display.
	/// </summary>
	public class ShortcutWindow : EditorWindow, IHasCustomMenu {
		/// <summary>Edit shortcuts context menu.</summary>
		private GUIContent editContextMenuItem = new GUIContent("Edit shortcuts", "Edit the shortcuts list.");
		/// <summary>Available shortcuts.</summary>
		private ShortcutData shortcuts;

		[MenuItem("Window/Shortcuter %&s")]
		public static void Init() {
			var window = (ShortcutWindow)EditorWindow.GetWindow(typeof(ShortcutWindow), false);
			window.Show();
		}

		public void AddItemsToMenu(GenericMenu menu) {
			menu.AddItem(editContextMenuItem, false, this.EditShortcuts);
		}

		private void OnEnable() {
			this.titleContent = new GUIContent("Shortcuter");
		}

		private void OnFocus() {
			this.shortcuts = AssetUtils.LoadShorcutData();
		}

		private void OnGUI() {
			if (this.shortcuts == null) {
				EditorGUILayout.HelpBox("No shortcut data found. Please reopen the window.", MessageType.Error);
				return;
			} else if (this.shortcuts.types.Count == 0) {
				EditorGUILayout.HelpBox("There are no shortcuts available." +
					"Click the button below to edit shortcuts", MessageType.Warning);

				if (GUILayout.Button(new GUIContent("Edit shortcuts", "Add, remove and organize shortcuts."))) {
					this.EditShortcuts();
				}

				return;
			}

			var totalLines = Mathf.Ceil(this.shortcuts.types.Count / (float)this.shortcuts.columns);
			var columnWidth = (this.position.width / this.shortcuts.columns) - 4;
			var index = 0;

			for (var line = 0; line < totalLines; line++) {
				EditorGUILayout.BeginHorizontal();

				for (var column = 0; column < this.shortcuts.columns; column++, index++) {
					if (index < this.shortcuts.types.Count) {
						this.DrawItem(this.shortcuts.types[index], columnWidth);
					}
				}

				EditorGUILayout.EndHorizontal();
			}
		}

		/// <summary>
		/// Open the shortcuts for edition.
		/// </summary>
		private void EditShortcuts() {
			AssetDatabase.OpenAsset(this.shortcuts);
		}

		/// <summary>
		/// Draws a shortcut type.
		/// </summary>
		/// <param name="shortcutType">Shortcut type to be drawn.</param>
		private void DrawItem(ShortcutType shortcutType, float columnWidth) {
			EditorGUILayout.BeginVertical(GUILayout.Width(columnWidth));

			EditorGUILayout.BeginHorizontal();
			GUILayout.FlexibleSpace();
			GUILayout.Label(shortcutType.columnTitle);
			GUILayout.FlexibleSpace();
			EditorGUILayout.EndHorizontal();

			if (shortcutType.guids.Count == 0) {
				EditorGUILayout.HelpBox("No shortcuts available.", MessageType.Info);
			} else {
				foreach (var guid in shortcutType.guids) {
					var path = AssetDatabase.GUIDToAssetPath(guid);
					var fileName = Path.GetFileNameWithoutExtension(path);

					if (GUILayout.Button(fileName)) {
						if (shortcutType.typeName == "Scene") {
							#if UNITY_5_3_OR_NEWER
                            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) {
								EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
                            }
							#else
							EditorApplication.OpenScene(path);
							#endif
						} else {
							var type = TypeUtils.GetShortcutType(shortcutType.typeName);
							var asset = AssetDatabase.LoadAssetAtPath(path, type);
							AssetDatabase.OpenAsset(asset);
						}
					}
				}
			}

			EditorGUILayout.EndVertical();
		}
	}
}