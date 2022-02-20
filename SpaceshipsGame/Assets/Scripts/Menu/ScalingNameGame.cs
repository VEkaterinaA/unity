using UnityEngine;
using UnityEngine.UI;

public class ScalingNameGame : MonoBehaviour
{
    public Text text;
    private bool down = false;
    private int EvenNumber = 0;
    // Update is called once per frame
    void Update()
    {
        EvenNumber++;
        if(EvenNumber%5==0)
        ChangeSizeTextNameGame();

    }

    private void ChangeSizeTextNameGame()
    {
        if (down)
            text.fontSize -= 1;
        else text.fontSize += 1;
        if (text.fontSize > 59)
            down = true;
        else if (text.fontSize < 11)
            down = false;
    }
}
