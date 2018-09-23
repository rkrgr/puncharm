using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour {

    public GameObject impactEffect;
    public GameObject loadMark;

    public float punchSpeed;
    public int damage;


    public float punchLoadSpeed;
    public float minPunchDistance;
    public float maxPunchDistance;

    float currentLoadDistance;

    internal bool isPunching = false;
    bool isFistExpanding = false;
    bool isLoading = false;
    Vector3 initialLocalFistPosition;
    Vector2 loadMarkPos;

    void Start()
    {
        loadMark.SetActive(false);
        initialLocalFistPosition = transform.localPosition;
    }

    void Update()
    {
        if(!isPunching)
        {
            if (!isLoading)
            {
                ResetAimMarkPosition();
            }

            if (Input.GetMouseButtonDown(0))
            {
                loadMark.SetActive(true);
                InitiateLoad();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                loadMark.SetActive(false);
                InitiatePunch();
            }
        }
        else
        {
            loadMark.transform.position = loadMarkPos;
        }
    }

    private void ResetAimMarkPosition()
    {
        loadMark.transform.localPosition = initialLocalFistPosition;
        loadMark.transform.Translate(Vector3.down * minPunchDistance);
    }

    private void InitiateLoad()
    {
        isLoading = true;
    }

    private void InitiatePunch()
    {
        loadMarkPos = loadMark.transform.position;
        isPunching = true;
        isFistExpanding = true;
        isLoading = false;
    }

    void FixedUpdate()
    {
        if (isLoading && IsAimMarkInPunchRange())
        {
            LoadAim();
        }

        if (isPunching)
        {
            if (isFistExpanding)
            {
                ExpandFist();

                if (transform.position == loadMark.transform.position)
                {
                    isFistExpanding = false;
                }
            }
            else
            {
                ContractFist();

                if (transform.localPosition == initialLocalFistPosition)
                {
                    isPunching = false;
                }
            }
        }
    }

    private void ExpandFist()
    {
        transform.position = Vector2.MoveTowards(transform.position, loadMark.transform.position, punchSpeed * Time.fixedDeltaTime);
    }

    private void ContractFist()
    {
        transform.localPosition = Vector2.MoveTowards(transform.localPosition, initialLocalFistPosition, punchSpeed * Time.fixedDeltaTime);
    }

    private void LoadAim()
    {
        loadMark.transform.Translate(Vector3.down * punchLoadSpeed * Time.fixedDeltaTime);
    }

    private bool IsAimMarkInPunchRange()
    {
        // use sqrMagnitude and not magnitude for distance checks because it performs better
        return (initialLocalFistPosition - loadMark.transform.localPosition).sqrMagnitude < maxPunchDistance * maxPunchDistance;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        FistCollide(col);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        FistCollide(col);
    }

    void FistCollide(Collider2D col)
    {
        Enemy enemy = col.GetComponent<Enemy>();
        if (enemy != null && isFistExpanding)
        {
            enemy.TakeDamage(damage);
            Instantiate(impactEffect, transform.position, transform.rotation);
            isFistExpanding = false;
        }

        isFistExpanding = false;
    }
}
