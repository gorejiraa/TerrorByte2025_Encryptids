using UnityEngine;

public class SpriteLookAtCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }
}
