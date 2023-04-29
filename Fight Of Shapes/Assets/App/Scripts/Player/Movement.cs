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
    private Location _playerLocation, _targetLocation;
    private int _indexOfLocations;

    #region My Mehtods
    private Location moveTowardsLocation(float horizontalSpeed){
      switch (horizontalSpeed){
        case 1: return Location.RIGHT;
        case -1: return Location.LEFT;
        default: return Location.NULL;
      }
    }

    private Location currentLocation(int index){
      switch (index){
        case 0: return Location.LEFT;
        case 2: return Location.RIGHT;
        case 1: return Location.CENTER;
        default: return Location.NULL;
      }
    }

    private int newIndex(Location loc){
      switch (loc){
        case Location.LEFT: return -1;
        case Location.RIGHT: return 1;
        default: return 0;
      }
    }

    private void playerMovement(){
      float horizontalSpeed = Input.GetAxisRaw("Horizontal");
      _targetLocation = moveTowardsLocation(horizontalSpeed);
      print(_targetLocation);
      if (_playerLocation != _targetLocation){
        transform.position = Vector3.MoveTowards(transform.position, locations[_indexOfLocations], _movementSpeed * Time.deltaTime);
        return;
      }
      _indexOfLocations += newIndex(_targetLocation);
      _playerLocation = currentLocation(_indexOfLocations);
    }

    #endregion

    #region Unity Methods
    private void Start(){
      _playerLocation = Location.CENTER;
      _indexOfLocations = 1;
    }

    private void Update(){
      playerMovement();
    }

    #endregion
  }
}

