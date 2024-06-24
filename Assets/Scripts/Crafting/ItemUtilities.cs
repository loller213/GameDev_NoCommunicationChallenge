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

    bool isClubActive;
    public bool HasClubUtility;

    [SerializeField] private GameObject AxeImage;
    [SerializeField] private GameObject PickAxeImage;
    [SerializeField] private GameObject ClubImage;

    private UnitType unit;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (HasAxeUtility && !isAxeActive)
            {
                Debug.Log(AxeImage + "is active");
                SetUtilityActive(isAxeActive, AxeImage);
                DeselectUtility(isPickAxeActive, PickAxeImage);
                DeselectUtility(isClubActive, ClubImage);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (HasPickAxeUtility && !isPickAxeActive)
            {
                Debug.Log(PickAxeImage + "is active");
                SetUtilityActive(isPickAxeActive, PickAxeImage);
                DeselectUtility(isAxeActive, AxeImage);
                DeselectUtility(isClubActive, ClubImage);
            }
        }

        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (HasClubUtility && !isClubActive)
            {
                Debug.Log(ClubImage + "is active");
                SetUtilityActive(isClubActive, ClubImage);
                DeselectUtility(isAxeActive, AxeImage);
                DeselectUtility(isPickAxeActive, PickAxeImage);
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
