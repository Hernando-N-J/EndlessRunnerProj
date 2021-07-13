using UnityEngine;

public class Move : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public float maxLeft, maxRight;
    public float maxBack, maxForw;
    public float yOriginal;
    public float speed;

    internal Transform tr; // not seen in inspector but accessible from other scripts

    private void Awake()
    {
        tr = transform;
        yOriginal = tr.position.y;
    }

    private void Update()
    {
        horizontal += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        horizontal = Mathf.Clamp(horizontal, maxLeft, maxRight);
        tr.position = new Vector3(horizontal, yOriginal, 0);

    }
}
