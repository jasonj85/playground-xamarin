﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;

namespace IncomePlanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        // edit texts
        EditText incomePerHourEditText;
        EditText workHourPerDayEditText;
        EditText taxRateEditText;
        EditText savingsRateEditText;

        // text views
        TextView workSummaryTextView;
        TextView grossIncomeTextView;
        TextView taxPayableTextView;
        TextView annualSavingsTextView;
        TextView spendableIncomeTextView;

        // other
        Button calculateButton;
        RelativeLayout resultLayout;

        bool calculationComplete = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            ConnectViews();
        }

        void ConnectViews()
        {
            incomePerHourEditText = FindViewById<EditText>(Resource.Id.incomePerHourEditText);
            workHourPerDayEditText = FindViewById<EditText>(Resource.Id.workHoursEditText);
            taxRateEditText = FindViewById<EditText>(Resource.Id.taxRateEditText);
            savingsRateEditText = FindViewById<EditText>(Resource.Id.savingsRateEditText);

            workSummaryTextView = FindViewById<TextView>(Resource.Id.workSummaryValue);
            grossIncomeTextView = FindViewById<TextView>(Resource.Id.grossIncomeValue);
            taxPayableTextView = FindViewById<TextView>(Resource.Id.annualTaxValue);
            annualSavingsTextView = FindViewById<TextView>(Resource.Id.annualSavingsValue);
            spendableIncomeTextView = FindViewById<TextView>(Resource.Id.spendableIncomeValue);

            calculateButton = FindViewById<Button>(Resource.Id.calculateButton);
            resultLayout = FindViewById<RelativeLayout>(Resource.Id.resultLayout);

            calculateButton.Click += CalculateButton_Click;
        }

        private void CalculateButton_Click(object sender, System.EventArgs e)
        {
            if (calculationComplete)
            {
                calculationComplete = false;
                ClearInputs();
                calculateButton.Text = "Calculate";
            }
            else
            {
                // take inputs
                double incomePerHour = double.Parse(incomePerHourEditText.Text);
                double workHourPerDay = double.Parse(workHourPerDayEditText.Text);
                double taxRate = double.Parse(taxRateEditText.Text);
                double savingsRate = double.Parse(savingsRateEditText.Text);

                // assuming they have 4 weeks off a year 
                double annualWorkHourSummary = workHourPerDay * 5 * 48;
                double annualIncome = incomePerHour * annualWorkHourSummary;
                double taxPayable = (taxRate / 100) * annualIncome;
                double annualSavings = (savingsRate / 100) * annualIncome;
                double spendableIncome = annualIncome - (annualSavings + taxPayable);

                // display results 
                grossIncomeTextView.Text = $"£ {annualIncome.ToString("#,##")}";
                workSummaryTextView.Text = $"{annualWorkHourSummary.ToString("#,##")} hrs";
                taxPayableTextView.Text = $"£ {taxPayable.ToString("#,##")}";
                annualSavingsTextView.Text = $"£ {annualSavings.ToString("#,##")}";
                spendableIncomeTextView.Text = $"£ {spendableIncome.ToString("#,##")}";

                resultLayout.Visibility = Android.Views.ViewStates.Visible;
                calculationComplete = true;

                calculateButton.Text = "Clear";
            }
        }

        void ClearInputs()
        {
            incomePerHourEditText.Text = "";
            workHourPerDayEditText.Text = "";
            taxRateEditText.Text = "";
            savingsRateEditText.Text = "";
        }
    }
}