using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryproAnalyzer.Models
{
    class User
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ChatId { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
