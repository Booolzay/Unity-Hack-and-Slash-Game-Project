using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public static int defaultScore;
    public Text score;

    void Start()
    {
        score.GetComponent<Text>();
        defaultScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score : " + defaultScore;
    }
}
