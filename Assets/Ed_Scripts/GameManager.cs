using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private float m_countDownTimer = 10;
    public float countDownTimer { get { return m_countDownTimer; } }

    [SerializeField]
    private TextMeshPro m_countDownText;

    [SerializeField]
    private List<Effect> m_potentialEffects;

    [SerializeField]
    private List<Effect> m_activeEffects;

    [SerializeField]
    private float m_totalScore = 0;

    [SerializeField]
    private float m_scoreMultiplier = 1;

    private bool m_hasGameStarted = false;
    private bool m_countDownPause = false;

    public void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;
    }

    public void Update()
    {
        if(m_hasGameStarted)
        {

            if(!m_countDownPause)
            {
                m_countDownTimer -= Time.deltaTime;

                int tempTimer = (int)m_countDownTimer;
                m_countDownText.text = tempTimer.ToString();

                if (m_countDownTimer < 0)
                    CountDownReached();
            }

            for(int i = 0; i < m_activeEffects.Count; i++)
            {
                m_activeEffects[i].TickEffect();
            }

        }
    }

    public void CountDownReached()
    {
        //ResetTimer and Pause Next CountDown;
        m_countDownPause = true;
        m_countDownTimer = 10;

        //How to chose what effect raiting we can allow:
        int chosenEffectRating = Random.Range(1, 10);

        //Find the appropriate next effect:
        Effect nextEffect = null;
        for(int i = 0; i < m_potentialEffects.Count; i++)
        {
            if(m_potentialEffects[i].effectRating <= chosenEffectRating)
            {
                nextEffect = m_potentialEffects[i];
                break; ;
            }

        }

        //Initiate the new Effect:
        nextEffect.InitEffect();

        //Adjust Score Multiplier:
        m_scoreMultiplier += nextEffect.effectMultiplier;

        //Add it to the Effect list;
        m_activeEffects.Add(nextEffect); 
    }

    //Called when the Game actually starts:
    public void OnGameStart()
    {
        m_countDownTimer = 10;
        m_hasGameStarted = true;
        m_countDownPause = false;
        m_activeEffects.Clear();
        m_scoreMultiplier = 1;
        m_totalScore = 0;
    }

    //Called when the Game Ends:
    public void OnGameEnd()
    {
        m_hasGameStarted = false;
        m_countDownPause = true;
        m_countDownText.text = "";
        m_countDownTimer = 10;

        for (int i = 0; i < m_activeEffects.Count; i++)
        {
            m_activeEffects[i].StopEffect();
        }
    }

}
