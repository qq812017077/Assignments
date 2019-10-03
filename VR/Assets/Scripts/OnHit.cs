using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class OnHit : MonoBehaviour
{
    public bool isHitted = false;
    public event Action Hit;
    // Start is called before the first frame update
    void Start()
    {
        isHitted = false;
    }

    // Update is called once per frame


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") {
            isHitted = true;
            Hit?.Invoke();
        }
    }

}
