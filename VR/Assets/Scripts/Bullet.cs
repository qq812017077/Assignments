using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bullet : MonoBehaviour
{
    public MySlicer slicer;
    public OnHit forwardOnHit;
    public OnHit backwardOnHit;
    [Range(1f,5f)]public float moveSpeed;

    public event Action<int> OnSlice;
    private int val = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (slicer == null) slicer = GetComponentInChildren<MySlicer>();
        forwardOnHit.Hit += HitForward;
        backwardOnHit.Hit += HitBackward;
        OnSlice += UIManager.instance.UpdateScore;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-transform.forward * Time.deltaTime * moveSpeed);
        if (Input.GetMouseButtonDown(0)) {
            slicer.ApplySlice();
        }

        if (forwardOnHit.isHitted == true && backwardOnHit.isHitted == true) {
            if (val == 2)
            {
                slicer.ApplySlice();
                OnSlice?.Invoke(50);
            }
            else
            {
                Debug.Log("失败");
            }
        }
    }

    void HitForward() {
        val = 1;
    }

    void HitBackward() {
        val *= 2;
    }

}
