using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CarManager : MonoBehaviour, ActionScriptInterface
{
    enum CarType
    {
        Black,
        Red,
        Green
    }

    [SerializeField] MoveToDestinationScript[] moveToDestinationScript_Black;
    [SerializeField] MoveToDestinationScript[] moveToDestinationScript_Red;
    [SerializeField] MoveToDestinationScript[] moveToDestinationScript_Green;

    public bool CanMove = false;

    [SerializeField] ShowImageScript Owner;
    WaitForSecondsRealtime secondsRealtime;
    int index_Black = 0;
    int index_Red = 0;
    int index_Green = 0;
    float DelayTimer = 1.0f;
    bool isPlaying = false;
    bool IsInit = false;
    bool Startfucntion = false;

    void Awake()
    {
        Owner = GetComponent<ShowImageScript>();
        secondsRealtime = new WaitForSecondsRealtime(1.0f);
    }

    void Update()
    {
        if (!Owner.StartTimer)
        {
            isPlaying = false;
            StopAllCoroutines();
        }

        if (Startfucntion)
            return;

        if (IsInit) 
        {
            DelayTimer += Time.deltaTime;

            if (DelayTimer > 3.0f && !Startfucntion) 
            {
                Startfucntion = true;
                StartCoroutine(MoveAllCar());
            }
        }
    }

    public void StartAction()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            initialize();
            //StartCoroutine(MoveAllCar());
        }

    }


    void initialize()
    {
        for (int i = 2; i >= 0; --i)
        {
            for (int j = 0; j <= i; ++j)
            {
                StartCoroutine(moveToDestinationScript_Black[j].MoveNextPointCoroutine());
                StartCoroutine(moveToDestinationScript_Red[j].MoveNextPointCoroutine());
                StartCoroutine(moveToDestinationScript_Green[j].MoveNextPointCoroutine());
            }
        }

        IsInit = true;
    }


    IEnumerator MoveAllCar()
    {
        while (Owner.StartTimer && isPlaying)
        {
            CarType startCar = (CarType)UnityEngine.Random.Range(0, 3);

            if (!Owner.StartTimer)
                yield break;

            switch (startCar)
            {
                case CarType.Black:
                    Debug.Log("Move Car Black");
                    yield return StartCoroutine(MoveCarCoroutine(index_Black, moveToDestinationScript_Black, newIndex => index_Black = newIndex));
                    break;
                case CarType.Red:
                    Debug.Log("Move Car Red");
                    yield return StartCoroutine(MoveCarCoroutine(index_Red, moveToDestinationScript_Red, newIndex => index_Red = newIndex));
                    break;
                case CarType.Green:
                    Debug.Log("Move Car Green");
                    yield return StartCoroutine(MoveCarCoroutine(index_Green, moveToDestinationScript_Green, newIndex => index_Green = newIndex));
                    break;
                default:
                    break;
            }

            yield return secondsRealtime;
        }
    }

    int ModuloCounter(int number)
    {
        return (number + 1) % 3;
    }

    IEnumerator MoveCarCoroutine(int index, MoveToDestinationScript[] moveToDestinationScripts, System.Action<int> onComplete)
    {
        Debug.Log("Start Index : " + index);
        int tempIndex = index;
        yield return StartCoroutine(moveToDestinationScripts[index].MoveNextPointCoroutine());
        Debug.Log("Move Car : " + tempIndex);
        tempIndex = ModuloCounter(tempIndex);
        yield return StartCoroutine(moveToDestinationScripts[tempIndex].MoveNextPointCoroutine());
        Debug.Log("Move Car : " + tempIndex);
        tempIndex = ModuloCounter(tempIndex);
        yield return StartCoroutine(moveToDestinationScripts[tempIndex].MoveNextPointCoroutine());
        Debug.Log("Move Car : " + tempIndex);
        yield return StartCoroutine(moveToDestinationScripts[index].MoveNextPointCoroutine());

        onComplete?.Invoke(ModuloCounter(index));

        /*        int count = moveToDestinationScripts.Length;
                Debug.Log("Front car index for chosen color: " + index);


                yield return StartCoroutine(moveToDestinationScripts[index].MoveNextPointCoroutine());
                Debug.Log("Moved front car at index: " + index);
                yield return new WaitForSeconds(0.05f);

                for (int i = 1; i < count; i++)
                {
                    int followingIndex = (index + i) % count;
                    Debug.Log("Moving following car at index: " + followingIndex);
                    yield return StartCoroutine(moveToDestinationScripts[followingIndex].MoveNextPointCoroutine());
                    //yield return new WaitForSeconds(0.05f);
                }

                int newIndex = (index + 1) % count;
                onComplete?.Invoke(newIndex);*/
    }

}


