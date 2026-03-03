using UnityEngine;

public interface IEffect<T> where T : IEffectable
{
    void Apply(T target);
}
