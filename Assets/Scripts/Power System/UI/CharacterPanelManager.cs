using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Utilities;
using System;

namespace PowerSystem.UI
{
	[RequireComponent(typeof (MyEventSystem))]
	[RequireComponent(typeof(HierarchyNavigationGroup))]
	[RequireComponent(typeof(StandaloneInputModule))]
	public class CharacterPanelManager : MonoBehaviour
	{
		[Range(1, 4)]
		public int playerID;
		
		private MyInputModule inputModule;
		private HierarchyNavigationGroup navGroup;

		[HideInInspector]	public MyEventSystem eventSystem;
		[HideInInspector]	public bool isInitialized = false;

		#region SubPanels
		[Header("SubPanels")]
		[SerializeField]
		private GameObject
			namePanel;
		[SerializeField]
		private GameObject
			joinPanel;

		[HideInInspector]
		public GameObject
			previewPanel,
			powerListPanel,
			controlSchemePanel,
			availableActionsPanel,
			powerClassListPanel,
			effectClassListPanel;
		#endregion

		#region Prefabs
		[Header("Prefabs")]
		[SerializeField]
		private GameObject
			previewPanelPrefab;
		
		[SerializeField]    private GameObject			
			powerListPanelPrefab,
			controlSchemePanelPrefab,
			availableActionsPanelPrefab,
			powerClassListPanelPrefab,
			effectClassListPanelPrefab;
		#endregion		

		private Character character;

		public void Initialize(int playerID)
		{
			this.playerID = playerID;
			eventSystem = gameObject.GetComponent<MyEventSystem>();
			inputModule = gameObject.GetComponent<MyInputModule>();
			navGroup = gameObject.GetComponent<HierarchyNavigationGroup>();

			eventSystem.SetSelectedGameObject(namePanel);
			eventSystem.inputModule = inputModule;

			inputModule.verticalAxis = "D-Pad Vertical" + playerID;
			inputModule.horizontalAxis = "D-Pad Horizontal" + playerID;
			inputModule.cancelButton = "Cancel" + playerID;
			inputModule.submitButton = "Submit" + playerID;
			inputModule.deleteButton = "X" + playerID;
			inputModule.enabled = true;			

			navGroup.mode = HierarchyNavigationGroup.Mode.Vertical;
			navGroup.eventSystem = eventSystem;
			navGroup.childrenExitTarget = null;

			joinPanel.SetActive(false);

			previewPanel = Instantiate(previewPanelPrefab, transform);
			character = previewPanel.GetComponentInChildren<Character>();
			powerListPanel = Instantiate(powerListPanelPrefab, transform);
			controlSchemePanel = Instantiate(controlSchemePanelPrefab, transform);
			powerClassListPanel = Instantiate(powerClassListPanelPrefab, transform);
			effectClassListPanel = Instantiate(effectClassListPanelPrefab, transform);
			availableActionsPanel = Instantiate(availableActionsPanelPrefab, transform);

			namePanel.GetComponent<NamePanelManager>().Initialize(this);
			previewPanel.GetComponent<PreviewPanelManager>().Initialize(this);
			powerListPanel.GetComponent<PowerListPanelManager>().Initialize(this);
			controlSchemePanel.GetComponent<ControlSchemePanelManager>().Initialize(this);
			powerClassListPanel.GetComponent<PowerClassListPanelManager>().Initialize(this);
			effectClassListPanel.GetComponent<EffectClassListPanelManager>().Initialize(this);

			isInitialized = true;
		}

		public void Update()
		{
			if(Input.GetButtonDown("Start"+playerID) && isInitialized)
			{
				character.playerID = playerID;
				character.GetComponent<Rolling>().enabled = true;
				character.GetComponent<Rigidbody2D>().gravityScale = 2f;
				PreviewPanelManager pPanelManager = previewPanel.GetComponent<PreviewPanelManager>();
				pPanelManager.Expand();

				foreach (GameObject powerPanel in powerListPanel.GetComponent<PowerListPanelManager>().powerPanels)
				{
					PowerPanelManager panelManager = powerPanel.GetComponent<PowerPanelManager>();
					Type controllerType = panelManager.powerCreator.GetType().BaseType.GetGenericArguments()[1];
					PowerController powerController = character.gameObject.AddComponent(controllerType) as PowerController;
					Power power = powerController.Power = panelManager.UpdatePower();
					power.activationKey = "A";
				}				
			}

			if (Input.GetButtonDown("Select" + playerID) && isInitialized)
			{
				eventSystem.SetSelectedGameObject(namePanel);
			}
		}
	}
}