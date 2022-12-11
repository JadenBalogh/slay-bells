using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private Animation2D moveAnim;

    protected Vector2 moveDir;

    protected new Rigidbody2D rigidbody2D;
    protected Animator2D animator2D;
    protected SpriteRenderer spriteRenderer;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator2D = GetComponent<Animator2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        if (moveDir != Vector2.zero)
        {
            animator2D.Play(moveAnim, true);
        }

        rigidbody2D.velocity = moveDir * moveSpeed;
    }
}
