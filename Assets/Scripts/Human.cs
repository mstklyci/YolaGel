using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Human : MonoBehaviour
{
    Rigidbody2D rb;

    //Movement
    [SerializeField] private float walkSpeed, rotationSpeed;
    private float rotationValue;

    //
    private bool dead;

    //RoadAI
    [SerializeField] private WayPoints[] WayPoints;
    private WayPoints currentWayPoint;
    private Vector3 targetPosition;

    //Animator
    Animator anim;

    //Audio
    AudioSource humanSource;
    [SerializeField] private AudioClip deathSound;

    private void Awake()
    {
        rotationValue = transform.eulerAngles.z;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentWayPoint = WayPoints[0];
        dead = false;
        anim.speed = 0;
        humanSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        float rotationValue;
        rotationValue = TurnNextPoint();

        Movement(rotationValue);
        Road();
    }

    private void Movement(float rotation)
    {
        if (dead != true)
        {
            rb.velocity = transform.up * walkSpeed;
            rotationValue -= rotation * rotationSpeed;
            rb.MoveRotation(rotationValue);
            anim.speed = 1;
        }
        else
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            anim.SetBool("Death", true);
            anim.speed = 1;
        }
    }

    private void Road()
    {
        if (currentWayPoint != null)
        {
            walkSpeed = currentWayPoint.pointMaxSpeed;

            targetPosition = currentWayPoint.transform.position;
            float distanceToWayPoint = (targetPosition - transform.position).magnitude;

            if (distanceToWayPoint <= currentWayPoint.minDistance)
            {
                currentWayPoint = currentWayPoint.nextPoint;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private float TurnNextPoint()
    {
        Vector2 nextPoint = (targetPosition - transform.position).normalized;

        float targetAngle = Vector2.SignedAngle(transform.up, nextPoint);
        targetAngle = Mathf.Clamp(-targetAngle / 45f, -1, 1);

        return targetAngle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Car" && !dead)
        {
            dead = true;
            humanSource.PlayOneShot(deathSound);
        }
    }
}