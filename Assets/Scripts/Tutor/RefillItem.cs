using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillItem : MonoBehaviour
{
    public void Refill(GameObject preset)
    {
        Debug.Log(transform.localPosition);
        Debug.Log(transform.position);
        Instantiate(preset, transform.position, Quaternion.identity);
    }
}
