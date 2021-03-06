﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using Utilities;
using PowerSystem.Powers;

namespace PowerSystem.UI
{
	public class PowerClassListPanelManager : MonoBehaviour
	{
		public PowerListPanelManager powerListPowerManager;
		
				
		[HideInInspector]	public	List<GameObject> powerClassPanels;
		[SerializeField]	private	GameObject powerClassPanelPrefab;
		[HideInInspector]	public	CharacterPanelManager characterPanelManager;

		public void Initialize(CharacterPanelManager characterPanelManager)
		{
			this.characterPanelManager = characterPanelManager;	
			powerClassPanels = new List<GameObject>();

			for (int i = 0; i < Manager.powerCreatorTypes.Count; i++)
			{
				Type t = Manager.powerCreatorTypes[i];
				string className = t.GetField("name").GetValue(null) as string;
				string classDescription = t.GetField("description").GetValue(null) as string;

				AddPowerClassPanel(i, className, classDescription);
			}

			gameObject.SetActive(false);
		}	

		public void AddPowerClassPanel(int classID, string name, string description)
		{
			GameObject g = Instantiate(powerClassPanelPrefab, transform.GetChild(0));
			g.GetComponent<PowerClassPanelManager>().Initialize(characterPanelManager, classID, name, description);
			powerClassPanels.Add(g);
		}
	}
}