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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace RealEstateWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {

            ResidentialRentalItems rentalItems = new ResidentialRentalItems(this.ReportTitleTextBox.Text, this.AddressTextBox.Text,
                this.CityTextBox.Text, this.StateTextBox.Text, this.ZipTextBox.Text, this.PropertyTaxTextBox.Text, this.MLSNumberTextBox.Text,
                Convert.ToInt32(this.GrossMonthlyRentTextBox.Text) + Convert.ToInt32(this.OtherMonthlyIncomeTextBox.Text),
                Convert.ToInt32(this.ElectricityTextBox.Text) + Convert.ToInt32(this.WaterSewerTextBox.Text) + Convert.ToInt32(this.PMITextBox.Text) +
                Convert.ToInt32(this.GarbageTextBox.Text) + Convert.ToInt32(this.HOATextBox.Text) + Convert.ToInt32(this.MonthlyInsuranceTextBox.Text) +
                Convert.ToInt32(this.PropertyTaxTextBox.Text)/12 + Convert.ToInt32(this.OtherExpensesTextBox.Text), Convert.ToInt32(this.VacancyTextBox.Text) +
                Convert.ToInt32(this.RepairsMaintenanceTextBox.Text) + Convert.ToInt32(CapitalExpendituresTextBox.Text) + Convert.ToInt32(ManagementFeesTextBox.Text),
                Convert.ToDouble(this.AnnualIncomeGrowthTextBox.Text), Convert.ToDouble(this.AnnualPVGrowthTextBox.Text), Convert.ToDouble(this.AnnualExpensesGrowthTextBox.Text),
                Convert.ToDouble(this.SalesExpensesTextBox.Text),Convert.ToDouble(this.TermTextBox.Text), Convert.ToDouble(this.InterestRateTextBox.Text), 
                Convert.ToDouble(this.PurchasePriceTextBox.Text),Convert.ToDouble(this.DownPaymentTextBox.Text), Convert.ToDouble(this.AfterRepairsTextBox.Text),
                Convert.ToDouble(this.EstimatedRepairsTextBox.Text), Convert.ToDouble(this.ClosingCostTextBox.Text), Convert.ToDouble(this.YearsToOwnTextBox.Text)
                 );

                ResultsWindow mResultsWindow = new ResultsWindow(rentalItems);
                mResultsWindow.Show();
        }

    }
}
