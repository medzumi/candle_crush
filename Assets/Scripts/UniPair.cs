using System;

[Serializable]
public struct UniPair<TKey, TValue>
{
    public TKey Key;
    public TValue Value;
}