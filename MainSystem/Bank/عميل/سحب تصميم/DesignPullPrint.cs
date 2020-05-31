using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace MainSystem
{
    public partial class DesignPullPrint : DevExpress.XtraReports.UI.XtraReport
    {
        public DesignPullPrint()
        {
            InitializeComponent();
        }

        public void InitData(DateTime dateNow, int customerDesignID, string transitionID, string branchName, string clientName, double paidMoney, string paymentMethod, string bank, string checkNumber, string payday, string description, string bankUserName, string DelegateName, string bathroomNum, string bathroomPrice, string bathroomTotal, string kitchenNum, string kitchenPrice, string kitchenTotal, string hallNum, string hallPrice, string hallTotal, string roomNum, string roomPrice, string roomTotal, string otherNum, string otherPrice, string otherTotal)
        {
            DesignNum.Value = customerDesignID;
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
            BankUserName.Value = bankUserName;
            Delegate_Name.Value = DelegateName;
            EngDesign.Value = "";
            
            BathroomNum.Value = bathroomNum;
            BathroomPrice.Value = bathroomPrice;
            BathroomTotal.Value = bathroomTotal;
         
            KitchenNum.Value = kitchenNum;
            KitchenPrice.Value = kitchenPrice;
            KitchenTotal.Value = kitchenTotal;
          
            HallNum.Value = hallNum;
            HallPrice.Value = hallPrice;
            HallTotal.Value = hallTotal;
          
            RoomNum.Value = roomNum;
            RoomPrice.Value = roomPrice;
            RoomTotal.Value = roomTotal;
        
            OtherNum.Value = otherNum;
            OtherPrice.Value = otherPrice;
            OtherTotal.Value = otherTotal;
        }
    }
}
