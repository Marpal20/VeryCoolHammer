using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int health = 4;
    public MMF_Player enemyPlayer;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hammer"))
        {
            enemyPlayer.PlayFeedbacks();
            health--;

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
