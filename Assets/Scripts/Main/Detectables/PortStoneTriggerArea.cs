using UnityEngine;

public class PortStoneTriggerArea : MonoBehaviour
{
    private bool inTriggerArea;

    public GameObject interactUI;

    private void Start()
    {
        inTriggerArea = false;
        interactUI.SetActive(false);
    }

    private void Update()
    {
        if (inTriggerArea)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                interactUI.SetActive(false);
                GameEventManager.instance.StartPortStoneEncounter();
                AudioManager.instance.PlayVictoryClip();
            }
        }
    }

    private void OnTriggerEnter()
    {
        inTriggerArea = true;
        interactUI.SetActive(true);
    }

    private void OnTriggerExit()
    {
        inTriggerArea = false;
        interactUI.SetActive(false);
    }
}
