using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using Utilities;
using PowerSystem.Powers;

namespace PowerSystem.UI
{
	public class EffectClassListPanelManager : MonoBehaviour
	{
		[HideInInspector]
		public List<GameObject> effectClassPanels;
		[SerializeField]
		private GameObject effectClassPanelPrefab;
		[HideInInspector]
		public CharacterPanelManager characterPanelManager;

		public void Initialize(CharacterPanelManager characterPanelManager)
		{
			this.characterPanelManager = characterPanelManager;
			effectClassPanels = new List<GameObject>();

			for (int i = 0; i < Manager.effectCreatorTypes.Count; i++)
			{
				Type t = Manager.effectCreatorTypes[i];
				string className = t.GetField("name").GetValue(null) as string;
				string classDescription = t.GetField("description").GetValue(null) as string;

				AddEffectClassPanel(i, className, classDescription);
			}

			gameObject.SetActive(false);
		}

		public void AddEffectClassPanel(int classID, string name, string description)
		{
			GameObject g = Instantiate(effectClassPanelPrefab);
			g.transform.SetParent(transform.GetChild(0));
			g.GetComponent<EffectClassPanelManager>().Initialize(characterPanelManager, classID, name, description);
			effectClassPanels.Add(g);
		}
	}
}