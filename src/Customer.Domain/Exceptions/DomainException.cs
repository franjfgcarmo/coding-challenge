using System.Runtime.Serialization;

namespace Customer.Domain.Exceptions;

[Serializable]
public class DomainException : Exception
{
    public DomainException()
    {
    }

    public DomainException(string message) : base(message)
    {
    }

    public DomainException(string message, Exception innerException) : base(message, innerException)
    {
    }

#pragma warning disable SYSLIB0051
    protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context)
#pragma warning restore SYSLIB0051
    {
    }
}