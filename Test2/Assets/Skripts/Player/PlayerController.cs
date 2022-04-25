using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 20.0f;
    private Vector2 direction;
    private Rigidbody2D rb;
    public Animator animator;

    public Camera cam;
    public Rigidbody2D angleOfAttack;
    Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        //смена значений полей Horizontal, Vertical и Speed в аниматоре, для смены анимаций
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePosition - rb.position;
        //расчёт угла относительно положения мышки
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        angleOfAttack.position = rb.position;
        angleOfAttack.rotation = angle;
    }
}
