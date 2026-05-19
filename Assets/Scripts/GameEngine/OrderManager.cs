using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] GameObject orderPrefab;
    [SerializeField] Transform orderParent;
    [SerializeField] int startNumberOfOrders;

    [Header("Configuration du Rythme")]
    [SerializeField] private float timeBetweenOrders = 8f;   // Temps moyen souhaité pour 1 commande
    [SerializeField] private int targetOrderCount = 3;        // Le nombre de commandes idéal visé
    [SerializeField] private int maxOrderCount = 5;           // NOMBRE MAX : Limite physique de l'écran
    [SerializeField] private float minSpawnInterval = 3f;     // Temps de respiration minimum entre 2 spawns
    [SerializeField] private float maxSpawnInterval = 20f;    // Limite max de rythme si le joueur galère

    private float spawnProgress;
    private float timeSinceLastSpawn;

    public static OrderManager instance;
    [SerializeField] Level level;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < startNumberOfOrders; i++)
        {
            CreateOrder();
        }
    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        int currentOrders = orderParent.childCount;

        if (currentOrders >= maxOrderCount)
        {
            return;
        }

        float difference = currentOrders - targetOrderCount;
        float orderRatio = (float)currentOrders / maxOrderCount;

        float currentTargetDuration = Mathf.Lerp(minSpawnInterval, maxSpawnInterval, orderRatio);

        spawnProgress += Time.deltaTime / currentTargetDuration;

        if (spawnProgress >= 1f && timeSinceLastSpawn >= minSpawnInterval)
        {
            CreateOrder();
        }
    }

    private void CreateOrder()
    {
        GameObject order = Instantiate(orderPrefab, orderParent);
        Order cOrder = order.GetComponent<Order>();
        int diff = level.GetRandomDifficulty();
        cOrder.Setup(OrderValueGenerator.instance.GetRandomValue(diff), diff);

        spawnProgress = 0f;
        timeSinceLastSpawn = 0f;
    }

    public void TrySolve(Value value)
    {
        List<Order> orders = orderParent.GetComponentsInChildren<Order>().ToList();

        var resultat = orders
            .Where(x => x.GetValue().value == value.value)
            .OrderBy(x => 0 - x.GetTimeBeforeExpire())
            .FirstOrDefault();

        if (resultat != null)
        {
            Destroy(resultat.gameObject);
        }
    }
}
