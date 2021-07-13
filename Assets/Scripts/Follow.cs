using UnityEngine;

public class Follow : MonoBehaviour
{
  private Transform tr;
  public Transform target;
  public float speed = 5f;

  private void Awake()
  {
    tr = transform;
  }

  private void Update()
  {
    tr.position = Vector3.Lerp(tr.position, target.position, speed * Time.deltaTime);
  }
}
