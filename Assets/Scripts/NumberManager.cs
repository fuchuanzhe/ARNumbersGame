using System.Collections.Generic;
using UnityEngine;

public class NumberManager : MonoBehaviour
{
    public Camera arCamera;
    public List<GameObject> numberPrefabs; // index 0 = number 1 etc
    private float spawnDistance = 1.0f;

    private GameObject current;
    private Queue<int> queue;

    public int CurrentNumber { get; private set; } = -1;

    void Awake()
    {
        if (!arCamera) arCamera = Camera.main;
        ShuffleNumbers();
    }

    private void ShuffleNumbers()
    {
        var nums = new List<int>();
        for (int i = 1; i <= 10; i++) nums.Add(i);
        for (int i = 0; i < nums.Count; i++)
        {
            int j = Random.Range(i, nums.Count);
            (nums[i], nums[j]) = (nums[j], nums[i]);
        }
        queue = new Queue<int>(nums);
    }

    public void SpawnNext()
    {
        Spawn(queue.Dequeue());
    }

    public bool hasNextToSpawn()
    {
        return queue.Count > 0;
    }

    private void Spawn(int number)
    {
        if (current) Destroy(current);

        CurrentNumber = number;

        Vector3 pos = arCamera.transform.position + arCamera.transform.forward * spawnDistance;
        Quaternion rot = Quaternion.LookRotation(pos - arCamera.transform.position);

        current = Instantiate(numberPrefabs[number - 1], pos, rot);
    }

    public void Clear()
    {
        if (current) Destroy(current);
    }
}
