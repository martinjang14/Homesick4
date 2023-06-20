using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerAttributes player;

    private void Update()
    {
        if(player.CurrentHealth <= 0)
        {
            WinLossManager.lose();
        }
    }

}
