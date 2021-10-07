using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
      if (other.tag == "Player") {
        Player p = other.GetComponent<Player>();
        if (p){
          p.WallJumping = true;
        }
      }
    }

    private void OnTriggerExit(Collider other) {
      if (other.tag == "Player") {
        Player p = other.GetComponent<Player>();
        if (p){
          p.WallJumping = false;
        }
      }
    }
}
