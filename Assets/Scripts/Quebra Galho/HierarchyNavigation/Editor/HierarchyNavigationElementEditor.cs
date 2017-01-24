using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;


[CustomEditor(typeof (HierarchyNavigationElement))]
public class HierarchyNavigationElementEditor : Editor
{
	public override void OnInspectorGUI()
	{
		HierarchyNavigationElement myTarget = (HierarchyNavigationElement)target;
		myTarget.overrideExitTarget = EditorGUILayout.Toggle("Override Exit Target", myTarget.overrideExitTarget);
		if(myTarget.overrideExitTarget)
		{
			myTarget.exitTarget = (Selectable)EditorGUILayout.ObjectField("Exit Target", myTarget.exitTarget, typeof(Selectable));
			//myTarget.exitTo = EditorGUILayout.
		}
	}
}
