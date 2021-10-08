using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    private float _yVelocity;
    private bool _canDoubleJump = false;
    [SerializeField]
    private int _coins;
    private UIManager _uiManager;
    [SerializeField]
    private int _lives = 3;

    private bool _wallJumping;

    private Vector3 _lastWallHitNormal;

    private float _justWallJumped;

    private float _velocityXWallJump;

    public int Coins {
      get {
        return _coins;
      }
      set {
        _coins = value;
      }
    }

    public bool WallJumping {
      get {
        return _wallJumping;
      }
      set {
        _wallJumping = value;
      }
    }

    // Start is called before the first frame update
    void Start()
    {
        _justWallJumped = 0;
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL."); 
        }

        _uiManager.UpdateLivesDisplay(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontalInput, 0, 0);

        if (_controller.isGrounded == true)
        {
            _velocityXWallJump = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_wallJumping) {
                  _yVelocity = _jumpHeight * 0.8f;
                  _velocityXWallJump = _lastWallHitNormal.x * _speed;
                  _canDoubleJump = false;
                  _justWallJumped = 0.3f;
                }

                if (_canDoubleJump == true)
                {
                    _yVelocity = _jumpHeight;
                    _canDoubleJump = false;
                }
            }

            _yVelocity -= _gravity * Time.deltaTime;
        }

        if (_justWallJumped == 0) {
          if (direction.x != 0) {
            _velocityXWallJump = 0;
          }
          Vector3 velocity = direction * _speed;
          velocity.y = _yVelocity;
          _controller.Move(velocity * Time.deltaTime);
        } else if (_justWallJumped > 0) {
          _justWallJumped-=Time.deltaTime;
          if (_justWallJumped < 0) {
            _justWallJumped = 0;
          }
        }

        if ( _velocityXWallJump != 0) {
          _controller.Move(new Vector3(_velocityXWallJump, _yVelocity, 0) * Time.deltaTime);
          if (_velocityXWallJump > 0) {
            _velocityXWallJump-=Time.deltaTime;
            if (_velocityXWallJump < 0) {
              _velocityXWallJump = 0;
            }
          } else {
            _velocityXWallJump+=Time.deltaTime;
            if (_velocityXWallJump > 0) {
              _velocityXWallJump = 0;
            }
          }
        }
    }

    public void AddCoins()
    {
        _coins++;

        _uiManager.UpdateCoinDisplay(_coins);
    }

    public void Damage()
    {
        _lives--;

        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
      if (hit.gameObject.tag == "Wall") {
        _lastWallHitNormal = hit.normal;
        Debug.DrawRay(transform.position, hit.normal, Color.blue);
      }

      if (hit.gameObject.tag == "MovableBox") {
        Rigidbody body = hit.gameObject.GetComponent<Rigidbody>();
        if (body && hit.moveDirection.y == 0) {
          body.velocity = new Vector3( hit.moveDirection.x * 2.0f, 0, 0);
        }
      }
    }
}
