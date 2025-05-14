using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EfCoreUi
{
    public partial class ContactUsForm : Form
    {
        public ContactUsForm()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ContactUsForm_Load(object sender, EventArgs e)
            
        {
            
            webBrowser1.DocumentText = "<!DOCTYPE html>\r\n<html lang=\"en\">\r\n<head>\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>Support This Project</title>\r\n    <style>\r\n        body {\r\n            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, sans-serif;\r\n            line-height: 1.6;\r\n            color: #24292e;\r\n            max-width: 600px;\r\n            margin: 0 auto;\r\n            padding: 20px;\r\n            background-color: #f6f8fa;\r\n        }\r\n        \r\n        h1 {\r\n            font-size: 1.5em;\r\n            color: #24292e;\r\n            margin-bottom: 20px;\r\n            font-weight: 600;\r\n        }\r\n        \r\n        .support-section {\r\n            background-color: white;\r\n            border-radius: 6px;\r\n            padding: 20px;\r\n            box-shadow: 0 1px 3px rgba(0,0,0,0.1);\r\n            margin-bottom: 20px;\r\n        }\r\n        \r\n        .option-title {\r\n            font-weight: 600;\r\n            margin-bottom: 8px;\r\n            display: block;\r\n        }\r\n        \r\n        .option-description {\r\n            color: #586069;\r\n            margin-bottom: 12px;\r\n            font-size: 0.95em;\r\n        }\r\n        \r\n        .btn {\r\n            display: inline-block;\r\n            background-color: #2ea44f;\r\n            color: white;\r\n            padding: 8px 16px;\r\n            border-radius: 6px;\r\n            text-decoration: none;\r\n            font-weight: 500;\r\n            margin-bottom: 15px;\r\n            transition: background-color 0.2s;\r\n        }\r\n        \r\n        .btn:hover {\r\n            background-color: #2c974b;\r\n        }\r\n        \r\n        .crypto-address {\r\n            font-family: monospace;\r\n            background-color: #f6f8fa;\r\n            padding: 4px 8px;\r\n            border-radius: 4px;\r\n            font-size: 0.9em;\r\n            word-break: break-all;\r\n        }\r\n        \r\n        .thank-you {\r\n            text-align: center;\r\n            color: #586069;\r\n            font-style: italic;\r\n            margin-top: 20px;\r\n        }\r\n    </style>\r\n</head>\r\n<body>\r\n    <h1>Support This Project</h1>\r\n    \r\n    <div class=\"support-section\">\r\n        <span class=\"option-title\">Star on GitHub</span>\r\n        <p class=\"option-description\">A simple star motivates us to keep improving!</p>\r\n       <a href=\"https://github.com/Afrino-co/EfCoreUI\"  target=\"_blank\">https://github.com/Afrino-co/EfCoreUI</a>\r\n</br> </br><a href=\"https://github.com/Afrino-co/EfCoreUI\" class=\"btn\" target=\"_blank\">Give a Star</a>\r\n    </div>\r\n    \r\n    <div class=\"support-section\">\r\n        <span class=\"option-title\">Crypto Donations</span>\r\n        <p class=\"option-description\">Help sustain the project financially:</p>\r\n        \r\n        <p><strong>ETH(ERC20):</strong> <span class=\"crypto-address\">0xF7d6eD03DF84ef027ABb661490732eE22DC4AF3a</span></p>\r\n        <p><strong>USDT And TRX:</strong> <span class=\"crypto-address\">TL4a3sf6t4LmYyc9s8ir9ffdWPjT5wQErs</span></p>\r\n    </div>\r\n    \r\n    <p class=\"thank-you\">Thank you for your support! 💬</p>\r\n</body>\r\n</html>";
        }
    }
}
