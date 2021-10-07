using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private Transform _targetA, _targetB;
    [SerializeField]
    private float _speed = 3.0f;
    private bool _goingDown = false;

    public bool ElevatorDir {
      get {
        return _goingDown;
      }
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (_goingDown == true)
        {

            transform.position = Vector3.MoveTowards(transform.position, _targetB.position, _speed * Time.deltaTime);
        }
        else if (_goingDown == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetA.position, _speed * Time.deltaTime);
        }
    }

    //collison detection with player
    //if we collide with player
    //take the player parent = this game object
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = this.transform;
        }
    }

    //exit collision 
    //check if the player exited
    //take the player parent = null 
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.parent = null;
        }
    }

    public void InteractWithElevator() {
      _goingDown = !_goingDown;
    }

}
