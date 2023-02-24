using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public float throwPower = 1f;
    private Rigidbody2D rb;
    public GameObject DragParticles;
    private MMF_Player feedback;
    public MMF_Player hitFeedback;
    public MMF_Player enemyFeedback;
    public MMF_Player enemyProximityFeedback;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        feedback = GetComponent<MMF_Player>();
    }

    private void OnMouseEnter()
    {
        MMTimeScaleEvent.Trigger(MMTimeScaleMethods.For, 0.07f, 1f, true, 10f, true);
        //Time.timeScale = 0.1f;
    }

    private void OnMouseExit()
    {
        if (Time.timeScale >= 0.07f)
            MMTimeScaleEvent.Trigger(MMTimeScaleMethods.Reset, 1f, 1f, true, 10f, true);
        //Time.timeScale = 1f;
    }

    private void OnMouseDown()
    {
        MMTimeScaleEvent.Trigger(MMTimeScaleMethods.For, 0f, 1f, true, 10f, true);
        //Time.timeScale = 0f;
        DragParticles.SetActive(true);
    }
    private void OnMouseUp()
    {
        ThrowHammer();
        DragParticles.SetActive(false);
        MMTimeScaleEvent.Trigger(MMTimeScaleMethods.Reset, 1f, 1f, true, 10f, true);
        //Time.timeScale = 1.0f;
    }

    private void OnMouseDrag()
    {
        //Time.timeScale = 0f;
        MMTimeScaleEvent.Trigger(MMTimeScaleMethods.For, 0f, 1f, true, 10f, true);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x -= DragParticles.transform.position.x;
        mousePos.y -= DragParticles.transform.position.y;
        DragParticles.transform.eulerAngles = new Vector3(0f, 0f, Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg - 90);
    }


    private void ThrowHammer()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 throwVector = mousePos;
        throwVector.x -= transform.position.x;
        throwVector.y -= transform.position.y;


        Vector2 forceVector = Vector2.ClampMagnitude(throwVector, 2) * throwPower * -1;

        rb.AddForce(forceVector, ForceMode2D.Impulse);
        rb.AddTorque(rb.totalTorque * 3 + 5);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            enemyFeedback.PlayFeedbacks();

        feedback.PlayFeedbacks();
        hitFeedback.transform.position = collision.GetContact(0).point;
        hitFeedback.PlayFeedbacks();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
            enemyProximityFeedback.PlayFeedbacks();
    }
}
