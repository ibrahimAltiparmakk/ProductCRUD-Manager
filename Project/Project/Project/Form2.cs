using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace yazgem
{
    public partial class Form2 : Form
    {
        private Product _objProduct;
        public Form2(Product objProducts)
        {
            InitializeComponent();
            _objProduct = objProducts;
            DisplayProductInfo();
        }

        public Form2() {
            InitializeComponent();
        }

        private void DisplayProductInfo()
        {
            if (_objProduct != null)
            {
                
                tbName.Text = _objProduct.Name;
                tbDescription.Text = _objProduct.Description;
                tbPrice.Text = _objProduct.Price.ToString();
                tbPrice2.Text = _objProduct.Price2.ToString();
            }//if
            else
            {
                
                
                tbName.Clear();
                tbDescription.Clear();
                tbPrice.Clear();
                tbPrice2.Clear();
            }//else
        }//void

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public Product ProductData { get { return _objProduct; } }
        private void btnOk_Click(object sender, EventArgs e)
        {


            if (_objProduct == null)
                _objProduct = new Product();

            
            _objProduct.Name = tbName.Text;
            _objProduct.Description = tbDescription.Text;
            _objProduct.Price = tbPrice.Text;
            _objProduct.Price2 = tbPrice2.Text;

            

            this.DialogResult = DialogResult.OK;
            this.Close();

        }//void


        

       
    }
}
