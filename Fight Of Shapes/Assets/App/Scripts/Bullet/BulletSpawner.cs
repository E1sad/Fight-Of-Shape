using SOG.Game_Manager;
using SOG.UI.Shop;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Bullet{ 
  public class BulletSpawner : MonoBehaviour{
    [Header("Variables")]
    [SerializeField] private int _numberOfBullets;
    [SerializeField] private int _ordinaryDamage;
    [SerializeField] private int _criticalDamageMin;
    [SerializeField] private int _criticalDamageMax;
    [SerializeField] private int _criticalBulletRandomRange;

    [Header("Links")]
    [SerializeField] private GameObject _bullet;

    //Internal varibales
    private List<GameObject> _throwableBulletList;
    private List<GameObject> _throwedBulletList;
    private List<GameObject> _bulletList;
    private IEnumerator _instantiateBullets;
    private System.Random random;
    [SerializeField] private Sprite _bulletShape; // Temporary SerializeField, Till Save Sytem implemented 

    #region My Methods
    private IEnumerator InstantiateBullets(){
      for(int i = 0; i < _numberOfBullets; i++){
        GameObject bullet = Instantiate(_bullet,transform);
        bullet.SetActive(false);
        bullet.GetComponent<Bullet>().SetIsGamePlayState(true);
        bullet.GetComponent<SpriteRenderer>().sprite = _bulletShape;
        _throwableBulletList.Add(bullet);
        _bulletList.Add(bullet);
        yield return new WaitForSeconds(0.1f);}
    }
    public void Destroyed(Bullet bullet){
      bullet.gameObject.SetActive(false);
      _throwableBulletList.Add(bullet.gameObject);
      _throwedBulletList.RemoveAll(Bullet => Bullet == bullet.gameObject);
    }
    private void instantiate(Vector3 position, Quaternion rotation){
      GameObject bullet = _throwableBulletList[0];
      _throwedBulletList.Add(bullet);
      _throwableBulletList.RemoveAt(0);
      bullet.transform.position = position;
      bullet.transform.rotation = rotation;
      if (random.Next(_criticalBulletRandomRange) == 0){
        bullet.GetComponent<SpriteRenderer>().color = new Color32(220, 47, 47, 255);
        bullet.GetComponent<Bullet>().SetTrailColor(220, 47, 47);
        bullet.GetComponent<Bullet>().Damage = 
          (int)((_ordinaryDamage*(100+random.Next(_criticalDamageMin, _criticalDamageMax)))/100);}
      else{
        bullet.GetComponent<Bullet>().Damage = _ordinaryDamage;
        bullet.GetComponent<SpriteRenderer>().color = new Color32(66, 111, 176, 255);
        bullet.GetComponent<Bullet>().SetTrailColor(66, 111, 176);}
      bullet.SetActive(true);
    }
    private void gameStateEventHandler(object sender, GameStateChangedEvent eventArgs){
      switch (eventArgs.CurrentGameState){
        case GameState.IDLE_STATE: restartAndIdleGameState(); break;
        case GameState.PLAY_STATE: playGameState(); break;
        case GameState.RESTART_STATE: restartAndIdleGameState(); break;
        case GameState.PAUSE_STATE: pauseState(); break;}
    }
    private void pauseState(){
      for (int i = 0; i < _throwedBulletList.Count; i++){
        _throwedBulletList[i].GetComponent<Bullet>().SetIsGamePlayState(false);
        _throwedBulletList[i].GetComponent<Bullet>().BulletRb.bodyType = RigidbodyType2D.Static;}
    }
    private void playGameState(){
      for (int i = 0; i < _throwedBulletList.Count; i++){
        _throwedBulletList[i].GetComponent<Bullet>().SetIsGamePlayState(true);
        _throwedBulletList[i].GetComponent<Bullet>().BulletRb.bodyType = RigidbodyType2D.Dynamic;}
    }
    private void restartAndIdleGameState(){
      for (int i = 0; i < _bulletList.Count; i++){
        _bulletList[i].SetActive(false);
        _bulletList[i].GetComponent<Bullet>().BulletRb.bodyType = RigidbodyType2D.Dynamic;
        _bulletList[i].GetComponent<Bullet>().SetIsGamePlayState(true);}
      for (int i = 0; i < _throwedBulletList.Count; i++){
        _throwableBulletList.Add(_throwedBulletList[0]);
        _throwedBulletList.RemoveAt(0);}
    }
    private void spawnBulletEventHandler(object sender, SpawnBulletEventArgs eventargs){
      instantiate(eventargs.Position, eventargs.Rotation);
    }
    private void destroyBulletEventHandler(object sender, DestroyBulletEventArgs eventArgs){
      Destroyed(eventArgs.ThisBullet);
    }
    private void bulletShapeChangedEventHandler(BulletScriptableObject newShape) {
      _ordinaryDamage = newShape.Damage; _bulletShape = newShape.BulletShape;
      for (int i = 0; i < _bulletList.Count; i++){
        _bulletList[i].GetComponent<SpriteRenderer>().sprite = _bulletShape;}
    }
    private void criticalBulletChanceEventHandler(int chance) { _criticalBulletRandomRange = chance; }
    #endregion

    #region Unity's Methods
    private void Awake(){
      random = new System.Random();
      _throwableBulletList = new List<GameObject>();
      _throwedBulletList = new List<GameObject>();
      _bulletList = new List<GameObject>();
      _instantiateBullets = InstantiateBullets();
      StartCoroutine(_instantiateBullets);
    }
    private void OnEnable(){
      GameStateEvents.OnGameStateChanged += gameStateEventHandler;
      SpawnBulletEvent.EventSpawnBullet += spawnBulletEventHandler;
      DestroyBulletEvent.EventDestroyBullet += destroyBulletEventHandler;
      BulletShapeChanged.BulletShapeChangedEvent += bulletShapeChangedEventHandler;
      CriticalBulletChance.CriticalBulletChanceEvent += criticalBulletChanceEventHandler;
    }
    private void OnDisable(){
      GameStateEvents.OnGameStateChanged -= gameStateEventHandler;
      SpawnBulletEvent.EventSpawnBullet -= spawnBulletEventHandler;
      DestroyBulletEvent.EventDestroyBullet -= destroyBulletEventHandler;
      BulletShapeChanged.BulletShapeChangedEvent -= bulletShapeChangedEventHandler;
      CriticalBulletChance.CriticalBulletChanceEvent -= criticalBulletChanceEventHandler;
    }
    #endregion
  }
}