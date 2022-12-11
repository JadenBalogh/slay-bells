using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] private float minMoveSpeed = 1f;
    [SerializeField] private float maxMoveSpeed = 1f;
    [SerializeField] private Animation2D idleAnim;
    [SerializeField] private Animation2D moveAnim;

    protected Vector2 moveDir;
    private float moveSpeed;

    protected new Rigidbody2D rigidbody2D;
    protected Animator2D animator2D;
    protected SpriteRenderer spriteRenderer;
    protected AudioSource audioSource;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator2D = GetComponent<Animator2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
    }

    protected virtual void Update()
    {
        animator2D.Play(moveDir != Vector2.zero ? moveAnim : idleAnim, true);

        rigidbody2D.velocity = moveDir * moveSpeed;
    }
}
