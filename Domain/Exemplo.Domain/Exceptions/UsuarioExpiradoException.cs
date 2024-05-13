using Exemplo.Exceptions;

namespace Exemplo.Domain.Exceptions;

public class UsuarioExpiradoException : Exception
{
    public ItemInfoException ExceptionItemInfo { get; set; }
    public UsuarioExpiradoException(string model, string reference, string message) : this(new ItemInfoException(model, reference, message))
    {

    }
    public UsuarioExpiradoException(ItemInfoException exceptionItemInfo) : base(exceptionItemInfo.Message) => this.ExceptionItemInfo = exceptionItemInfo;
}









