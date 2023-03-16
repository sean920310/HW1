using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DamageEffect : MonoBehaviour
{
    static private PostProcessProfile postProcess;
    // Start is called before the first frame update
    void Start()
    {
        postProcess = GetComponent<PostProcessVolume>().profile;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getDamage()
    {
        postProcess.GetSetting<Vignette>().active = true;

    }

    static public void lowHealth(bool active)
    {
        postProcess.GetSetting<Vignette>().active = active;
    }
}
