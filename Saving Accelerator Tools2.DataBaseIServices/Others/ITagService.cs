using Saving_Accelerator_Tools2.Model.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseIServices.Others
{
    public interface ITagService
    {
        public void Add(Tag newTag);
        public void Update(Tag updateTag);
        public ICollection<Tag> Get();
        public ICollection<Tag> Get(TagSearchCriteria searchCriteria);


    }
}
