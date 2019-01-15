using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Intentor.Shortcuter.Util;
using Intentor.Shortcuter.ValueObjects;

namespace Intentor.Shortcuter.Partials {
	/// <summary>
	/// Provides adding of an item from a given type.
	/// </summary>
	public class AddItemEditor : PartialEditor {
		/// <summary>Names of the available types.</summary>
		private string[] typeNames;
		/// <summary>Type index, used when adding types.</summary>
		private int typeIndex;
		/// <summary>Add mode internal value.</summary>
		private bool _addMode;

		/// <summary>Indicates whether the editor is in add mode.</summary>
		public bool addMode { 
			get { return _addMode; }  
			set {
				_addMode = value;
				this.typeIndex = 0;
			}
		}

		public AddItemEditor(ShortcutData editorItem) : base(editorItem) { 
			var types = TypeUtils.GetShortcutTypes();

			this.typeNames = new string[types.Keys.Count];
			types.Keys.CopyTo(this.typeNames, 0);
		}

		public override void OnInspectorGUI() {
			if (!this.addMode) return;

			EditorGUI.indentLevel = 1;
			EditorGUILayout.BeginHorizontal();

			this.typeIndex = EditorGUILayout.Popup(typeIndex, this.typeNames);
			var typeName = this.typeNames[this.typeIndex];

			//Save.
			if (GUILayout.Button(new GUIContent("+", "Add the shortcut type."), GUILayout.Width(30))) {
				var item = new ShortcutType() {
					columnTitle = typeName,
					typeName = typeName,
					foldout = true,
					guids = new List<string>()
				};
				this.editorItem.types.Add(item);

				this.addMode = false;
			}

			//Cancel.
			if (GUILayout.Button(new GUIContent("X", "Cancel the adding of the shortcut type."), GUILayout.Width(30))) {
				this.addMode = false;
			}

			EditorGUILayout.EndHorizontal();
			EditorGUI.indentLevel = 0;
		}
	}
}
