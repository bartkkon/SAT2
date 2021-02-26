using System;
using System.Collections.Generic;
using System.Text;

namespace Saving_Accelerator_Tools2.Models.Action
{
    public class ANCSpecialModels
    {
        public int ID { get; set; }
        public string Platform { get; set; }
        public string Installation { get; set; }
    }
    public class PlusMinusModel
    {
        public int ID { get; set; }
        public string Item { get; set; }
        public bool Plus { get; set; }
        public bool Minus { get; set; }
        public bool Active { get; set; } = false;
        /*
         Zmienna active - wykorzystywana przy zapisie.
         Jesli zostanie zapisana jako false przy zapisie akcji to należy ja usunąć z bazy danych (to połaczenie)
         
         */
    }
}
