using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class livesUi : MonoBehaviour
{
    public Text livesText;

    private void Update()
    {
        livesText.text = playerStats.lives + " LIVES";
    }
}
