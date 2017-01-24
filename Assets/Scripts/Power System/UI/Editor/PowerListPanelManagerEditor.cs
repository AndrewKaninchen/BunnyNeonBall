using UnityEngine;
using System.Collections;
using UnityEditor;

namespace PowerSystem.UI
{
	[CustomEditor(typeof(PowerListPanelManager))]
	public class PowerListPanelManagerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			PowerListPanelManager mainPanel = (PowerListPanelManager)target;
			if (GUILayout.Button("AddPower"))
			{
				//mainPanel.AddPower();
			}
		}
	}
}