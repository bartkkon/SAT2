using Saving_Accelerator_Tools2.Contracts.Services;
using Saving_Accelerator_Tools2.Core.Models.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.ViewModels.Action
{
    public class GlobalCommentViewModel : INotifyProperty
    {
        #region Constructors
        public GlobalCommentViewModel()
        {
            Mediator.Mediator.Register("GlobalComment_Set", SetData);
            Mediator.Mediator.Register("GlobalComment_Get", GetData);
        }
        ~GlobalCommentViewModel()
        {
            Mediator.Mediator.Unregister("GlobalComment_Set", SetData);
            Mediator.Mediator.Unregister("GlobalComment_Get", GetData);
        }
        #endregion

        #region Privates Variables
        private string comment;
        #endregion

        #region Public Variables
        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                RisePropoertyChanged();
            }
        } 
        #endregion

        #region Mediator Functions
        private void SetData(object obj)
        {
            var commentObject = (obj as List<Comments_DB>).Where(item => item.Month == 0 && string.IsNullOrEmpty(item.Revision)).FirstOrDefault();

            Comment = commentObject == null ? string.Empty : commentObject.Value;

        }
        private void GetData(object obj)
        {
            (obj as Comments_DB).Value = comment;
        } 
        #endregion
    }
}
