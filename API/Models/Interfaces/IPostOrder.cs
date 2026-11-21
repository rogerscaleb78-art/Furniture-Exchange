using Mysqlx.Crud;

namespace API.Models.Interfaces
{
    public interface IPostOrder
    {
        public void PostOrder(OrderForm value);
    }
}
