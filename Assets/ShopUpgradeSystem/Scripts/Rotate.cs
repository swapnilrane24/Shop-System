using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField] private float rotSpeed;

    void Update()
    {
        transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
    }
}
