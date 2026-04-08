using UnityEngine;
using UnityEngine.InputSystem;

public class CardThrowingController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject SpawnObj;
    public GameObject CardPrefab;

    [Header("Variables")]
    public float m_CardSpeed;
    
    void Start()
    {
        RegisterInputs();
    }

    void RegisterInputs()
    {
        InputSystem.actions["Attack"].performed += ctx => SpawnCard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnCard()
    {
        if (SpawnObj != null)
        {
            GameObject go = Instantiate(CardPrefab);
            go.transform.position = SpawnObj.transform.position;
            go.transform.rotation = SpawnObj.transform.rotation;
            go.transform.rotation *= Quaternion.Euler(90, 0, 0);
        }
    }
}
