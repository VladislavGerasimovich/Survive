using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [SerializeField] private ExplosionArea _explosionArea;
    [SerializeField] private GameObject _grenadePrefab; 
    [SerializeField] private GrenadeCollisionHandler _collisionHandler;

    private void OnEnable()
    {
        _collisionHandler.TouchedGround += Explosion;
    }

    private void OnDisable()
    {
        _collisionHandler.TouchedGround -= Explosion;
    }

    public void SetExplosionAreaCreator(ExplosionAreaCreator explosionAreaCreator)
    {
        //_explosionAreaCreator = explosionAreaCreator;
    }

    public void ShowGrenadePrefab(Vector3 position, Vector3 force)
    {
        _grenadePrefab.SetActive(true);
        _grenadePrefab.transform.position = position;
        _grenadePrefab.GetComponent<Rigidbody>().velocity = force;
    }

    private void Explosion(Vector3 position)
    {
        _explosionArea.GetComponent<ExplosionArea>().Run(position);
    }
}
