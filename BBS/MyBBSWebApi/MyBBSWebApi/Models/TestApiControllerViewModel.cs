using System.ComponentModel.DataAnnotations;

namespace MyBBSWebApi.Models
{
    public class TestApiControllerViewModel
    {
        [MaxLength(2)]
        public string Id { get; set; }
    }
}