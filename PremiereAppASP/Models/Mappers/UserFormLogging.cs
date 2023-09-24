using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PremiereAppASP.Models.Mappers {
    public class UserFormLogging {

        [Required]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }
    }
}
