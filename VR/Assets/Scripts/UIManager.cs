using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public static UIManager GetInstance() { return instance; }
    public Text Score;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateScore(int reward)
    {
        score += reward;
        Score.text = "Score:" + score;
    }
}
