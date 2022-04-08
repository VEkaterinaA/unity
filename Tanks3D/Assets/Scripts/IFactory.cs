using System.Threading.Tasks;
using UnityEngine;

public interface IFactory<T>
{
    public string PrefabName { get; }
    public Object PrefabObject { get; set; }

    public Task Load();
    /// <summary>
    /// Create weapon
    /// </summary>
    public Task Create(T Marker, Vector3 StartPos);

}

