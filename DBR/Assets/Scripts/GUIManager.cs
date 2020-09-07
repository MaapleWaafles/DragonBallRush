using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController Player;

    [SerializeField]
    private Image KiBar;
    [SerializeField]
    private Text KiCounter;
    [SerializeField]
    private Image healthBar;

    private void Update()
    {
        KiCounter.text = Player.Ki.ToString();
    }
}
