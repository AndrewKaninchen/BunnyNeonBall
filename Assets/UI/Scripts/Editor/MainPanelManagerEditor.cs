using UnityEngine;
using System.Collections;
using UnityEditor;

namespace PowerSystem.UI
{
	[CustomEditor(typeof(MainPanelManager))]
	public class MainPanelManagerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			MainPanelManager mainPanel = (MainPanelManager)target;
			if (mainPanel.InitializedCharacterCount < 4 && GUILayout.Button("AddPlayer"))
			{
				mainPanel.AddPlayer();
			}
		}
	}
}