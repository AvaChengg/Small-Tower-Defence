using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TradingController : MonoBehaviour
{
    [Header("Sell Setting")]
    [SerializeField] private int _price = 5;
    [SerializeField] private GameObject _towerPlacementSpot;

    private int _currentCoin;
    private Transform _building;

    public UnityEvent<int> OnCoinUpdated;
    public UnityEvent<string> OnUpdateObjective;

    public void GetCurrentCoin(int coin)
    {
         _currentCoin = coin;
    }

    public void GetCurrentTransform(Transform building)
    {
        _building = building;
    }

    public void Sell()
    {
        _currentCoin += _price;
        OnCoinUpdated.Invoke(_currentCoin);
        OnUpdateObjective.Invoke("" + _currentCoin);
        Instantiate(_towerPlacementSpot, _building.position, _building.rotation);
        Destroy(_building.gameObject);
    }

}
