using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;

public class Car : MonoBehaviour
{
    SpecialRoads specialRoad;

    Rigidbody2D rb;

    //Car Variables
    public float maxSpeed;
    [SerializeField] private float acceleration, rotationSpeed, braking;
    public float currentSpeed;

    [SerializeField] private float rotation, rotationValue;
    [SerializeField] private float weight, height;

    //Wheel
    [SerializeField] private GameObject WheelLeft, WheelRight;
    private Vector3 CarAngle;
    private float wheelAngle, maxWheelAngle = 30f;

    //Effect
    [SerializeField] private ParticleSystem crashEffect;
    [SerializeField] private SpriteRenderer leftLight, rightLight;
    private SpriteRenderer backLight;
    private Sprite backLightOn, backLightOff;
    [SerializeField] private Sprite rightLightOn, leftLightOn;
    private Sprite rightLightOff, leftLightOff;
    private bool lightAnim;

    //Light
    [SerializeField] private Light2D leftSignalLight, rightSignalLight, leftSignalLightBack, rightSignalLightBack , stopSignalLeft, stopSignalRight;
    [SerializeField] private bool police;
    [SerializeField] private Light2D policeLightRed, policeLightBlue;

    //Win
    public string carID;
    [SerializeField] private WinConditionManager winConditionManager;

    //Loose
    [SerializeField] private GameObject looseScreen;
    [SerializeField] private GameObject PoliceCar;

    //Audio
    public bool brake = false;
    public bool crash = false;
    public bool turnSignal = false;
    public bool siren = false;
    private float sirenTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rotationValue = transform.eulerAngles.z;
        crashEffect.Stop();

        looseScreen.SetActive(false);
        specialRoad = GameObject.FindWithTag("SpecialRoad").GetComponent<SpecialRoads>();

        rightLightOff = rightLight.sprite;
        leftLightOff = leftLight.sprite;
        //backLightOff = backLight.sprite;
        lightAnim = false;

        if(leftSignalLight != null && rightSignalLight != null && leftSignalLightBack != null && rightSignalLightBack != null)
        {
            leftSignalLight.enabled = false;
            rightSignalLight.enabled = false;
            leftSignalLightBack.enabled = false;
            rightSignalLightBack.enabled = false;
        }

        if(stopSignalLeft != null && stopSignalRight != null)
        {
            stopSignalLeft.enabled = false;
            stopSignalRight.enabled = false;
        }

        if(policeLightBlue!= null || policeLightRed != null)
        {
            policeLightBlue.enabled = false;
            policeLightRed.enabled = false;
        }

        if (police == true)
        {
            StartCoroutine(PoliceLight());
        }
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
            StartCoroutine(LightController(rightLight, rightLightOn, rightLightOff, rightSignalLight, rightSignalLightBack));
        }
        else if (rotation < -0.5f && !lightAnim)
        {
            StartCoroutine(LightController(leftLight, leftLightOn, leftLightOff, leftSignalLight, leftSignalLightBack));
        }
        else if (!lightAnim)
        {
            leftLight.sprite = leftLightOff;
            rightLight.sprite = rightLightOff;
        }

        if (maxSpeed + 0.5f > currentSpeed)
        {
           // backLight.sprite = backLightOff;
            stopSignalLeft.enabled = false;
            stopSignalRight.enabled = false;
            brake = false;
        }
        else
        {
            // backLight.sprite = backLightOn;
            brake = true;
            stopSignalLeft.enabled = true;
            stopSignalRight.enabled = true;
        }
    }

    public void SetInput(float rotationValue)
    {
        rotation = rotationValue;
        wheelAngle = -rotationValue * maxWheelAngle;
    }

    private IEnumerator LightController(SpriteRenderer currentLight, Sprite currentLightOn, Sprite currentLightOff, Light2D currentSiganl , Light2D currentSiganlBack)
    {
        lightAnim = true;
        turnSignal = true;

        for (int i = 0; i < 3; i++) 
        {
            currentLight.sprite = currentLightOn;
            currentSiganl.enabled = true;
            currentSiganlBack.enabled = true;
            yield return new WaitForSeconds(0.5f);
            currentLight.sprite = currentLightOff;
            currentSiganl.enabled = false;
            currentSiganlBack.enabled = false;
            yield return new WaitForSeconds(0.5f);
        }

        lightAnim = false;
        turnSignal = false;
    }

    private IEnumerator PoliceLight()
    {
        while (true)
        {
            for (int i = 0; i < 3; i++)
            {
                policeLightBlue.enabled = true;
                policeLightRed.enabled = false;
                yield return new WaitForSeconds(0.1f);
                policeLightBlue.enabled = false;
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.1f);

            for (int i = 0; i < 3; i++)
            {
                policeLightRed.enabled = true;
                policeLightBlue.enabled = false;
                yield return new WaitForSeconds(0.1f);
                policeLightRed.enabled = false;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void PoliceRoad()
    {
        Car carMoveSc = PoliceCar.GetComponent<Car>();

        if(carMoveSc != null)
        {
            carMoveSc.enabled = true;
            carMoveSc.siren = true;
        }

        if(carMoveSc.police && carMoveSc.maxSpeed <= 0)
        { 
            Invoke(nameof(LooseMenu), 2f);
            sirenTimer += Time.deltaTime;

            if (sirenTimer >= 0.02f)
            {
                carMoveSc.siren = false;
            }
        }
    }

    private void LooseMenu()
    {
        looseScreen.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        crashEffect.Play();
        crash = true;

        if (collision.gameObject.tag == "Human"){
            AchievementManager.Instance.IncreaseAccident(false,true);
        }else {
            AchievementManager.Instance.IncreaseAccident(true,false);
        }
        AchievementManager.Instance.AchievementCheck();
        InvokeRepeating(nameof(PoliceRoad),0f,0.5f);
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
                AchievementManager.Instance.IncreaseAccident(true,false);
                AchievementManager.Instance.AchievementCheck();
           }
           if(weight > specialRoad.maxWeight)
           {
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
                crashEffect.Play();
                AchievementManager.Instance.carCrash = true;
                AchievementManager.Instance.IncreaseAccident(true,false);
                AchievementManager.Instance.AchievementCheck();
           }
        }

        if(other.gameObject.tag == "Destory")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("WinPoint") && winConditionManager != null)
        {
            WinPoint winPoint = collision.GetComponent<WinPoint>();
            if (winPoint != null)
            {
                winConditionManager.CarReachedTarget(carID, winPoint.targetID);
            }
        }
    }
}