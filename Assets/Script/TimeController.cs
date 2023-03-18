using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UIElements;

// reference: https://www.youtube.com/watch?v=L4t2c1_Szdk&ab_channel=KetraGames

public class TimeController : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeMutiplier;
    public TextMeshProUGUI timeText;

    public float sunriseTimeHours;
    public float sunsetTimeHours;

    public float startHours;

    public Color dayLight;
    public Color nightLight;

    public AnimationCurve lightChangeCurve;
    public AnimationCurve dayAtmosphereCurve;
    public AnimationCurve nightAtmosphereCurve;

    public Light sunLight;
    public float sunLightIntensity;

    public Light moonLight;
    public float moonLightIntensity;

    public bool isDay;

    private TimeSpan sunriseTime;
    private TimeSpan sunsetTime;

    public Material skyboxMaterial;

    private DateTime currentTime;

    [ReadOnly]
    [SerializeField]
    private float currentTimeHours;

    [ReadOnly]
    [SerializeField]
    double percentage = 0.0f;

    [ReadOnly]
    [SerializeField]
    float sunRotation = 0.0f;

    [ReadOnly]
    [SerializeField]
    float atmos = 0.0f;


    void Start()
    {

        currentTime = currentTime.AddHours(startHours);
    }

    // Update is called once per frame
    void Update()
    {

        sunriseTime = TimeSpan.FromHours(sunriseTimeHours);
        sunsetTime = TimeSpan.FromHours(sunsetTimeHours);

        currentTime = currentTime.AddSeconds( Time.deltaTime * timeMutiplier );

        if(timeText!= null )
            timeText.text = currentTime.ToString("HH:mm");

        currentTimeHours = currentTime.Hour;

        rotateSun();
        updateLight();
    }
    private void updateLight()
    {
        float dotProduct = Vector3.Dot(sunLight.transform.forward, Vector3.down);
        sunLight.intensity= Mathf.Lerp(0f, sunLightIntensity, lightChangeCurve.Evaluate(dotProduct));
        moonLight.intensity = Mathf.Lerp(0f, 0.5f, lightChangeCurve.Evaluate(dotProduct));
        RenderSettings.ambientLight = Color.Lerp(nightLight, dayLight, lightChangeCurve.Evaluate(dotProduct));
    }
    private void rotateSun()
    {
        
        if(currentTime.TimeOfDay > sunriseTime && currentTime.TimeOfDay < sunsetTime)
        {
            // day
            isDay = true;
            TimeSpan dayTime = TimeDifference(sunriseTime, sunsetTime);
            TimeSpan nowTime = TimeDifference(sunriseTime, currentTime.TimeOfDay);

            percentage = nowTime / dayTime;

            sunRotation = Mathf.Lerp(0.0f, 180.0f, (float)percentage);
            atmos = Mathf.Lerp(0.5f, 5.0f, dayAtmosphereCurve.Evaluate((float)percentage));
            skyboxMaterial.SetFloat("_AtmosphereThickness", atmos);

            Quaternion rotation = Quaternion.Euler(0, sunRotation, 0);
            skyboxMaterial.SetMatrix("_Rotation", Matrix4x4.Rotate(rotation));

            sunLight.gameObject.SetActive(true);
            moonLight.gameObject.SetActive(false);
        }
        else
        {
            // night
            isDay = false;
            TimeSpan nightTime = TimeDifference(sunsetTime, sunriseTime);
            TimeSpan nowTime = TimeDifference(currentTime.TimeOfDay, sunriseTime);

            percentage = nowTime / nightTime;

            sunRotation = Mathf.Lerp(360.0f, 180.0f, (float)percentage);

            atmos = Mathf.Lerp(0.0f, 0.5f, nightAtmosphereCurve.Evaluate((float)percentage));
            skyboxMaterial.SetFloat("_AtmosphereThickness", atmos);

            sunRotation = Mathf.Lerp(180.0f, 0.0f, (float)percentage);
            Quaternion rotation = Quaternion.Euler(0, sunRotation, 0);
            skyboxMaterial.SetMatrix("_Rotation", Matrix4x4.Rotate(rotation));

            sunLight.gameObject.SetActive(false);
            moonLight.gameObject.SetActive(true);
        }

        sunLight.transform.rotation = Quaternion.Euler(sunRotation, 0.0f, 0.0f);
        moonLight.transform.rotation = Quaternion.Euler(sunRotation, 0.0f, 0.0f);
    }
    private TimeSpan TimeDifference(TimeSpan fromTime, TimeSpan toTime)
    {
        TimeSpan result = toTime - fromTime;
        if(result.TotalSeconds < 0)
        {
            result += TimeSpan.FromHours(24);
        }
        return result;
    }
    public void setTime(string hours)
    {
        float tmpHours;
        float.TryParse(hours, out tmpHours);

        tmpHours =  Mathf.Clamp(tmpHours, 0, 24);
        currentTime = new DateTime();
        currentTime.AddHours(tmpHours);
    }
    public void setTimeMultiplier(string mult)
    {
        float tmpMult;
        float.TryParse(mult, out tmpMult);
        tmpMult = Mathf.Clamp(tmpMult, 0, 20000);
        timeMutiplier = tmpMult;
    }
}
