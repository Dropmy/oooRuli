using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oooRuli
{
    public partial class ShoppingCartForm : Form
    {
        private string _priceFormat;

        public ShoppingCartForm()
        {
            InitializeComponent();

            _priceFormat = priceLabel.Text;

            UpdatePrice();
            ShoppingCart.Update += UpdatePrice;
            ShoppingCart.Update += FillData;
          
            FillData();

            
                
        }
        private void FillData()
        {
            using (var dbContext = new oooWhellEntities1())
            {
                var table = new List<CartProductCell>();

                foreach (var pair in ShoppingCart.Content)
                {
                    var product = dbContext.Products.FirstOrDefault(p => p.Id == pair.Key);

                    table.Add(new CartProductCell()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Manufacturer = dbContext.Manufacturers.FirstOrDefault(m => m.Id == product.ManufacturerId).Name,
                        Image = ImagesController.GetImageByPath(product.ImagePath)
                    });
                }


                dataGridView1.DataSource = table;
                var imageColumn = (DataGridViewImageColumn)dataGridView1.Columns["Image"];
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;

                comboBox1.Items.Clear();

                foreach (var point in dbContext.Points)
                {
                    comboBox1.Items.Add(point.Address);
                }


            }
        }

        protected override void OnClosed(EventArgs e)
        {
            ShoppingCart.Update -= UpdatePrice;
            ShoppingCart.Update -= FillData;
        }

        private void UpdatePrice()
        {
            using (var dbContext = new oooWhellEntities1())
            {
                foreach (var pair in ShoppingCart.Content)
                {
                    var product = dbContext.Products.FirstOrDefault(p => p.Id == pair.Key);
                    var price = product.Price * pair.Value;
                priceLabel.Text = string.Format(_priceFormat, price);
                }
            }
                
            
        }

        private void buyButton_Click(object sender, EventArgs e)
        {
            using (var dbContext = new oooWhellEntities1())
            {

                var point = dbContext.Points.FirstOrDefault(p => p.Address == comboBox1.Text);



                foreach(var pair in ShoppingCart.Content)
                {
                   
                    var order = dbContext.Orders.Add(new Order()
                    {

                        ProductId = pair.Key,
                        Count = pair.Value,


                    });

                    dbContext.Tickets.Add(new Ticket()
                    {
                        OrderId = order.Id,
                        PointId = point.Id,
                        UserId = 1,
                    });
                }
                dbContext.SaveChanges();
            }

            ShoppingCart.Clear();

            MessageBox.Show("Товар куплен");
        }
    }
}
