using UnityEngine;

public class Text3D : MonoBehaviour
{
    #region Variables

    Camera camera;

    #endregion

    #region MonoBehaviour Callbacks

    private void Awake()
    {
        camera = Camera.main;
    }

    private void Start()
    {
        transform.LookAt(camera.transform);
    }

    #endregion
}
