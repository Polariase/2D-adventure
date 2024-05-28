using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatBar : MonoBehaviour
{
    public Image hpImage;
    public Image hpDecayImage;
    public Image energeImage;
    private void Update()
    {
        if(hpDecayImage.fillAmount>hpImage.fillAmount)
        {
            hpDecayImage.fillAmount -= Time.deltaTime / 2;
        }
    }
    public void OnHealthChange(float percentage)
    {
        hpImage.fillAmount = percentage;
    }
}
