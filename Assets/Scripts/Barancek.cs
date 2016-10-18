using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Barancek : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource audio;
    public AudioReverbZone reverbZone;
    public float DecayRate = 0.1f;
    public float MinPitch = 0.15f;

    [Header("Visual")]
    public Image fadeOutBG;
    public Image ludo;
    public float fadeRate = 0.1f;

    private bool startDecay = false;

    void Start()
    {
        ludo.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (startDecay)
        {
            reverbZone.enabled = true;
            fadeOutBG.gameObject.SetActive(true);

            if (audio.pitch > MinPitch)
            {
                audio.pitch -= DecayRate * Time.deltaTime;
            }

            if (fadeOutBG.color.a < 1)
            {
                fadeOutBG.color = new Color(0, 0, 0, fadeOutBG.color.a + Mathf.Clamp01(fadeRate * Time.deltaTime));
            }
            else
            {
                WaitForSec(1f);

                ludo.color = new Color(1, 1, 1, ludo.color.a + 0.1f * Time.deltaTime);
                ludo.gameObject.SetActive(true);
            }

            if (ludo.color.a >= 1 && audio.pitch <= MinPitch)
                startDecay = false;
        }
    }

    IEnumerable WaitForSec(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void StartDecay()
    {
        startDecay = true;
    }
}
