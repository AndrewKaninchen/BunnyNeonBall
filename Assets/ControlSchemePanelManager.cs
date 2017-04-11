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
	public class ControlSchemePanelManager : MonoBehaviour
	{
		[HideInInspector]
		public CharacterPanelManager characterPanelManager;
		[HideInInspector]
		public MyEventSystem eventSystem;	
		private GameObject addNewActionPanel;

		[HideInInspector]
		public List<GameObject> actionPanels;

		[Header("Prefabs")]
		[SerializeField]
		private GameObject actionPanelPrefab;
		[SerializeField]
		private GameObject addNewActionPanelPrefab;

		private ScrollRect scrollRect;

		public void Initialize(CharacterPanelManager characterPanelManager)
		{
			this.characterPanelManager = characterPanelManager;
			eventSystem = characterPanelManager.eventSystem;

			addNewActionPanel = Instantiate(addNewActionPanelPrefab);
			addNewActionPanel.transform.SetParent(transform.GetChild(0)); //Bug maroto que faz o treco não aparecer me forçou a usar o jeito errado		

			//addNewActionPanel.GetComponent<AddNewActionPanelManager>().Initialize(this, characterPanelManager);
			actionPanels = new List<GameObject>();
			scrollRect = GetComponent<ScrollRect>();
		}
	}
}
