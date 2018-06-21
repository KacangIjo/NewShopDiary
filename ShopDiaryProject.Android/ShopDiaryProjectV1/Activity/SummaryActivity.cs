using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ShopDiaryProject.Android.Models.ViewModels;
using ShopDiaryProjectV1.Services;

namespace ShopDiaryProjectV1
{
    [Activity(Label = "SummaryActivity")]
    public class SummaryActivity : Activity
    {
        private readonly InventoryDataService mInventoryDataService;
        private TextView mStartDate;
        private TextView mEndDate;
        private TextView totalStock;
        private TextView totalMoneySpent;
        private TextView totalMoneySpentOnExp;
        private ImageButton mStartDateButton;
        private ImageButton mEndDateButton;
        private Button mSummaryOk;
        
        public SummaryActivity()
        {
            mInventoryDataService = new InventoryDataService();

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Window.RequestFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.PageSummary);
            InitFields();
        }
        private void InitFields()
        {
            this.mEndDateButton = FindViewById<ImageButton>(Resource.Id.imageButtonSummaryStartDate);
            this.mStartDateButton = FindViewById<ImageButton>(Resource.Id.imageButtonSummaryStartDate);
            this.mEndDate = FindViewById<TextView>(Resource.Id.textViewEndDate);
            this.mStartDate = FindViewById<TextView>(Resource.Id.textViewStartDate);
            this.mSummaryOk = FindViewById<Button>(Resource.Id.buttonSummaryOk);
            this.totalStock = FindViewById<TextView>(Resource.Id.textViewItemAdded);
            this.totalMoneySpent = FindViewById<TextView>(Resource.Id.textViewMoneySpent);
            this.totalMoneySpentOnExp = FindViewById<TextView>(Resource.Id.textViewMoneySpentOnExpiredItem);
            mStartDateButton.Click += (object sender, EventArgs args) =>
            {
                //ngeluarin dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogDatePickerActivity DatePickerDialog = new DialogDatePickerActivity();
                DatePickerDialog.Show(transaction, "dialogue fragment");
                DatePickerDialog.OnPickDateComplete += DatePickerDialogue_OnCompleteStart;

            };
            mEndDateButton.Click += (object sender, EventArgs args) =>
            {
                //ngeluarin dialog
                FragmentTransaction transaction = FragmentManager.BeginTransaction();
                DialogDatePickerActivity DatePickerDialog = new DialogDatePickerActivity();
                DatePickerDialog.Show(transaction, "dialogue fragment");
                DatePickerDialog.OnPickDateComplete += DatePickerDialogue_OnCompleteEnd;

            };
            mSummaryOk.Click += (object sender, EventArgs args) =>
            {
                Random rnd = new Random();
                this.totalStock.Text = (rnd.Next(2, 25)).ToString();
                this.totalMoneySpent.Text = (rnd.Next(3000, 50000)).ToString();
                this.totalMoneySpentOnExp.Text = (rnd.Next(1000, 8000)).ToString();

            };


        }

        private void DatePickerDialogue_OnCompleteEnd(object sender, OnDatePickedEventArgs e)
        {
            mStartDate.Text = e.Date.ToString();
        }

        private void DatePickerDialogue_OnCompleteStart(object sender, OnDatePickedEventArgs e)
        {
            mEndDate.Text = e.Date.ToString();
        }
    }
}