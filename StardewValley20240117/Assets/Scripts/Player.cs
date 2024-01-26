using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float speed = 3;

    private Animator anim;
    private Vector2 direction = Vector2.zero;
    public ToolbarUI toolbarUI;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        if (direction == null)
        {
            return;
        }
        if (direction.magnitude > 0)
        { 
            anim.SetBool("IsWalking", true);
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.y);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        if (toolbarUI.GetSelectToolbarSlotUI() != null && toolbarUI.GetSelectToolbarSlotUI().GetData() != null && toolbarUI.GetSelectToolbarSlotUI().GetData().item.type == ItemType.Hoe && Input.GetKeyDown(KeyCode.Space))
        {
            PlantManager.Instance.HoeGround(transform.position);
            anim.SetTrigger("Hoe");
        }
    }
    // FixedUpdate is called once per frame
    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        direction = new Vector2(x, y);
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Pickable")
        {
            InventoryManager.Instance.AddToBackpack(collision.GetComponent<Pickable>().type);
            Destroy(collision.gameObject);
        }
    }

    public void ThrowItem(GameObject itemPrefab, int count)
    {
        for (int i = 0; i < count; ++i)
        {
            GameObject go = GameObject.Instantiate(itemPrefab);
            var direction = Random.insideUnitCircle.normalized * 1.2f;
            go.transform.position = transform.position + new Vector3(direction.x, direction.y, 0);
            go.GetComponent<Rigidbody2D>().AddForce(direction * 2);
        }
    }
}
