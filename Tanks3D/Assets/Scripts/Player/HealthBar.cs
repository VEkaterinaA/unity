using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Image Bar;
    public void ChangeFill(float fill)
    {
        Bar.fillAmount = fill;
    }

}
