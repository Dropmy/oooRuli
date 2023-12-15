using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace oooRuli
{
    public partial class Form1 : Form
    {
        private int? _contextId;

        public Form1()
        {
            InitializeComponent();

            ShoppingCart.Update += UpdateBuyButtonEnabled;
            UpdateBuyButtonEnabled();

            dataGridView1.RowTemplate.Height = 70;
        }

        protected override void OnClosed(EventArgs e)
        {
            ShoppingCart.Update -= UpdateBuyButtonEnabled;
        }


        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            using (var dbContext = new oooWhellEntities1())
            {
                if (dbContext.Products.Any() == false)
                {
                    ErrorMessager.ShowError("Товаров нет на складе");
                }

                List<ProductCell> table = new List<ProductCell>();

                foreach(var product in dbContext.Products) 
                {
                    table.Add(new ProductCell()
                    {
                        Id= product.Id,
                        Name= product.Name,
                        Description= product.Description,
                        Price= product.Price,
                        Manufacturer = dbContext.Manufacturers.FirstOrDefault(m => m.Id== product.ManufacturerId).Name,
                        Image = ImagesController.GetImageByPath(product.ImagePath)
                    });
                }
                dataGridView1.DataSource = table;

               var imageColumn = (DataGridViewImageColumn)dataGridView1.Columns["Image"];
                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;


            }
        }

        private void AddProductMenuItem_Click(object sender, EventArgs e)
        {
            if(_contextId is null)
             return;

            ShoppingCart.AddOne(_contextId.Value);
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            _contextId= null;

            if (e.Button != MouseButtons.Right)
                return;

            var rowIndex = dataGridView1.HitTest(e.X, e.Y).RowIndex;
            var row = dataGridView1.Rows[rowIndex];

            productContextMenu.Show(MousePosition.X, MousePosition.Y);

            _contextId = (int)row.Cells["Id"].Value;
            
        }

        private void shoppingCartButton_Click(object sender, EventArgs e)
        {
            var form = new ShoppingCartForm();
            form.Show();
        }

        private void UpdateBuyButtonEnabled()
        {
            shoppingCartButton.Enabled = ShoppingCart.Content.Any();
        }

    }
}
