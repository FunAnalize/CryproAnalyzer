using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryproAnalyzer.Models
{
    class User
    {
        [Key]
        public string ChatID { get; set; }
        public bool IsSubscriber { get; set; }
    }
}
