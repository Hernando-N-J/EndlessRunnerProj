using System;
using System.Collections;
using UnityEngine;

public class Move : MonoBehaviour
{
  public float horizontal;
  public float maxLeft, maxRight;
  float yOriginal;
  float yOffset;
  public float speed;
  float xRotation;
  public Transform pivot;
  internal Transform tr;

  public AnimationCurve jumpCurve;
  public bool jumping = false;
  public float jumpDuration = 1f;
  public float jumpScale = 5f; // not seen in inspector but accessible from other scripts

  public AnimationCurve slideCurve;
  public bool sliding = false;
  public float slideDuration = 1f;
  public float slideUpDownDuration = 1f;
  public float slideScale = 90f;

  public float dJump;
  public float dSlide;

  internal Animator animator;

  private void Awake()
  {
    animator = GetComponentInChildren<Animator>();
    tr = transform;
    yOriginal = tr.position.y;
  }

  private void OnEnable()
  {
    Scores.Instance.current.time = 0f;
  }

  private void Update()
  {
    //horizontal += Input.GetAxis("Horizontal") * speed * Time.deltaTime;
    horizontal += SimpleInput.GetAxis("Horizontal") * speed * Time.deltaTime;

    //if (!jumping && !sliding && Input.GetButton("Jump")) StartCoroutine(Fly());
    if (!jumping && !sliding && SimpleInput.GetButton("Jump")) StartCoroutine(Fly());

    //if (!jumping && !sliding && Input.GetButton("Fire1")) StartCoroutine(Slide());
    if (!jumping && !sliding && SimpleInput.GetButton("Fire1")) StartCoroutine(Slide());

    horizontal = Mathf.Clamp(horizontal, maxLeft, maxRight);
    tr.position = new Vector3(horizontal, yOriginal + yOffset, 0);
    pivot.rotation = Quaternion.Euler(xRotation, 0, 0);

    Scores.Instance.current.time += Time.deltaTime;
  }

  private IEnumerator Fly()
  {
    jumping = true; 
    animator.CrossFade("Jump",0.2f);
    dJump = 0;

    while (dJump < jumpDuration)
    {
      dJump += Time.deltaTime;
      yOffset = jumpCurve.Evaluate(dJump / jumpDuration) * jumpScale;

      yield return null;
    }
    animator.CrossFade("Run",0.1f);
    jumping = false;
  }

  private IEnumerator Slide()
  {
    sliding = true; 
    dSlide = 0;
    animator.CrossFade("Slide",slideUpDownDuration);

    while (dSlide < slideUpDownDuration)
    {
      dSlide += Time.deltaTime;
      xRotation = slideCurve.Evaluate(dSlide / slideUpDownDuration) * slideScale;

      yield return null;
    }

    yield return new WaitForSeconds(slideDuration);

    while (dSlide > 0)
    {
      dSlide -= Time.deltaTime;
      xRotation = slideCurve.Evaluate(dSlide / slideUpDownDuration) * slideScale;

      yield return null;
    }
    animator.CrossFade("Run",slideUpDownDuration);
    sliding = false;
  }
}
