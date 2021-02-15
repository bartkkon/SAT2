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
        public static List<Tag_DB> LoadTagToAction(decimal Year)
        {
            var context = new DataBaseConnetionContext();

            var TagDataBase = context.Tag.Where(u => u.Start <= Year && u.Finish >= Year).ToList();

            return TagDataBase;
        }

        public static ICollection<Tag_DB> Load_Year (decimal Year)
        {
            var context = new DataBaseConnetionContext();

            return context.Tag.Where(u => u.Start <= Year && u.Finish >= Year).ToList();
        }

        public static Tag_DB LoadByName (string TagName)
        {
            var context = new DataBaseConnetionContext();

            return context.Tag.Where(u => u.Name == TagName).FirstOrDefault();
        }
    }
}
