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
}
