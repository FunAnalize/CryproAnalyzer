using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryproAnalyzer.Models
{
    internal class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ChatId { get; set; }

        public bool IsSubscribed { get; set; }
    }
}