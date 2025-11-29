using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurrencyConverter;

public partial class Form1 : Form
{

    private ComboBox comboBoxForm;
    private ComboBox comboBoxTo;
    private TextBox textBoxAmount;
    private Button btnConvert;
    private Label labelResult;
    private Label labelAmount;
    private Label labelFrom;
    private Label labelTo;

    private Dictionary<string, string> currencies = new Dictionary<string,string>()
    {
        {"USD", "US Dollar"},
        {"EUR", "Euro"},
        {"GBP", "British Pound"},
        {"JPY", "Japanese Yen"},
        {"CAD", "Canadian Dollar"},
        {"AUD", "Australian Dollar"},
        {"CHF", "Swiss Franc"},
        {"CNY", "CHinese Yuan"},
        {"INR", "Indian Rupee"}
    };  
    public Form1()
    {
        InitializeComponent();
        InitializeCurrencies();
    }

    private void InitializeComponent()
    {
        this.Text = "Currency converter";
        this.Size = new Size (400,300);
        this.StartPosition = FormStartPosition.CenterScreen;

        labelAmount = new Label();
        labelAmount.Text = "Amount:";
        labelAmount.Location = new Point(20,20);
        labelAmount.Size = new Size(60,20);
        this.Controls.Add(labelAmount);

        textBoxAmount = new TextBox();
        textBoxAmount.Location = new Point(90,20);
        textBoxAmount.Size = new Size(150,20);
        textBoxAmount.Text = "1.00";
        this.Controls.Add(textBoxAmount);

        labelTo = new Label();
        labelTo.Text = "To:";
        labelTo.Location = new Point(20, 100);
        labelTo.Size = new Size(60, 20);
        this.Controls.Add(labelTo);

        comboBoxTo = new ComboBox();
        comboBoxTo.Location = new Point(90, 100);
        comboBoxTo.Size = new Size(150, 20);
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
}
