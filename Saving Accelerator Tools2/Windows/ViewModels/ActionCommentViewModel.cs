using Saving_Accelerator_Tools2.Core.Models.Other;
using Saving_Accelerator_Tools2.Core.Models.ProductionData;
using Saving_Accelerator_Tools2.Tasks.Calculation_Function;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Saving_Accelerator_Tools2.Windows.ViewModels
{
    public class ActionCommentViewModel
    {
        public ActionCommentViewModel()
        {
            //var LoadApprovals = new Approvals();
            //var DevisionCheck = LoadApprovals.Check_Devisions(devision, actionYear);
            //if()
        }

        #region Private Variables
        private List<Approvals_DB> OpenRevisions = new List<Approvals_DB>();
        private decimal actionYear;
        private string devision;
        private List<Comments_DB> commentList;
        private readonly string Separator = "-------------------------------------------";

        private bool show_FirstYear = true;
        private bool show_SecondYear = true;
        private bool show_Revision = true;
        private bool show_Months = true;

        private string commentsList_ToShow;
        private bool newComment_Enabled = false;
        #endregion


        #region Public Variables
        public bool Show_FirstYear
        {
            get { return show_FirstYear; }
            set
            {
                show_FirstYear = value;
                PrepareShowData();
                //RisePropoertyChanged();
            }
        }
        public bool Show_SecondYear
        {
            get { return show_SecondYear; }
            set
            {
                show_SecondYear = value;
                PrepareShowData();
                //RisePropoertyChanged();
            }
        }
        public bool Show_Revision
        {
            get { return show_Revision; }
            set
            {
                show_Revision = value;
                PrepareShowData();
                //RisePropoertyChanged();
            }
        }
        public bool Show_Months
        {
            get { return show_Months; }
            set
            {
                show_Months = value;
                PrepareShowData();
                //RisePropoertyChanged();
            }
        }
        public string CommentList_ToShow
        { 
            get { return commentsList_ToShow; }
            set
            {
                commentsList_ToShow = value;
                //RisePropoertyChanged();
            }
        }
        public bool NewComment_Eabled
        {
            get { return newComment_Enabled; }
            set
            {
                newComment_Enabled = value;
                //RisePropoertyChanged();
            }
        }
        #endregion

        private void PrepareShowData()
        {
            commentsList_ToShow = string.Empty;

            if (show_FirstYear)
            {
                var FirstYear = commentList.Where(item => item.Year == actionYear).ToList();

                if (show_Revision)
                {
                    var FirstYear_Revison = FirstYear.Where(item => !string.IsNullOrEmpty(item.Revision)).ToList();
                    foreach (var FirstYear_Revison_Record in FirstYear_Revison)
                    {
                        string ToAdd = Separator + Environment.NewLine +
                            $"Revision: {FirstYear_Revison_Record.Revision}" +
                            "Comment:" + Environment.NewLine +
                            FirstYear_Revison_Record.Value + Environment.NewLine;
                        commentsList_ToShow += ToAdd;
                    }
                }
                if (show_Months)
                {
                    var FirstYear_Revison = FirstYear.Where(item => item.Month != 0).ToList();
                    foreach (var FirstYear_Revison_Record in FirstYear_Revison)
                    {
                        string ToAdd = Separator + Environment.NewLine +
                            $"Month: {FirstYear_Revison_Record.Month}" +
                            "Comment:" + Environment.NewLine +
                            FirstYear_Revison_Record.Value + Environment.NewLine;
                        commentsList_ToShow += ToAdd;
                    }
                } 
            }
            if (show_SecondYear)
            {
                var CarryOver = commentList.Where(item => item.Year == actionYear + 1).ToList();

                if (show_Revision)
                {
                    var CarryOver_Revison = CarryOver.Where(item => !string.IsNullOrEmpty(item.Revision)).ToList();
                    foreach (var CarryOver_Revison_Record in CarryOver_Revison)
                    {
                        string ToAdd = Separator + Environment.NewLine +
                            $"Revision: {CarryOver_Revison_Record.Revision}   CarryOver" +
                            "Comment:" + Environment.NewLine +
                            CarryOver_Revison_Record.Value + Environment.NewLine;
                        commentsList_ToShow += ToAdd;
                    }
                }
                if (show_Months)
                {
                    var CarryOver_Revison = CarryOver.Where(item => item.Month != 0).ToList();
                    foreach (var CarryOver_Revison_Record in CarryOver_Revison)
                    {
                        string ToAdd = Separator + Environment.NewLine +
                            $"Month: {CarryOver_Revison_Record.Month}   CarryOver" +
                            "Comment:" + Environment.NewLine +
                            CarryOver_Revison_Record.Value + Environment.NewLine;
                        commentsList_ToShow += ToAdd;
                    }
                }
            }
            CommentList_ToShow = commentsList_ToShow;
        }
    }
}
