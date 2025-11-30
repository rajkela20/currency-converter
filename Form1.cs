using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurrencyConverter
{
    public partial class Form1 : Form
    {
        private ComboBox comboBoxFrom;
        private ComboBox comboBoxTo;
        private TextBox textBoxAmount;
        private Button btnConvert;
        private Label labelResult;
        private Label labelAmount;
        private Label labelFrom;
        private Label labelTo;

        private Dictionary<string, string> currencies = new Dictionary<string, string>()
        {
            {"USD", "US Dollar"},
            {"EUR", "Euro"},
            {"GBP", "British Pound"},
            {"JPY", "Japanese Yen"},
            {"CAD", "Canadian Dollar"},
            {"AUD", "Australian Dollar"},
            {"CHF", "Swiss Franc"},
            {"CNY", "Chinese Yuan"},
            {"INR", "Indian Rupee"}
        };

        public Form1()
        {
            InitializeCustomComponent();
            InitializeCurrencies();
        }

        private void InitializeCustomComponent()
        {
            this.Text = "Currency converter";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            labelAmount = new Label();
            labelAmount.Text = "Amount:";
            labelAmount.Location = new Point(20, 20);
            labelAmount.Size = new Size(70, 30);
            this.Controls.Add(labelAmount);

            textBoxAmount = new TextBox();
            textBoxAmount.Location = new Point(90, 20);
            textBoxAmount.Size = new Size(150, 20);
            textBoxAmount.Text = "1.00";
            this.Controls.Add(textBoxAmount);

            labelFrom = new Label();
            labelFrom.Text = "From:";
            labelFrom.Location = new Point(20, 60);
            labelFrom.Size = new Size(60, 20);
            this.Controls.Add(labelFrom);

            comboBoxFrom = new ComboBox();
            comboBoxFrom.Location = new Point(90, 60);
            comboBoxFrom.Size = new Size(150, 20);
            comboBoxFrom.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(comboBoxFrom);

            labelTo = new Label();
            labelTo.Text = "To:";
            labelTo.Location = new Point(20, 100);
            labelTo.Size = new Size(60, 20);
            this.Controls.Add(labelTo);

            comboBoxTo = new ComboBox();
            comboBoxTo.Location = new Point(90, 100);
            comboBoxTo.Size = new Size(150, 20);
            comboBoxTo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(comboBoxTo);

            btnConvert = new Button();
            btnConvert.Text = "Convert";
            btnConvert.Location = new Point(90, 140);
            btnConvert.Size = new Size(150, 30);
            btnConvert.Click += btnConvert_Click;
            this.Controls.Add(btnConvert);

            labelResult = new Label();
            labelResult.Location = new Point(20, 190);
            labelResult.Size = new Size(350, 40);
            labelResult.Text = "Enter amount and select currencies";
            this.Controls.Add(labelResult);
        }

        private void InitializeCurrencies()
        {
            if (comboBoxFrom == null || comboBoxTo == null)
                return;

            foreach (var currency in currencies)
            {
                comboBoxFrom.Items.Add($"{currency.Key} - {currency.Value}");
                comboBoxTo.Items.Add($"{currency.Key} - {currency.Value}");
            }

            comboBoxFrom.SelectedIndex = 0;
            comboBoxTo.SelectedIndex = 1;
        }

        private async void btnConvert_Click(object sender, EventArgs e)
        {
            if (textBoxAmount?.Text == null || comboBoxFrom?.SelectedItem == null || comboBoxTo?.SelectedItem == null)
            {
                MessageBox.Show("Please fill in all fields", "Error");
                return;
            }

            if (!double.TryParse(textBoxAmount.Text, out double amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount", "Error");
                return;
            }

            string fromCurrency = comboBoxFrom.SelectedItem.ToString()!.Substring(0, 3);
            string toCurrency = comboBoxTo.SelectedItem.ToString()!.Substring(0, 3);

            try
            {
                if (btnConvert != null)
                    btnConvert.Enabled = false;

                double exchangeRate = await GetExchangeRate(fromCurrency, toCurrency);
                double convertedAmount = amount * exchangeRate;

                if (labelResult != null)
                    labelResult.Text = $"{amount:N2} {fromCurrency} = {convertedAmount:N2} {toCurrency}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Conversion Error!");
            }
            finally
            {
                if (btnConvert != null)
                    btnConvert.Enabled = true;
            }
        }

        private async Task<double> GetExchangeRate(string fromCurrency, string toCurrency)
        {
            string apiUrl = $"https://api.exchangerate-api.com/v4/latest/{fromCurrency}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                string jsonString = await response.Content.ReadAsStringAsync();

                using JsonDocument doc = JsonDocument.Parse(jsonString);
                JsonElement root = doc.RootElement;
                JsonElement rates = root.GetProperty("rates");

                if (rates.TryGetProperty(toCurrency, out JsonElement rateElement))
                {
                    return rateElement.GetDouble();
                }
                else
                {
                    throw new Exception($"Exchange rate for {toCurrency} not found");
                }
            }
        }
    }
}