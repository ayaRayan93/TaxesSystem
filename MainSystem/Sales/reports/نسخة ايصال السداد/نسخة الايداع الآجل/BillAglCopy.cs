using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MainSystem
{
    public partial class BillAglCopy : DevExpress.XtraReports.UI.XtraReport
    {
        public BillAglCopy()
        {
            InitializeComponent();
        }

        public void InitData(DateTime dateNow, string transitionID, string branchName, string clientName, double paidMoney, string paymentMethod, string bank, string checkNumber, string payday, string visaType, string operationNumber, string description, string bankUserName, int qq200, int qq100, int qq50, int qq20, int qq10, int qq5, int qq1, int qqH, int qqQ, int rr200, int rr100, int rr50, int rr20, int rr10, int rr5, int rr1, int rrH, int rrQ)
        {
            TransitionID.Value = transitionID;
            DateNow.Value = dateNow;
            Branch_Name.Value = branchName;
            Client_Name.Value = clientName;
            PaidMoney.Value = paidMoney;
            Description.Value = description;
            PaymentMethod.Value = paymentMethod;
            Bank.Value = bank;
            CheckNumber.Value = checkNumber;
            //Payday.Value = payday.Date;
            Payday.Value = payday;
            VisaType.Value = visaType;
            OperationNumber.Value = operationNumber;
            BankUserName.Value = bankUserName;
            q200.Value = qq200;
            q100.Value = qq100;
            q50.Value = qq50;
            q20.Value = qq20;
            q10.Value = qq10;
            q5.Value = qq5;
            q1.Value = qq1;
            qH.Value = qqH;
            qQ.Value = qqQ;

            r200.Value = rr200;
            r100.Value = rr100;
            r50.Value = rr50;
            r20.Value = rr20;
            r10.Value = rr10;
            r5.Value = rr5;
            r1.Value = rr1;
            rH.Value = rrH;
            rQ.Value = rrQ;
        }
    }
}
