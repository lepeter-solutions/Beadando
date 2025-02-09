using System.ComponentModel.DataAnnotations;

namespace Beadando.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A név megadása kötelező")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Az email megadása kötelező")]
        [EmailAddress(ErrorMessage = "Érvénytelen email cím")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Az életkor megadása kötelező")]
        [Range(13, 100, ErrorMessage = "Az életkornak 13 és 100 év között kell lennie")]
        public int Age { get; set; }

    }
}
