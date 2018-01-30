using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class SignalResult
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public Signal Signal { get; set; }
        [Column(TypeName = "numeric(30,28)")]
        public decimal Price { get; set; }
        public int SignalId { get; set; }
    }
}
