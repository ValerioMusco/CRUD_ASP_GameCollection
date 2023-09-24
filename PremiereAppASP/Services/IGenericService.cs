namespace PremiereAppASP.Services {
    public interface IGenericService<T, U> where T : class {

        T GetById( U Id );
        IEnumerable<T> GetAll( );
        bool Delete ( U Id );
    }
}
