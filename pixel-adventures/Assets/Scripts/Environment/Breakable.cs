using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] GameObject blueRupee;
    [SerializeField] enum ObjectType {pot, bush};
    [SerializeField] ObjectType objectType;

    public void BreakObject(){
        gameObject.GetComponent<Animator>().SetTrigger("Break");
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        StartCoroutine(DelayDestroyRoutine(gameObject));
    }

    private IEnumerator DelayDestroyRoutine(GameObject other){
        switch(objectType){
            case ObjectType.pot:
                Instantiate(blueRupee, transform.position, Quaternion.identity);
                AudioManager.Instance.PlayDestructionSFX(AudioManager.Instance.destructionSFX[1]);
                break;
            case ObjectType.bush:
            AudioManager.Instance.PlayDestructionSFX(AudioManager.Instance.destructionSFX[0]);
                break;
        }

        yield return new WaitForSeconds(2f);
        Destroy(other);
    }
}
