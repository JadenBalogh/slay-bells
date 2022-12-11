using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField] private float slamCooldown = 2f;
    [SerializeField] private float slamLength = 1.5f;
    [SerializeField] private LayerMask slamLayer;
    [SerializeField] private Animation2D slamAnim;

    private bool canSlam = true;

    protected override void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        moveDir = new Vector2(moveX, moveY * 0.5f).normalized;

        if (canSlam && Input.GetButtonDown("Fire1"))
        {
            animator2D.Play(slamAnim, false, true);
            StartCoroutine(SlamCooldown());

            Vector2 sourcePos = transform.position;
            Vector2 attackDir = GetAttackDir();

            Collider2D[] targets = Physics2D.OverlapBoxAll(sourcePos + attackDir * slamLength / 2f, Vector2.one * slamLength, 0f, slamLayer);

            foreach (Collider2D target in targets)
            {
                if (target.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.Die();
                }
            }

            spriteRenderer.flipX = attackDir.x > 0f;
        }
        else if (moveX != 0f)
        {
            spriteRenderer.flipX = moveX > 0f;
        }

        base.Update();
    }

    private Vector2 GetAttackDir()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        return (mousePos - playerPos).normalized;
    }

    private IEnumerator SlamCooldown()
    {
        canSlam = false;
        yield return new WaitForSeconds(slamCooldown);
        canSlam = true;
    }
}
