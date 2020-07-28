using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UIGame uiGame;
    public float time;
    private float _time;

    public bool IsGameOver { get; private set; }
    public bool IsGameStart { get;  set; }
    public Action GameStart;
    public Action AfterGameStart;
    public Action onGameOver;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameStart += Init;
        _time = time;
    }

    public void Init()
    {
        IsGameOver = false;
        IsGameStart = true;
        uiGame.SetDisplayTime(time);
        Human.Instance.onHumanTap = GameOver;
        Human.Instance.onMosquitoEnter = GameOver;
        time = _time;
        AfterGameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver || !IsGameStart)
            return;
        TimePlaying();
    }

    public void TimePlaying()
    {
        time -= Time.deltaTime;
        uiGame.DisplayTime(time);
        IsGameOver = time <= 0;
    }

    void GameOver()
    {
        IsGameOver = true;
        IsGameStart = false;
        Human.Instance.onHumanTap = Nothing;
        Human.Instance.onMosquitoEnter = Nothing;
        onGameOver();
        Debug.Log("Game Over");
    }

    void Nothing()
    {
        // Do Nothing
    }
}
