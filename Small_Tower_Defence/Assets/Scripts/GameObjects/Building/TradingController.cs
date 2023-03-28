using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TradingController : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private PlayerController _cameraRig;
    [SerializeField] private GameObject _towerPlacementSpot;
    [SerializeField] private BuildingController _upgradBuilding;
    [SerializeField] private AudioSource _upgradSFX;


    [Header("Upgrade Setting")]
    [SerializeField] private int _upgradePrice;

    private int _currentCoin;
    private Transform _building;
    private BuildingController _buildingController;

    public UnityEvent OnUpgrade;
    public UnityEvent<int> OnCoinUpdated;
    public UnityEvent<string> OnUpdateObjective;
    public UnityEvent<string> OnWarningMoney;

    public void GetCurrentCoin(int coin)
    {
         _currentCoin = coin;
    }

    public void GetCurrentTransform(Transform building)
    {
        _building = building;
        _buildingController = _building.GetComponent<BuildingController>();
    }

    public void Sell()
    {
        if (!_buildingController.IsUpgraded) _currentCoin += _cameraRig.BuildingPrice / 2;
        else _currentCoin += _upgradePrice / 2;

        OnCoinUpdated.Invoke(_currentCoin);
        OnUpdateObjective.Invoke("" + _currentCoin);
        Instantiate(_towerPlacementSpot, _building.position, _building.rotation);
        Destroy(_building.gameObject);
    }

    public void Upgrade()
    {
        if (_currentCoin < _upgradePrice)
        {
            OnWarningMoney.Invoke("You need more money");
            StartCoroutine(ClearText());
            return;
        }
        _upgradSFX.Play();
        _currentCoin -= _upgradePrice;
        OnCoinUpdated.Invoke(_currentCoin);
        OnUpdateObjective.Invoke("" + _currentCoin);
        BuildingController upgradbuilding = Instantiate(_upgradBuilding, _building.position, _building.rotation) as BuildingController;
        upgradbuilding.IsUpgraded = true;
        Destroy(_building.gameObject);
        OnUpgrade.Invoke();
    }

    private IEnumerator ClearText()
    {
        yield return new WaitForSeconds(1);
        OnWarningMoney.Invoke("");
    }
}
