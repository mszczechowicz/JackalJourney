using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightblink : MonoBehaviour
{
    public float czasMigotania = 1.0f; // Czas trwania jednego migotania w sekundach
    public float amplitudaMigotania = 0.5f; // Amplituda zmiany jasno�ci
    private Light punktoweSwiatlo;
    private float czasDoNastepnegoMigotania;

    void Start()
    {
        punktoweSwiatlo = GetComponent<Light>();
        czasDoNastepnegoMigotania = czasMigotania;
    }

    void Update()
    {
        czasDoNastepnegoMigotania -= Time.deltaTime;

        if (czasDoNastepnegoMigotania <= 0)
        {
            float nowaJasnosc = Random.Range(1.0f - amplitudaMigotania, 1.0f + amplitudaMigotania);
            punktoweSwiatlo.intensity = nowaJasnosc; // Zmiana jasno�ci �wiat�a

            czasDoNastepnegoMigotania = czasMigotania; // Resetowanie licznika czasu do nast�pnego migotania
        }
    }
}
