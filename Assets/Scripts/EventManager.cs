using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    //Destination Event (Unit Script)
    public static Action<GameObject> ON_CLICK_SET_DESTINATION;

    //UI_Manager (UI_Manager_Script)
    public static Action UPDATE_WOOD_UI;
    public static Action UPDATE_STONE_UI;


}
