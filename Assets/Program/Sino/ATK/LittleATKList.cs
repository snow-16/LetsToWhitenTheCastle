using System.Collections;
using UnityEngine;

public class LittleATKList : MonoBehaviour
{
    [SerializeField] GameObject _arrowPrefab;
    [SerializeField] GameObject _gunPrefab;
    [SerializeField] GameObject _firePrefab;
    [SerializeField] float _moveSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Arrow()
    {
        GameObject arrow = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - arrow.transform.position;
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * _moveSpeed;
        yield return null;
    }

    public IEnumerator Gun()
    {
        GameObject gun = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - gun.transform.position;
        Rigidbody2D rb = gun.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * _moveSpeed * 1.5f;
        yield return null;
    }

    public IEnumerator FireDast()
    {
        GameObject fireDast = Instantiate(_arrowPrefab, transform.position, Quaternion.identity);
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 direction = player.transform.position - fireDast.transform.position;
        Rigidbody2D rb = fireDast.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * _moveSpeed * 0.8f;
        fireDast.transform.localScale *= 2;
        yield return null;
    }

    public void RandamLTLATK()
    {
        int AttackNum = Random.Range(0, 3);
        if (AttackNum == 0)
        {
            StartCoroutine("Arrow");
        }
        else if (AttackNum == 1)
        {
            StartCoroutine("Gun");
        }
        else
        {
            StartCoroutine("FireDast");
        }
    }
}
