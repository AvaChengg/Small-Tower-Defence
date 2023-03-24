using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingController : MonoBehaviour
{
    private int _currentCoin;

    public void GetCurrentCoin(int coin)
    {
         _currentCoin = coin;
    }

}
