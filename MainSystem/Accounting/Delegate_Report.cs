using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaxesSystem
{
    public partial class Delegate_Report : Form
    {
        public GridControl gridControl;
        dataX d;
        string title= "تقرير مبيعات المناديب";
        string branchName = "";
        int flag = 0;
        public Delegate_Report(GridControl gridControl, dataX d)
        {
            try
            {
                InitializeComponent();
                this.gridControl = gridControl;
                this.d = d;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public Delegate_Report(GridControl gridControl, string branchName, dataX d)
        {
            try
            {
                InitializeComponent();
                this.gridControl = gridControl;
                this.d = d;
                this.branchName = branchName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public Delegate_Report(GridControl gridControl, dataX d,string title)
        {
            try
            {
                InitializeComponent();
                this.gridControl = gridControl;
                this.d = d;
                this.title = title;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public Delegate_Report(GridControl gridControl, dataX d, string title,int flag)
        {
            try
            {
                InitializeComponent();
                this.gridControl = gridControl;
                this.d = d;
                this.title = title;
                this.flag = flag;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Product_Report_Load(object sender, EventArgs e)
        {
            try
            {
                PrintableComponentLink printableComponentLink = new PrintableComponentLink();
         
                printableComponentLink.Component = gridControl;

                if(flag==1)
                    printableComponentLink.RightToLeftLayout = true;
                else
                    printableComponentLink.RightToLeftLayout = false;

                printableComponentLink.Landscape = false;
                printableComponentLink.Margins = new System.Drawing.Printing.Margins(20, 20, 20, 20);
                printingSystem1.Links.Add(printableComponentLink);
                printableComponentLink.CreateReportHeaderArea += PrintableComponentLink_CreateReportHeaderArea;
                printableComponentLink.CreateMarginalFooterArea += PrintableComponentLink_CreateMarginalFooterArea;
                printableComponentLink.CreateDocument();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PrintableComponentLink_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            // Declare bricks.
            PageInfoBrick pageInfoBrick;

            // Specify required settings for the brick graphics.
            BrickGraphics brickGraphics = e.Graph;
            brickGraphics.BackColor = Color.White;
            brickGraphics.Font = new Font("Neo Sans Arabic", 10);

            // Set the rectangle for a page info brick. 
            RectangleF r = RectangleF.Empty;
            r.Height = 10;

            // Display the PageInfoBrick containing the page number among total pages. The page number
            // is displayed in the right part of the MarginalHeader section.
            pageInfoBrick = brickGraphics.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", Color.Black, r, BorderSide.None);
            pageInfoBrick.Alignment = BrickAlignment.Far;
        }

        private void PrintableComponentLink_CreateReportHeaderArea(object sender, DevExpress.XtraPrinting.CreateAreaEventArgs e)
        {
            // Specify required settings for the brick graphics.
            BrickGraphics brickGraphics = e.Graph;
            Height = 100;
            brickGraphics.BackColor = Color.White;
            brickGraphics.Font = new Font("Neo Sans Arabic", 10);
            
            // Declare bricks.
            PageInfoBrick pageInfoBrickFrom;
            PageInfoBrick pageInfoBrickTo;
            PageInfoBrick pageInfoBrick;
            PageInfoBrick pageInfo;
            PageInfoBrick pageInfo1;
            PageImageBrick pageImageBrick;

            // Define the image to display.
            Image pageImage = TaxesSystem.Properties.Resources.logo_option2;

            // Display the PageImageBrick containing the DevExpress logo.
            pageImageBrick = brickGraphics.DrawPageImage(pageImage, new Rectangle(10, 0, 80, 100), BorderSide.None, Color.Transparent);
            pageImageBrick.Alignment = BrickAlignment.Far;

            // Display the PageInfoBrick containing date-time information. Date-time information is displayed
            // in the left part of the MarginalHeader section using the FullDateTimePattern.
            //{0:F}
            pageInfoBrickFrom = brickGraphics.DrawPageInfo(PageInfo.None, ":من" , Color.Black, new Rectangle(180, 110, 60, 80), BorderSide.None);
            pageInfoBrickFrom.Font = new Font("Tahoma", 10, FontStyle.Bold);
            pageInfoBrickFrom = brickGraphics.DrawPageInfo(PageInfo.None, d.dateFrom, Color.Black, new Rectangle(40, 110, 140, 80), BorderSide.None);

            pageInfoBrickFrom.Alignment = BrickAlignment.Near;
            pageInfoBrickFrom.Font = new Font("Tahoma", 10, FontStyle.Bold);
            pageInfoBrickTo = brickGraphics.DrawPageInfo(PageInfo.None, ":الي", Color.Black, new Rectangle(180, 130, 60, 80), BorderSide.None);
            pageInfoBrickTo.Font = new Font("Tahoma", 10, FontStyle.Bold);
            pageInfoBrickTo = brickGraphics.DrawPageInfo(PageInfo.None,  d.dateTo, Color.Black, new Rectangle(40, 130, 140, 80), BorderSide.None);

            pageInfoBrickTo.Alignment = BrickAlignment.Near;
            pageInfoBrickTo.Font = new Font("Tahoma", 10, FontStyle.Bold);
            BrickGraphics brickGraphics1 = e.Graph;
            pageInfo = brickGraphics1.DrawPageInfo(PageInfo.None, d.delegateName, Color.Black, new Rectangle(525, 110, 180, 70), BorderSide.None);
            pageInfo.Alignment = BrickAlignment.Near;
            pageInfo.Font = new Font("Tahoma", 10, FontStyle.Bold);
            BrickGraphics brickGraphics3 = e.Graph;
            pageInfo1 = brickGraphics3.DrawPageInfo(PageInfo.None, d.company, Color.Black, new Rectangle(528, 130, 180, 70), BorderSide.None);
            pageInfo1.Alignment = BrickAlignment.Near;
            pageInfo1.Font = new Font("Tahoma", 10, FontStyle.Bold);

            if (d.company_profit_list != null)
            {
                pageInfoBrickTo = brickGraphics.DrawPageInfo(PageInfo.None, "الشركة", Color.Black, new Rectangle(525, 110, 100, 80), BorderSide.None);
                pageInfoBrickTo.Font = new Font("Tahoma", 10, FontStyle.Bold);
                pageInfoBrickTo = brickGraphics.DrawPageInfo(PageInfo.None,"قيمة ربح المندوب", Color.Black, new Rectangle(528, 130, 150, 80), BorderSide.None);

                pageInfoBrickTo.Alignment = BrickAlignment.Far;
                pageInfoBrickTo.Font = new Font("Tahoma", 10, FontStyle.Bold);
                int y = 130;
                PageInfoBrick pageInfoBrick3;
                foreach (company_profit item in d.company_profit_list)
                {
                    y += 20;
                    pageInfoBrick3 = brickGraphics.DrawPageInfo(PageInfo.None, item.companyName, Color.Black, new Rectangle(725, y, 140, 80), BorderSide.None);
                    pageInfoBrick3.Font = new Font("Tahoma", 10, FontStyle.Regular);
                    pageInfoBrick3 = brickGraphics.DrawPageInfo(PageInfo.None, item.delegateProfit, Color.Black, new Rectangle(528, y, 140, 80), BorderSide.None);

                    pageInfoBrick3.Alignment = BrickAlignment.Far;
                    pageInfoBrick3.Font = new Font("Tahoma", 10, FontStyle.Regular);
                }
                pageInfoBrick3 = brickGraphics.DrawPageInfo(PageInfo.None, d.delegateProfit+":اجمالي ربح المندوب ", Color.Black, new Rectangle(685, y+40, 230, 80), BorderSide.None);

                pageInfoBrick3.Alignment = BrickAlignment.Far;
                pageInfoBrick3.Font = new Font("Tahoma", 11, FontStyle.Bold);
            }

            // Declare text strings.
            string devexpress = title;
            // Specify required settings for the brick graphics.
            BrickGraphics brickGraphics2 = e.Graph;
            brickGraphics2.BackColor = Color.White;
            brickGraphics2.Font = new Font("Times New Roman", 14, FontStyle.Bold);

            // Display the DevExpress text string.
            SizeF size = brickGraphics2.MeasureString(devexpress);
            pageInfoBrick = brickGraphics2.DrawPageInfo(PageInfo.None, devexpress, Color.Black, new RectangleF(new PointF(320, 50), size), BorderSide.None);
            pageInfoBrick.Alignment = BrickAlignment.Center;

            if (branchName != "")
            {
                SizeF sizea = brickGraphics2.MeasureString(branchName);
                pageInfoBrick = brickGraphics2.DrawPageInfo(PageInfo.None, branchName, Color.Black, new RectangleF(new PointF(370, 70), size), BorderSide.None);
                pageInfoBrick.Alignment = BrickAlignment.Center;
            }
            e.Graph.PrintingSystem.Document.PrintingSystem.PageMargins.Bottom = 0;
        }

    }
}
