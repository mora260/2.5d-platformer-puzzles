using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
  private bool _pressable;

  private void Update() {
    if (_pressable) {
      MeshRenderer[] button = GetComponentsInChildren<MeshRenderer>();
      foreach (var item in button)
      {
        if(item.tag == "ElevatorLight") {
          if (Input.GetKeyDown(KeyCode.E)) {
            item.material.color = Color.green;
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
      Debug.Log("bye");
      _pressable = false;        
    }    
  }

}
