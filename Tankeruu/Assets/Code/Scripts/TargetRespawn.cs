using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

[RequireComponent(typeof(SphereCollider))]
public class TargetRespawn : MonoBehaviour
{
    [SerializeField] private Vector3 _worldBounds;
    [SerializeField] private float _respawnDelay;
    private float _radius;
    private float _height;
    private const int MAX_TRYS = 10;

    private void Awake() 
    {
        _radius = GetComponent<SphereCollider>().radius;
        _height = transform.position.y;
    }

    private void OnCollisionEnter(Collision other) 
    {
        // take it offscreen
        transform.position = Vector3.up * _worldBounds.x;
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(_respawnDelay);
        transform.position = FindValidPosition();
    }

    private Vector3 FindValidPosition()
    {
        // try to find a valid position 
        Vector3 bounds = _worldBounds * 0.5f;
        Vector3 position;
        int maxTrys = MAX_TRYS;

        do
        {
            position = new Vector3(Random.Range(-bounds.x, bounds.x), _height, Random.Range(-bounds.z, bounds.z));
            Collider[] colliders = Physics.OverlapSphere(position, _radius);

            if(colliders.Length == 0)
                return position;

            maxTrys--;

        } while(maxTrys != 0);

        return default;
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(Vector3.up + new Vector3(0, _worldBounds.y * 0.5f, 0), _worldBounds);    
    }
}
