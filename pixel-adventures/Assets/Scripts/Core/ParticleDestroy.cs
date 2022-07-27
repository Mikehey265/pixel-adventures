using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    [SerializeField] float waitTime;

    private void Start() {
        Destroy(gameObject, waitTime);
    }
}
