using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainControl : MonoBehaviour
{
    public bool isEnable = false;
    public Transform player;
    public WindZone wind;
    public float rainProbability = 0f;
    public float rainHeigt = 20f;
    public float minRainTime = 10f;
    public float maxRainTime = 60f;

    public float minRainDensity = 20f;
    public float maxRainDensity = 300f;


    private ParticleSystem.EmissionModule emission;
    private Vector3 pos;
    [ReadOnly] [SerializeField] private float turbulence;
    [ReadOnly] [SerializeField] private float rainDensity;
    [ReadOnly] [SerializeField] private float rainTime = 0;


    // Start is called before the first frame update
    void Start()
    {
        emission = gameObject.GetComponent<ParticleSystem>().emission;
        turbulence = wind.windTurbulence;
    }

    // Update is called once per frame
    void Update()
    {
        //update Rain position
        pos = player.position;
        pos.y = rainHeigt;
        transform.position = pos;

        rainTime -= Time.deltaTime;

        if (rainTime <= 0)
        {
            rainTime = Random.Range(minRainTime, maxRainTime); 
            
            if (Random.Range(0, 1f) < rainProbability)
            {
                rainDensity = Random.Range(minRainDensity, maxRainDensity);
                isEnable = true;
            }
            else
            {
                isEnable = false;
            }
        }
        EnableRain(isEnable);
    }

    void EnableRain(bool enable)
    {
        if (enable)
        {
            if(emission.rateOverTime.constant < rainDensity )
                emission.rateOverTime = emission.rateOverTime.constant + Time.deltaTime * 50;
            if (wind.windTurbulence < turbulence + 2f)
                wind.windTurbulence += Time.deltaTime;
        }
        else
        {
            if (emission.rateOverTime.constant > 0)
                emission.rateOverTime = emission.rateOverTime.constant - Time.deltaTime * 50;
            else
                emission.rateOverTime = 0;

            if (wind.windTurbulence > turbulence)
                wind.windTurbulence -= Time.deltaTime;
        }
    }

    public void Enable()
    {
        isEnable = true;
    }

    public void Disable()
    {
        isEnable = false;
    }
}
