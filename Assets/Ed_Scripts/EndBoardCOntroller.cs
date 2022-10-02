using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndBoardCOntroller : MonoBehaviour
{
    public TextMeshPro scoreText;


    public void OnEnable()
    {
        scoreText.text = "Final Score: " + GameManager.instance.totalScore;
    }
}
