using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
  private bool _pressable;

  [SerializeField]
  private int _coinAmount;

  private Elevator _elevator;

  private void Start() {
    _elevator = GameObject.Find("Elevator").GetComponent<Elevator>();
    if (_elevator == null) {
      Debug.LogError("Elevator script reference is null");
    }
  }

  private void Update() {
    if (_pressable) {
      MeshRenderer[] button = GetComponentsInChildren<MeshRenderer>();
      foreach (var item in button)
      {
        if(item.tag == "ElevatorLight") {
          if (Input.GetKeyDown(KeyCode.E)) {
            Player p = GameObject.Find("Player").GetComponent<Player>();
            if (p && p.Coins >= _coinAmount) {
              item.material.color = Color.green;
              _elevator.InteractWithElevator();
            }
          }
        }
      }
    }    
  }
  private void OnTriggerEnter(Collider other) {
    if (other.tag == "Player") {
      _pressable = true;
    }
  }

  private void OnTriggerExit(Collider other) {
    if (other.tag == "Player") {
      _pressable = false;        
    }    
  }

}
