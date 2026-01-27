using UnityEngine;
using UnityEngine.UI;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private Image[] rocketImages;

    public void UpdateUI(int currentAmmo)
    {

        for (int i = 0; i < rocketImages.Length; i++)
        {
            rocketImages[i].color = i < currentAmmo ? Color.white : Color.gray;
        }

    }
}
