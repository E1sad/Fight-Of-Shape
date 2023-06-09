using SOG.Game_Manager;
using UnityEngine;

namespace SOG.Enemy
{
  public class EnemyMovement : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private float _speed;

    [Header("Links")]
    [SerializeField] private Rigidbody2D _enemyRb;

    //Internal varibales
    [HideInInspector] public Rigidbody2D EnemyRb { get { return _enemyRb; } set { } }
    [SerializeField] private bool _isGamePlayState;
    private GameState state;
    private int _hardness;

    #region My Methods
    public void SetIsGamePlayState(bool state) { _isGamePlayState = state; }
    public void SetSpeed(float _speed) { this._speed = _speed; }
    public void SetHardness(int hardness) { _hardness = hardness; }
    #endregion

    #region Unity's Methods
    private void Update(){
      if (!_isGamePlayState) return;
      _enemyRb.velocity = Vector2.up * (-(_speed+ _hardness)) * Time.deltaTime; 
    }
    #endregion
  }
}
