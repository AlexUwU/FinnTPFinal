using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    [SerializeField] private float offSet=2f;
    [SerializeField] private float smooth=0.5f;
    [SerializeField] private GameObject targetCamera;

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        direction= new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);

        targetCamera.transform.position= Vector3.Lerp(transform.position,transform.position + (direction * offSet), smooth);
    }
}
