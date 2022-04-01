using UnityEngine;

public interface IFactory<T>
{
    public string PrefabName { get; }
    public Object PrefabObject { get; set; }

    void Load();
    /// <summary>
    /// Create weapon
    /// </summary>
    void Create(T Marker, Vector3 StartPos);

}

