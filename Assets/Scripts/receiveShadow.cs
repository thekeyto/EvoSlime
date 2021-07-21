using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class receiveShadow : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().receiveShadows = true;
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;
    }
}
