using UnityEngine;

public class WallTrigger : MonoBehaviour
{
    private void OnTriggerEnter()
    {
        GameEventManager.instance.StartCompactor();
        Destroy(gameObject);
    }
}
