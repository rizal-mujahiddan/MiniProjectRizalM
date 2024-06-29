namespace ManagerApprove.Contracts
{
    public interface ICrud<T>
    {
        T Update(T entity);
    }
}
