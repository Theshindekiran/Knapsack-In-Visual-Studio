using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestApplication1.Model;

namespace TestApplication1.ViewModel 
{ 
    public class DataViewModel: BaseViewModel
    {

        #region
        public ObservableCollection<DataModel> ListObject;
        public ObservableCollection<DataModel> ObjectList
        {
            get => ListObject;
            set => SetProperty(ref ListObject, value);
        }

        int[] ObjectValueIL;
        int[] ObjectWeightIL;
       
        private int ObjectNumber;
        public int NumberOfObject
        {
            get => ObjectNumber;
            set => SetProperty(ref ObjectNumber, value);
        }

        private int CapacitySack;
        public int SackCapacity
        {
            get => CapacitySack;
            set => SetProperty(ref CapacitySack, value);
        }

        private string Error;
        public string ErrorMessage
        {
            get => Error;
            set => SetProperty(ref Error, value);
        }

        private string calError;
        public string CalculateErrorMessage
        {
            get => calError;
            set => SetProperty(ref calError, value);
        }

        private string Ans;
        public string CalculateAns
        {
            get => Ans;
            set => SetProperty(ref Ans, value);
        }
        #endregion

        public DataViewModel()
        {
            ObjectList = new ObservableCollection<DataModel>();
        }

        /// <summary>
        /// Create object list for insert data
        /// </summary>
        /// <param name="NumberOfObject"></param>
        public void CreateObjectDataList(int NumberOfObject)
        {
            //Clear previous list
            ClearDataList();

            //Create number of object list
            for (int i = 1; i <= NumberOfObject; i++)
            {
                DataModel dataModel = new DataModel();
                dataModel.ObjectNumber = i;
                dataModel.ObjectWeight = "0";
                dataModel.ObjectValue = "0";
                dataModel.ObjectCount = NumberOfObject;
                dataModel.SackCapacity = SackCapacity;
                ObjectList.Add(dataModel);
            }

            //Create list for further calculations
            
            //List of object value 
            ObjectValueIL = new int[NumberOfObject];
            //List of object weight 
            ObjectWeightIL = new int[NumberOfObject];
        }

        /// <summary>
        /// Calculate Profite method
        /// </summary>
        public void CalculateProfit()
        {
            Regex IsNumber = new Regex(@"^\d+$");
            bool ValidationResult = IsNumber.IsMatch("1245");
            foreach (DataModel dataModel in ObjectList)
            {
                if(!IsNumber.IsMatch(dataModel.ObjectValue) || !IsNumber.IsMatch(dataModel.ObjectWeight))
                {
                    CalculateErrorMessage = "Enter all valid values";
                    CalculateAns = null;
                    return;
                }
                ObjectValueIL[dataModel.ObjectNumber - 1] = Convert.ToInt16(dataModel.ObjectValue);
                ObjectWeightIL[dataModel.ObjectNumber - 1] = Convert.ToInt16(dataModel.ObjectWeight);
            }
            int CalculatedProfit = KnapSack(SackCapacity, ObjectWeightIL, ObjectValueIL, ObjectList.Count);

            SelectedObejct(ObjectList, SackCapacity);

            CalculateAns = "Calculated profit: " + CalculatedProfit;
            CalculateErrorMessage = null;
        }

        /// <summary>
        /// Clear object list
        /// </summary>
        public void ClearDataList()
        {
            ObjectList.Clear();
        }

        /// <summary>
        /// Calculate profit using Knapsack-Greedy algorithm                           
        /// </summary>                                                                  
        /// <param name="SackCapacity"></param>
        /// <param name="ObjectWeight"></param>
        /// <param name="ObjectValue"></param>
        /// <param name="ObjectCount"></param>
        /// <returns></returns>
        public int KnapSack(int SackCapacity, int[] ObjectWeight, int[] ObjectValue, int ObjectCount)
        {
            int[,] K = new int[ObjectCount + 1, SackCapacity + 1];

            for (int i = 0; i <= ObjectCount; ++i)
            {
                for (int w = 0; w <= SackCapacity; ++w)
                {
                    if (i == 0 || w == 0)
                        K[i, w] = 0;
                    else if (ObjectWeight[i - 1] <= w)
                        K[i, w] = Math.Max(ObjectValue[i - 1] + K[i - 1, w - ObjectWeight[i - 1]], K[i - 1, w]);
                    else
                        K[i, w] = K[i - 1, w];
                }
            }
            return K[ObjectCount, SackCapacity];
        }

        private void SelectedObejct(ObservableCollection<DataModel> ObjectList, int SackCapacity)
        {
            List<DataModel> MaxWeightObject = ObjectList.Where(x => Convert.ToInt16(x.ObjectWeight) <= SackCapacity).ToList<DataModel>();
            

        }

    }
}
