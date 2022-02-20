using UnityEngine;
using UnityEngine.UI;

public class moneyUi : MonoBehaviour
{
    public Text moneyText;

    private void Update()
    {
        moneyText.text = "$" + playerStats.money.ToString();
    }
}
