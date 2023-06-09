using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Enemy
{
  public class EnemySpawner : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private Vector3[] _locations;
    [SerializeField] private bool _gameStatePlay;
    [SerializeField] private float _frequency;

    [Header("Links")]
    [SerializeField] private GameObject[] _enemyTypes;

    //Internal varibales
    private System.Random _random;
    private List<GameObject> _enemies;
    private IEnumerator _instantiateEnemies, _sendEnemies;

    #region My Methods
    private IEnumerator InstantiateEnemies(){
      for (int type = 0; type < _enemyTypes.Length; type++){
        for (int i = 0; i < 5; i++){
          GameObject enemy = Instantiate(_enemyTypes[type], transform);
          enemy.GetComponent<EnemyStats>().SetSpawner(this);
          enemy.SetActive(false);
          _enemies.Add(enemy);
          yield return new WaitForSeconds(0.1f);}}
    }

    private IEnumerator SendEnemies()
    {while (_gameStatePlay){
        GameObject selectedEnemy = null;
        int random = _random.Next(0, 100);
        if ( random % 6 == 0)
        {for (int i = 0; i < _enemies.Count; i++)
          {if (_enemies[i].GetComponent<EnemyStats>().Corner == 4)
            {selectedEnemy = _enemies[i]; _enemies.RemoveAt(i); break;}}}
        else if(random % 3 == 0)
        {for (int i = 0; i < _enemies.Count; i++)
          {if (_enemies[i].GetComponent<EnemyStats>().Corner == 3)
            { selectedEnemy = _enemies[i]; _enemies.RemoveAt(i); break; }}}
        else
        {for (int i = 0; i < _enemies.Count; i++)
          {if (_enemies[i].GetComponent<EnemyStats>().Corner == 0)
            { selectedEnemy = _enemies[i]; _enemies.RemoveAt(i); break; }}}

        if (selectedEnemy == null) continue;
        selectedEnemy.transform.position = _locations[_random.Next(0, 3)];
        selectedEnemy.SetActive(true);
        yield return new WaitForSeconds(_frequency);}
    }

    public void Destroyed(EnemyStats enemy){
      enemy.gameObject.SetActive(false); _enemies.Add(enemy.gameObject);
    }

    private void StartCoroutineSendEnemies(){StartCoroutine(_sendEnemies);}
    #endregion

    #region Unity's Methods
    private void Start()
    {
      _random = new System.Random();
      _enemies = new List<GameObject>();
      _instantiateEnemies = InstantiateEnemies();
      _sendEnemies = SendEnemies();
      StartCoroutine(_instantiateEnemies);
      Invoke("StartCoroutineSendEnemies", 3f);
    }
    #endregion
  }
}
