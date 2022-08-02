using UnityEngine;

public class Boulder : MonoBehaviour
{
    public void DestroyBoulder(){
        AudioManager.Instance.PlayDestructionSFX(AudioManager.Instance.destructionSFX[2]);
        Destroy(gameObject);
    }
}
