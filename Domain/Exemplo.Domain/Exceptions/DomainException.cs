namespace Exemplo.Exceptions;

public class DomainException : Exception
{
    public ItemInfoException ExceptionItemInfo { get; set; }
    public DomainException(string model, string reference, string message, params object[] arguments) : this(new ItemInfoException(model, reference, message, arguments))
    {

    }
    public DomainException(ItemInfoException exceptionItemInfo) : base(exceptionItemInfo.Message) => this.ExceptionItemInfo = exceptionItemInfo;
}





