using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Starfield_Scrolling : MonoBehaviour
{
    [Range(0f, 8f)]
    [SerializeField] float _BackgroundSpeed; 
   
    // Update is called once per frame
    void Update()
    {

        var _MeshR = GetComponent<MeshRenderer>();
        var _Mat = _MeshR.material;
        Vector2 _Offset = _Mat.mainTextureOffset;

        _Offset.x =  (transform.position.x /  transform.localScale.x) * _BackgroundSpeed;
        _Offset.y = (transform.position.y / transform.localScale.y) * _BackgroundSpeed;
        _Mat.mainTextureOffset = _Offset;

           
    }
}
