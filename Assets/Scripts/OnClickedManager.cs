using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickedManager : MonoBehaviour
{
    //On click destination
    private void OnMouseDown()
    {
        EventManager.ON_CLICK_SET_DESTINATION?.Invoke(this.gameObject);
    }

}
