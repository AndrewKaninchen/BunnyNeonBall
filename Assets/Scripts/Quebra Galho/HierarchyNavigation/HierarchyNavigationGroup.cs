using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

namespace Utilities
{
	[ExecuteInEditMode]
	//[RequireComponent(typeof(Selectable))]
	public class HierarchyNavigationGroup : MonoBehaviour, ISubmitHandler
	{
		public EventSystem eventSystem;
		public enum Mode { Vertical, Horizontal }
		public Mode mode;

		[Tooltip("GameObject selected when OnCancel is called from a child element of this hierarchy. Default is self.")]
		public Selectable childrenExitTarget;

		private List<Selectable> hierarchyNavigationElements = new List<Selectable>();

		private void UpdateHierarchyNavigationElements()
		{
			foreach (Selectable sel in hierarchyNavigationElements)
				sel.navigation = Navigation.defaultNavigation;
			hierarchyNavigationElements.Clear();

			for (int i = 0; i < transform.childCount; i++)
			{
				Selectable sel;
				if ((sel = transform.GetChild(i).GetComponent<Selectable>()) != null)
				{
					if (sel.GetComponent<HierarchyNavigationElement>() != null && sel.enabled)
						hierarchyNavigationElements.Add(sel);
				}
			}
		}

		private void OrganizeHierarchyNavigation()
		{
			for (int i = 0; i < hierarchyNavigationElements.Count; i++)
			{
				Navigation nav = new Navigation();
				nav.mode = Navigation.Mode.Explicit;

				if (i + 1 < hierarchyNavigationElements.Count)
				{
					if (mode == Mode.Vertical)
						nav.selectOnDown = hierarchyNavigationElements[i + 1];
					else
						nav.selectOnRight = hierarchyNavigationElements[i + 1];
				}

				if (i > 0)
				{
					if (mode == Mode.Vertical)
						nav.selectOnUp = hierarchyNavigationElements[i - 1];
					else
						nav.selectOnLeft = hierarchyNavigationElements[i - 1];
				}

				hierarchyNavigationElements[i].navigation = nav;
			}


		}

		void OnEnable()
		{
			if(childrenExitTarget == null)
				childrenExitTarget = GetComponent<Selectable>();
			if (eventSystem == null)
				eventSystem = GetComponentInParent<EventSystem>();
			if (eventSystem == null)
				eventSystem = EventSystem.current;
			UpdateHierarchyNavigationElements();
			OrganizeHierarchyNavigation();
			UpdateChildrenExits();
		}

		private void UpdateChildrenExits()
		{
			foreach (Selectable sel in hierarchyNavigationElements)
			{
				HierarchyNavigationElement ele = sel.GetComponent<HierarchyNavigationElement>();
				if (ele != null && !ele.overrideExitTarget)
					ele.exitTarget = childrenExitTarget;
			}
		}

		public void OnTransformChildrenChanged()
		{
			UpdateHierarchyNavigationElements();
			OrganizeHierarchyNavigation();
			UpdateChildrenExits();
		}

		public void OnSubmit(BaseEventData eventData)
		{
			if (hierarchyNavigationElements.Count > 0)
				eventSystem.SetSelectedGameObject(hierarchyNavigationElements[0].gameObject);
		}
	}
}