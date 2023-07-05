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
    [SerializeField] private int[] _upgradePricesForFirst;
    [SerializeField] private int[] _upgradePricesForSecond;
    [SerializeField] private int[] _upgradePricesForThird;
    [SerializeField] private int[] _bulletCriticalChanceUpgrade;

    [Header("Links")]
    [SerializeField] private ShopController controller;
    [SerializeField] private TMP_Text _coinText;
    [SerializeField] private TMP_Text _detailsOfUpgrade;
    [SerializeField] private Text _priceOfUpgrade;
    [SerializeField] private TMP_Text _nameOfUpgrade;
    [SerializeField] private TMP_Text _warningText;
    [SerializeField] private TMP_Text _levelText;

    //Internal varibales
    private int[] _referenceUpgradePrice;
    private int[] _indexOfUpgradeLevel = { 0, 0, 0 };
    private int _selectedIndex;
    private Coroutine _routine;

    #region My Methods
    public void OnBackButtonPressed() { controller.OnBackButtonPressed(); }
    public void CoinChangesOnUI(int coin) { _coinText.text = System.Convert.ToString(coin); }
    public void OnFirstUpgradeButtonPressed() {
      _detailsOfUpgrade.text = _descriptions[0]; _nameOfUpgrade.text = "== "+_nameOfUpgrades[0]+" =="; 
      _priceOfUpgrade.text = ""+_upgradePricesForFirst[_indexOfUpgradeLevel[0]];
      _referenceUpgradePrice = _upgradePricesForFirst; _selectedIndex = 0; stopCoroutine();
      setWarningTextAlpha(0); _levelText.text = "Current Level: " +(_indexOfUpgradeLevel[0]+1);
    }
    public void OnSecondUpgradeButtonPressed(){
      _detailsOfUpgrade.text = _descriptions[1]; _selectedIndex = 1; stopCoroutine();
      _priceOfUpgrade.text = "" + _upgradePricesForSecond[_indexOfUpgradeLevel[1]];
      _nameOfUpgrade.text = "== " + _nameOfUpgrades[1] + " =="; _referenceUpgradePrice = _upgradePricesForSecond;
      setWarningTextAlpha(0); _levelText.text = "Current Level: " + (_indexOfUpgradeLevel[1] + 1);
    }
    public void OnThirdUpgradeButtonPressed(){
      _detailsOfUpgrade.text = _descriptions[2]; _selectedIndex = 2; stopCoroutine();
      _priceOfUpgrade.text = "" + _upgradePricesForThird[_indexOfUpgradeLevel[2]];
      _nameOfUpgrade.text = "== "+_nameOfUpgrades[2]+" =="; _referenceUpgradePrice = _upgradePricesForThird;
      setWarningTextAlpha(0); _levelText.text = "Current Level: " + (_indexOfUpgradeLevel[2] + 1);
    }
    public void OnUpgradeButtonPressed() {
      if (_selectedIndex > 2) return;
      if (controller.GetCoin() >= _referenceUpgradePrice[_indexOfUpgradeLevel[_selectedIndex]]){
        controller.SetCoin(controller.GetCoin() - _referenceUpgradePrice[_indexOfUpgradeLevel[_selectedIndex]]);
        _indexOfUpgradeLevel[_selectedIndex]++; sendUpgradeEvents(_selectedIndex);}
      else startCoroutine();
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
        case 0: /*Send event*/ break; case 1: /*Send event*/ break;
        case 2: /*Send event*/ break; default: break;}
    }
    #endregion

    #region Unity's Methods

    #endregion
  }
}
