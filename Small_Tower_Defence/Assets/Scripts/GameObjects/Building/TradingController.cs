using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TradingController : MonoBehaviour
{
    private int _currentCoin;

    public void GetCurrentCoin(int coin)
    {
         _currentCoin = coin;
    }

}
