using System.ComponentModel.DataAnnotations;

namespace PremiereAppASP.Models.Mappers {
    public class UserFormRegister {

        [Required]
        [MinLength(3,ErrorMessage = "Le nom d'utilisateur doit faire au moins 3 caractères." )]
        public string Username { get; set; }

        [Required]
        [DataType (DataType.Password)]
        [RegularExpression( "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}$", ErrorMessage = "Le mot de passe doit contenir au moins 8 caractères dont une majuscule," +
            "une minuscule et un chiffre." )]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Les deux mot de passes ne correspondent pas.")]
        [DataType( DataType.Password )]
        public string ConfirmPassword { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Entrez une adresse mail valide.")]
        public string Email { get; set; }

    }
}
