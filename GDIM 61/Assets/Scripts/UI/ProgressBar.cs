using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI percentage;

    [SerializeField]
    private int currentValue;

    [SerializeField]
    private int addValue;

    [SerializeField]
    private Slider slider;

    public void onSliderChanged(int value)
    {
        percentage.text = currentValue.ToString(value.ToString());
    }

    void Start()
    {
        currentValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            currentValue += addValue;
            slider.value = currentValue;
        }
    }
}
