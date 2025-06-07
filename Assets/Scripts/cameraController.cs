using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform target;

    private void Update()
    {
        cameraTransform.position = new Vector3(target.position.x, target.position.y, -10f);
    }
}
