using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private float m_countDownTimer = 10;
    public float countDownTimer { get { return m_countDownTimer; } }

    [SerializeField]
    private float m_countDownLag = 0.1f;

    [SerializeField]
    private TextMeshPro m_countDownText;

    [SerializeField]
    private GameObject effectParents;

    private List<Effect> m_potentialEffects = new List<Effect>();

    [SerializeField]
    private List<Effect> m_activeEffects = new List<Effect> ();

    [SerializeField]
    private float m_totalScore = 0;

    [SerializeField]
    private float m_scoreMultiplier = 1;

    [SerializeField]
    private float m_globalSpeed = 1;
    public float globalSpeed { get { return m_globalSpeed; } }

    [SerializeField]
    private float m_spawnTimer = 1f;
    public float spawnTimer { get { return m_spawnTimer; } }

    [SerializeField]
    private float spawnDensityAdj = 0.25f;

    private float timer = 100f;

    [SerializeField]
    private TextMeshProUGUI m_totalScoreText;

    [SerializeField]
    private bool m_hasGameStarted = false;

    [SerializeField]
    private bool m_countDownPause = false;

    [SerializeField]
    private SignController m_signController;

    public void Awake()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;

        for(int i = 1; i < effectParents.transform.childCount; i++)
        {
            Effect tempEffect = effectParents.transform.GetChild(i).GetComponent<Effect>();
            m_potentialEffects.Add(tempEffect);
        }
        
    }

    public void Update()
    {
        if(m_hasGameStarted)
        {
            m_globalSpeed += Time.deltaTime/2;

            timer += Time.deltaTime;

            if(timer > spawnTimer)
            {
                timer = 0;
                m_spawnTimer -= spawnDensityAdj;
                float newSpawnTimer = m_spawnTimer - spawnDensityAdj;

                newSpawnTimer = Mathf.Clamp(newSpawnTimer, 0.15f, 100);

                m_spawnTimer = newSpawnTimer;

                SpawnManager.instance.SpawnObstacle(0);
            }

            if (!m_countDownPause)
            {
                m_totalScore += Time.deltaTime * m_scoreMultiplier;

                m_totalScoreText.text = "Score: " + (int)m_totalScore;

                m_countDownTimer -= Time.deltaTime;

                int tempTimer = (int)m_countDownTimer;
                m_countDownText.text = tempTimer.ToString();

                if (m_countDownTimer < 0)
                    CountDownReached();
            }


        }
        else
        {
            if (PlayerMovement.instance.playerInput != Vector2.zero)
                OnGameStart();
        }
    }

    public void CountDownReached()
    {
        //ResetTimer and Pause Next CountDown;
        m_countDownPause = true;
        m_countDownText.text = "";
        m_signController.SwitchSprites(false);

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

        if (nextEffect == null)
            return;

        //Initiate the new Effect:
        nextEffect.InitEffect();

        //Add it to the Effect list;
        m_activeEffects.Add(nextEffect);

        Invoke("CountDownReset", m_countDownLag);
    }

    public void CountDownReset()
    {
        m_countDownTimer = 10;
        m_countDownText.text = m_countDownTimer.ToString();
        m_signController.SwitchSprites(true);
        m_countDownPause = false;
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
        m_signController.SwitchSprites(true);
    }

    //Called when the Game Ends:
    public void OnGameEnd()
    {
        m_hasGameStarted = false;
        m_countDownPause = true;
        m_countDownText.text = "";
        m_countDownTimer = 10;
        timer = 100f;
        m_signController.SwitchSprites(false);

        for (int i = 0; i < m_activeEffects.Count; i++)
        {
            m_activeEffects[i].StopEffect();
        }
    }

}
