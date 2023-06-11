using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Bullet
{ 
  public class BulletSpawner : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private int _numberOfBullets;
    [SerializeField] private int _ordinaryDamage;
    [SerializeField] private int _criticalDamageMin;
    [SerializeField] private int _criticalDamageMax;

    [Header("Links")]
    [SerializeField] private GameObject _bullet;

    //Internal varibales
    private List<GameObject> _bulletList;
    private IEnumerator _instantiateBullets;
    private System.Random random;

    #region My Methods
    
    private IEnumerator InstantiateBullets()
    {
      for(int i = 0; i < _numberOfBullets; i++)
      {
        GameObject bullet = Instantiate(_bullet,transform);
        bullet.SetActive(false);
        bullet.GetComponent<Bullet>().SetBulletSpawner(this);
        _bulletList.Add(bullet);
        if(random.Next(6) == 0){
          bullet.GetComponent<SpriteRenderer>().color = Color.red;
          bullet.GetComponent<Bullet>().Damage = random.Next(_criticalDamageMin, _criticalDamageMax);}
        else{bullet.GetComponent<Bullet>().Damage = _ordinaryDamage;
          bullet.GetComponent<SpriteRenderer>().color = Color.white; }
        yield return new WaitForSeconds(0.1f);
      }
    }

    public void Destroyed(Bullet bullet)
    {
      bullet.gameObject.SetActive(false);
      _bulletList.Add(bullet.gameObject);
    }

    public void Instantiate(Vector3 position, Quaternion rotation)
    {
      GameObject bullet = _bulletList[0];
      _bulletList.RemoveAt(0);
      bullet.transform.position = position;
      bullet.transform.rotation = rotation;
      bullet.SetActive(true);
    }

    #endregion

    #region Unity's Methods
    private void Awake()
    {
      random = new System.Random();
      _bulletList = new List<GameObject>();
      _instantiateBullets = InstantiateBullets();
      StartCoroutine(_instantiateBullets);
    }
    #endregion
  }
}
