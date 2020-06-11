
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;






using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Diagnostics;
using TestApplication1.ViewModel;

using MathLibrary;
using System.Text.RegularExpressions;

namespace TestApplication1
{
    public sealed partial class MainPage : Page
    {
        private DataViewModel DataVMObject { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            this.DataVMObject = new DataViewModel();


            int[] value = { 10, 50, 70};
            int[] weight = { 10, 20, 30 };
            int capacity = 40;
            int itemsCount = 3;

            int result = DataVMObject.KnapSack(capacity, weight, value, itemsCount);
            Debug.WriteLine("Result: " + result);

            DataVMObject.CalculateAns = "Calculated Profit:";
            DataVMObject.ErrorMessage = "*Enter int value";
        }


        private void SubmitData(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!string.IsNullOrWhiteSpace(CapacityTbo.Text) && !string.IsNullOrWhiteSpace(ObjectNumTBL.Text))
                {
                    //Use Regex for validate input value is number
                    Regex IsOnlyNumber = new Regex(@"^\d+$");
                    bool ObjectResult = IsOnlyNumber.IsMatch(CapacityTbo.Text);
                    bool SackResult = IsOnlyNumber.IsMatch(ObjectNumTBL.Text);
                    CalculateButon.IsEnabled = false;
                    DataVMObject.CalculateAns = "Calculated profit: ";
                    if (ObjectResult && SackResult)
                    {
                        DataVMObject.SackCapacity = Convert.ToInt16( CapacityTbo.Text);
                        DataVMObject.NumberOfObject = Convert.ToInt16(ObjectNumTBL.Text);
                        DataVMObject.ErrorMessage = "*Enter int value";

                        DataVMObject.CreateObjectDataList(DataVMObject.NumberOfObject);

                        CalculateButon.IsEnabled = true;
                    }
                    else
                    {
                        DataVMObject.ClearDataList();
                    }
                }
                else
                {
                    DataVMObject.ErrorMessage = "Enter all int value";
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.Message);
            }
        }

        /// <summary>
        /// Calculate button method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateProfit(object sender, RoutedEventArgs e)
        {
            DataVMObject.CalculateProfit();
        }

        /// <summary>
        /// Clear all input data and list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearData(object sender, RoutedEventArgs e)
        {
            CapacityTbo.Text = string.Empty;
            ObjectNumTBL.Text = string.Empty;
            DataVMObject.ClearDataList();
            DataVMObject.CalculateAns = "Calculated profit: ";
            CalculateButon.IsEnabled = false;
        }
    }
}

