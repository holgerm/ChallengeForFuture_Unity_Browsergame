using System;
using System.Collections;
using System.Collections.Generic;
using Assets.HeroEditor4D.Common.CharacterScripts;
using UnityEngine;

public class CharacterCtrl : MonoBehaviour
{
    public Character4D character;

    public AnimationManager animation;

    public float speed = 1.0f;
    public float jump = 600f;

    private float xMove, yMove;
    private bool jumping;

    // Start is called before the first frame update
    void Start()
    {
        character.SetDirection(Vector2.left);
        animation.SetState(CharacterState.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");

        if (xMove > 0)
        {
            character.SetDirection(Vector2.right);
        }
        else if (xMove < 0)
        {
            character.SetDirection(Vector2.left);
        }
        else if (yMove > 0)
        {
            character.SetDirection(Vector2.up);
        }
        else if (yMove < 0)
        {
            character.SetDirection(Vector2.down);
        }

        transform.Translate(new Vector3(xMove, yMove, 0) * Time.deltaTime * speed);

        if (xMove == 0 && yMove == 0)
        {
            animation.SetState(CharacterState.Idle);
        }
        else
        {
            animation.SetState(CharacterState.Walk);
        }

        if (Input.GetButtonDown("Jump") && !jumping)
        {
            animation.SetState((CharacterState.Jump));
            character.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jump));
            jumping = true;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (null != interactive)
            {
                interactive.Interact();
                interactive = null;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumping = false;
        }
    }

    private Interactive interactive;

    private void OnTriggerEnter2D(Collider2D other)
    {
        interactive = other.GetComponent<Interactive>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        interactive = null;
    }
}