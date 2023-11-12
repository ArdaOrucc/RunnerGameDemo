using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public interface IService { } 

public static class ServiceProvider 
{
    private static readonly Dictionary<Type, object> ServiceDictionary = new Dictionary<Type, object>();

    public static void Register<T>(T service) where T : IService
    {
        if (ServiceDictionary.ContainsKey(typeof(T)))
        {
            Debug.LogWarning($"<color=red> Service {typeof(T)} is already registered! </color>");
        }
        else
        {
            ServiceDictionary.Add(typeof(T), service);
            Debug.Log($"<color=green> Service {typeof(T)} is successfully registered </color>");
        }
    }

    public static void Unregister<T>(T service) where T : class , IService
    {
        if (ServiceDictionary.ContainsKey(typeof(T)))
        {
            Debug.Log($"<color=green> Service {typeof(T)} is successfully unregistered </color>");
            ServiceDictionary.Remove(typeof(T));
        }
        else
        {
            Debug.LogWarning($"<color=yellow> Service {typeof(T)} is not registered, so can't register </color>");
        }
    }

    public static T GetService<T>() where T : Component, IService, new()
    {
        var isRegistered = ServiceDictionary.TryGetValue(typeof(T), out var serviceObject);
        if (isRegistered)
        {
            Debug.Log($"<color=green> Service {typeof(T)} is registered and returned </color>");
            return (T)serviceObject;
        }
        else
        {
            var foundObject = Object.FindObjectOfType(typeof(T));
            if (foundObject != null)
            {
                Debug.LogWarning($"<color=yellow> Service {typeof(T)} is not registered, but found in scene! </color>");
                return (T)foundObject;
            }
            else
            {
                Debug.LogWarning($"<color=red> Service {typeof(T)} is not in scene, and creating a new one! </color>");
                var newCreatedService = new GameObject(typeof(T).Name).AddComponent<T>();
                return newCreatedService;
            }
        }
    }
}
