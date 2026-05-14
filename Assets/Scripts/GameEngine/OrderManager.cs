using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] GameObject orderPrefab;
    [SerializeField] Transform orderParent;
    [SerializeField] int startNumberOfOrders;
    [SerializeField] float timeBetweenOrders;
    float currentTimeBetweenOrders;
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

    private void CreateOrder()
    {
        GameObject order = Instantiate(orderPrefab, orderParent);
        Order cOrder = order.GetComponent<Order>();
        cOrder.Setup(OrderValueGenerator.instance.GetRandomValue(level.GetRandomDifficulty()));
        currentTimeBetweenOrders = 0;
    }

    private void Update()
    {
        currentTimeBetweenOrders += Time.deltaTime;
        if (currentTimeBetweenOrders >= timeBetweenOrders)
        {
            CreateOrder();
        }
    }

    public void TrySolve(Value value)
    {
        List<Order> orders = FindObjectsByType<Order>(FindObjectsSortMode.None).ToList();
        var resultat = orders
            .Where(x => x.value.value == value.value)
            .OrderBy(x => 0-x.currentTimeBeforeExpire)
            .FirstOrDefault();
        if (resultat != null)
        {
            Destroy(resultat.gameObject);
        }
    }
}
