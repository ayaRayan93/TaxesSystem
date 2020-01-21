using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Collections.Generic;

namespace MainSystem
{
    public partial class DeliveryPermissionReport : DevExpress.XtraReports.UI.XtraReport
    {
        bool flag = false;
        List<DeliveryPermissionClass> listOfData;
        int rowCount = 0,count=0;
        public DeliveryPermissionReport(List<DeliveryPermissionClass> listOfData, string customerName, string customerPhone, string delegateName, string date,string BranchBillNumber1, string PerNum, string branchId, string branchName,string storeKeeper,string customerdelivery,string store_Name,bool flag,string customerAddress)
        {
            try
            {
                InitializeComponent();
                DataSource = listOfData;
                rowCount = listOfData.Count;
                PermissionNumber.Value = PerNum;
                BranchBillNumber.Value = BranchBillNumber1;
                Client_Name.Value = customerName;
                PhoneNum.Value = customerPhone;
                Bill_Date.Value = date;
                Delegate_Name.Value = delegateName;
                Branch.Value = branchName;
                StoreKeeper.Value = storeKeeper;
                customerDelivery.Value = customerdelivery;
                Store_Name.Value = store_Name;
                CustomerAddress.Value = customerAddress;
                if (!flag)
                {
                    Watermark.Text = "";
                }
                this.flag = flag;
                this.listOfData = listOfData;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void DeliveryPermissionReport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (!flag)
                {
                    //xrTableRow1.Cells["StoreName"].Visible = false;
                    //xrTableRow2.Cells["StoreName"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void xrTable1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (!flag)
                {
                    xrTableRow1.Cells[0].Text = "الكمية المتبقية";
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        
        private void xrTableCell7_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            try
            {
                if (!flag)
                {
                    if (count < rowCount)
                    {
                        XRTableCell cell = sender as XRTableCell;
                        cell.Text = "";// (Convert.ToDouble(listOfData[count].TotalQuantity) - Convert.ToDouble(listOfData[count].DeliveryQuantity)).ToString();
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

    }
}
