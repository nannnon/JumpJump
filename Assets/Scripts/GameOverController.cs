using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    int m_score;

    // Start is called before the first frame update
    void Start()
    {
        Text scoreText = GameObject.Find("Score").GetComponent<Text>();
        scoreText.text = "Score : " + m_score;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetScore(int score)
    {
        m_score = score;
    }

    public void OnClick()
    {
        SceneManager.LoadScene("MainScene");
    }
}
