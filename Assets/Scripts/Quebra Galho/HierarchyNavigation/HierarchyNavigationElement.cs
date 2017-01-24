using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;


namespace Utilities
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Selectable))]
	public class HierarchyNavigationElement : MonoBehaviour, ICancelHandler
	{
		private EventSystem eventSystem;
		private HierarchyNavigationGroup group;
		public bool overrideExitTarget = false;
		public Selectable exitTarget;

		void OnEnable()
		{
			if (transform.parent != null)
				group = transform.parent.GetComponent<HierarchyNavigationGroup>();
			if (group != null)
			{
				eventSystem = group.eventSystem;
				if (!overrideExitTarget)
					exitTarget = group.childrenExitTarget;
			}
		}

		void OnTransformParentChanged()
		{
			OnEnable();
		}

		public void OnCancel(BaseEventData eventData)
		{
			if (!overrideExitTarget)
			{
				if (group == null)
					OnEnable();
				if (group != null)
					if (group.childrenExitTarget != null)
						eventSystem.SetSelectedGameObject(group.childrenExitTarget.gameObject);
			}
			else
			{
				if (exitTarget != null)
					eventSystem.SetSelectedGameObject(exitTarget.gameObject);
			}
		}
	}
}