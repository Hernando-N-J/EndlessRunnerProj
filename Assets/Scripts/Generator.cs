using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Generator : MonoBehaviour
{
    private Queue<Transform> elements;
    private Transform tr;
    public Pool itemsPool;
    public Vector3 direction;
    public Vector3 offset;
    private Vector3 originalPos;
    private float currentDisplace;
    public float displace = 15f; // space from one object to another
    public float increment = 0.01f;
    public int moved = 0;
    public int quantity = 25;
    public float speed;

    private void OnEnable()
    {
        itemsPool.Initialize();
        tr = transform;
        originalPos = tr.position;

        elements = new Queue<Transform>();
        for (int i = 0; i < quantity; i++)
        {
           var elementsTransform =  itemsPool.GetRandom();
           elementsTransform.position = offset - direction * displace * i;
           elementsTransform.gameObject.SetActive(true);
           elements.Enqueue(elementsTransform);
        }
    }

    private void Update()
    {
        tr.position += direction * speed * Time.deltaTime;
        currentDisplace = Mathf.Abs(Vector3.Distance(tr.position, originalPos));
        var timesToInfinite = currentDisplace / displace;

        if (timesToInfinite > moved + 2) ToInfinite();

        speed += Time.deltaTime * increment;

    }

    public void ToInfinite()
    {
        var last = elements.LastOrDefault();
        var tel = elements.Dequeue();
        tel.gameObject.SetActive(false);

        var elementsTransform =  itemsPool.GetRandom();
        elementsTransform.position = last.position - direction * displace;
        elementsTransform.gameObject.SetActive(true);
        elements.Enqueue(elementsTransform);

        moved++;


    }
}
