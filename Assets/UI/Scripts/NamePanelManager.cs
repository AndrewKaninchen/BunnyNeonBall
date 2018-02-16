using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

namespace PowerSystem.UI
{
	public class NamePanelManager : MonoBehaviour
	{
		public void Initialize(CharacterPanelManager characterPanelManager)
		{
         	GetComponentInChildren<Text>().text = "P" +  characterPanelManager.playerID + " - Character Name";			
		}
	}
}