using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSFX : MonoBehaviour
{
    public AudioSource engineSource;
    public AudioSource brakeSource;
    public AudioSource crashSource;
    public AudioSource signalSource;
    public AudioSource sirenSource;

    float desiredEnginePitch = 0.5f;
    float tireScreechPitch = 0.5f;
    private bool crashSoundPlayed = false;
    private bool lastSignalState = false;

    Car car;
    private void Awake()
    {
        car = GetComponentInParent<Car>();
    }

    private void Update()
    {
        EngineSound();
        BrakeSound();
        CrashSound();
        SignalSound();
        SirenSource();
    }

    private void EngineSound()
    {
        float speed = car.currentSpeed;

        float engineVolume = speed * 0.05f;

        engineVolume = Mathf.Clamp(engineVolume, 0.2f, 1.0f);

        engineSource.volume = Mathf.Lerp(engineSource.volume, engineVolume, Time.deltaTime * 10f);

        desiredEnginePitch = speed * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2f);
        engineSource.pitch = Mathf.Lerp(engineSource.pitch, desiredEnginePitch, Time.deltaTime * 1.5f);
    }

    private void BrakeSound()
    {
        bool braking = car.brake;

        if (braking)
        {
            brakeSource.volume = Mathf.Lerp(brakeSource.volume, 1.0f, Time.deltaTime * 10);
            tireScreechPitch = Mathf.Lerp(tireScreechPitch, 0.5f, Time.deltaTime * 10);
        }
        else
        {
            brakeSource.volume = 0;
        }
    }

    private void CrashSound()
    {
        bool crashing = car.crash;

        if (crashing && !crashSoundPlayed)
        {
            crashSource.PlayOneShot(crashSource.clip);
            crashSoundPlayed = true;
        }
        else if (!crashing)
        {
            crashSoundPlayed = false;
        }
    }

    private void SignalSound()
    {
        bool signal = car.turnSignal;

        if (signal != lastSignalState)
        {
            if (signal)
            {
                signalSource.Play();
            }
            else
            {
                signalSource.Stop();
            }

            lastSignalState = signal;
        }
    }

    private void SirenSource()
    {
        bool Siren = car.siren;

        if (Siren && !sirenSource.isPlaying)
        {
            sirenSource.Play();
        }

        else if (!Siren && sirenSource.isPlaying)
        {
            sirenSource.Stop();
        }
    }
}