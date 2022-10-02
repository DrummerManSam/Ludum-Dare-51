using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public delegate void OnCountDown();
    public static event OnCountDown onCountDownReached;
    public static event OnCountDown onCountDownStarted;

    [SerializeField]
    private float m_countDownTimer = 10;
    public float countDownTimer { get { return m_countDownTimer; } }

    [SerializeField]
    private float m_countDownLag = 0.1f;

    [SerializeField]
    private float m_totalScore = 0;
    public float totalScore { get { return m_totalScore; } }

    [SerializeField]
    private GameObject endBoardObj;

    [SerializeField]
    private float m_scoreMultiplier = 1;
    public float scoreMultiplier { get { return m_scoreMultiplier; } set { m_scoreMultiplier = value; } }

    [SerializeField]
    private float m_globalSpeed = 1;
    public float globalSpeed { get { return m_globalSpeed; } set { m_globalSpeed = value; } }

    [SerializeField]
    private float m_spawnTimer = 1f;
    public float spawnTimer { get { return m_spawnTimer; } set { m_spawnTimer = value; } }

    [SerializeField]
    private float m_spawnDensityAdj = 0.25f;
    public float spawnDensityAdj { get { return m_spawnDensityAdj; } set { m_spawnDensityAdj = value; } }

    private float timer = 100f;

    public TextMeshPro m_totalScoreText;

    [SerializeField]
    private bool m_hasGameStarted = false;

    [SerializeField]
    private bool m_countDownPause = false;
    public bool countDownPause { get { return m_countDownPause; } }

    [SerializeField]
    private SignController m_signController;

    private List<EffectController> tempEffectList = new List<EffectController>();

    private float m_chaosFactor = 0f;
    public float chaosFactor { get { return m_chaosFactor; } set { m_chaosFactor = value; } }

    [SerializeField]
    private bool m_isHomming = false;
    public bool isHomming { get { return m_isHomming; } set { m_isHomming = value; } }

    public void Awake()
    {
        if (instance != null)
            Destroy(instance);


        instance = this;

        onCountDownReached += CountDownReached;
        
    }

    public void Update()
    {
        if(m_hasGameStarted)
        {
            if (PlayerMovement.instance.IsDead)
                endBoardObj.SetActive(true);

            if (m_countDownPause)
                return;

            timer += Time.deltaTime;

            if(timer > spawnTimer)
            {
                timer = 0;
                m_spawnTimer -= m_spawnDensityAdj;
                float newSpawnTimer = m_spawnTimer - m_spawnDensityAdj;

                newSpawnTimer = Mathf.Clamp(newSpawnTimer, 0f, 100);

                m_spawnTimer = newSpawnTimer;

                SpawnManager.instance.SpawnObstacle(0);
            }
                
                m_countDownTimer -= Time.deltaTime;

                if (m_countDownTimer < 0 && !PlayerMovement.instance.IsDead)
                    onCountDownReached?.Invoke();
            

        }
        else
        {
            if (PlayerMovement.instance.playerInput != Vector2.zero)
                OnGameStart();
        }
    }

    public void AddPoint()
    {
        m_totalScore += 1;
        m_totalScoreText.text = "Score: " + (int)m_totalScore;
    }

    public void AddEffectToList(EffectController tempEffectCon)
    {
        tempEffectList.Add(tempEffectCon);
    }

    public void EffectSelected()
    {
        for(int i = 0; i < tempEffectList.Count; i++)
        {
            if (tempEffectList[i].EffectSelected)
            {
                tempEffectList[i].IntEffect();
            }
                

                if (!tempEffectList[i].EffectSelected)
                Destroy(tempEffectList[i].gameObject);
        }

        tempEffectList.Clear();
        CancelInvoke();
        CountDownReset();
    }

    public void CountDownReached()
    {
        //ResetTimer and Pause Next CountDown;
        m_countDownPause = true;
        m_signController.SwitchSprites(m_countDownPause);

        SpawnManager.instance.SpawnEffectChoice();

        Invoke("CountDownReset", m_countDownLag);
    }

    public void CountDownReset()
    {
        if (tempEffectList.Count > 0)
            tempEffectList[Random.Range(0, tempEffectList.Count)].SelectedEffect();

        onCountDownStarted?.Invoke();
        m_countDownTimer = 10;
        m_countDownPause = false;
        m_signController.SwitchSprites(m_countDownPause);
    }

    //Called when the Game actually starts:
    public void OnGameStart()
    {
        m_countDownTimer = 10;
        m_hasGameStarted = true;
        m_countDownPause = false;
        m_scoreMultiplier = 1;
        m_totalScore = 0;
        m_signController.SwitchSprites(m_countDownPause);
    }

    //Called when the Game Ends:
    public void OnGameEnd()
    {
        m_hasGameStarted = false;
        m_countDownPause = true;
        m_countDownTimer = 10;
        timer = 100f;
      //  m_signController.SwitchSprites(m_countDownPause);
      //  SpawnManager.instance.ResetAllObstacles();
      //  PlayerMovement.instance.ResetPlayer();

    }

    public void OnDestroy()
    {
        onCountDownReached -= CountDownReached;
        CancelInvoke();
    }


}
