using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [SerializeField] private float slamCooldown = 2f;
    [SerializeField] private float slamLength = 1.5f;
    [SerializeField] private LayerMask slamLayer;
    [SerializeField] private GameObject slamFXPrefab;
    [SerializeField] private Animation2D slamAnim;
    [SerializeField] private float killStreakMaxInterval = 2f;
    [SerializeField] private KillAnnouncement[] killAnnouncements;

    private bool canSlam = true;
    private int killStreak = 0;
    private Coroutine killTimerCR;

    protected override void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY * 0.5f).normalized;

        int killCount = 0;

        if (canSlam && Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(SlamCooldown());
            animator2D.Play(slamAnim, false, true);

            Vector2 sourcePos = transform.position;
            Vector2 attackDir = GetAttackDir();

            Instantiate(slamFXPrefab, sourcePos + attackDir * slamLength / 2f, Quaternion.identity);
            Collider2D[] targets = Physics2D.OverlapBoxAll(sourcePos + attackDir * slamLength / 2f, Vector2.one * slamLength, 0f, slamLayer);

            foreach (Collider2D target in targets)
            {
                if (target.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.Die();
                    killCount++;
                }
            }

            spriteRenderer.flipX = attackDir.x > 0f;
        }
        else if (moveX != 0f)
        {
            spriteRenderer.flipX = moveX > 0f;
        }

        if (killCount > 0)
        {
            PlayKillAnnouncement(killCount);
            killStreak += killCount;

            if (killTimerCR != null) StopCoroutine(killTimerCR);
            killTimerCR = StartCoroutine(KillStreakTimer());
        }

        base.Update();
    }

    private void PlayKillAnnouncement(int killCount)
    {
        for (int i = killAnnouncements.Length - 1; i >= 0; i--)
        {
            KillAnnouncement killAnnouncement = killAnnouncements[i];
            if (killStreak + killCount >= killAnnouncement.killThreshold && killStreak < killAnnouncement.killThreshold)
            {
                audioSource.PlayOneShot(killAnnouncement.voiceLine);
            }
        }
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

    private IEnumerator KillStreakTimer()
    {
        yield return new WaitForSeconds(killStreakMaxInterval);
        killStreak = 0;
    }

    [System.Serializable]
    private class KillAnnouncement
    {
        public int killThreshold;
        public string killText;
        public AudioClip voiceLine;
    }
}
