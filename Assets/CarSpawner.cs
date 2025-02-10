using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CarSpawner : MonoBehaviour
{
    public enum CarType
    {
        Black,
        Red,
        Blue
    }

    [Header("Custom Property")]
    [SerializeField] GameObject[] CarPrefabs;
    [SerializeField] float SpawnTime;
    [SerializeField] GameObject BlackCarPath;
    [SerializeField] GameObject RedCarPath;
    [SerializeField] GameObject BlueCarPath;
    [SerializeField] CarType Type;

    RectTransform[] CarPath;
    Image targetImage;
    float SpawnTimer;
    List<GameObject> SpwanedCar;
    public bool isStart;

    private void Start()
    {
        targetImage = GetComponent<Image>();

        switch (Type)
        {
            case CarType.Black:
                InitCarPath(BlackCarPath);
                break;
            case CarType.Red:
                InitCarPath(RedCarPath);
                break;
            case CarType.Blue:
                InitCarPath(BlueCarPath);
                break;
            default:
                break;
        }
    }

    void Update()
    {
        if (!isStart)
            return;

        if (targetImage.enabled) 
        {
            SpawnTimer += Time.deltaTime;

            if (SpawnTimer > SpawnTime)
            {
                SpawnCar();
            }
        }
    }

    void SpawnCar() 
    {
        GameObject gameObject = Instantiate(CarPrefabs[(int)Type], transform);
        SpwanedCar.Add(gameObject);
        MoveToDestinationScript moveToDestinationScript = gameObject.GetComponent<MoveToDestinationScript>();
        
    }

    void InitCarPath(GameObject PathParent) 
    {
        CarPath = PathParent.GetComponentsInChildren<RectTransform>();
    }
}
