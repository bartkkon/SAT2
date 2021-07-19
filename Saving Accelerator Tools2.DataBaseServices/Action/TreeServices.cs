using Saving_Accelerator_Tools2.DataBaseIServices.ActionService;
using Saving_Accelerator_Tools2.DataBaseServices.Data;
using Saving_Accelerator_Tools2.Model.ActionCriteria;
using SavingAcceleratorTools2.ProjectModels.Action;
using System.Collections.ObjectModel;

namespace Saving_Accelerator_Tools2.DataBaseServices.Action
{
    public class TreeServices : ITreeServices
    {
        private readonly ConnectionContext connection;

        public TreeServices(ConnectionContext connection)
        {
            this.connection = connection;
        }
        public ObservableCollection<ActionTree> Load(TreeCriteria criteria)
        {
            ObservableCollection<ActionTree> NewTree = new();

            ActionTree Actual = new()
            {
                ActionID = 0,
                ActionName = "New Action",
                Bold = true,
            };
            ActionTree Carry = new()
            {
                ActionID = 0,
                ActionName = "Carry Over",
                Bold = true,
            };
            NewTree.Add(Actual);
            NewTree.Add(Carry);

            ActionTree Action = new()
            {
                ActionID = 1,
                ActionName = "Actual Akcja pierwsza",
            };
            Actual.Tree.Add(Action);
            ActionTree Action2 = new()
            {
                ActionID = 2,
                ActionName = "Ta statruje",
                ActualStart = true,
            };
            Actual.Tree.Add(Action2);

            ActionTree Action3 = new()
            {
                ActionID = 3,
                ActionName = "Ta jest Carry",
            };
            Carry.Tree.Add(Action3);

            ActionTree Action4 = new()
            {
                ActionID = 4,
                ActionName = "Ta już skończyła",
                Finish = true,
            };
            Carry.Tree.Add(Action4);


            return NewTree;
        }
    }
}
