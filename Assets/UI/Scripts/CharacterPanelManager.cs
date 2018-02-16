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
	[RequireComponent(typeof(MyInputModule))]
	public class CharacterPanelManager : MonoBehaviour
	{
		[Range(1, 4)]
		public int playerID;

        public bool IsInitialized { get; private set; }

        public GameObject namePanel;
        public GameObject joinPanel;
        public GameObject previewPanel;
        public GameObject controlSchemePanel;
        public GameObject powerListPanel;
        public GameObject availableActionsPanel;
        public GameObject powerClassListPanel;
        public GameObject effectClassListPanel;

        public MyEventSystem eventSystem;

        private MyInputModule inputModule;
		private HierarchyNavigationGroup navGroup;

        
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

            if (!previewPanel)          previewPanel            = transform.Find("Preview Panel").gameObject;
            if (!powerListPanel)        powerListPanel          = transform.Find("Power List Panel").gameObject;
            if (!powerClassListPanel)   powerClassListPanel     = transform.Find("Power Class List Panel").gameObject;
            if (!effectClassListPanel)  effectClassListPanel    = transform.Find("Effect Class List Panel").gameObject;
            if (!availableActionsPanel) availableActionsPanel   = transform.Find("Available Actions Panel").gameObject;
            if (!controlSchemePanel)    controlSchemePanel      = transform.Find("Control Scheme Panel").gameObject;


            namePanel.SetActive(true);
            previewPanel.SetActive(true);
            powerListPanel.SetActive(true);
            powerClassListPanel.SetActive(true);
            availableActionsPanel.SetActive(true);
            effectClassListPanel.SetActive(true);

            namePanel.GetComponent<NamePanelManager>().Initialize(this);
			previewPanel.GetComponent<PreviewPanelManager>().Initialize(this);
			powerListPanel.GetComponent<PowerListPanelManager>().Initialize(this);
			powerClassListPanel.GetComponent<PowerClassListPanelManager>().Initialize(this);
			effectClassListPanel.GetComponent<EffectClassListPanelManager>().Initialize(this);

			IsInitialized = true;
		}

		public void Update()
		{
			if(Input.GetButtonDown("Start"+playerID) && IsInitialized)
			{
                PreviewPanelManager previewPanelManager = previewPanel.GetComponent<PreviewPanelManager>();
                Character character = previewPanelManager.Character;
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

                previewPanelManager.Expand();
            }

            if (Input.GetButtonDown("Select" + playerID) && IsInitialized)
			{
				eventSystem.SetSelectedGameObject(namePanel);
			}
		}
	}
}