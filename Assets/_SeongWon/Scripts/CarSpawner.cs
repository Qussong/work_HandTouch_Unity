using System.Collections;
using System.Collections.Generic;
//using Mono.Cecil;
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
    [SerializeField] float delay;

    RectTransform[] CarPath;
    Image targetImage;
    float SpawnTimer;
    List<GameObject> SpwanedCar;
    public bool isStart;
    int currentIndex;

    private void Start()
    {
        targetImage = GetComponent<Image>();
        SpwanedCar = new List<GameObject>();

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
                SpawnTimer = 0.0f;
                SpawnCar();
            }
        }
    }

    void SpawnCar() 
    {
        if (currentIndex -2 <= 0)
        {
            isStart = false;
        }
        GameObject gameObject = Instantiate(CarPrefabs[(int)Type], transform);

        if (Type == CarType.Red)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            gameObject.transform.rotation = transform.rotation;
        }
        SpwanedCar.Add(gameObject);

        MoveToDestinationScript moveToDestinationScript = gameObject.GetComponent<MoveToDestinationScript>();
        moveToDestinationScript.wayPointTransforms = CarPath;
        moveToDestinationScript.IsStart = true;
        moveToDestinationScript.delay = delay;

        if (Type == CarType.Black) 
        {
            if(currentIndex == 8 || currentIndex == 5)
            currentIndex--;
        }

        moveToDestinationScript.EndIndex = currentIndex;
        currentIndex--;

    }

    void InitCarPath(GameObject PathParent) 
    {
        CarPath = PathParent.GetComponentsInChildren<RectTransform>();
        currentIndex = CarPath.Length;
    }

    public void DestoryAllCar() 
    {
        if (SpwanedCar.Count <= 0)
            return;

       foreach (var car in SpwanedCar)
        {
            MoveToDestinationScript moveToDestinationScript = car.GetComponent<MoveToDestinationScript>();

            if (moveToDestinationScript != null)
            {
                moveToDestinationScript.DiableEditAlphaValue();
            }

            Destroy(car.gameObject);
        }

        SpwanedCar.Clear();
        currentIndex = CarPath.Length;
    }

    public void EnableChangeAlphaValue() 
    {
        if (SpwanedCar.Count <= 0)
            return;

        foreach (var car in SpwanedCar)
        {
            MoveToDestinationScript moveToDestinationScript = car.GetComponent<MoveToDestinationScript>();
            moveToDestinationScript.EnableEditAlphaValue();
        }
    }
}
