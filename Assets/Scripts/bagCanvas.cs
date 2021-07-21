using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bagCanvas : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
            gameObject.SetActive(false);
    }
}
