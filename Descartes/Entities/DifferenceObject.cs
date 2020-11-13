using Descartes.DifferenceDeterminator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Descartes.Entities
{
    [Table("DifferenceObject")]
    public class DifferenceObject
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("ID")]
        public int Id { get; set; }

        [Column("DiffResult")]
        public string DiffResult { get; set; }

        [Column("RightValue")]
        public string RightValue { get; set; }

        [Column("LeftValue")]
        public string LeftValue { get; set; }
    }
}
