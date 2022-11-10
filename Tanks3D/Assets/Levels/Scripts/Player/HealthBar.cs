using UnityEngine.UI;

namespace Assets.Levels.Scripts.Player
{

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
}
