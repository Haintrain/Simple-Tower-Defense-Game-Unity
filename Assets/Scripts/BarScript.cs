using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class BarScript : MonoBehaviour
{
    public Image image, imageHover;

    public int selected = 0;

    private int resourceMax;
    private float manaMax;

    private void Start()
    {
        resourceMax = GameyManager.resourcesMax;
        manaMax = GameyManager.manaMax;

        UpdateImage();
    }

    public void UpdateImage()
    {
        switch (selected)
        {
            case 0:
                image.fillAmount = (float)GameyManager.levelResources / resourceMax;
                imageHover.fillAmount = (float)GameyManager.levelResources / resourceMax;
                break;
            case 1:
                if (manaMax > 0)
                {
                    image.fillAmount = (float)GameyManager.levelMana / manaMax;
                    imageHover.fillAmount = (float)GameyManager.levelMana / manaMax;
                }
                break;
        }
        
    }

    public void UpdateHover(int cost, bool hovering)
    {
        if (hovering)
        {
            switch (selected)
            {
                case 0:
                    image.fillAmount = (float)(GameyManager.levelResources - cost) / resourceMax;
                    break;
                case 1:
                    if (manaMax > 0)
                    {
                        image.fillAmount = (float)(GameyManager.levelMana - cost) / manaMax;
                    }
                    break;
            }
            
        }
        else
        {
            switch (selected)
            {
                case 0:
                    image.fillAmount = (float)GameyManager.levelResources / resourceMax;
                    break;
                case 1:
                    if (manaMax > 0)
                    {
                        image.fillAmount = (float)GameyManager.levelMana / manaMax;
                    }
                    break;
            }
        }
    }
}
