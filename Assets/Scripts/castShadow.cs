using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class castShadow : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.TwoSided;
    }
}
