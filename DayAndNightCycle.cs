using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    public Light słońce;
    [Range(0, 360)] public float sekundyCyklu;
    [Range(0, 1)] public float aktualnyCzas;
    private float mnożnikCzasu = 1f;
    private float intensywnośćSłońca;
    private float intensywnośćCieni;

    private Quaternion rotacjaSłońca;

    public void Start()
    {
        intensywnośćSłońca = słońce.intensity;
        intensywnośćCieni = słońce.shadowStrength;
    }

    public void AktualizujSłońce()
    {
        rotacjaSłońca = Quaternion.Euler((aktualnyCzas * 360f) - 90, 0, 0);
        słońce.transform.localRotation = rotacjaSłońca;

        float aktualnaIntensywnośćSłońca = 1;
        float aktualnaIntensywnośćCieni = 1;

        if (aktualnyCzas >= 0.23f || aktualnyCzas <= 0.75f)
        {
            aktualnaIntensywnośćCieni = 0.8f;
        }

        if (aktualnyCzas <= 0.23f || aktualnyCzas >= 0.75f)
        {
            aktualnaIntensywnośćSłońca = 0;
            aktualnaIntensywnośćCieni = 1;
        }

        else if (aktualnyCzas < 0.25f)
        {
            aktualnaIntensywnośćSłońca = Mathf.Clamp01((aktualnyCzas - 0.23f) * (1 / 0.02f));
            aktualnaIntensywnośćCieni = Mathf.Clamp((aktualnyCzas - 0.23f) * (1 / 0.02f), 1, 0.6f);
        }

        else if (aktualnyCzas >= 0.73f)
        {
            aktualnaIntensywnośćSłońca = Mathf.Clamp01(1 - (aktualnyCzas - 0.73f) * (1 / 0.02f));
            aktualnaIntensywnośćCieni = Mathf.Clamp((aktualnyCzas - 0.73f) * (1 / 0.02f), 0.6f, 1);
        }

        słońce.intensity = intensywnośćSłońca * aktualnaIntensywnośćSłońca;
        słońce.shadowStrength = intensywnośćCieni * aktualnaIntensywnośćCieni;
    }

    public void Update()
    {
        AktualizujSłońce();

        aktualnyCzas += (Time.deltaTime / sekundyCyklu) * mnożnikCzasu;

        if (aktualnyCzas >= 1)
            aktualnyCzas = 0;
    }
}
