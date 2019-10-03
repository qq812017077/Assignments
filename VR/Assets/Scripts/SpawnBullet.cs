using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    public GameObject prefab;
    public enum EDirection {
        Up,
        Down,
        Left,
        Right
    }
    public float waitTime = 5f;
    private float curTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        //curTime += Time.deltaTime;
        //if (curTime > waitTime) {
        //    curTime = 0;
        //    GameObject go = Instantiate(prefab) as GameObject;
        //    go.transform.position = transform.position;
        //    go.transform.rotation = transform.rotation;
        //    //go.transform.Rotate(Vector3.up,180f);
        //}

    }

    void Spawn(EDirection direct = EDirection.Up) {
        GameObject go = Instantiate(prefab) as GameObject;
        go.transform.position = transform.position;
        go.transform.rotation = transform.rotation;
        switch (direct) {
            case EDirection.Up:
                break;
            case EDirection.Down:
                go.transform.Rotate(new Vector3(0,0,180));
                break;
            case EDirection.Left:
                go.transform.Rotate(new Vector3(0, 0, -90));
                break;
            case EDirection.Right:
                go.transform.Rotate(new Vector3(0, 0, 90));
                break;
        }
    }
}
