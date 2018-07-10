using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour
{
    public int Lives;
    public int _sticksDestroyed;

    private readonly int _maxLives = 3;
    private GameManager _manager;

    void Awake()
    {
        _manager = FindObjectOfType<GameManager>();

        Lives = _maxLives;
        _sticksDestroyed = 0;
    }

    void Update()
    {
        CheckAmountOfLives();
        CheckForStickCombo();
    }

    void CheckAmountOfLives()
    {
        bool ZeroLifesLeft = (Lives <= 0);
        if (ZeroLifesLeft)
        {
            _manager.GameOver();
            return;
        }
    }

    void CheckForStickCombo()
    {
        float MaxSticksDestroyedToGetAnotherLife = 5;
        bool CanGiveAnotherLife = (_sticksDestroyed >= MaxSticksDestroyedToGetAnotherLife);
        if (CanGiveAnotherLife)
        {
            Lives++;
            _sticksDestroyed = 0;
        }
    }
}
