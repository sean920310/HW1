using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DamageEffect : MonoBehaviour
{
    static private PostProcessProfile postProcess;

    public AnimationCurve lowHealthAnim;

    static private bool lowHealthActive;
    static private bool getDamageActive;

    private float timeCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        postProcess = GetComponent<PostProcessVolume>().profile;
    }

    // Update is called once per frame
    void Update()
    {
        if(lowHealthActive && !getDamageActive)
            UpdateLowHealth();
    }

    /// <summary>
    /// use StartCoroutine() to call
    /// </summary>
    static public IEnumerator GetDamage()
    {
        getDamageActive = true;
        postProcess.GetSetting<Vignette>().enabled.value = true;
        postProcess.GetSetting<Vignette>().color.value = new Color(1f, 0.2f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        postProcess.GetSetting<Vignette>().enabled.value = lowHealthActive;
        getDamageActive = false;
    }

    static public void LowHealth(bool active)
    {
        postProcess.GetSetting<Vignette>().enabled.value = (active || getDamageActive);
        lowHealthActive = active;
    }

    private void UpdateLowHealth()
    {
        timeCount += Time.deltaTime;
        if (timeCount > 1f) timeCount -= 1f;
        postProcess.GetSetting<Vignette>().color.value = new Color(1f, lowHealthAnim.Evaluate(timeCount), lowHealthAnim.Evaluate(timeCount)); 
    }
}
