using UnityEngine.UI;
using UnityEngine;

public class Tests : MonoBehaviour
{
    public Text PercentText;
    public Text PercentNumText1;
    public Text PercentNumText2;


    #region Percentage Variables
    [HideInInspector]
    [Header("Percentage variables")]
    public float Percentage;
    public float NumberOutOfPercent;
    #endregion


    [Header("Buffer Variables")]
    private float TextBuffer;
    public float textbufferValue = 70;

    void Start()
    {
        BufferPercent = MaxBufferValue;
    }

    void Update()
    {
        CalculatePercentageTextOutput();
        ChangeValues();

        if (Input.GetMouseButton(0))
        {
            ChangeValue = true;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            ChangeValue = false;
        }
    }

    void CalculatePercentageTextOutput()
    {
        float FinalBufferValue;

        Percentage = NumberOutOfPercent;
        TextBuffer = Percentage + textbufferValue;

        FinalBufferValue = Mathf.RoundToInt(TextBuffer);
        PercentNumText1.text = Percentage + "%";
        PercentNumText2.text = Mathf.RoundToInt(FinalBufferValue) + "%";
        PercentText.text = "Cut Between " + "        " + " and";
    }

    public bool ChangeValue = false;
    public float BufferPercent;
    public float MaxBufferValue = 0.7f;

    void ChangeValues()
    {
        if (ChangeValue)
        {
            BufferPercent -= 0.05f;
            textbufferValue -= 5;

            //float NewLength = Length * 0.05f;
            //Length += NewLength;
            //Debug.Log(Length);

            ChangeValue = false;
            return;
        }
    }

}
