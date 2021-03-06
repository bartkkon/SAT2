﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Other.Data
{
    public abstract class Quantity_DB
    {
        [Key]
        public int ID { get; set; }

        [Column(TypeName = "varchar(9)")]
        public string Item { get; set; }
        [Column(TypeName ="decimal(4,0")]
        public decimal Year { get; set; }
        
        [Column(TypeName ="varchar(3)")]
        public string Revision { get; set; }    
        
        public int Month { get; set; }

        [Column(TypeName = "decimal(18,4")]
        public decimal Quantity { get; set; }
    }
}
