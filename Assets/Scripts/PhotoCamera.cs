using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoCamera : MonoBehaviour
{
	#region Fields
	[SerializeField] private Camera snapCamera;
    [SerializeField] private Material matSnap;
    [SerializeField] private Texture2D snapTexture;
    private readonly string textureMat = "_SecondTexture";
    private readonly string lerpMat = "_Lerping";
    #endregion

    #region Constructors
    #endregion

    #region Getters
    #endregion

    #region Setters
    #endregion

    #region Public_Methods
    #endregion

    #region Private_Methods
  



   

    public void CallAll()
    {
        StartCoroutine(CallTakeSnapshot());
    }

    private IEnumerator CallTakeSnapshot()
    {
        yield return new WaitForEndOfFrame();
            Texture2D snapShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            snapCamera.Render();
            RenderTexture.active = snapCamera.targetTexture;
            snapShot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            byte[] bytes = snapShot.EncodeToPNG();

            snapShot.Apply();

            snapTexture = snapShot;

             matSnap.SetTexture(textureMat, snapTexture);
            matSnap.SetFloat(lerpMat, 1);

        StartCoroutine(LerpingSnapShot());

    }

    private IEnumerator LerpingSnapShot()
    {
        float lerping = 1;
        while (lerping > 0)
        {
            lerping -= 0.02f;
            matSnap.SetFloat(lerpMat, lerping);
            yield return new WaitForSeconds(0.1f);
        }
    }
    #endregion

    #region Static_Methods
    #endregion
}
