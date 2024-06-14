using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUtilities : MonoBehaviour
{

    //Item Selection for player from HUD screen
    bool isAxeActive;
    public bool HasAxeUtility;

    bool isPickAxeActive;
    public bool HasPickAxeUtility;

    [SerializeField] private GameObject AxeImage;
    [SerializeField] private GameObject PickAxeImage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (HasAxeUtility && !isAxeActive)
            {
                Debug.Log(AxeImage + "is active");
                SetUtilityActive(isAxeActive, AxeImage);
                DeselectUtility(isPickAxeActive, PickAxeImage);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (HasPickAxeUtility && !isPickAxeActive)
            {
                Debug.Log(PickAxeImage + "is active");
                SetUtilityActive(isPickAxeActive, PickAxeImage);
                DeselectUtility(isAxeActive, AxeImage);
            }
        }
    }


    void SetUtilityActive(bool wpActive, GameObject wpName)
    {
        wpActive = true;
        wpName.SetActive(true);
        Debug.Log($"{wpActive} is Active");
    }

    void DeselectUtility(bool wpActive, GameObject wpName)
    {
        wpActive = false;
        wpName.SetActive(false);
        Debug.Log($"{wpActive} is Disabled");
    }

}
