using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerControl : MonoBehaviourPunCallbacks, IPunObservable
{
    #region Private Fields

    public static GameObject Instance;
    public static GameObject LocalPlayerInstance;
    private Rigidbody2D _rb;
    private TMP_Text _namePlayer;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private float _playerSpeed = 10f;
    private Vector3 networkPosition;
    private string _nickname;
    private bool _isGrounded;

    #endregion

    #region Properties

    public Vector2 Movement { get; set; }
    public float JumpForce => _jumpForce;
    public float PlayerSpeed
    {
        get => _playerSpeed;
        set => _playerSpeed = value;
    }

    #endregion

    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this.gameObject;
        //}   
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _namePlayer = GetComponentInChildren<TMP_Text>();

        if (photonView.IsMine)
        {
            if (LocalPlayerInstance != null) { LocalPlayerInstance = this.gameObject; }
            _nickname = PhotonNetwork.LocalPlayer.NickName;
            _namePlayer.text = _nickname;
        }
        else
        {
            _namePlayer.text = _nickname;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveH = Input.GetAxis("Horizontal");
        bool isJumpPressed = Input.GetButtonDown("Jump");

        // Handle movement on the horizontal axis (2D)
        Movement = new Vector2(moveH * PlayerSpeed, _rb.velocity.y);

        // Handle jump (only when grounded)
        if (isJumpPressed && _isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, JumpForce);
        }
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            // Local player control
            _rb.velocity = new Vector2(Movement.x, _rb.velocity.y);
        }
        else
        {
            // Network player control (smooth synchronization)
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player is on the ground when colliding with the ground layer
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Player is no longer grounded
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = false;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(_nickname);
            stream.SendNext(_isGrounded);
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            _nickname = (string)stream.ReceiveNext();

            if (photonView.IsMine)
            {
                _namePlayer.text = PhotonNetwork.LocalPlayer.NickName;
            }
            else
            {
                _namePlayer.text = _nickname;
            }
        }
    }
}