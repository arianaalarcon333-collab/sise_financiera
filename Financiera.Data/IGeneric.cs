using Financiera.Model;

namespace Financiera.Data
{
    public interface IGeneric<T> where T:class
    {
        List<T> Listar();
        T ObtenerPorID(int id);
        int Registrar(T entidad);
        bool Eliminar(int id);
        bool Actualizar(T entidad);
    }
}
