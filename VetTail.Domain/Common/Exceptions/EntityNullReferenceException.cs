using System;

namespace VetTail.Domain.Common.Exceptions;

public class EntityNullReferenceException: Exception
{

    private EntityNullReferenceException(string message): base(message)
    {
        
    }


    private EntityNullReferenceException(string message, Exception exception): base(message, exception)
    {
    }

    public static EntityNullReferenceException Build<TEntity, TKey>(TKey key)
    {
        return new EntityNullReferenceException($"Entity of type {typeof(TEntity).Name} with Id: {key} doesnt or no longer exists.");
    }

    public static EntityNullReferenceException Build<TEntity, TKey>(TKey key, Exception innerException)
    {
        return new EntityNullReferenceException($"Entity of type {typeof(TEntity).Name} with Id: {key} doesnt or no longer exists.", innerException);
    }

    public static EntityNullReferenceException Build<TEntityType, Tkey>(string keyName, Tkey keyValue)
    {
        return new EntityNullReferenceException($"Entity of type {typeof(TEntityType).Name} with {keyName}: {keyValue} doesnt or no longer exists.");
    }

    public static EntityNullReferenceException Build<TEntityType, Tkey>(string keyName, Tkey keyValue, Exception innerException)
    {
        return new EntityNullReferenceException($"Entity of type {typeof(TEntityType).Name} with {keyName}: {keyValue} doesnt or no longer exists.", innerException);
    }

    public static EntityNullReferenceException Build(string customMessage)
    {
        return new EntityNullReferenceException(customMessage);
    }
    public static EntityNullReferenceException Build(string customMessage, Exception innerException)
    {
        return new EntityNullReferenceException(customMessage, innerException);
    }
}
