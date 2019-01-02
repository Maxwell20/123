using System;

namespace RealEstateWPF
{
    public class ResidentialRentalItems
    {
        //inputs
        public string mTitle;
        public string mAddress;
        public string mCity;
        public string mSate;
        public int mZip;
        public double mProperyTax;
        public int mMLSNo;
        public double mMonthlyIncome;
        public double mMonthlyExpenses;
        public double mMonthlyVariableExpenses;//%
        public double mAnnualIncomeGrowth;//%
        public double mAnnualPVGrowth;//%
        public double mAnnualExpensesGrowth;//%
        public double mSalesExpenses;//%
        public double mInterestRate;
        public double mMortgageYears;
        public double mPurchasePrice;
        public double mDownPayment;
        public double mAfterRepairValue;
        public double mRepairCost;
        public double mClosingCost;
        public double mYearsOwned;
        //results
        public double mMonthlyCashflow;
        public double mProFormaCap;
        public double mNOI;
        public double mTotalCashNeeded;
        public double mCashOnCashRoi;
        public double mPurchaseCapRate;
        public double mMortgagePayment;
        public double mPrincipal;
        public double mTotalCost;


        ResidentialRentalItems()
        {

        }
         public ResidentialRentalItems(string title,
         string address,
         string city,
         string state,
         string zip,
         string properyTax,
         string MLSNo,
         double monthlyIncome,
         double monthlyFixedExpenses,
         double monthlyVariableExpenses,
         double annualIncomeGrowth,
         double annualPVGrowth,
         double annualExpensesGrowth,
         double salesExpenses,
         double mortgageYears,
         double mortgageInterestRate,
         double purchasePrice,
         double downPayment,
         double afterRepairValue,
         double repairCost,
         double closingCost,
         double yearsToOwn)
        {          
            mTitle = title;
            mCity = city;
            mAddress = address;
            mSate = state;
            mZip = Convert.ToInt32(zip);
            mProperyTax = Convert.ToDouble(properyTax);
            mMLSNo = Convert.ToInt32(MLSNo);
            mMonthlyIncome = Convert.ToDouble(monthlyIncome);
            mMonthlyExpenses = monthlyFixedExpenses;
            mAnnualIncomeGrowth = (annualIncomeGrowth / 100);
            mAnnualPVGrowth = (annualPVGrowth / 100);
            mAnnualExpensesGrowth = (annualExpensesGrowth / 100);
            mSalesExpenses = (salesExpenses / 100);
            mMonthlyVariableExpenses = (monthlyVariableExpenses / 100) * mMonthlyIncome;
            mMortgageYears = mortgageYears;
            mInterestRate = mortgageInterestRate / 100;
            mDownPayment = downPayment;
            mPurchasePrice = purchasePrice;
            mPrincipal = purchasePrice - downPayment;
            mAfterRepairValue = afterRepairValue;
            mRepairCost = repairCost;
            mClosingCost = closingCost;
            mYearsOwned = yearsToOwn;
            mTotalCashNeeded = closingCost + downPayment + repairCost;
            CalcNOI();
            CalcMortgagePayment();
            CalcCashflow();
            CalcROI();


            //live chart





        }
        //M = P[i(1 + i)^n] / [(1 + i)^n – 1]
        //M = monthly mortgage payment
        //P = the principal amount
        //i = your monthly interest rate.Your lender likely lists interest rates as an annual figure, so you’ll need to divide by 12, for each month of the year.So, if your rate is 5 %, then the monthly rate will look like this: 0.05 / 12 = 0.004167.
        //n = the number of payments over the life of the loan.If you take out a 30 - year fixed rate mortgage, this means: n = 30 years x 12 months per year, or 360 payments.
        void CalcMortgagePayment()
        {
            double n = mMortgageYears * 12;
            double i = mInterestRate / 12;
            double I = Math.Pow((1 + i), n);
            mMortgagePayment = Math.Round(mPrincipal * ((i * I)/(I - 1)), 2);
        }
        void CalcCashflow() 
        {           
            mMonthlyCashflow = Math.Round((mMonthlyIncome - mMonthlyVariableExpenses - mMonthlyExpenses - mMortgagePayment ) , 2 );
        }
        void CalcNOI()
        {
            mNOI = (mMonthlyIncome - (mMonthlyVariableExpenses) - mMonthlyExpenses - mMortgagePayment) * 12;
        }
        void CalcROI()
        {
            double totalCashInvestment = mClosingCost + mDownPayment + mRepairCost;
            mCashOnCashRoi =Math.Round (mNOI / totalCashInvestment , 2);
        }
        void CalcCapRate()
        {
            mPurchaseCapRate = (mMonthlyCashflow * 12) / (mNOI);
        }


    }
}
