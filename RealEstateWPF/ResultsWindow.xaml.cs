using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace RealEstateWPF
{
    /// <summary>
    /// Interaction logic for ResultsWindow.xaml
    /// </summary>
    public partial class ResultsWindow : Window
    {

        public ChartValues<double> Values1 { get; set; }
        public ChartValues<double> Values2 { get; set; }
        SeriesCollection seriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }
        public ChartValues<double> yAxis { get; set; }
        public ChartValues<double>[] xAxis { get; set; }
        public int nMonths = 12;
        public int nTotal;
        public ResidentialRentalItems mRentalItems { get; set; }

        public ResultsWindow(ResidentialRentalItems rentalItems)
        {
            InitializeComponent();
            mRentalItems = rentalItems;
            this.Title = "Results for: " + rentalItems.mAddress;
            PriceTextBox.Text = rentalItems.mPurchasePrice.ToString();
            ClosingCostTextBox.Text = rentalItems.mClosingCost.ToString();
            RepairsTextBox.Text = rentalItems.mRepairCost.ToString();
            //TotalCostTextBox.Text = rentalItems.total
            InterestRateTextBox.Text = rentalItems.mInterestRate.ToString();
            YearsTextBox.Text = rentalItems.mMortgageYears.ToString() + " years";
            AfterRepairsValueTextBox.Text = rentalItems.mAfterRepairValue.ToString();
            LoanAmountTextBox.Text = rentalItems.mPrincipal.ToString();
            DownPaymentTextBox.Text = rentalItems.mDownPayment.ToString();
            MothlyIncomeTextBox.Text = rentalItems.mMonthlyIncome.ToString();
            MonthlyExpensesTextBox.Text = (rentalItems.mMonthlyExpenses + rentalItems.mMonthlyVariableExpenses).ToString();
            CashFlowTextBlock.Text = rentalItems.mMonthlyCashflow.ToString();
            ProFormaCapTextBox.Text = rentalItems.mProFormaCap.ToString();
            NOITextBox.Text = rentalItems.mNOI.ToString();
            TotalCashNeededTextBox.Text = rentalItems.mTotalCashNeeded.ToString();
            CAPRateTextBox.Text = rentalItems.mPurchaseCapRate.ToString();
            ROITextBox.Text = rentalItems.mCashOnCashRoi.ToString();
            MothlyPITextBox.Text = rentalItems.mMortgagePayment.ToString();

            List<string> chartTypes = new List<string> { "Cash Flow" , "Amoritization" };

            foreach(var item in chartTypes)
                ChartTypeComboBox.Items.Add(item);

        }
        public bool IsDivisible(int a, int b)
        {
            return (a % b) == 0;
        }
        public void ChartAmoritization()
        {
            ChartValues<double> PrincipalValues = new ChartValues<double>();
            ChartValues<double> EquityValues = new ChartValues<double>();
            ChartValues<double> InterestValues = new ChartValues<double>();
            double Interest = (mRentalItems.mInterestRate / 12);
            double Principal = mRentalItems.mPrincipal;
            double interestPayment = Principal * (mRentalItems.mInterestRate / 12);
            double pricipalPayment;
            double equity = mRentalItems.mAfterRepairValue;
            EquityValues.Add(equity - Principal);
            PrincipalValues.Add(Principal);

            for (int i = 1; i < (mRentalItems.mMortgageYears * 12) + 1; i++)
            {
                pricipalPayment = mRentalItems.mMortgagePayment - interestPayment;
                Principal = Math.Round ( Principal - pricipalPayment , 2);

                if (IsDivisible(i, 12))
                {
                    PrincipalValues.Add(Principal);
                    //calc equity
                    equity = equity + (equity * mRentalItems.mAnnualPVGrowth) ;
                    EquityValues.Add(equity - Principal);
                }

                interestPayment = Principal * (mRentalItems.mInterestRate / 12);
                //Interest = Principal * (mRentalItems.mInterestRate / 12);
                //InterestValues.Add(Interest);
            }
            Chart1.AxisY.Add(new Axis
            {
                Title = "Loan Principal"
            });
            Chart1.AxisX.Add(new Axis
            {
                Title = "Years"
            });

            Chart1.Series.Add(new LineSeries
            {
                Values = PrincipalValues,
                Stroke = Brushes.LightPink,
                Fill = new SolidColorBrush
                {
                    Color = System.Windows.Media.Color.FromRgb(255,182, 193),
                    Opacity = 0.7
                },
                Title = "Loan Principal"
            });

            Chart1.Series.Add(new LineSeries
            {
                Values = EquityValues,
                Stroke = Brushes.LightGreen,
                Fill = new SolidColorBrush
                {
                    Color = System.Windows.Media.Color.FromRgb(50, 205, 50),
                    Opacity = 0.7
                },
                Title = "Equity"
            });

            DataContext = this;
        }
        public void ChartCashflow()
        {
            Chart1.AxisY.Add(new Axis
            {
                Title = "$"
            });
            Chart1.AxisX.Add(new Axis
            {
                Labels = new List<string> { "Income", "Expenses"}
            });
            Chart1.Series.Add(new ColumnSeries
            {
                Values = new ChartValues<double> { mRentalItems.mMonthlyIncome , mRentalItems.mMonthlyExpenses + mRentalItems.mMortgagePayment }
            });
            DataContext = this;
        }

        private void ChartTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Chart1.Series.Clear();
            Chart1.AxisX.Clear();
            Chart1.AxisY.Clear();
           if (e.AddedItems[0].ToString() == "Cash Flow")
            {
                ChartCashflow();
            }
            if (e.AddedItems[0].ToString() == "Amoritization")
            {
                ChartAmoritization();
            }
        }
    }
}
