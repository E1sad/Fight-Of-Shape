using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SOG.Player
{
  public class Movement : MonoBehaviour
  {
    [Header("Variables")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Vector3[] locations;

    [Header("Links")]
    [SerializeField] private Rigidbody2D _playerRb;

    //internal variables
    [HideInInspector] public Location _playerLocation { private set; get; }
    private int _indexOfLocations;
    private bool _isMoving;

    #region My Mehtods
    private Location fromIndexToLocation(int index) { 
      switch (index){
        case 1: return Location.CENTER;
        case 0: return Location.LEFT;
        case 2: return Location.RIGHT;
        default: return Location.NULL;
      }
    }

    private void move(Vector3 target)
    {
      if (target == transform.position) { _isMoving = false; return; }
      _isMoving = true;
      transform.position = Vector3.MoveTowards(transform.position, target, _movementSpeed * Time.deltaTime);
    }

    private void playerMovement(){
      if (_isMoving) return;
      float horizontalSpeed = Input.GetAxisRaw("Horizontal");
      if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
        if(_indexOfLocations < 2) _indexOfLocations += 1;
        _playerLocation = fromIndexToLocation(_indexOfLocations);
      }
      if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
        if (_indexOfLocations > 0) _indexOfLocations -= 1;
        _playerLocation = fromIndexToLocation(_indexOfLocations);
      }
    }

    #endregion

    #region Unity Methods
    private void Start(){
      _playerLocation = Location.CENTER;
      _isMoving = false;
      _indexOfLocations = 1;
    }

    private void Update(){
      playerMovement();
      move(locations[_indexOfLocations]);
    }

    #endregion
  }
}

