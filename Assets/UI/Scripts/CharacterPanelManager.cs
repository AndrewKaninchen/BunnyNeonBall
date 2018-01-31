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
			inputModule.DeleteButton = "X" + playerID;
			inputModule.enabled = true;			

			navGroup.mode = HierarchyNavigationGroup.Mode.Vertical;
			navGroup.eventSystem = eventSystem;
			navGroup.childrenExitTarget = null;

			joinPanel.SetActive(false);

			previewPanel = Instantiate(previewPanelPrefab, transform);
			powerListPanel = Instantiate(powerListPanelPrefab, transform);
			powerClassListPanel = Instantiate(powerClassListPanelPrefab, transform);
			effectClassListPanel = Instantiate(effectClassListPanelPrefab, transform);
			availableActionsPanel = Instantiate(availableActionsPanelPrefab, transform);

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
                PreviewPanelManager pPanelManager = previewPanel.GetComponent<PreviewPanelManager>();
                Character character = pPanelManager.Character;
                character.playerID = playerID;
                character.GetComponent<Rolling>().enabled = true;
				character.GetComponent<Rigidbody2D>().gravityScale = 2f;
				

				foreach (GameObject powerPanel in powerListPanel.GetComponent<PowerListPanelManager>().powerPanels)
				{
					PowerPanelManager panelManager = powerPanel.GetComponent<PowerPanelManager>();
					Type controllerType = panelManager.powerCreator.GetType().BaseType.GetGenericArguments()[1];
					PowerController powerController = character.gameObject.AddComponent(controllerType) as PowerController;
                    //panelManager.UpdatePower();

                    Debug.Log(panelManager.PowerInstance);
                    powerController.PowerInstance = panelManager.PowerInstance;
                    Debug.Log(powerController.PowerInstance);
                    powerController.PowerInstance.activationKey = "A";
				}

                pPanelManager.Expand();
            }

            if (Input.GetButtonDown("Select" + playerID) && isInitialized)
			{
				eventSystem.SetSelectedGameObject(namePanel);
			}
		}
	}
}