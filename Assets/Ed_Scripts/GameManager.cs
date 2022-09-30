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
        //ResetTimer;
        m_countDownPause = true;
        m_countDownTimer = 10;
        m_hasGameStarted = false;

        int chosenEffectRaiting = Random.Range(1, 10);

        Effect nextEffect = null;
        for(int i = 0; i < m_potentialEffects.Count; i++)
        {
            if(m_potentialEffects[i].effectRaiting <= chosenEffectRaiting)
            {
                nextEffect = m_potentialEffects[i];
                break; ;
            }

        }

        nextEffect.InitEffect();

        m_activeEffects.Add(nextEffect); 
    }


    public void OnGameStart()
    {
        m_countDownTimer = 10;
        m_hasGameStarted = true;
        m_countDownPause = false;
        m_activeEffects.Clear();
        
    }

    public void OnGameEnd()
    {
        m_hasGameStarted = false;
        m_countDownText.text = "";
        m_countDownTimer = 10;
        m_countDownPause = true;
        for (int i = 0; i < m_activeEffects.Count; i++)
        {
            m_activeEffects[i].StopEffect();
        }
    }

}
