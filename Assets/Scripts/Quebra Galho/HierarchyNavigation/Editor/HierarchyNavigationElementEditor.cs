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
			myTarget.overrideNextTarget = EditorGUILayout.Toggle("Override Next Target", myTarget.overrideNextTarget);
			myTarget.overridePreviousTarget = EditorGUILayout.Toggle("Override Previous Target", myTarget.overridePreviousTarget);

			if (myTarget.overrideExitTarget)			
				myTarget.exitTarget = (Selectable)EditorGUILayout.ObjectField("Exit Target", myTarget.exitTarget, typeof(Selectable), false);			
			
			if (myTarget.overrideNextTarget)
				myTarget.nextTarget = (Selectable)EditorGUILayout.ObjectField("Next Target", myTarget.nextTarget, typeof(Selectable), false);				
			
			if (myTarget.overridePreviousTarget)
				myTarget.previousTarget = (Selectable)EditorGUILayout.ObjectField("Previous Target", myTarget.previousTarget, typeof(Selectable), false);				
		}
	}
}