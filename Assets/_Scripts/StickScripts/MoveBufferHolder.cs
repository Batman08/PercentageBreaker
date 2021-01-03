using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBufferHolder : MonoBehaviour
{
    private string MoveBufferHolderKey = "NumberOfRoundsKey";
    private float _offsetMove = -0.111f;
    private float x;
    private float y;
    private float DefaultXPosition = -0.315f;

    void Start()
    {
        if (PlayerPrefs.GetFloat("CurrentPosition") == 0)
        {
            PlayerPrefs.SetFloat("CurrentPosition", DefaultXPosition);
        }

        bool PastFiveRounds = (PlayerPrefs.GetInt(MoveBufferHolderKey) >= 2);
        if (PastFiveRounds)
        {
            x = PlayerPrefs.GetFloat("CurrentPosition");
            y = transform.localPosition.y;

            PlayerPrefs.SetFloat("CurrentPosition", x + _offsetMove);
        }

        transform.localPosition = new Vector2(PlayerPrefs.GetFloat("CurrentPosition"), y);
    }
}
