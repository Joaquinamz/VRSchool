using UnityEngine;

public class AutoHide : MonoBehaviour
{
    public float delay = 20f;

    private void Start()
    {
        Invoke(nameof(HideObject), delay);
    }

    void HideObject()
    {
        gameObject.SetActive(false);
    }
}
