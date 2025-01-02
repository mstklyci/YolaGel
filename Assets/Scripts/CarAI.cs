using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAI : MonoBehaviour
{
    [SerializeField] private WayPoints currentWayPoint;
    Vector3 targetPosition;
    Car car;

    private void Awake()
    {
        car = GetComponent<Car>();
    }

    private void FixedUpdate()
    {
        float rotationValue;
        rotationValue = TurnNextPoint();
        car.SetInput(rotationValue);

        Road();
    }

    private void Road()
    {
        if(currentWayPoint != null)
        {
            car.maxSpeed = currentWayPoint.pointMaxSpeed;

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
        targetAngle = Mathf.Clamp(-targetAngle / 45f, -1,1);

        return targetAngle;
    }
}