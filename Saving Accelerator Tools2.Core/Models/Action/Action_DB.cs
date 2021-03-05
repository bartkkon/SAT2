using Saving_Accelerator_Tools2.Core.Models.Action.InterTable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Models.Action
{
    public class Action_DB
    {

        [Key]
        public int ID { get; set; }
        public int ActionID { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Column(TypeName = "varchar(1000)")]
        public string Description { get; set; }
        [Column(TypeName = "decimal(4,0)")]
        public decimal StartYear { get; set; }
        public int Month { get; set; }
        public bool Active { get; set; }
        public int Calculation { get; set; } //1 -> ANC; 2 -> ANCSpec; 3 -> PNC; 4 -> PNCSpec

        #region Platform
        public bool DMD { get; set; }
        public bool D45 { get; set; }
        public bool FS { get; set; }
        public bool FI { get; set; }
        public bool BI { get; set; }
        public bool FSBU { get; set; }
        #endregion

        #region ECCC
        public bool ECCC { get; set; }
        public bool ECCCSpec { get; set; }
        [Column(TypeName = "decimal(5,1)")]
        public decimal ECCCValue { get; set; }
        #endregion

        #region Quantity Estymation
        [Column(TypeName = "decimal(8,4")]
        public decimal QEstymation { get; set; }
        #endregion

        public virtual List<Action_Devision_InterTable> Action_Devision { get; set; } = new List<Action_Devision_InterTable>();
        public virtual List<Action_Plant_InterTable> Action_Plant { get; set; } = new List<Action_Plant_InterTable>();
        public virtual List<Action_Leader_InterTable> Action_Leader { get; set; } = new List<Action_Leader_InterTable>();
        public virtual List<Action_Tag_InterTable> Action_Tag { get; set; } = new List<Action_Tag_InterTable>();
        public virtual List<Action_ANCChage_InterTable> Action_ANCChange { get; set; } = new List<Action_ANCChage_InterTable>();
        public virtual List<Action_PNC_InterTable> Action_PNC { get; set; } = new List<Action_PNC_InterTable>();
        public virtual List<Action_ANCChangePlatform_InterTable> Action_ANCChange_Platform { get; set; } = new List<Action_ANCChangePlatform_InterTable>();
        public virtual List<Action_ANCChange_Items_InterTable> Action_ANCChange_Items { get; set; } = new List<Action_ANCChange_Items_InterTable>();
        public virtual List<Action_PNCSpecial_InterTable> Action_PNCSpecial { get; set; } = new List<Action_PNCSpecial_InterTable>();
    }
}