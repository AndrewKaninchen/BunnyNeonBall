using UnityEngine;
using System.Collections;
using UnityEditor;

namespace PowerSystem.UI
{
	[CustomEditor(typeof(CharacterPanelManager))]
	public class CharacterPanelManagerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			CharacterPanelManager myScript = (CharacterPanelManager)target;
			if (!myScript.isInitialized && GUILayout.Button("Initialize"))
			{
				myScript.Initialize(myScript.playerID);
			}
		}
	}
}