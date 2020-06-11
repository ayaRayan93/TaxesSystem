using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class Print_ShippingWareHouse : DevExpress.XtraReports.UI.XtraReport
    {
        public Print_ShippingWareHouse()
        {
            InitializeComponent();
        }

        public void InitData(int shippingID, int BillNum, string branchName, string Customer, string Client, string ReceivedClient, string area, string address, string Phone, string Delegate, DateTime dateBill, DateTime dateReceived, string quantity, string cartons, string Bank, double money)
        {
            Shipping_ID.Value = shippingID;
            Bill_Number.Value = BillNum;
            Client_Name.Value = Client;
            Customer_Name.Value = Customer;
            Recipient_Name.Value = ReceivedClient ;
            PhoneNum.Value = Phone;
            Address.Value = address;
            Area.Value = area;
            Delegate_Name.Value = Delegate;
            Branch_Name.Value = branchName;
            Bill_Date.Value = dateBill.Date;
            ReceivedDate.Value = dateReceived;
            Cartons.Value = cartons;
            Quantity.Value = quantity;
            BankName.Value = Bank;
            Money.Value = money;
            DateNow.Value = DateTime.Now;
        }
    }
}
