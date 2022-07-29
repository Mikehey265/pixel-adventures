using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : Singleton<UIFade>
{
    #region Private Variables

    [SerializeField] Image fadeScreen;
    [SerializeField] float fadeSpeed;
    IEnumerator fadeRoutine;
        
    #endregion

    #region Unity Methods

    private void Start() {
        FadeToClear();
    }
        
    #endregion

    #region Public Methods

    public void FadeToBlack(){
        if(fadeRoutine != null){
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(fadeScreen, 1);
        StartCoroutine(fadeRoutine);
    }

    public void FadeToClear(){
        if(fadeRoutine != null){
            StopCoroutine(fadeRoutine);
        }

        fadeRoutine = FadeRoutine(fadeScreen, 0);
        StartCoroutine(fadeRoutine);
    }
        
    #endregion

    #region Private Methods

    private IEnumerator FadeRoutine(Image image, float targetAlpha){
        while(!Mathf.Approximately(image.color.a, targetAlpha)){
            float alpha = Mathf.MoveTowards(image.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return new WaitForEndOfFrame();
        }
    }
        
    #endregion
}
