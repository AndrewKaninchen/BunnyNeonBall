using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace Utilities
{
	[CustomEditor(typeof(HierarchyNavigationElement))]
	public class HierarchyNavigationElementEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			HierarchyNavigationElement myTarget = (HierarchyNavigationElement)target;
			myTarget.overrideExitTarget = EditorGUILayout.Toggle("Override Exit Target", myTarget.overrideExitTarget);
			if (myTarget.overrideExitTarget)
			{

				myTarget.exitTarget = (Selectable)EditorGUILayout.ObjectField("Exit Target", target, typeof(Selectable), true);
				//myTarget.exitTo = EditorGUILayout.
			}
		}
	}
}