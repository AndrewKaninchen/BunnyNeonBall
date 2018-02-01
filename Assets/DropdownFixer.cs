using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

public class DropdownFixer : MonoBehaviour
{
    [SerializeField]
    private Dropdown dropdown;

    [SerializeField]
    private EventSystem eventSystem;

    private void Start()
    {
        if (!dropdown) dropdown = GetComponentInParent<Dropdown>();
        if (!eventSystem) eventSystem = GetComponentInParent<EventSystem>();
    }

    public void SetFirstItemSelected()
    {
        var content = transform.Find("Dropdown List").Find("Viewport").Find("Content");
        var target = content.GetChild(1);

        for (int i = 1; i < content.childCount; i++)
        {
            //content.GetChild(i).GetComponent<Dropdown>(on;
        }

        eventSystem.SetSelectedGameObject(target.gameObject);
        //Debug.Log("Well fuck you");
    }

    public void OnDropdownSubmitCancel()
    {
        eventSystem.SetSelectedGameObject(gameObject);
    }
}
