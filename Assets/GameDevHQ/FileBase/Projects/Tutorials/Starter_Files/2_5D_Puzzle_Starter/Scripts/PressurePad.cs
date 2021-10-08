using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
    private void OnTriggerStay(Collider other) {
        if (other.tag == "MovableBox") {
            if (Vector3.Distance(transform.position, other.transform.position) < 0.1 ) {
                Rigidbody box = other.transform.GetComponent<Rigidbody>();
                if (box) {
                    box.isKinematic = true;
                }
                MeshRenderer mesh = GetComponentInChildren<MeshRenderer>();
                if (mesh) {
                    mesh.material.color = Color.blue;
                }
            }
        }
    }
}
