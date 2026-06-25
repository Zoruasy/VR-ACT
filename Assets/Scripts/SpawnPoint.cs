using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Header("Spawn Instellingen")]
    public Transform spawnPoint;   // Waar de speler moet spawnen
    public Transform xrOrigin;     // De XR Origin (je speler rig)

    private void Start()
    {
        if (spawnPoint == null || xrOrigin == null)
        {
            Debug.LogWarning(" SpawnPoint: spawnPoint of xrOrigin niet toegewezen!");
            return;
        }

        StartCoroutine(PlacePlayerAfterTracking());
    }

    private IEnumerator PlacePlayerAfterTracking()
    {
        // Wacht een fractie van een seconde zodat XR tracking eerst klaar is
        yield return new WaitForSeconds(0.1f);

        // Zet positie en richting
        xrOrigin.position = spawnPoint.position;
        xrOrigin.rotation = Quaternion.Euler(0f, spawnPoint.rotation.eulerAngles.y, 0f);

        Debug.Log(" XR Origin succesvol gespawned op spawnpoint");
    }
}