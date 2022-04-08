using UnityEngine;
using UnityEngine.UI;
public class HealthBar
{
    private Image Bar;

    public HealthBar(Image bar)
    {
        Bar = bar;
    }

    public void ChangeFill(float fill)
    {
        Bar.fillAmount = fill;
    }

}
