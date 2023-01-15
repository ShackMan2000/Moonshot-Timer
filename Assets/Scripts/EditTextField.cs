using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditTextField : UIPanel
{





    [SerializeField] Transform topContainer;

    Transform originalContainer;

    public void OpenEditField(Transform originalParent = null)
    {
        originalContainer = originalParent;

        if (topContainer != null)
            transform.SetParent(topContainer);


        gameObject.SetActive(true);
    }







    protected override void OnDisable()
    {
        //stupid error that doesn't allow to set parent and disable in the same frame
        if (originalContainer != null)
            Invoke("ChangeParentToOriginal", 0.1f);

        base.OnDisable();
    }


    void ChangeParentToOriginal()
    {

            transform.SetParent(originalContainer);

    }

}
