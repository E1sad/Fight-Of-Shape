using SOG.Bullet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Player
{
  public class Shooting : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private float _ShootingFrequency;

    [Header("Links")]
    [SerializeField] private Movement _playerMovement;
    [SerializeField] private BulletSpawner _spawner;

    //Internal varibales
    private IEnumerator _shootCoroutine;
    private bool _gameStatePlay;

    #region My Methods
    private Vector3 fromLocationToVector(Location loc){
      switch (loc){
        case Location.LEFT: return new Vector3(-1.5f, -4, 0);
        case Location.CENTER: return new Vector3(0, -4, 0);
        case Location.RIGHT: return new Vector3(1.5f, -4, 0);
        default: return new Vector3(10,10,0);
      }
    }

    private IEnumerator shootingCoroutine(){
      while (_gameStatePlay){
        yield return new WaitForSeconds(_ShootingFrequency);
        _spawner.Instantiate(fromLocationToVector(_playerMovement._playerLocation), Quaternion.identity);
      }
    }

    #endregion

    #region Unity's Methods
    private void Start()
    {
      _gameStatePlay = true; // when Menu impliment you should change that
      _shootCoroutine = shootingCoroutine();
      StartCoroutine(_shootCoroutine);

    }

    private void Update()
    {
    }
    #endregion

  }
}

