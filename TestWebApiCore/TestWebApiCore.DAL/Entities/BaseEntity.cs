using System.ComponentModel.DataAnnotations;

namespace TestWebApiCore.DAL.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
