using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    SpecialRoads specialRoad;

    Rigidbody2D rb;

    //Car Variables
    public float maxSpeed;
    [SerializeField] private float acceleration, rotationSpeed, currentSpeed, braking;
    [SerializeField] private float rotation, rotationValue;
    [SerializeField] private float weight, height;

    //Wheel
    [SerializeField] private GameObject WheelLeft, WheelRight;
    private Vector3 CarAngle;
    private float wheelAngle, maxWheelAngle = 30f;

    //Effect
    [SerializeField] private ParticleSystem crashEffect;
    [SerializeField] private SpriteRenderer leftLight, rightLight,backLight;
    [SerializeField] private Sprite rightLightOn, leftLightOn, backLightOn;
    private Sprite backLightOff, rightLightOff, leftLightOff;
    private bool lightAnim;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rotationValue = transform.eulerAngles.z;
        crashEffect.Stop();

        specialRoad = GameObject.FindWithTag("SpecialRoad").GetComponent<SpecialRoads>();

        rightLightOff = rightLight.sprite;
        leftLightOff = leftLight.sprite;
        backLightOff = backLight.sprite;
        lightAnim = false;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void LateUpdate()
    {
        CarAngle = transform.localEulerAngles;
        CarAngle.z = wheelAngle;
        WheelLeft.transform.localEulerAngles = CarAngle;
        WheelRight.transform.localEulerAngles = CarAngle;
    }

    private void Movement()
    {
        currentSpeed = rb.velocity.magnitude;

        if(maxSpeed > currentSpeed)
        {
            rb.AddForce(transform.up * acceleration, 0);       
        }
        else
        {
            if (currentSpeed > 0.1f) 
            {
                rb.AddForce(-rb.velocity * braking, 0);
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
     
        rotationValue -= rotation * rotationSpeed;
        rb.MoveRotation(rotationValue);
        rb.velocity = transform.up * rb.velocity.magnitude;

        if (rotation > 0.5f && !lightAnim)
        {
            StartCoroutine(LightController(rightLight, rightLightOn, rightLightOff));
        }
        else if (rotation < -0.5f && !lightAnim)
        {
            StartCoroutine(LightController(leftLight, leftLightOn, leftLightOff));
        }
        else if (!lightAnim)
        {
            leftLight.sprite = leftLightOff;
            rightLight.sprite = rightLightOff;
        }

        if (maxSpeed + 0.5f > currentSpeed)
        {
            backLight.sprite = backLightOff;
        }
        else
        {
            backLight.sprite = backLightOn;
        }
    }

    public void SetInput(float rotationValue)
    {
        rotation = rotationValue;
        wheelAngle = -rotationValue * maxWheelAngle;
    }

    private IEnumerator LightController(SpriteRenderer currentLight, Sprite currentLightOn, Sprite currentLightOff)
    {
        lightAnim = true;

        for (int i = 0; i < 3; i++) 
        {
            currentLight.sprite = currentLightOn;
            yield return new WaitForSeconds(0.5f);
            currentLight.sprite = currentLightOff;
            yield return new WaitForSeconds(0.5f);
        }

        lightAnim = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        crashEffect.Play();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "SpecialRoad")
        {
           if(height > specialRoad.maxHeight)
           {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
                crashEffect.Play();
           }
           if(weight > specialRoad.maxWeight)
           {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
                crashEffect.Play();
           }
        }
    }
}