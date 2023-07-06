using SOG.Bullet;
using SOG.Player;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SOG.UI.Shop{
  public class ShopView : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private float _warningTextDuration;
    [SerializeField] private string[] _nameOfUpgrades;
    [SerializeField] private string[] _descriptions;
    [SerializeField] private string[] _improoveMentTextFirst;
    [SerializeField] private string[] _improoveMentTextSecond;
    [SerializeField] private string[] _improoveMentTextThird;
    [SerializeField] private int[] _upgradePricesForFirst;
    [SerializeField] private int[] _upgradePricesForSecond;
    [SerializeField] private int[] _upgradePricesForThird;
    [SerializeField] private int[] _bulletCriticalChanceUpgrade;
    [SerializeField] private PlayerScriptableObject[] _playerStats;
    [SerializeField] private BulletScriptableObject[] _bulletStats;
    

    [Header("Links")]
    [SerializeField] private ShopController controller;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _detailsOfUpgrade;
    [SerializeField] private Text _priceOfUpgrade;
    [SerializeField] private TMP_Text _nameOfUpgrade;
    [SerializeField] private TMP_Text _warningText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _improvementText;
    [SerializeField] private Button _upgradeButton;

    //Internal varibales
    private int[] _referenceUpgradePrice;
    private int[] _indexOfUpgradeLevel = { 0, 0, 0 };
    private int _selectedIndex;
    private Coroutine _routine;
    private string[] _referenceImprovementTexts;

    #region My Methods
    public void OnBackButtonPressed() { controller.OnBackButtonPressed(); }
    public void CoinChangesOnUI(int coin) { _coinText.text = System.Convert.ToString(coin); }
    public void OnFirstUpgradeButtonPressed() {
      _detailsOfUpgrade.text = _descriptions[0]; _nameOfUpgrade.text = "== "+_nameOfUpgrades[0]+" =="; 
      _priceOfUpgrade.text = ""+_upgradePricesForFirst[_indexOfUpgradeLevel[0]];
      _referenceUpgradePrice = _upgradePricesForFirst; _selectedIndex = 0; stopCoroutine();
      setWarningTextAlpha(0); _levelText.text = "Current Level: " +(_indexOfUpgradeLevel[0]+1);
      _improvementText.text = _improoveMentTextFirst[_indexOfUpgradeLevel[0]];
      _referenceImprovementTexts = _improoveMentTextFirst;
      if (_indexOfUpgradeLevel[0] >= 4) { _upgradeButton.interactable = false; _priceOfUpgrade.text = "MAX"; }
      else _upgradeButton.interactable = true;
    }
    public void OnSecondUpgradeButtonPressed(){
      _detailsOfUpgrade.text = _descriptions[1]; _selectedIndex = 1; stopCoroutine();
      _priceOfUpgrade.text = "" + _upgradePricesForSecond[_indexOfUpgradeLevel[1]];
      _nameOfUpgrade.text = "== " + _nameOfUpgrades[1] + " =="; _referenceUpgradePrice = _upgradePricesForSecond;
      setWarningTextAlpha(0); _levelText.text = "Current Level: " + (_indexOfUpgradeLevel[1] + 1);
      _improvementText.text = _improoveMentTextSecond[_indexOfUpgradeLevel[1]];
      _referenceImprovementTexts = _improoveMentTextSecond;
      if (_indexOfUpgradeLevel[1] >= 4) { _upgradeButton.interactable = false; _priceOfUpgrade.text = "MAX"; }
      else _upgradeButton.interactable = true;
    }
    public void OnThirdUpgradeButtonPressed(){
      _detailsOfUpgrade.text = _descriptions[2]; _selectedIndex = 2; stopCoroutine();
      _priceOfUpgrade.text = "" + _upgradePricesForThird[_indexOfUpgradeLevel[2]];
      _nameOfUpgrade.text = "== "+_nameOfUpgrades[2]+" =="; _referenceUpgradePrice = _upgradePricesForThird;
      setWarningTextAlpha(0); _levelText.text = "Current Level: " + (_indexOfUpgradeLevel[2] + 1);
      _improvementText.text = _improoveMentTextThird[_indexOfUpgradeLevel[2]];
      _referenceImprovementTexts = _improoveMentTextThird;
      if (_indexOfUpgradeLevel[2] >= 4) { _upgradeButton.interactable = false; _priceOfUpgrade.text = "MAX"; }
      else _upgradeButton.interactable = true;
    }
    public void OnUpgradeButtonPressed() {
      if (_selectedIndex > 2) return;
      if (controller.GetCoin() >= _referenceUpgradePrice[_indexOfUpgradeLevel[_selectedIndex]]){
        controller.SetCoin(controller.GetCoin() - _referenceUpgradePrice[_indexOfUpgradeLevel[_selectedIndex]]);
        _indexOfUpgradeLevel[_selectedIndex]++; sendUpgradeEvents(_selectedIndex);
        _levelText.text = "Current Level: " + (_indexOfUpgradeLevel[_selectedIndex] + 1);
        _priceOfUpgrade.text = "" + _referenceUpgradePrice[_indexOfUpgradeLevel[_selectedIndex]];
        _improvementText.text = _referenceImprovementTexts[_indexOfUpgradeLevel[_selectedIndex]];}
      else startCoroutine();
      if (_indexOfUpgradeLevel[_selectedIndex] >= 4){
        _upgradeButton.interactable = false; _priceOfUpgrade.text = "MAX";
      }
    }
    private IEnumerator notEnoughMoneyText() {
      float elapsed = 0f; setWarningTextAlpha(1);
      while (_warningTextDuration > elapsed) {
        setWarningTextAlpha(1 - (elapsed / _warningTextDuration));
        elapsed += Time.deltaTime;
        yield return null;}
    }
    private void startCoroutine() { 
      if (_routine != null) StopCoroutine(_routine);  _routine = StartCoroutine(notEnoughMoneyText());
    }
    private void stopCoroutine() { if (_routine != null) StopCoroutine(_routine); _routine = null; }
    private void setWarningTextAlpha(float alpha) { _warningText.alpha = alpha; }
    private void sendUpgradeEvents(int index) {
      switch (index){
        case 0: CriticalBulletChance.Raise(_bulletCriticalChanceUpgrade[_indexOfUpgradeLevel[0]]); break; 
        case 1: PlayerStatsChanged.Raise(_playerStats[_indexOfUpgradeLevel[1]]); break;
        case 2: BulletShapeChanged.Raise(_bulletStats[_indexOfUpgradeLevel[2]]); break; 
        default: break;}
    }
    #endregion

    #region Unity's Methods

    #endregion
  }
}
