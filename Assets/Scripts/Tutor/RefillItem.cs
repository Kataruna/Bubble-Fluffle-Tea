using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillItem : MonoBehaviour
{
    [SerializeField] private float cooldown;

    private float countdown;

    private bool usable = true; 

    private void Start()
    {
        countdown = cooldown;
    }

    private void Update()
    {
        if (usable) return;

        countdown += Time.deltaTime;

        if (countdown >= cooldown) usable = true;
    }

    public void Refill(GameObject preset)
    {
        countdown = 0f;
        usable = false;

        Instantiate(preset, transform.position, Quaternion.identity);
    }
}
