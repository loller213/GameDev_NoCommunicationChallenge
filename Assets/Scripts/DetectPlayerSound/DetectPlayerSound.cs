using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayerSound : MonoBehaviour
{
    [SerializeField] private GameObject soundRadius;
    UnitScript _unitScipt;
    private void Start()
    {
        _unitScipt = GetComponent<UnitScript>();
    }

    private void Update()
    {
        if (_unitScipt._isPlayerMoving)
        {
            StartCoroutine(EnableSoundRadius());
        }
    }

    IEnumerator EnableSoundRadius()
    {
        while (_unitScipt._isPlayerMoving)
        {
            soundRadius.GetComponent<CircleCollider2D>().enabled = true;
            soundRadius.GetComponent<CircleCollider2D>().radius = 12.5f;
            yield return new WaitForSeconds(1);
        }
        soundRadius.GetComponent<CircleCollider2D>().enabled = false;
    }
}
