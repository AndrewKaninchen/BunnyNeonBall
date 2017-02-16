using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using Utilities;
using System;

namespace PowerSystem.UI
{
	[RequireComponent (typeof (MyEventSystem))]
	[RequireComponent(typeof(HierarchyNavigationGroup))]
	[RequireComponent(typeof(StandaloneInputModule))]
	public class CharacterPanelManager : MonoBehaviour
	{
		[Range(1, 4)]
		public int playerID;

		public MyEventSystem eventSystem;
		private MyInputModule inputModule;
		private HierarchyNavigationGroup navGroup;

		[HideInInspector]	public bool isInitialized = false;

		[SerializeField]	private GameObject namePanel;
		[SerializeField]	private GameObject joinPanel;

		[SerializeField]	private GameObject previewPanelPrefab;
		[SerializeField]	private GameObject powerListPanelPrefab;
		[SerializeField]	private GameObject availableActionsPanelPrefab;
		[SerializeField]	private GameObject powerClassListPanelPrefab;
		[SerializeField]	private GameObject effectClassListPanelPrefab;

		[HideInInspector]	public GameObject previewPanel;
		[HideInInspector]	public GameObject powerListPanel;
		[HideInInspector]	public GameObject availableActionsPanel;		
		[HideInInspector]	public GameObject powerClassListPanel;
		[HideInInspector]	public GameObject effectClassListPanel;

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
			//previewPanel.transform.SetParent(transform);

			character = previewPanel.GetComponentInChildren<Character>();

			powerListPanel = Instantiate(powerListPanelPrefab, transform);
			//powerListPanel.transform.SetParent(transform);

			powerClassListPanel = Instantiate(powerClassListPanelPrefab, transform);
			//powerClassListPanel.transform.SetParent(transform);

			effectClassListPanel = Instantiate(effectClassListPanelPrefab, transform);
			//effectClassListPanel.transform.SetParent(transform);

			availableActionsPanel = Instantiate(availableActionsPanelPrefab, transform);
			//availableActionsPanel.transform.SetParent(transform);

			namePanel.GetComponent<NamePanelManager>().Initialize(this);
			previewPanel.GetComponent<PreviewPanelManager>().Initialize(this);
			powerListPanel.GetComponent<PowerListPanelManager>().Initialize(this);
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
		}
	}
}