namespace CleverCore.Data.Interfaces
{
    public interface IHasOwner<T>
    {
        T OwnerId { set; get; }


    }
}
