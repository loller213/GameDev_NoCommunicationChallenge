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
    public static Action UPDATE_INVENTORY_UI;
    
    //Objectives
    public static Action<ItemType> ON_OBJECTIVE_COMPLETE;

    //Unit Update HP (Unit Script)
    public static Action<int> UPDATE_UNIT_HP;
    
    //Actions
    public static Action ON_DROP_RESOURCES;
    public static Action ON_ENEMY_HIT;

}
