using UnityEngine;

public interface IFactory<T1, T2> where T1 : class where T2 : class
{
    T1 Create(T2 model);
}
