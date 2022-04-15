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
    private float currentValue;

    [SerializeField]
    private float addValue;

    [SerializeField]
    private Slider slider;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float range;

    [SerializeField]
    private Animator anim;

    //percentage number change with the bar value
    public void onSliderChanged(int value)
    {
        percentage.text = currentValue.ToString(value.ToString());
    }

    //zero percent when start
    void Start()
    {
        currentValue = 0;
    }

    void Update()
    {
        //if player is within the bar range
        if(Vector2.Distance(player.transform.position, slider.transform.position) < range)
        {
            anim.enabled = true;

            //value is added and bar is filled
            if (Input.GetKey(KeyCode.Space))
            {
                currentValue += addValue;
                slider.value = currentValue;
            }
        }
        else
        {
            anim.enabled = false;
        }
    }
}
