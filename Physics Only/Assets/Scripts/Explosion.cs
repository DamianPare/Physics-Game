using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private int blastPower;
    [SerializeField] private float blastCooldown;
    [SerializeField] private LayerMask placeableMask;
    private Vector3 startPos;
    private Vector3 endPos;
    private GameObject bomb;
    private SpriteRenderer bombRenderer;

    Collider2D[] inExplosionRadius = null;
    [SerializeField] private float explosionForceMulti = 5;
    [SerializeField] private float explosionRadius = 5;

    [SerializeField] private GameObject circle;
    [SerializeField] private int subdivisions = 10;
    private LineRenderer circleRenderer;
    private float circleRadius = 0.1f;

    private void Awake()
    {
        bomb = this.gameObject;
        bombRenderer = bomb.GetComponent<SpriteRenderer>();
        circleRenderer = circle.GetComponent<LineRenderer>();
    }

    void Start()
    {
        circleRenderer.material.color = Color.red;
        
    }
    
    void Update()
    {
        DrawCircle();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerBlast();
        }

        Vector2 hitInfo = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            bombRenderer.enabled = true;
            circle.SetActive(true);
            startPos = hitInfo;
            transform.position = startPos;
            Debug.Log(startPos);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            SetUpBlast(hitInfo);
            Debug.Log("setting up");
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            bombRenderer.enabled = false;
            TriggerBlast();
            circle.SetActive(false);
        }
    }

    public void SetUpBlast(Vector3 place)
    {
        circleRadius = place.x - startPos.x;
        explosionForceMulti = 1000 * (circleRadius);
        Debug.Log(explosionForceMulti);
    }

    public void TriggerBlast()
    {
        inExplosionRadius = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D o in inExplosionRadius)
        {
            Rigidbody2D o_rigidbody2D = o.GetComponent<Rigidbody2D>();
            if (o_rigidbody2D != null)
            {
                Vector2 distanceVector = o.transform.position - transform.position;
                if (distanceVector.magnitude > 0 )
                {
                    float explosionForce = explosionForceMulti / distanceVector.magnitude;
                    o_rigidbody2D.AddForce(distanceVector.normalized * explosionForce);
                    Debug.Log("boom");
                }
            }
        }
    }

    private void DrawCircle()
    {
        float angleStep = 2f * Mathf.PI / subdivisions;

        circleRenderer.positionCount = subdivisions;

        for (int i = 0; i < subdivisions; i++)
        {
            float xPosition = circleRadius * Mathf.Cos(angleStep * i);
            float yPosition = circleRadius * Mathf.Sin(angleStep * i);

            Vector3 pointInCircle = new Vector3(xPosition, yPosition, 0f);

            circleRenderer.SetPosition(i, pointInCircle);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
