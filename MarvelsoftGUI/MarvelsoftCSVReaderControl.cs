using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using CsvHelper;
using System.IO;
using System.Threading;
using MarvelsoftGUI.models;

namespace MarvelsoftGUI
{
    public partial class MarvelsoftCSVReaderControl : UserControl
    {
        private readonly List<ListViewItem> LoadedProducts = new List<ListViewItem>();

        private int ItemsPerPage = 200;
        private int CurrentPage = 0;
        private int TotalPages = 0;
        private int TotalRecords = 0;

        public MarvelsoftCSVReaderControl()
        {
            InitializeComponent();
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            BrowseFile();
        }

        /// <summary>
        /// Handler method for File Browser. After file selections, clears the LoadedProducts and ListView control, then it proceeds by
        /// starting a spearate thread to process the CSV file and fill it up in LoadedProducts and ListView control, respectivelly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CsvFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            AfterFileOpen();
            StartCSVLoadThread();
        }
        private void MarvelsoftCSVReaderControl_Load(object sender, EventArgs e)
        {
            InitializeListView();
        }

        /// <summary>
        /// Event handler for DropDown control when it's index has been changed.
        /// 
        /// When invoked, it will also update the ListView with selected amount of records to show.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropDownPerPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemsPerPage = int.Parse(DropDownPerPage.SelectedItem.ToString());

            UpdateTotalPages();

            int fromIndex = (CurrentPage - 1) * ItemsPerPage;

            UpdateListView(fromIndex);
        }

        private void UpdateTotalPages()
        {
            decimal chunks = (decimal)TotalRecords / (decimal)ItemsPerPage;
            TotalPages = (int)Math.Round(chunks, 0);
            LblTotalPages.Text = TotalPages.ToString();
        }

        /// <summary>
        /// Pagination handler which returns back to the first page and updates the ListView with appropriate data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 1;
            int fromIndex = (CurrentPage - 1) * ItemsPerPage;

            BtnPrev.Enabled = false;
            BtnFirst.Enabled = false;

            BtnNext.Enabled = true;
            BtnLast.Enabled = true;

            LblCurrentPage.Text = CurrentPage.ToString();

            UpdateListView(fromIndex);
        }

        /// <summary>
        /// Pagination handler to move back one position and updates the ListView with appropriate data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrev_Click(object sender, EventArgs e)
        {
            CurrentPage--;
            int fromIndex = (CurrentPage - 1) * ItemsPerPage;

            if (CurrentPage > 1)
            {
                BtnNext.Enabled = true;
                BtnLast.Enabled = true;
            }

            if (CurrentPage == 1)
            {
                BtnFirst.Enabled = false;
                BtnPrev.Enabled = false;
            }

            LblCurrentPage.Text = CurrentPage.ToString();

            UpdateListView(fromIndex);
        }

        /// <summary>
        /// Pagination handler to move forward one position and updates the ListView with appropriate data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnNext_Click(object sender, EventArgs e)
        {
            CurrentPage++;
            int fromIndex = (CurrentPage - 1) * ItemsPerPage;

            int lastExpectedBound = fromIndex + ItemsPerPage;
            int perPage = ItemsPerPage;

            if (LoadedProducts.Count < lastExpectedBound)
            {
                perPage = TotalRecords - fromIndex;
            }

            if (CurrentPage > 1)
            {
                BtnFirst.Enabled = true;
                BtnPrev.Enabled = true;
            }

            if (CurrentPage == TotalPages)
            {
                BtnNext.Enabled = false;
                BtnLast.Enabled = false;
            }

            LblCurrentPage.Text = CurrentPage.ToString();

            UpdateListView(fromIndex, perPage);
        }

        /// <summary>
        /// Pagination handler which moves forward to the last page and updates the ListView with appropriate data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLast_Click(object sender, EventArgs e)
        {
            CurrentPage = Convert.ToInt32(TotalPages);
            int fromIndex = (CurrentPage - 1) * ItemsPerPage;

            int lastExpectedBound = fromIndex + ItemsPerPage;
            int perPage = ItemsPerPage;

            if (LoadedProducts.Count < lastExpectedBound)
            {
                perPage = TotalRecords - fromIndex;
            }

            BtnNext.Enabled = false;
            BtnLast.Enabled = false;

            BtnPrev.Enabled = true;
            BtnFirst.Enabled = true;

            LblCurrentPage.Text = CurrentPage.ToString();

            UpdateListView(fromIndex, perPage);
        }

        /// <summary>
        /// Initializes the ListView with columns and other fancy options.
        /// </summary>
        private void InitializeListView()
        {
            ProductListView.Visible = false;
            ProductListView.View = View.Details;
            ProductListView.AllowColumnReorder = true;
            ProductListView.GridLines = true;
            ProductListView.FullRowSelect = true;
            ProductListView.AllowColumnReorder = true;

            ProductListView.Columns.Add("#", 50, HorizontalAlignment.Left);
            ProductListView.Columns.Add("Filename", 100, HorizontalAlignment.Left);
            ProductListView.Columns.Add("Id", 100, HorizontalAlignment.Left);
            ProductListView.Columns.Add("Quantity", 70, HorizontalAlignment.Right);
            ProductListView.Columns.Add("Price", 70, HorizontalAlignment.Right);
        }

        /// <summary>
        /// Wrapper for opening the File Browser. It's set to public to be accessible from parent Forms.
        /// </summary>
        public void BrowseFile()
        {
            CsvFileDialog.ShowDialog();
        }

        /// <summary>
        /// Sets some labels and visibility, clears data containers.
        /// </summary>
        private void AfterFileOpen()
        {
            TxtFilename.Text = CsvFileDialog.FileName;

            LoadedProducts.Clear();
            ProductListView.Items.Clear();
            TotalRecords = 0;
            TotalPages = 0;
            CurrentPage = 0;

            ProductListView.Visible = false;
            LblProcess.Visible = true;
        }

        /// <summary>
        /// Clears the ListView and locks any visible changes in the ListView, until EndUpdate is called.
        /// 
        /// It creates a range fromIndex with count of ItemsPerPage to show appropriate number of items in the list view and at a specific positions.
        /// </summary>
        /// <param name="fromIndex"></param>
        private void UpdateListView(int fromIndex, int perPage = -1)
        {
            ProductListView.Items.Clear();

            ProductListView.BeginUpdate();

            int perPageContext = (perPage == -1) ? ItemsPerPage : perPage;

            if(perPageContext > TotalRecords)
            {
                perPageContext = TotalRecords;
            }

            ListViewItem[] range = LoadedProducts.GetRange(fromIndex, perPageContext).ToArray();
            ProductListView.Items.AddRange(range);

            ProductListView.EndUpdate();
        }

        /// <summary>
        /// This method is ran in a separate thread and it process the CSV file, generates the list, makes different colors of items based on
        /// their respective file name, and then updates the UI with loaded data.
        /// 
        /// While iterating, it's updating the label.
        /// </summary>
        private void LoadCSV()
        {
            using var reader = new StreamReader(CsvFileDialog.FileName);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Configuration.HasHeaderRecord = false;
            csv.Configuration.IgnoreBlankLines = true;
            csv.Configuration.BadDataFound = null;
            csv.Configuration.Delimiter = ";";
            csv.Configuration.MissingFieldFound = null;
            csv.Configuration.AutoMap<CsvProduct>();

            try
            {
                var records = csv.GetRecords<CsvProduct>().ToList();

                int index = 0;
                string firstFilename = null;

                foreach (CsvProduct product in records)
                {
                    if (firstFilename == null)
                    {
                        firstFilename = product.Filename;
                    }

                    if ((index % 100) == 0)
                    {
                        UpdateProcessLabelOnUI("Processed " + index.ToString() + " records...");
                    }

                    string[] listItems = new string[]
                    {
                            (index + 1).ToString(),
                            product.Filename,
                            product.Id,
                            product.Quantity.ToString(),
                            product.Price.ToString("C", CultureInfo.CurrentCulture)
                    };

                    LoadedProducts.Add(new ListViewItem(listItems)
                    {
                        ForeColor = (firstFilename == product.Filename) ? Color.DarkGreen : Color.Blue
                    });

                    index++;
                }

                UpdateProcessLabelOnUI("Finished processing all records!");
                UpdateUIAfterLoad();
            }
            catch (Exception ex)
            {
                string errorMessage = "There was a problem parsing your CSV file.\n\nError thrown by the CSV parser:\n" + ex.Message;

                MessageBox.Show(errorMessage, "CSV Parsing error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                UpdateProcessLabelOnUI("Your CSV is invalid or is not parsable. Try with another file.");
            }
        }

        /// <summary>
        /// Wrapper method to update ProcessLabel back on the UI thread.
        /// </summary>
        /// <param name="text"></param>
        private void UpdateProcessLabelOnUI(string text)
        {
            if (LblProcess.InvokeRequired)
            {
                LblProcess.Invoke((MethodInvoker)delegate ()
                {
                    LblProcess.Text = text;
                });
            }
        }

        /// <summary>
        /// Runs also on UI thread: it updates some labels, sets default per-page value, updates the list view and shows pagination.
        /// 
        /// When this task is done, your items in the ListVIew should appear.
        /// </summary>
        private void UpdateUIAfterLoad()
        {
            if (ProductListView.InvokeRequired)
            {
                ProductListView.Invoke((MethodInvoker)delegate ()
                {
                    TotalRecords = LoadedProducts.Count;
                    UpdateTotalPages();

                    CurrentPage = 1;

                    LblCurrentPage.Text = CurrentPage.ToString();
                    LblTotalRecords.Text = TotalRecords.ToString();

                    ProductListView.Visible = true;

                    /* On first file load we call SetDefaultPerPageValue() since it will trigger it's SelectedIndexChanged event which
                     * will call UpdateListView(0) for us, but when we try to load another file in the same context, we'll update the
                     * list from here. */
                    if(DropDownPerPage.SelectedIndex == -1)
                    {
                        SetDefaultPerPageValue();
                    } else
                    {
                        UpdateListView(0);
                    }

                    TogglePaginationVisibility();
                });
            }
        }

        /// <summary>
        /// Starts the CSVLoad thread.
        /// </summary>
        public void StartCSVLoadThread()
        {
            Thread worker = new Thread(LoadCSV)
            {
                IsBackground = true
            };
            worker.SetApartmentState(ApartmentState.STA);
            worker.Start();
        }

        /// <summary>
        /// Toggles pagination visibility. Default = true.
        /// </summary>
        /// <param name="visible"></param>
        private void TogglePaginationVisibility(bool visible = true)
        {
            label3.Visible = visible;
            label4.Visible = visible;
            label5.Visible = visible;
            label6.Visible = visible;
            label7.Visible = visible;
            label8.Visible = visible;

            DropDownPerPage.Visible = visible;

            LblCurrentPage.Visible = visible;
            LblTotalPages.Visible = visible;
            LblTotalRecords.Visible = visible;

            BtnFirst.Visible = visible;
            BtnPrev.Visible = visible;
            BtnNext.Visible = visible;
            BtnLast.Visible = visible;

            BtnFirst.Enabled = false;
            BtnPrev.Enabled = false;

            if (TotalPages > 1)
            {
                DropDownPerPage.Enabled = true;
                BtnNext.Enabled = true;
                BtnLast.Enabled = true;
            } else
            {
                DropDownPerPage.Enabled = false;
                BtnNext.Enabled = false;
                BtnLast.Enabled = false;
            }
        }

        /// <summary>
        /// Sets default per-page value by using index 4 which contains value of 200.
        /// 
        /// When SelectedIndex is changed, the UpdateListView is invoked by the event handler of the DropDown control.
        /// </summary>
        private void SetDefaultPerPageValue()
        {
            // We will select fourth index as default, which contains value of 200.
            // That's the default number of items shown per page.
            DropDownPerPage.SelectedIndex = 4;
            ItemsPerPage = int.Parse(DropDownPerPage.SelectedItem.ToString());
        }

        /// <summary>
        /// Opens a small InfoWindow form which contains information about a selected item from the ListView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductListView_DoubleClick(object sender, EventArgs e)
        {
            string index = ProductListView.SelectedItems[0].SubItems[0].Text;
            int intIndex = Convert.ToInt32(index);
            int productIndex = intIndex - 1;

            if(LoadedProducts.ElementAtOrDefault(productIndex) == null)
            {
                MessageBox.Show("Selected index is not found or does not exist in current loaded products.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ListViewItem selectedProduct = LoadedProducts[productIndex];

            FormItem formItem = new FormItem
            {
                BackColor = ProductListView.SelectedItems[0].SubItems[0].ForeColor,
                ForeColor = Color.White,

                Product = new InfoProduct()
                {
                    Index = Convert.ToInt32(index),
                    Color = ProductListView.SelectedItems[0].SubItems[0].ForeColor,
                    Filename = selectedProduct.SubItems[1].Text,
                    Id = selectedProduct.SubItems[2].Text,
                    Quantity = selectedProduct.SubItems[3].Text,
                    Price = selectedProduct.SubItems[4].Text
                }
            };

            formItem.ShowDialog();
        }
    }
}
