using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{
    public Pool itemsPool;
    public Pool safeItemsPool;
    
    private Queue<Transform> elements;
    private Transform tr;
    private Vector3 originalPos;
    public Vector3 direction;
    public Vector3 offset;
    
    private float currentDisplace;
    public float displace = 15f; // space from one object to another
    public float increment = 0.01f;
    public int moved = 0;
    public int quantity = 25;
    public int safeQuantity = 3;
    public float Speed = 6;
    private float speed;

    private void Awake()
    {
        tr = transform;
        itemsPool.Initialize();
        safeItemsPool.Initialize();
        originalPos = tr.position;
        elements = new Queue<Transform>();
    }

    public void Clean()
    {
        while (elements.Any())
            elements.Dequeue().gameObject.SetActive(false);
    }

    public void Generate()
    {
        tr.position = originalPos;
        speed = Speed;
        currentDisplace = 0;
        moved = 0;

        for (int i = 0; i < quantity; i++)
        {
            var elementTransform = i < safeQuantity ? safeItemsPool.GetRandom() : itemsPool.GetRandom();
            elementTransform.position = offset - direction * displace * i;
            elementTransform.gameObject.SetActive(true);
            elements.Enqueue(elementTransform);
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

        var elementsTransform = itemsPool.GetRandom();
        elementsTransform.position = last.position - direction * displace;
        elementsTransform.gameObject.SetActive(true);
        elements.Enqueue(elementsTransform);

        moved++;
    }
}