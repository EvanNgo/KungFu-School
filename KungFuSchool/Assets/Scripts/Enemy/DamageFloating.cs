using UnityEngine;
using UnityEngine.UI;
public class DamageFloating : MonoBehaviour {
    public float destroyTime;
    public float moveSpeed;
    public int damageNumber;
    public Text tvDamage;
	// Use this for initialization
	void Start () {
        tvDamage.text = "-" + damageNumber;
    }
	
	// Update is called once per frame
	void Update () {
        destroyTime -= Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y + (moveSpeed * Time.deltaTime), transform.position.z);
        if (destroyTime <= 0)
        {
            Destroy(gameObject);
        }
	}
}
