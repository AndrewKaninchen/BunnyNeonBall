using UnityEngine;
using System.Collections;
using UnityEditor;

namespace PowerSystem
{
	[CustomEditor(typeof(Power))]
	public class PowerEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			Power myScript = (Power)target;
			if (GUILayout.Button("Initialize"))
			{
			}
		}
	}
}