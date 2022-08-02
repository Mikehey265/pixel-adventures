using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject explodePrefab;

    public void Explode(){
        GameObject newBomb = Instantiate(explodePrefab, transform.position, transform.rotation);
        newBomb.GetComponent<Attack>().isBombExplosion = true;
        AudioManager.Instance.PlayExplosionSFX();
        Destroy(gameObject);
    }
}
