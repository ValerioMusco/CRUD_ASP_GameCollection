using PremiereAppASP.Models.Mappers;

namespace PremiereAppASP.Services {
    public interface IUserService : IGenericService<UserFormRegister, int>{

        public bool Create( UserFormRegister userFormRegister);
        string GetPassword( UserFormLogging logging );
        public bool Update( UserFormRegister userFormRegister, int id);
    }
}
