using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using System;
using Utilities;

namespace PowerSystem.UI
{
	public class PreviewPanelManager : MonoBehaviour, ISubmitHandler
	{
		[HideInInspector]
		public CharacterPanelManager characterPanelManager;
		MyEventSystem eventSystem;		
		public Camera previewCamera, previewCamera2;
		[SerializeField]
		private GameObject previewImage;

		public GameObject looksPanel;
		public Dropdown coreMaterialDropdown, ringMaterialDropdown, coreShapeDropDown;

		private HierarchyNavigationGroup navGroup;


		public void Initialize(CharacterPanelManager characterPanelManager)
		{
			this.characterPanelManager = characterPanelManager;
			eventSystem = characterPanelManager.eventSystem;
			RenderTexture previewTexture = Resources.Load("Character " + characterPanelManager.playerID) as RenderTexture;
			previewCamera.targetTexture = previewTexture;
			previewImage.GetComponentInChildren<RawImage>().texture = previewTexture;
		}		

		public void OnSubmit(BaseEventData eventData)
		{
			eventSystem.SetSelectedGameObject(ringMaterialDropdown.gameObject);
		}

		public void OnDropdownCancel()
		{
			eventSystem.SetSelectedGameObject(gameObject);
		}

		public void Expand()
		{
			previewCamera.gameObject.SetActive(false);
			previewCamera2.gameObject.SetActive(true);
			looksPanel.SetActive(false);
		}
	}
}