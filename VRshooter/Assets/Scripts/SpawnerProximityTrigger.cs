using UnityEngine;

public class SpawnerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject spawnerToControl;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && spawnerToControl != null)
        {
            spawnerToControl.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && spawnerToControl != null)
        {
            spawnerToControl.SetActive(true);
        }
    }
}
