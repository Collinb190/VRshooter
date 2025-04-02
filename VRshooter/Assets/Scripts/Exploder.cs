using UnityEngine;
using UnityEngine.VFX;

public class ExploderLoop : MonoBehaviour
{
    public VisualEffect vfxGraph;          // Your blood explosion VFX Graph
    public GameObject[] gibsPrefabs;       // Body part prefabs
    public int gibsToSpawn = 5;            // Number of gibs per burst
    public float delayBetweenExplosions = 3f; // Time between explosions

    void Start()
    {
        InvokeRepeating(nameof(Explode), 1f, delayBetweenExplosions);
    }

    void Explode()
    {
        // ?? Play the blood explosion VFX
        if (vfxGraph != null)
        {
            vfxGraph.SendEvent("OnPlay");
        }

        // ?? Spawn body part gibs
        if (gibsPrefabs != null && gibsPrefabs.Length > 0)
        {
            for (int i = 0; i < gibsToSpawn; i++)
            {
                GameObject gibPrefab = gibsPrefabs[Random.Range(0, gibsPrefabs.Length)];
                GameObject gib = Instantiate(gibPrefab, transform.position, Random.rotation);

                Rigidbody rb = gib.GetComponent<Rigidbody>();
                if (rb)
                {
                    Vector3 force = Random.onUnitSphere * Random.Range(3f, 6f);
                    rb.AddForce(force, ForceMode.Impulse);
                    rb.AddTorque(Random.insideUnitSphere * 300f, ForceMode.Impulse);
                }

                Destroy(gib, 5f); // optional cleanup
            }
        }
    }
}
