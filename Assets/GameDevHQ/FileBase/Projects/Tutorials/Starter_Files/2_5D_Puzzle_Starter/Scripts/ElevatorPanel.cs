using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
  private bool _pressable;

  [SerializeField]
  private int _coinAmount;

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
