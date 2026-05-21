using UnityEngine;

public class AxeToggle : MonoBehaviour
{
    public GameObject axe;

    public bool axeEquipped = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            axeEquipped = !axeEquipped;

            axe.SetActive(axeEquipped);
        }
    }
}