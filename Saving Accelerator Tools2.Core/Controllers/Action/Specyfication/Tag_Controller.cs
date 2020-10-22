using Saving_Accelerator_Tools2.Core.Data;
using Saving_Accelerator_Tools2.Core.Models.Action.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Core.Controllers.Action.Specyfication
{
    public class Tag_Controller
    {
        public static Dictionary<int,string> LoadTagToAction(decimal Year)
        {
            var TagList = new Dictionary<int, string>();
            var context = new DataBaseConnetionContext();
            List<Tag_DB> TagDataBase;
            TagDataBase = context.Tag.Where(u => u.Start <= Year && u.Finish >= Year).ToList();
            foreach(var Tag in TagDataBase) {
                TagList.Add(Tag.ID, Tag.Name);
            }

            return TagList;
        }

        public static ICollection<Tag_DB> Load_Year (decimal Year)
        {
            var context = new DataBaseConnetionContext();

            return context.Tag.Where(u => u.Start <= Year && u.Finish >= Year).ToList();
        }
    }
}
