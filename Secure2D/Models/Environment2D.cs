using System.ComponentModel.DataAnnotations;

namespace Secure2D.Models
{
    public class Environment2D
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int MaxHeight { get; set; }
        public int MaxLength { get; set; }
    }
}
