using Saving_Accelerator_Tools2.DataBaseIServices.Others;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.Model.Others;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saving_Accelerator_Tools2.DataBaseServices.Others
{
    public class TagServices : ITagService
    {
        private readonly ConnectionContext connection;

        public TagServices(ConnectionContext connection)
        {
            this.connection = connection;
        }
        public void Add(Tag newTag)
        {
            connection.Tags.Add(newTag);
            connection.SaveChanges();
        }

        public ICollection<Tag> Get()
        {
            return connection.Tags.ToList();
        }

        public ICollection<Tag> Get(TagSearchCriteria searchCriteria)
        {
            IQueryable<Tag> tags = Get().AsQueryable();

            if (searchCriteria.From.HasValue)
            {
                tags.Where(s => s.From >= searchCriteria.From);
            }
            if(searchCriteria.Until.HasValue)
            {
                tags.Where(s => s.Until <= searchCriteria.Until);
            }
            if(searchCriteria.FactoryID.HasValue)
            {
                tags.Where(s => s.FactoryID == searchCriteria.FactoryID);
            }
            if(searchCriteria.Active.HasValue)
            {
                tags.Where(s => s.Active == searchCriteria.Active);
            }

            return tags.ToList();
        }

        public void Update(Tag updateTag)
        {
            connection.Tags.Update(updateTag);
            connection.SaveChanges();
        }
    }
}
