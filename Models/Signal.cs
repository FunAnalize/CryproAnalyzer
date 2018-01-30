using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Signal
    {
        public string MarketName { get; set; }
        public DateTime Timestamp { get; set; }
        [Column(TypeName = "numeric(30,28)")]
        public decimal Price { get; set; }
        public int Id { get; set; }
        public int Interval { get; set; }
        public ICollection<SignalResult> SignalResults { get; set; }=new List<SignalResult>();
        [Column(TypeName = "numeric(30,28)")]
        public decimal Average { get; set; }
        [Column(TypeName = "numeric(30,28)")]
        public decimal? Ratio { get; set; }

    }
}
